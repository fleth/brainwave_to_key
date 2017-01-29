using System;
using System.Windows.Forms;
using MMFrame.Windows.GlobalHook;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;

namespace TrainingDataCreator
{
    public partial class Form1 : Form
    {
        private MindWaveProxy.MindWaveConnector mindwaveConnector;

        private TraningDataCreator traningDataCreator;

        private const int keyDataLength = 128;

        private StreamWriter streamWriter = null;

        private string filePath = "trainingdata{0}.json";

        private const int dataCountMax = 10000;

        private int dataCount = 0;

        private HotKey startHotKey;

        private HotKey stopHotKey;

        public Form1()
        {
            InitializeComponent();
            mindwaveConnector = new MindWaveProxy.MindWaveConnector();
        }

        private StreamWriter CreateStreamWriter()
        {
            var fileCount = (dataCount % dataCountMax) + 1;
            var fileName = string.Format(filePath, fileCount);
            return new StreamWriter(fileName, false, System.Text.Encoding.GetEncoding("utf-8"));
        }

        public void OnKeyboardEvent(ref KeyboardHook.StateKeyboard s)
        {
            Console.WriteLine(s.Stroke + " : " + s.Key.GetHashCode() + " : " + s.Key + " : " + s.ScanCode);
            switch (s.Stroke)
            {
                case KeyboardHook.Stroke.KEY_DOWN:
                case KeyboardHook.Stroke.SYSKEY_DOWN:
                    traningDataCreator.KeyDown(s.ScanCode);
                    break;
                case KeyboardHook.Stroke.KEY_UP:
                case KeyboardHook.Stroke.SYSKEY_UP:
                    traningDataCreator.KeyUp(s.ScanCode);
                    break;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            startHotKey = new HotKey(0, Keys.F11);
            startHotKey.HotKeyPush += new EventHandler(button1_Click);
            stopHotKey = new HotKey(MOD_KEY.CONTROL, Keys.F11);
            stopHotKey.HotKeyPush += new EventHandler(button2_Click);

            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.Text = "TraningDataCreator";
            notifyIcon1.BalloonTipTitle = "TraningDataCreator";
            this.Icon = SystemIcons.Application;
        }

        public void OnComplete(int[] keyData, float[] brainWaveData)
        {
            var data = new { label = keyData, brainwaves = brainWaveData };
            try
            {
                var json = JsonConvert.SerializeObject(data);
                streamWriter.Write(json.ToString() + Environment.NewLine);
                dataCount++;
                if (dataCountMax < dataCount)
                {
                    streamWriter.Close();
                    streamWriter = CreateStreamWriter();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void OnReceive(string data)
        {
            var definition = new { rawEeg = 0 };
            try
            {
                var json = JsonConvert.DeserializeAnonymousType(data, definition);
                traningDataCreator.PushBrainWave(json.rawEeg, OnComplete);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("ignore data: " + data);
            }
        }

        private void CaptureStart()
        {
            mindwaveConnector.Connect(OnReceive, true);
            traningDataCreator = new TraningDataCreator(keyDataLength, 50);
            streamWriter = CreateStreamWriter();
            KeyboardHook.AddEvent(OnKeyboardEvent);
            KeyboardHook.Start();
            button1.Enabled = false;
            button2.Enabled = true;
            
            notifyIcon1.BalloonTipText = "キャプチャを開始しました";
            notifyIcon1.ShowBalloonTip(3000);
        }

        private void CaptureStop()
        {
            mindwaveConnector.Disconnect();
            if(streamWriter != null)
            {
                streamWriter.Close();
            }

            if (KeyboardHook.IsHooking)
            {
                KeyboardHook.Stop();
                button1.Enabled = true;
                button2.Enabled = false;
            }
            
            notifyIcon1.BalloonTipText = "キャプチャを終了しました";
            notifyIcon1.ShowBalloonTip(3000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CaptureStart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CaptureStop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            startHotKey.Dispose();
            stopHotKey.Dispose();
            CaptureStop();
        }
    }
}

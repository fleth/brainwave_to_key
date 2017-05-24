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

        private int dataCount = 0;

        private HotKey startHotKey;

        private HotKey stopHotKey;

        public Form1()
        {
            InitializeComponent();
            mindwaveConnector = new MindWaveProxy.MindWaveConnector();
        }

        private StreamWriter CreateStreamWriter(string path)
        {

            DateTime dtNow = DateTime.Now;
            string filePath = dtNow.ToString("yyyyMMdd_HHmmss") + ".json";
            return new StreamWriter(path + "\\" + filePath, false, System.Text.Encoding.GetEncoding("utf-8"));
        }

        public void OnKeyboardEvent(ref KeyboardHook.StateKeyboard s)
        {
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

        public void OnMouseEvent(ref MouseHook.StateMouse s)
        {
            switch (s.Stroke)
            {
                case MouseHook.Stroke.LEFT_DOWN:
                    traningDataCreator.MouseDown(MouseButton.Left);
                    break;
                case MouseHook.Stroke.LEFT_UP:
                    traningDataCreator.MouseUp(MouseButton.Left);
                    break;
                case MouseHook.Stroke.RIGHT_DOWN:
                    traningDataCreator.MouseDown(MouseButton.Right);
                    break;
                case MouseHook.Stroke.RIGHT_UP:
                    traningDataCreator.MouseUp(MouseButton.Right);
                    break;
                case MouseHook.Stroke.MIDDLE_DOWN:
                    traningDataCreator.MouseDown(MouseButton.Middle);
                    break;
                case MouseHook.Stroke.MIDDLE_UP:
                    traningDataCreator.MouseUp(MouseButton.Middle);
                    break;
                case MouseHook.Stroke.WHEEL_DOWN:
                    traningDataCreator.MouseDown(MouseButton.Wheel);
                    break;
                case MouseHook.Stroke.WHEEL_UP:
                    traningDataCreator.MouseUp(MouseButton.Wheel);
                    break;
                case MouseHook.Stroke.MOVE:
                    traningDataCreator.MousePosition(s.X, s.Y);
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
            
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        public void OnComplete(TrainingData trainingData)
        {
            var data = new {
                keys = trainingData.keyData,
                brainwaves = trainingData.brainWaveData,
                mouse = new {
                    move = trainingData.mouseMoveData,
                    button = trainingData.mouseButtonData
                }
            };

            try
            {
                var json = JsonConvert.SerializeObject(data);
                streamWriter.Write(json.ToString() + Environment.NewLine);
                dataCount++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void OnThinkGearReceive(string data)
        {
            var definition = new { rawEeg = 0 };
            try
            {
                var json = JsonConvert.DeserializeAnonymousType(data, definition);
                var eeg = new float[1];
                eeg[0] = json.rawEeg;
                traningDataCreator.PushBrainWave(eeg, OnComplete);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("ignore data: " + data);
            }
        }

        private void CaptureStart()
        {
            mindwaveConnector.Connect(OnThinkGearReceive, true);
            traningDataCreator = new TraningDataCreator(keyDataLength, 50);
            streamWriter = CreateStreamWriter(textBox1.Text);
            KeyboardHook.AddEvent(OnKeyboardEvent);
            KeyboardHook.Start();
            MouseHook.AddEvent(OnMouseEvent);
            MouseHook.Start();
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;
            
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
            }

            if (MouseHook.IsHooking)
            {
                MouseHook.Stop();
            }

            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;

            notifyIcon1.BalloonTipText = "キャプチャを終了しました";
            notifyIcon1.ShowBalloonTip(3000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!button1.Enabled) return;
            CaptureStart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!button2.Enabled) return;
            CaptureStop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            startHotKey.Dispose();
            stopHotKey.Dispose();
            CaptureStop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}

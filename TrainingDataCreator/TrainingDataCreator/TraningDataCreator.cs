using System;
using System.Windows;

namespace TrainingDataCreator
{
    public enum MouseMove
    {
        Angle = 0,
        Distance
    }

    public enum MouseButton
    {
        Left = 0,
        Right,
        Middle,
        Wheel
    }

    public struct TrainingData
    {
        public int[] keyData;
        public float[] brainWaveData;
        public float[] mouseMoveData;
        public int[] mouseButtonData;
    }

    class TraningDataCreator
    {

        private TrainingData trainingData;

        private int brainWaveDataCount;

        private Vector position, lastPosition;
        
        public delegate void OnComplete(TrainingData trainingData);

        public TraningDataCreator(int keyDataLength, int brainWaveDataLength)
        {
            trainingData.keyData = new int[keyDataLength];
            trainingData.brainWaveData = new float[brainWaveDataLength];
            trainingData.mouseButtonData = new int[Enum.GetValues(typeof(MouseButton)).Length];
            trainingData.mouseMoveData = new float[Enum.GetValues(typeof(MouseMove)).Length];
            brainWaveDataCount = 0;
        }

        public void KeyDown(uint code)
        {
            if (trainingData.keyData[code] != 0)
            {
                trainingData.keyData[code] *= -1;
            } else {
                trainingData.keyData[code] = -1;
            }
        }

        public void KeyUp(uint code)
        {
            trainingData.keyData[code] = 1;
        }

        public void MouseUp(MouseButton mouseButton)
        {
            trainingData.mouseButtonData[mouseButton.GetHashCode()] = 1;
        }

        public void MouseDown(MouseButton mouseButton)
        {
            trainingData.mouseButtonData[mouseButton.GetHashCode()] = -1;
        }

        public void MousePosition(int x, int y)
        {
            Vector current = new Vector(x, y);
            var angle = Vector.AngleBetween(position, current);
            var distance = Math.Sqrt(Math.Pow(current.X - position.X, 2) + Math.Pow(current.Y - position.Y, 2));

            lastPosition = current;

            trainingData.mouseMoveData[MouseMove.Angle.GetHashCode()] = (float)angle;
            trainingData.mouseMoveData[MouseMove.Distance.GetHashCode()] = (float)distance;
            
        }

        private void UpStatusReset()
        {
            for(var i = 0; i < trainingData.keyData.Length; i++)
            {
                if(trainingData.keyData[i] == 1)
                {
                    trainingData.keyData[i] = 0;
                }
                
            }
            
            foreach (var mouseMove in Enum.GetValues(typeof(MouseMove)))
            {
                trainingData.mouseMoveData[mouseMove.GetHashCode()] = 0;
            }

            foreach (var mouseButton in Enum.GetValues(typeof(MouseButton)))
            {
                if(trainingData.mouseButtonData[mouseButton.GetHashCode()] == 1)
                {
                    trainingData.mouseButtonData[mouseButton.GetHashCode()] = 0;
                }
            }
        }

        public void PushBrainWave(float data, OnComplete onComplete)
        {
            trainingData.brainWaveData[brainWaveDataCount] = data;
            brainWaveDataCount++;
            if(trainingData.brainWaveData.Length == brainWaveDataCount)
            {
                brainWaveDataCount = 0;
                position = lastPosition;
                onComplete(trainingData);
                UpStatusReset();
            }
        }
    }
}

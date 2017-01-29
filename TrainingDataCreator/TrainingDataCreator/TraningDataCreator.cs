
namespace TrainingDataCreator
{
    class TraningDataCreator
    {
        private int[] keyData;

        private float[] brainWaveData;

        private int brainWaveDataCount;

        public delegate void OnComplete(int[] keyData, float[] brainWaveData);

        public TraningDataCreator(int keyDataLength, int brainWaveDataLength)
        {
            keyData = new int[keyDataLength];
            brainWaveData = new float[brainWaveDataLength];
            brainWaveDataCount = 0;
        }

        public void KeyDown(uint code)
        {
            if(keyData[code] != 0)
            {
                keyData[code] *= -1;
            } else {
                keyData[code] = -1;
            }
        }

        public void KeyUp(uint code)
        {
            keyData[code] = 1;
        }

        private void UpStatusReset()
        {
            for(var i = 0; i < keyData.Length; i++)
            {
                if(keyData[i] == 1)
                {
                    keyData[i] = 0;
                }
                
            }
        }

        public void PushBrainWave(float data, OnComplete onComplete)
        {
            brainWaveData[brainWaveDataCount] = data;
            brainWaveDataCount++;
            if(brainWaveData.Length == brainWaveDataCount)
            {
                brainWaveDataCount = 0;
                onComplete(keyData, brainWaveData);
                UpStatusReset();
            }
        }
    }
}

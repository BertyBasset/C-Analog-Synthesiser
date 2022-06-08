using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Oscillators {
    public abstract class OscillatorBase {
        protected int _SampleNo;


        public float Frequency { get; set; }             // 20 to 10,000
        public float Amplitude { get; set; } = 1f;       // 0 to 1
        public float Duty { get; set; } = 0.5f;          // 0 to 1 - no effect on Sine, although maybe we can distort later ?
        abstract internal float Read(int SamplesPerSecond);
        protected void IncrementSampleCounter() {
            if (_SampleNo < int.MaxValue)
                _SampleNo++;
            else
                _SampleNo = 0;

        }
    }
}

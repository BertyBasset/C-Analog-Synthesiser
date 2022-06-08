using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Oscillators {
    public class OscillatorSquare : OscillatorBase {
        internal override float Read(int SamplesPerSecond) {
            // Get m - number of samples in 1 cycle
            float m = SamplesPerSecond / Frequency;
            float sample = (_SampleNo % m > m * Duty ? 1 : 0) * 2f - 1f;
            base.IncrementSampleCounter();
            return sample;
        }
    }
}

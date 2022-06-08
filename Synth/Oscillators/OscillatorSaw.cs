using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Oscillators {
    public class OscillatorSaw : OscillatorBase {

        internal override float Read(int SamplesPerSecond) {
            float cycles = SamplesPerSecond / Frequency;
            float sample = _SampleNo % cycles * 2f / cycles - 1f;
            base.IncrementSampleCounter();
            return sample;
        }
    }
}


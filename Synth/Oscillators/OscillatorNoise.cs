using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Oscillators {
    public class OscillatorNoise : OscillatorBase {
        Random r = new Random();

        internal override float Read(int SamplesPerSecond) {
            base.IncrementSampleCounter();
            return (float)(r.NextDouble() * 2.0 - 1.0) * Amplitude;
        }
    }
}

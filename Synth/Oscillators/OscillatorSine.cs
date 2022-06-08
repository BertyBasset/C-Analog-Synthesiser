using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Synth.Oscillators {
 

    public class OscillatorSine : OscillatorBase {


        internal override float Read(int SamplesPerSecond) {
            float sample = (float)Math.Sin((2.0 * (float)Math.PI * _SampleNo * Frequency) / SamplesPerSecond) * Amplitude;
            base.IncrementSampleCounter();
            return sample;
        }

    }
}

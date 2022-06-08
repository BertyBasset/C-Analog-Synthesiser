using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Oscillators {
    public class OscillatorTriangle : OscillatorSaw {

        internal override float Read(int SamplesPerSecond) {
            // Just rectify and shift a saw

            var a = base.Read(SamplesPerSecond);
            base.IncrementSampleCounter();
            return (Math.Abs(a) - .5f) * 2f;
        }
    }
}

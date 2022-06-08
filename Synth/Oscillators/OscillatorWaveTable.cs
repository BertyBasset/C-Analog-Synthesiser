using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Oscillators {
    public class OscillatorWaveTable : OscillatorBase {
        private float[] wavetable;
        public OscillatorWaveTable(string WaveTablePath) {
            wavetable = Utils.WavReader.readWav(WaveTablePath);
        }

        internal override float Read(int SamplesPerSecond) {
            
            // Get index of sample to retrieve, then interpolate between the adjacent integral samples
            float i = (wavetable.Length * Frequency * _SampleNo/SamplesPerSecond) % wavetable.Length;


            // Need to add interpolaiton here
            // wt[floor(i)] + (i - floor(i))(wt[floor(i)]-wt[(floor(i)+1)%wt.length])

            float sample = wavetable[(int)i] * Amplitude;

            base.IncrementSampleCounter();
            return sample;
        }
    }
}

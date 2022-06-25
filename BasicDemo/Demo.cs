using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Synth {

    internal class Demo : WaveProvider32 {
        private AsioOut? asioOut;

        internal Demo() {
            SetWaveFormat(44100, 2);    // Sample Rate, Channels
            asioOut = new AsioOut();    // Audio Stream Input/Output protocol api
            asioOut.Init(this);
            asioOut.Play();
        }

        internal void Stop() {
            asioOut.Stop();
            asioOut.Dispose();
            asioOut = null;
        }


        public override int Read(float[] buffer, int offset, int sampleCount) {
            var r = new Random();
            for (int n = 0; n < sampleCount; n++) {
                var v = (float)((r.NextDouble() - .5) * .2);
                buffer[n + offset] = v;
            }
            return sampleCount;
        }
    }
}

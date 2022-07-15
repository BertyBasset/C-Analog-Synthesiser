using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using Synth.Modules.Modulators;
using Synth.Modules.Sources;

namespace Synth {
    // Reworking of Synth Engine using Phase Accumulators for shaping oscillator waveform
    // Old technique of using Time Accumulator was leading to waveform discontinuities
    // when changin frequecny.

    public class SynthEngine : WaveProvider32 {
        private DirectSoundOut? asioOut;

        // These config settings are injected into constructor by client application
        int _SampleRate;
        int _Channels;


        #region 'Real Time' Graph
        // These allow any client app to display a 'real time' graph.
        // There will be a discontinuity at index=DisplayGraphCounter, so a helper method
        // is provided to return an array that wraps round if necessary going backwards
        // from index=DisplayGraphCounter
        private int _DisplayGraphCounter;
        private float[] _DisplayGraph = new float[1000];

        public float[] GetGraphData(int arraySize = 512) {
            // Array size maxes out at _DisplayGraph - 10 to keep away from the discontinuity which might have moved slightly
            // We've still got a bit of a glitch, but we can live wit it

            if (arraySize > _DisplayGraph.Length - 10)
                arraySize = _DisplayGraph.Length - 10;

            float[] rv = new float[arraySize];

            int src = _DisplayGraphCounter;

            for (int i = arraySize - 1; i >= 0; i--) {
                rv[i] = _DisplayGraph[src];
                src--;
                if (src < 0)
                    src = _DisplayGraph.Length - 1;
            }

            return rv;
        }
        #endregion


        #region Stop/Start
        public void Start() {
            // Maybe this needs to be in config


            SetWaveFormat(_SampleRate, _Channels);                   // 16kHz stereo

            asioOut = new();
            asioOut.Init(this);
            asioOut.Play();
            Started = true;
        }

        public void Stop() {
            if (asioOut != null) {
                asioOut.Stop();
                asioOut.Dispose();
                asioOut = null;
                Started = false;
            }
        }
        #endregion

        #region Public Synth Properties
        public bool Started;            // Is synth running or not
        public float Volume { get; set; } = .25f;
        public List<Oscillator> Oscillators = new List<Oscillator>();

        public ModWheel ModWheel { get; set; } = new ModWheel();


        private int _PitchWheel;
        public int PitchWheel {
            get => _PitchWheel;
            set {
                _PitchWheel = value;
                foreach (var o in Oscillators)
                    o.Frequency.PitchWheel = _PitchWheel;
            }
        }

        private Utils.Note _Note = new Utils.Note();
        public Utils.Note Note {
            get { return _Note; }
            set {
                _Note = value;
                foreach(var o in Oscillators)
                    o.Frequency.Note = _Note;
            }
        }

        #endregion

        #region Constructor
        public SynthEngine(Config config, float volume = 0.25f) {
            _SampleRate = config.SampleRate;
            _Channels = config.Channels;
            Volume = volume;
        }
        #endregion


        #region Sound Generation loop
        // Looks like this is a callback function which gets called when NAudio needs more wave data
        public override int Read(float[] buffer, int offset, int sampleCount) {
            float timeIncrement = 1f / (float)_SampleRate;
            for (int n = 0; n < sampleCount; n++) {
                float wave = 0;


                // Process oscillators
                foreach (var osc in Oscillators) {
                    wave += osc.Read(timeIncrement);
                }


                // More processing here


                // Housekeeping - set final sample value with overall Volume
                float currentSample = (Volume * wave);
                buffer[n + offset] = currentSample;


                // Populate array for displaying waveform
                if (n % 2 == 0) {
                    //_DisplayGraph[_DisplayGraphCounter] = currentSample;
                    _DisplayGraph[_DisplayGraphCounter] = buffer[n + offset];
                    _DisplayGraphCounter++;
                    if(_DisplayGraphCounter >= _DisplayGraph.Length - 1)
                        _DisplayGraphCounter =0;


                }

            }
            return sampleCount;
        }
        #endregion

    }
}

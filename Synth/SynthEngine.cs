using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using Synth.Oscillators;


namespace Synth {


    // To Do
    // Freq for wavetables
    // Get more wavetables
    // Use PW for phase distortion
    // Plum freq + PW to UI
    // Add note, pitch+/-, oct+/-, pitch mod inputs
    // Sync?
    // Nice UI with FM, wave selectors etc.
    // Subosc property?


    // This will hold array of oscillators of various kinds
    // The entire class provides the final wave to be played back

    public class SynthEngine : WaveProvider32 {
        private AsioOut? asioOut;

        #region Stop/Start
        public void Start() {
            
            EnableDebug(20000);   // Log 20,000 samples so we can make sure we don't have looping errors
            SetWaveFormat(16000, 2);                   // 16kHz stereo

            asioOut = new AsioOut();
            asioOut.Init(this);
            asioOut.Play();
        }

        public void Stop() {
            if (asioOut != null) {
                asioOut.Stop();
                asioOut.Dispose();
                asioOut = null;
            }
        }
        #endregion

        #region Debug Logger


        private int _NumSamplesToLog;
        private int _SamplesLogged = 0;
        private StringBuilder? _DebugLog;
        private bool _Debug;

        public void EnableDebug(int NumSamplesToLog) {
            _Debug = true;
            _NumSamplesToLog = NumSamplesToLog;
            _SamplesLogged = 0;
            _DebugLog = new StringBuilder(NumSamplesToLog);
        }
        public void DisableDebug() {
            _Debug = false;
            if(_DebugLog != null)
                _DebugLog.Clear();
        }



        private void Log(float value) {
            if (!_Debug)
                return;

            _SamplesLogged++;
            if (_DebugLog != null) {

                _DebugLog.Append(value.ToString() + "\r\n");
                if (_SamplesLogged >= _NumSamplesToLog) 
                {
                    // Copy to clipboard and clear
                    var c = new AsyncWindowsClipboard.WindowsClipboardService();
                    c.SetTextAsync(_DebugLog.ToString());
                    _DebugLog.Clear();
                    _SamplesLogged = 0;
                    _Debug = false;
                }
            }
        }
        #endregion

        #region Public Synth Properties
        public float Volume { get; set; } = .25f;
        public List<OscillatorBase> Oscillators = new List<OscillatorBase>();

        #endregion


        #region Constructor
        public SynthEngine(float volume = 0.25f) {
            Volume = volume;

  


        }
        #endregion

        #region Sound Generation loop
        // Looks like this is a callback function which gets called when NAudio needs more wave data
        public override int Read(float[] buffer, int offset, int sampleCount) {
            

            for (int n = 0; n < sampleCount; n++) {
                double wave = 0;


                // Process oscillators
                foreach(var osc in Oscillators)
                    wave += osc.Read(WaveFormat != null ? WaveFormat.SampleRate: -1);


                // More processing here




                // Housekeeping - set final sample value with overall Volume
                float currentSample = (float)(Volume * wave);
                buffer[n + offset] = currentSample;
                if (_Debug)
                    Log(currentSample);


              

            }
            return sampleCount;
        }
        #endregion
    }
}

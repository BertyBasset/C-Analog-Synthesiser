using Synth;
using Synth.Oscillators;

namespace WinFormsApp1 {
    public partial class Form1 : Form {
        #region Init Synth
        Synth.SynthEngine synth = new Synth.SynthEngine();

        void InitSynth() {

      
            /*
            synth.Oscillators.Add(new OscillatorSine() { Frequency = 39.4f });
            synth.Oscillators.Add(new OscillatorSine() { Frequency = 40f });
            */

            /*
            synth.Oscillators.Add(new OscillatorSine() { Frequency = 39.4f });
            synth.Oscillators.Add(new OscillatorSine() { Frequency = 40f });
            */

            /*
            synth.Oscillators.Add(new OscillatorTriangle() { Frequency = 39.7f });
            synth.Oscillators.Add(new OscillatorTriangle() { Frequency = 40f });
            */

            /*
            synth.Oscillators.Add(new OscillatorSaw() { Frequency = 40f });
            synth.Oscillators.Add(new OscillatorSaw() { Frequency = 39.7f });
            synth.Oscillators.Add(new OscillatorSaw() { Frequency = 20.2f });
            */


            synth.Oscillators.Add(new OscillatorSquare() { Frequency = 40f });
            synth.Oscillators.Add(new OscillatorSquare() { Frequency = 39.7f, Duty = .1f });
            synth.Oscillators.Add(new OscillatorSquare() { Frequency = 20.2f });

            /*
            synth.Oscillators.Add(new OscillatorWaveTable("wavetables/AKWF_aguitar_0001.wav") { Frequency = 39.4f });
            synth.Oscillators.Add(new OscillatorWaveTable("wavetables/AKWF_aguitar_0002.wav") { Frequency = 40f });
            synth.Oscillators.Add(new OscillatorWaveTable("wavetables/AKWF_aguitar_0003.wav") { Frequency = 40.3f });
            */



        }
        #endregion

        #region Init Form
        public Form1() {
            InitializeComponent();
            InitSynth();
        }
        #endregion

        #region Stop Start
        private void cmdStart_Click(object sender, EventArgs e) {
            cmdStart.Enabled = false;
            cmdStop.Enabled = true;
            synth.Start();

        }

        private void cmdStop_Click(object sender, EventArgs e) {
            cmdStart.Enabled = true;
            cmdStop.Enabled = false;

            synth.Stop();
        }
        #endregion
    }
}
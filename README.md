# C-Analog-Synthesiser

Set UI as startup project.

The project uses the ASIO protocol to provide a low latency output. These are specific to Soundcards, but if you don't have a Soundcard, the generic ASIO Windows driver can be installed from https://www.asio4all.org/

To test. Run UI project, click Start. You should get a sound.

In Form1.cs of the UI project, comment the oscillator lines in and out:

```cs
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

            /*synth.Oscillators.Add(new OscillatorNoise());*/


            synth.Oscillators.Add(new OscillatorSquare() { Frequency = 40f });
            synth.Oscillators.Add(new OscillatorSquare() { Frequency = 39.7f, Duty = .1f });
            synth.Oscillators.Add(new OscillatorSquare() { Frequency = 20.2f });

            /*
            synth.Oscillators.Add(new OscillatorWaveTable("wavetables/AKWF_aguitar_0001.wav") { Frequency = 39.4f });
            synth.Oscillators.Add(new OscillatorWaveTable("wavetables/AKWF_aguitar_0002.wav") { Frequency = 40f });
            synth.Oscillators.Add(new OscillatorWaveTable("wavetables/AKWF_aguitar_0003.wav") { Frequency = 40.3f });
            */

        }
        ```

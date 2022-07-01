# C# Analog Synthesiser

Three oscillator monophonic virtual analog synthesizer. At the moment, only the oscillator modules are completed. However, you can produce a suprising number of sounds with them. The synthesizer modules are patched together in te Windows Forms UI project.

Features:
-Three Oscillator:
-8 waveform
  - Sine
  - Trinagle
  - Sawtooth
  - Square/Pulse
  - Wavetable
  - Harmonic (Fourier coeffs)
  - Supersaw (7 de-tuned oscillators)
- Virtual keyboard:
  - Mouse control
  - Computer keyboard control
  - Midi keybboard control
Octave tune
Semitone tune
Fine tune
Modulator input  (other oscillator selectable)
Modulator amount



Set UI as startup project.

The project uses the ASIO protocol to provide a low latency output. These are specific to Soundcards, but if you don't have a Soundcard, the generic ASIO Windows driver can be installed from https://www.asio4all.org/

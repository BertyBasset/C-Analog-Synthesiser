# C# Analog Synthesiser

Three oscillator monophonic virtual analog synthesizer. At the moment, only the oscillator modules are completed. However, you can produce a suprising number of sounds with them. The synthesizer modules are patched together in te Windows Forms UI project.

Features:
Three Oscillators:
- 8 waveforms
  - Sine
  - Triangle
  - Sawtooth
  - Square/Pulse
  - Wavetable
  - Harmonic (Fourier coeffs)
  - Supersaw (7 de-tuned oscillators)
- Virtual keyboard:
  - Mouse control
  - Computer keyboard control
  - Midi keybboard control
- Frequency control:
  - Keyboard tracking on/off
  - Octave tune
  - Semitone tune
  - Fine tune
  - Modulator input  (other oscillator selectable)
  - Modulator amount
 - Pulse width mosulation (square wave)
 - Phase distortion (sine, triangle, wavetable waves)
 - Oscillator Sync
 - Realtime waveform display
 - Realtime spectrum display (FFT)
 - Save/Recall patch

![Demo UI](https://raw.githubusercontent.com/BertyBasset/C-Analog-Synthesiser/main/UI.png)

Set UI as startup project.

The project uses the ASIO protocol to provide a low latency output. These are specific to Soundcards, but if you don't have a Soundcard, the generic ASIO Windows driver can be installed from https://www.asio4all.org/



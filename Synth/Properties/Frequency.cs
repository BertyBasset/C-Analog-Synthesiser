using Synth.Utils;
using Synth.Modules.Modulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Modules.Properties {
    public class Frequency {
        private const float DEFAULT_FREQUENCY = 110f;

        public Frequency(Note Note) {
            this.Note = Note;
        }


        // Keyboard Control
        // If false, Note property has no effect on frequecny
        private bool _Kbd = true;
        public bool Kbd {
            get { return _Kbd; }
            set { 
                _Kbd = value;
                setFrequency();
            }
        }


        // Frequency is now a derived property, driven by the following
        public float PreModFrequency { get; internal set; }        // 20 to 10,000  - This is pre modulation

        private int _Octave = 0;
        public int Octave {                                        // -3 to +3 octaves
            get { return _Octave; }
            set {
                _Octave = Utils.Misc.Constrain(value, -3, 3);
                setFrequency();
            }
        }

        private float _Tune = 0;
        public float Tune {                                        // -1 to +1 octave
            get { return _Tune; }
            set {
                _Tune = Utils.Misc.Constrain(value, -1f, 1f);
                setFrequency();
            }
        }

        private float _FineTune = 0;
        public float FineTune {                                    // -1 to +1 octave, but normally will be +/- seimitone  +/- 12th root of 2
            get { return _FineTune; }
            set {
                _FineTune = Utils.Misc.Constrain(value, -1f, 1f);
                setFrequency();
            }
        }


        private float _PitchWheel = 0;
        internal int PitchWheel {
            set {
                float semitone = (value - 8192f) / 4096 / 12;
                _PitchWheel = semitone * MathF.Pow(2, 1 / 12);
                setFrequency();
            }
        }

        private Utils.Note _Note = new Utils.Note();
        internal Utils.Note Note {
            get { return _Note; }
            set {
                _Note = value;
                setFrequency();
            }
        }


        public iModulator? Modulator;

        private float _ModulationAmount;
        public float ModulationAmount {                            // 0 to 10000
            get { return _ModulationAmount; }
            set {
                _ModulationAmount = Utils.Misc.Constrain(value, 0f, 10000f);
            }   
        }

        //  Frequency scaling is 1.0 per octave
        private void setFrequency() {
            // Whenever one of the frequency controlling properties change, we update Pre Mod Frequency
            if(Kbd)
                PreModFrequency = _Note.Frequency;                                  // Base Frequency
            else
                PreModFrequency = DEFAULT_FREQUENCY;                                  // Base Frequency

            PreModFrequency = PreModFrequency * (float)Math.Pow(2, _Octave);    // Adjust Octave
            // Both Tune and FineTune are 1 per octave, however they have separate values as they'll normally have separate UI controls
            PreModFrequency = PreModFrequency * (float)Math.Pow(2, _Tune);      // Tune within octave
            PreModFrequency = PreModFrequency * (float)Math.Pow(2, _FineTune);  // Tune within semitone
        }

        public float GetFrequency() {
            // NB     / 2 because of stereo interleaving
            // This is final frequency used for driving Phase Accumulator
            var f = PreModFrequency / 2f;
            // <<-- ** Apply modulation here
             

            if(Modulator != null)
                f = f + _ModulationAmount * Modulator.Value;

            return f;
        }

    }
}

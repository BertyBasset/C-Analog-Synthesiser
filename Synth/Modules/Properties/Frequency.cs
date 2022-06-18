using Synth.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Modules.Properties {
    public class Frequency {
        public Frequency(Note Note) {
            this.Note = Note;
        }


        // Frequency is now a derived property, driven by the following
        public float PreModFrequency { get; internal set; }        // 20 to 10,000  - This is pre modulation
        internal float _PostModFrequency;                         // This is post modulation. This drives the Phase Accumulator

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

        private Utils.Note _Note = new Utils.Note();
        public Utils.Note Note {
            get { return _Note; }
            set {
                _Note = value;
                setFrequency();
            }
        }

        private float _ModulationIn = 0f;
        public float ModulationIn {                                // Nominally -1 to 1, but don't constrain as it might be signal, 
            get { return _ModulationIn; }                          // +1 will float freq, -1 will halve
            set {
                _ModulationIn = value;
                setFrequency();
            }
        }

        private float _ModulationAmount;
        public float ModulationAmount {                            // 0 to 1
            get { return _ModulationAmount; }
            set {
                _ModulationAmount = Utils.Misc.Constrain(value, 0f, 1f);
                setFrequency();
            }
        }

        //  Frequency scaling is 1.0 per octave
        private void setFrequency() {
            // Whenever one of the frequency controlling properties change, we update Frequency
            PreModFrequency = _Note.Frequency;                                  // Base Frequency
            PreModFrequency = PreModFrequency * (float)Math.Pow(2, _Octave);    // Adjust Octave
            PreModFrequency = PreModFrequency * (float)Math.Pow(2, _Tune);      // Tune within octave


            // This is final frequency used for driving Phase Accumulator
            _PostModFrequency = PreModFrequency / 2f;                           // <<-- ** Apply modulation here

            // NB     / 2 because of stereo interleaving
        }
    }
}

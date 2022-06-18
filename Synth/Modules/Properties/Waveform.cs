using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Modules.Properties {
    public enum WaveformType {
        Sine = 0,
        Saw,
        Triangle,
        Square,
        Noise,
        WaveTable,
        Harmonic,
        SuperSaw
    }

    public class WaveForm {

        public int ID;
        public string Name = "";
        public WaveformType Type;
        internal Sources.iGenerator? _Generator;

        internal Sources.iGenerator Generator {
            get {
                if (_Generator == null) {
                    switch (Type) {
                        case WaveformType.Sine: _Generator = new Sources.GeneratorSine(); break;
                        case WaveformType.Saw: _Generator = new Sources.GeneratorSaw(); break;
                        case WaveformType.Triangle: _Generator = new Sources.GeneratorTriangle(); break;
                        case WaveformType.Square: _Generator = new Sources.GeneratorSquare(); break;
                        case WaveformType.Noise: _Generator = new Sources.GeneratorNoise(); break;
                        case WaveformType.WaveTable: _Generator = new Sources.GeneratorWaveTable(); break;
                        case WaveformType.Harmonic: _Generator = new Sources.GeneratorHarmonic(); break;
                        case WaveformType.SuperSaw: _Generator = new Sources.GeneratorSuperSaw(); break;
                        default: _Generator = new Sources.GeneratorSine(); break;
                    }
                }

                return _Generator;
            }
        }


        public static WaveForm GetByID(int ID) {
            return GetWaveFormList().Where(w => w.ID == ID).First();
        }

        public static WaveForm GetByType(WaveformType Type) {
            return GetWaveFormList().Where(w => w.Type == Type).First();
        }

        public static WaveForm GetByName(string Name) {
            return GetWaveFormList().Where(w => w.Name.ToLower() == Name.ToLower()).First();
        }

        public static List<WaveForm> GetWaveFormList() {
            var waveforms = new List<WaveForm>();

            waveforms.Add(new WaveForm() { ID = 0, Name = "Sine", Type = WaveformType.Sine });
            waveforms.Add(new WaveForm() { ID = 1, Name = "Saw", Type = WaveformType.Saw });
            waveforms.Add(new WaveForm() { ID = 2, Name = "Triangle", Type = WaveformType.Triangle });
            waveforms.Add(new WaveForm() { ID = 3, Name = "Square", Type = WaveformType.Square });
            waveforms.Add(new WaveForm() { ID = 4, Name = "Noise", Type = WaveformType.Noise });
            waveforms.Add(new WaveForm() { ID = 5, Name = "WaveTable", Type = WaveformType.WaveTable });
            waveforms.Add(new WaveForm() { ID = 6, Name = "Harmonic", Type = WaveformType.Harmonic });
            waveforms.Add(new WaveForm() { ID = 7, Name = "SuperSaw", Type = WaveformType.SuperSaw });

            return waveforms;
        }
    }
}

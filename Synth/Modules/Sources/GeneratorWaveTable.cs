namespace Synth.Modules.Sources {
    // This will change to hold an array of samples


    internal class GeneratorWaveTable : iGenerator {
        private float[]? _WaveTable;
        public string FileName {
            set {
                // Load wav file to _WaveTable
                if(!string.IsNullOrEmpty(value))
                    _WaveTable = Utils.WavReader.readWav(value);
            }
        }

        //                                                             Not Used
        float iGenerator.GenerateSample(float Phase, float Duty, float PhaseIncrement) {
            if (_WaveTable == null)
                return 0f;

            // Should really interpolate between samples in Wavetable, but for now, just go to nearest one
            int index = (int)Math.Round(Phase / 360f * _WaveTable.Length, 0);
            return _WaveTable[index >= _WaveTable.Length ? _WaveTable.Length - 1 :index];
        }
    }
}

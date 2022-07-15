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


        // To prevent clicks, only apply changed Duty on a zero crossing
        float _oldPhase;
        float _newDuty;

        //                                                             Not Used
        float iGenerator.GenerateSample(float Phase, float Duty, float PhaseIncrement) {
            if (_WaveTable == null)
                return 0f;

            // Phase Distortion
            // For wavetable, instead of varying SQ duty cycle, we can do phase distortion a la Casio CZ100 !
            float phase = Phase;
            if (Duty != 0) {
                // If we've wrapped round 360 -> 0, we're safe to do Phase Distortion
                if (Phase < _oldPhase)
                    _newDuty = Duty;

                phase = PhaseDistortionTransferFunction.GetPhase(phase, _newDuty, this);

            }

            // Should really interpolate between samples in Wavetable, but for now, just go to nearest one
            int index = (int)Math.Round(phase / 360f * _WaveTable.Length, 0);

            _oldPhase = Phase;

            try {
                return _WaveTable[index >= _WaveTable.Length ? _WaveTable.Length - 1 : index];
            } catch (Exception) {
                return 0f;
            }
        }


        void iGenerator.Sync() {
            // Don't have Phase Accumulator(s), so do nothing
        }
    }
}

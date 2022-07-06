namespace Synth.Modules.Sources {
    internal class GeneratorTriangle : iGenerator {
        //                                                       Not Used
        float iGenerator.GenerateSample(float Phase, float Duty, float PhaseIncrement) {

            // Phase Distortion
            // For triangle, instead of varying SQ duty cycle, we can do phase distortion a la Casio CZ100 !
            var phase = Phase;
            if (Duty != 0.5)
                phase = PhaseDistortionTransferFunction.GetPhase(phase, Duty, this);

            var sample = phase / 180f - 1;               // Get Saw First
            sample = (Math.Abs(sample) - .5f) * 2f;      // Rectify and shift
            return sample;
        }

        void iGenerator.Sync() {
            // Don't have Phase Accumulator(s), so do nothing
        }
    }
}

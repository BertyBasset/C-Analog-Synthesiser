namespace Synth.Modules.Sources {
    internal class GeneratorSine : iGenerator {
        //                                                             Not Used
        float iGenerator.GenerateSample(float Phase, float Duty, float PhaseIncrement) {

            // Phase Distortion
            // For sine, instead of varying SQ duty cycle, we can do phase distortion a la Casio CZ100 !
            float phase = Phase;
            if (Duty != 0.5)
                phase = PhaseDistortionTransferFunction.GetPhase(phase, Duty);


            var sample = (float)Math.Sin(phase * Math.PI / 180f); 
            return sample;
        }

        void iGenerator.Sync() {
            // Don't have Phase Accumulator(s), so do nothing
        }
    }
}

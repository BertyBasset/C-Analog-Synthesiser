namespace Synth.Modules.Sources {
    internal class GeneratorSquare : iGenerator{
        const float AMPLITUDE_NORMALISATION = .7f;
        //                                                             Not Used
        float iGenerator.GenerateSample(float Phase, float Duty, float PhaseIncrement) {

            float sample;
            if (Phase > 360 * ((Duty + 1f)/2f))
                sample =  1;
            else
                sample = 0;

            return sample * AMPLITUDE_NORMALISATION;
        }

        void iGenerator.Sync() {
            // Don't have Phase Accumulator(s), so do nothing
        }
    }
}

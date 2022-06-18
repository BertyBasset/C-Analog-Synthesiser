namespace Synth.Modules.Sources {
    internal class GeneratorSquare : iGenerator{
        const float AMPLITUDE_NORMALISATION = .7f;
        //                                                             Not Used
        float iGenerator.GenerateSample(float Phase, float Duty, float PhaseIncrement) {
            float sample;
            if (Phase > 360 * Duty)
                sample =  1;
            else
                sample = 0;



            return sample * AMPLITUDE_NORMALISATION;
        }
    }
}

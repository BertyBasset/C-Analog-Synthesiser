namespace Synth.Modules.Sources {
    internal class GeneratorNoise : iGenerator {
        const float AMPLITUDE_NORMALISATION = .5f;
        Random r = new Random();
        
        //                                                             Not Used
        float iGenerator.GenerateSample(float Phase, float Duty, float PhaseIncrement) {
            float sample = (float)(r.NextDouble() * 2.0 - 1.0);
            return sample * AMPLITUDE_NORMALISATION;
        }
    }
}

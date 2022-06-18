namespace Synth.Modules.Sources {
    internal class GeneratorSaw : iGenerator{
        // For waves, like Saw which sound louder than waves like Sine, try and get them all to sounds about as loud
        const float AMPLITUDE_NORMALISATION = .5f;
        //                                                             Not Used
        float iGenerator.GenerateSample(float Phase, float Duty, float PhaseIncrement) {


            var sample = Phase / 180f - 1;
            return sample * AMPLITUDE_NORMALISATION;
        }
    }
}

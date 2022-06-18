namespace Synth.Modules.Sources {
    internal class GeneratorTriangle : iGenerator {
        //                                                       Not Used
        float iGenerator.GenerateSample(float Phase, float Duty, float PhaseIncrement) {



            var sample = Phase / 180f - 1;               // Get Saw First
            sample = (Math.Abs(sample) - .5f) * 2f;      // Rectify and shift
            return sample;
        }
    }
}

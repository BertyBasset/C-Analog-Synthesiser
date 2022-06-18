namespace Synth.Modules.Sources {
    internal class GeneratorSine : iGenerator {
        //                                                             Not Used
        float iGenerator.GenerateSample(float Phase, float Duty, float PhaseIncrement) {


            var sample = (float)Math.Sin(Phase * Math.PI / 180f); 
            return sample;

        }
    }
}

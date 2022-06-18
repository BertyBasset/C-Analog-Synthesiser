namespace Synth.Modules.Sources {

    // Generates a single audio sample using a mathematcial algorithm for a given Phase Angle
    // Wave shape is determined by Frequency and Duty
    internal interface iGenerator {


        // The Generator can either use Phase - the Phase Accumulator maintained by the Oscillator,
        // or it may chose to maintain it's own Phase Accumulator(s) by incrementing using floatIncrement
        // Cases where this might be don is if the Generator needs to track the Phase Angles of separate harmonics
        //                            Degrees 0-360     0 - 1 (0-100% duty cycle)
        internal float GenerateSample(float Phase, float Duty, float floatIncrement);
    }
}

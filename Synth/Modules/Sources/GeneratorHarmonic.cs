using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Modules.Sources;
internal class GeneratorHarmonic : iGenerator {
        
    // This needs to be a double as with floats, the accumulators drift with respect to each other
    private double[] _PhaseAccumulators = new double[1];


    private float[] _FourierCoefficients = new float[1];      // Default to fundamental!
    public float[] FourierCoefficients {
        get { return _FourierCoefficients; }
        set { 
            _FourierCoefficients = value;

            // Need to setup as many Phase Accumulators as there are elements in the Coefficients Array
            _PhaseAccumulators = new double[_FourierCoefficients.Length];
        }
    }


    // Uses phaseIncrement to maintain it's own Phase Accumulators
    float iGenerator.GenerateSample(float Phase, float Duty, float PhaseIncrement) {
        // This will take an array of floats, each element correspondong to the amplitude of
        // succesive harmonics, element 0 being amplitude of the fundamental

        float sample = 0f;

        for (int i = 0; i < _PhaseAccumulators.Length; i++) {

            double phase = _PhaseAccumulators[i] % 360f;
            if (Duty != 0.5)
                phase = PhaseDistortionTransferFunction.GetPhase((float)phase, Duty, this);

            // Calculate each harmonic
            // Shortciruit so we don't have to calculate sin if the coefficient is zero
            sample += (float)Math.Sin(phase * Math.PI / 180f) * _FourierCoefficients[i];

            // Increment Phase Accumulators
            _PhaseAccumulators[i] += (PhaseIncrement * (i + 1)) % 360;      // [0] fundamental, [1..n] subsequent overtones

        }

        return sample;
    }

    void iGenerator.Sync() {
        for (int i = 0; i < _PhaseAccumulators.Length; i++)
            _PhaseAccumulators[i] = 0f;
    }

}


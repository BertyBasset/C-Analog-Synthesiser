using Synth.Modules.Properties;

namespace Synth.Modules.Sources {
    public class Oscillator {

        #region Private Members
        // Generator class will change according to selected waveform
        // The Oscillator will call _Generators GenerateSample(float Phase) method each time it requires a new waveform sample
        iGenerator _Generator = new GeneratorSine();

        // _Phase is the oscillator's 360 degree modulo phase accumulator
        float _Phase = 0f;
        #endregion

        #region Constructor
        public Oscillator(WaveForm? waveForm = null) {
            // Default to Sine if no waveform specifified
            this.WaveForm = waveForm ?? WaveForm.GetByType(WaveformType.Sine);
        }
        #endregion

        #region Public Members
        // To simplify Oscillator, delegate all Frequency operations to containment class Frequecny
        public Frequency Frequency = new Frequency(Utils.Note.GetByDesc("A2"));     // Default to A2


        public float Value { get; internal set; }         // This is pre amplitude modified value suitable as modulation source  



        private float _Amplitude = 1f;
        public float Amplitude {
            get { return _Amplitude; }
            set {
                _Amplitude = Utils.Misc.Constrain(value, 0f, 1f);
            }
        }

        private float _Duty = 0.5f;
        public float Duty {
            get { return _Duty; }
            set {
                _Duty = Utils.Misc.Constrain(value, .01f, .99f);        // 5% to 95% as otherwise you get no sound
            }
        }

        private WaveForm _WaveForm = new WaveForm();
        public WaveForm WaveForm {
            get { return _WaveForm; }
            set {
                _WaveForm = value;
                _Generator = _WaveForm.Generator;   // This is where we assign Waveform Specific Generator to private _Generator object
            }
        }


        // **** Properties specific only some to some generator types
        //      at the moment, WaveTable, Harmonic and SuperSaw


        // Wavetable Oscillator Only !!
        private string _WaveTableFullPath = "";          // Full Path
        private string _WaveTablePath = "";              // Relative to current working doler
        public string WaveTableFileName {
            set {
                _WaveTableFullPath = Directory.GetCurrentDirectory() + "\\wavetables\\" + value;
                _WaveTablePath = value;

                if (_Generator.GetType() == typeof(GeneratorWaveTable))
                    ((GeneratorWaveTable)_Generator).FileName = _WaveTableFullPath;
            }

            get {
                return _WaveTablePath;
            }
        }


        // Harmonic Oscillator Only !!
        // This is an array containing fourier coefficients for fundamental, First Overtone, Second overtone etc.
        // Overtones > than about 2kHz will be ignored - at least in this early version
        private float[] _FourierCoefficients = new float[] { 1f, -.5f, .33f, -.25f, .2f, -.17f};     // Default to a Saw tooth
        public float[] FourierCoefficients {
            get { return _FourierCoefficients; }
            set { 
                _FourierCoefficients = value;
                if (_Generator.GetType() == typeof(GeneratorHarmonic))
                    ((GeneratorHarmonic)_Generator).FourierCoefficients = _FourierCoefficients;
            }
        }


        // Supersaw Oscillator Only !!
        // This is an array containing relative frequencies for the sawtooths. 
        // Generally, put nominal frequenct 1 in centre of array, then detune or retune (e.g. fifths) either side
        private double[] _FrequencyRatios = new double[] { 1f, -.5f, .33f, -.25f, .2f, -.17f };     // Default to a 'narrow' supersaw
        public double[] FrequencyRatios {
            get { return _FrequencyRatios; }
            set {
                _FrequencyRatios = value;
                if (_Generator.GetType() == typeof(GeneratorSuperSaw))
                    ((GeneratorSuperSaw)_Generator).FrequencyRatios = _FrequencyRatios;
            }
        }




        #endregion

        #region Public Methods

        // When selecting a new waveform, all we need to do is swap in the relevant wave Generator object
        public void WaveFormSelectByID(int id) {
            var w = WaveForm.GetByID(id);

            // Plug in a specific Generator for the wave type selected
            _Generator = w.Generator;


            // pPecial cases for WaveTable and Harmonic Generator as they need an extra parameter compare to the other generators
            if (_Generator.GetType() == typeof(GeneratorWaveTable))
                ((GeneratorWaveTable)_Generator).FileName = _WaveTableFullPath;
            if (_Generator.GetType() == typeof(GeneratorHarmonic))
                ((GeneratorHarmonic)_Generator).FourierCoefficients = _FourierCoefficients;

        }

        public float Read(float timeIncrement) {
            // Advance Phase Accumulator acording to timeIncrement and current frequency
            float delta = timeIncrement * Frequency._PostModFrequency * 360f; 
            _Phase += delta;
            _Phase = _Phase % 360;

            // Use Generator to return wave value for current state of the Phase Accumulator
            
            // Place un attenuatted version in public Value property for use elsewhere
            Value = _Generator.GenerateSample(_Phase, Duty, delta);
            return Value * Amplitude;
        }


        #endregion


    }
}

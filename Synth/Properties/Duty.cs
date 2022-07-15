using Synth.Modules.Modulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Modules.Properties
{
    public class Duty {
        public float Value;

        public iModulator? Modulator;

        private float _ModulationAmount;
        public float ModulationAmount {                            // 0 to 10000
            get { return _ModulationAmount; }
            set {
                _ModulationAmount = Utils.Misc.Constrain(value, -1f, 1f);           // not sure what this should be at the moment
            }
        }

        public float GetDuty() {
            float value = Value;
            if(Modulator != null)    
                value = value + Modulator.Value * _ModulationAmount;

            return Utils.Misc.Constrain(value, -0.98f, 0.98f);
        }
    }
}

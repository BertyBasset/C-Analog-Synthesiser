using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Modules.Modulators
{
    public class ModWheel : iModulator {
        private float _value;
        public float Value {
            get { return _value; }
            set { _value = value/127f; }
        
        }

        public void Tick(float TimeIncrement)
        {
            throw new NotImplementedException();
        }
    }
}

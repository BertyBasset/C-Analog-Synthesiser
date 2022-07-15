using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth.Modules.Modulators;
public interface iModulator {

    // All a modulator needs to do is to provide a value between -1 and +1
    public float Value { get; }
    //public void Tick(float TimeIncrement);

}


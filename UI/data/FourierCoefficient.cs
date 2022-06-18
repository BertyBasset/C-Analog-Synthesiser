using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UI.Utils;

namespace UI.Data {
    internal class FourierCoefficient {
        public string Name { get; set; } = "";
        public float[] Coefficients { get; set; } = new float[10];

        override public string ToString() { 
            return Name;
        }

        internal static List<FourierCoefficient> GetSampleList(bool AddBlank = false) {
            var list = Json<FourierCoefficient>.GetListFromFile("data\\SampleFourierCoefficients.json");

            if(AddBlank)
                list.Insert(0, new FourierCoefficient() { Name = "", Coefficients = new float[10]});   

            return list;
        }
    }
}

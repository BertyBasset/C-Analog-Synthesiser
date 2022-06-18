using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UI.Utils;

namespace UI.Data {
    internal class SuperSaw {
        public string Name { get; set; } = "";
        public float Limit { get; set; }                // This is min/max, otherwise all wave reinforce -> infinity at some point
        public double[] FrequencyRatios { get; set; } = new double[0];

        public override string ToString() {
            return Name;
        }

        internal static List<SuperSaw> GetSampleList(bool AddBlank = false) {
            var list = Json<SuperSaw>.GetListFromFile("data\\SuperSaws.json");

            if (AddBlank)
                list.Insert(0, new SuperSaw() { Name = "", FrequencyRatios = new double[7] });

            return list;
        }
    }

}


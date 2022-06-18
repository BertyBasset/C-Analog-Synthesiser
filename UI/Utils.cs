using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UI.Utils {
    internal class Json<T> {
        public static List<T> GetListFromFile(string FileName) {
            if(!FileName.StartsWith("\\"))
                FileName = "\\" + FileName;

            var list = JsonConvert.DeserializeObject<List<T>>(
                File.OpenText(Directory.GetCurrentDirectory() + FileName).ReadToEnd(),
                new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore }
            );

            Debug.Assert(list != null);
            return list;
        }
    }
}

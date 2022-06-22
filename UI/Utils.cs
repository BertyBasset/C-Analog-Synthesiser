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

            string content = "";
            using (var f = File.OpenText(Directory.GetCurrentDirectory() + FileName)) {
                content = f.ReadToEnd();
            }


            var list = JsonConvert.DeserializeObject<List<T>>(
                content,
                new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore }
            );

            Debug.Assert(list != null);
            return list;
        }


        public static void SaveListToFile(string FileName, List<T> list) {
            if (!FileName.StartsWith("\\"))
                FileName = "\\" + FileName;

            var s = JsonConvert.SerializeObject(list);
            File.WriteAllText(Directory.GetCurrentDirectory() + FileName, s); 
        }
    }
}

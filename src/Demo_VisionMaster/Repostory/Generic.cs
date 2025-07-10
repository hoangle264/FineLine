using Demo_VisionMaster.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_VisionMaster.Repostory
{
    public class Generic
    {
        public Generic() { }
        public ROI_Information Load(string filePath)
        {
            if (!File.Exists(filePath))
                return new ROI_Information();
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ROI_Information>(json);
        }

        public void Save(string filePath, ROI_Information data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}

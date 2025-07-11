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

        public T Load<T>(string filePath) where T : new()
        {
            if (!File.Exists(filePath))
                return new T();
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Save<T>(string filePath, T data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}

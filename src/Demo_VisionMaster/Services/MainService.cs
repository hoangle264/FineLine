using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace Demo_VisionMaster.Services
{
    public class MainService
    {
        public MainService() { }
        public async Task Init() 
        {
            
        }
        public void Run() 
        {
            Crop();
            Enhance();
            Surface();
            Blod();
            FindLiner();
        }
        public void Crop() { }
        public void Enhance() { }
        public void Surface() { }
        public void Blod() { }
        public void FindLiner() { }
    }
}

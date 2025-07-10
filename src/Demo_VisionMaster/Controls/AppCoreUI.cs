using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_VisionMaster.Controls
{
    public partial class AppCoreBackend
    {
        private void StartUI() 
        {
            Application.Run(MainView.Instance);
        }
    }
}

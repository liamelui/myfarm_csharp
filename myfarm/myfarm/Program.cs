using System;
using System.Windows.Forms;

namespace myfarm
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GardenForm());
        }
    }
}

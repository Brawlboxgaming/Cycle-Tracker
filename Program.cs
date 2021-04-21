using System;
using System.Windows.Forms;

namespace Cycle_Tracker
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new VisualForm();
            var _ = form.Handle;
            Application.Run(form);
        }
    }
}
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SkyForBusLH
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                using (var writer = new StreamWriter("C:\\LyncHistoryErrorLog.txt", true, Encoding.Unicode))
                {
                    writer.WriteLine("Exception caught at {0}", DateTime.Now);
                    writer.WriteLine(e);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace VPNAuto
{
    class Program
    {

        [DllImport("user32.dll")]
        internal static extern bool SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, Int32 lParam);
        static Int32 WM_SYSCOMMAND = 0x0112;
        static Int32 SC_MINIMIZE = 0x0F020;


        static Thread t;
        static void Main(string[] args)
        {
            Console.WriteLine("VPNAuto Start");
            while (true) {
                string cmd = Console.ReadLine();

                if (cmd == "start")
                {
                    t = new Thread(new ThreadStart(Connecting));
                    t.Start();
                }
                else if (cmd == "stop")
                {
                    if (t != null) t.Abort();
                }
            }
        }

        static string[] getCred()
        {
            string[] c = new string[2];

            StreamReader r = new StreamReader("conf.txt");

            c[0] = r.ReadLine();
            c[1] = r.ReadLine();
            r.Close();

            return c;
        }

        static void Connecting()
        {
            if (!TA.isAnonymous("vpn.anonine.net"))
            {
                string[] asd = getCred();
                string v = "Anonine";
                string u = asd[0];
                string p = asd[1];

                string conPar = v + " " + u + " " + p;
                //System.Diagnostics.Process.Start("rasdial.exe", v + u + p);
                System.Diagnostics.Process.Start("rasdial.exe", conPar);
                //System.Diagnostics.Process.Start("rasdial.exe", "Anonine krigaruffe aqdymn56lk");
            }
            SendMessage(Process.GetCurrentProcess().MainWindowHandle, WM_SYSCOMMAND, SC_MINIMIZE, 0);
            Thread.Sleep(21600000);
        }


    }
}

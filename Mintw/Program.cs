using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mintw
{
    static class Program
    {
        static Tray tf;
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            tf = new Tray();
            Application.Run();
            //Application.Run(new Tray());
        }
    }
}

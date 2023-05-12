/**************************************************************************
 *                                                                        *
 *  File:        Programs.cs                                              *
 *  Copyright:   (c) 2022, Ichim Paula-Mădălina                           *
 *  E-mail:      paula-madalina.ichim@student.tuiasi.ro                   *
 *                                                                        *
 *  Description: Main entry point of Application                          *
 *                                                                        *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ALPHABETA_MINIMAX
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

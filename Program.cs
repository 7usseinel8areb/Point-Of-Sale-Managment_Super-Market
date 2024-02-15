using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_Demo.Forms;
using POS_Demo.Forms.Customers;
using POS_Demo.Forms.Products;
using POS_Demo.Forms.SalesBill;
using POS_Demo.Forms.Suppliers;
using POS_Demo.Forms.Users;

namespace POS_Demo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_Welcome());
        }
    }
}

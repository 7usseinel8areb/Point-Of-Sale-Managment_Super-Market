using DevExpress.XtraEditors;
using POS_Demo.Forms.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Demo.Forms
{
    public partial class frm_Welcome : DevExpress.XtraEditors.XtraForm
    {
        public frm_Welcome()
        {
            InitializeComponent();
        }

        private void frm_Welcome_Load(object sender, EventArgs e)
        {
            
        }
        void OpenForm()
        {
            Application.Run(new frm_Login());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            this.Close();
            Thread th = new Thread(OpenForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
    }
}

using DevExpress.XtraEditors;
using POS_Demo.DB;
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
    public partial class frm_Login : DevExpress.XtraEditors.XtraForm
    {
        POSTutEntities db = new POSTutEntities();
        public frm_Login()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var CheckUserNameAndPassword = db.Users.Where(i => i.UserName == txt_UserName.Text && i.Password == txt_Password.Text).Count() == 1;
            if (CheckUserNameAndPassword)
            {
                //openinig new page and close this one
                this.Close();
                Thread th = new Thread(OpenForm);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            else
            {
                MessageBox.Show("User Name or Password are invalid please try again");
            }
            
        }
        void OpenForm()
        {
            Application.Run(new frm_Main());
        }
    }
}

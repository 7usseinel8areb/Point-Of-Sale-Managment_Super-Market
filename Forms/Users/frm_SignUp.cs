using DevExpress.XtraEditors;
using POS_Demo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Demo.Forms.Users
{
    public partial class frm_SignUp : DevExpress.XtraEditors.XtraForm
    {
        POSTutEntities db = new POSTutEntities();
        string imgPath;//button1_Click عشان اعرف اضيفه في الداتا بيز لازم يكون متشاف في جزء
        public frm_SignUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool CheckUserName = db.Users.Where(u => u.UserName ==txt_UserName.Text).Count() == 1;
            if (CheckUserName)
            {
                MessageBox.Show("User name already exists please try again!");
                return;
            }
            User user = new User();
            user.UserName = txt_UserName.Text;
            user.Password = txt_Password.Text;
            user.Image = imgPath;

            db.Users.Add(user);
            db.SaveChanges();//Go to the db and save the changes
            #region Another Way Using Anonymous Type
            /*db.Users.Add(new User { UserName = txt_UserName.Text, Password = txt_Password.Text });
            db.SaveChanges();*/
            #endregion

            if (imgPath != "")
            {
                string newImagePath = Environment.CurrentDirectory + $"\\Images\\Users\\{user.ID}.png";
                File.Copy(imgPath, newImagePath);
                user.Image = newImagePath;
                db.SaveChanges();
            }
            MessageBox.Show("تم الحفظ");
            txt_UserName.Clear();
            txt_Password.Clear();
            pictureBox.ImageLocation = "";
            
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)//OK لو هو اختار صوره وضغط
            {
                imgPath = openFileDialog.FileName;
                pictureBox.ImageLocation  = imgPath;//الصوره بتاع الفورم Box بيجيب الفايل لوكيشن اللي عملتها وبيخزناه في 

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txt_UserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

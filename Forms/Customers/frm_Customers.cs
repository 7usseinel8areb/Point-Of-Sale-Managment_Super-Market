using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraEditors.Repository;
using POS_Demo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace POS_Demo.Forms.Customers
{
    public partial class frm_Customers : DevExpress.XtraEditors.XtraForm
    {
        POSTutEntities db = new POSTutEntities();
        string imgPath = "";
        int ID;
        Customer_And_Supplier SelectedCustomer;
        public frm_Customers()
        {
            InitializeComponent();
            dgv_Customers.DataSource = db.Customer_And_Supplier.Where(c => c.IsCutomer).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_CustomerSearchPhone.Text == "")
            {
                dgv_Customers.DataSource = db.Customer_And_Supplier.Where(p =>p.IsCutomer && p.Name.Contains(txt_CustomerSearchName.Text)).ToList();
            }
            else
            {
                dgv_Customers.DataSource = db.Customer_And_Supplier.Where(p => p.IsCutomer && (p.Name.Contains(txt_CustomerSearchName.Text) || p.Phone.Contains( txt_CustomerSearchPhone.Text))).ToList();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txt_CustomerName.Text == "" || txt_CustomerPhone.Text == "" || txt_CustomerEmail.Text == "")
            {
                MessageBox.Show("برجاء ملئ جميع البيانات المطلوبه اولا");
                return;
            }
            Customer_And_Supplier customer = new Customer_And_Supplier()
            {
                Name = txt_CustomerName.Text,
                Address = txt_CustomerAddress.Text,
                Company = txt_CustomerCompany.Text,
                IsCutomer = true,
                IsActive = IsActive_.Checked,
                Phone = txt_CustomerPhone.Text,
                Notes = txt_CustomerNotes.Text,
                Email = txt_CustomerEmail.Text,
                Image = ""
            };
            db.Customer_And_Supplier.Add(customer);
            db.SaveChanges();
            if (imgPath != "")
            {
                string newImgPath = Environment.CurrentDirectory + $"\\Images\\Customers\\{customer.ID}.png";
                File.Copy(imgPath, newImgPath);
                customer.Image = newImgPath;
                db.SaveChanges();
            }
            MessageBox.Show("تم اضافة مستخدم جديد");
            txt_CustomerName.Text = "";
            txt_CustomerAddress.Text = "";
            txt_CustomerCompany.Text = "";
            IsActive_.Checked = false;
            txt_CustomerPhone.Text = "";
            txt_CustomerNotes.Text = "";
            txt_CustomerEmail.Text = "";
            pictureBox.ImageLocation = "";
            dgv_Customers.DataSource = db.Customer_And_Supplier.Where(c => c.IsCutomer).ToList();
        }
        private void pictureBox_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("هل تريد اضافة صورة؟", "", MessageBoxButtons.YesNoCancel);
            if(res == DialogResult.No)
            {
                imgPath = "";
                pictureBox.ImageLocation = imgPath;
            }
            else if (res == DialogResult.Yes)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)//OK لو هو اختار صوره وضغط
                {
                    imgPath = openFileDialog.FileName;
                    pictureBox.ImageLocation = imgPath;//الصوره بتاع الفورم Box بيجيب الفايل لوكيشن اللي عملتها وبيخزناه في 

                }
            }
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SelectedCustomer.Name = txt_CustomerName.Text;
            SelectedCustomer.Email = txt_CustomerEmail.Text;
            SelectedCustomer.Notes = txt_CustomerNotes.Text;
            SelectedCustomer.Address = txt_CustomerAddress.Text;
            SelectedCustomer.Company = txt_CustomerCompany.Text;
            SelectedCustomer.Phone = txt_CustomerPhone.Text;
            SelectedCustomer.Image = imgPath;
            SelectedCustomer.IsActive = IsActive_.Checked;
            if (imgPath != "")
            {
                string newImagePath = Environment.CurrentDirectory + $"\\Images\\Customers\\{SelectedCustomer.ID}.png";
                File.Copy(imgPath, newImagePath, true);//true to remove old photo and replace the new one because they have the same name
                SelectedCustomer.Image = newImagePath;
                db.SaveChanges();
            }
            db.SaveChanges();
            MessageBox.Show("تم الحفظ بنجاح");
            dgv_Customers.DataSource = db.Customer_And_Supplier.Where(c => c.IsCutomer).ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("تأكيد الحذف", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)//if the user is sure to delete and choose YES button
            {
                if (SelectedCustomer.Image != "")
                {
                    File.Delete(Environment.CurrentDirectory + $"\\Images\\Customers\\{SelectedCustomer.ID}.png");
                }
                db.Customer_And_Supplier.Remove(SelectedCustomer);
                db.SaveChanges();
                MessageBox.Show("تم الحذف");
                dgv_Customers.DataSource = db.Customer_And_Supplier.Where(u => u.IsCutomer).ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgv_Customers.DataSource = db.Customer_And_Supplier.Where(c => c.IsCutomer).ToList();
        }




        private void dgv_Customers_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ID = int.Parse(dgv_Customers.CurrentRow.Cells[0].Value.ToString());
                //Cells[0] => first column // value => ID
                SelectedCustomer = db.Customer_And_Supplier.SingleOrDefault(p => p.ID == ID);
                txt_CustomerName.Text = SelectedCustomer.Name;
                txt_CustomerEmail.Text = SelectedCustomer.Email;
                txt_CustomerPhone.Text = SelectedCustomer.Phone;
                txt_CustomerCompany.Text = SelectedCustomer.Company;
                txt_CustomerNotes.Text = SelectedCustomer.Notes;
                txt_CustomerAddress.Text = SelectedCustomer.Address;
                IsActive_.Checked = SelectedCustomer.IsActive;
                pictureBox.ImageLocation = SelectedCustomer.Image.ToString();
            }
            catch
            {
                //Nothing
            }
        }
    }
}

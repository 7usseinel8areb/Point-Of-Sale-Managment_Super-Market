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

namespace POS_Demo.Forms.Suppliers
{
    public partial class frm_Supplier : DevExpress.XtraEditors.XtraForm
    {
        POSTutEntities db = new POSTutEntities();
        string imgPath = "";
        int ID;
        Customer_And_Supplier SelectedSupplier;
        public frm_Supplier()
        {
            InitializeComponent();
            dgv_Customers.DataSource = db.Customer_And_Supplier.Where(c => !c.IsCutomer).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_CustomerSearchPhone.Text == "")
            {
                dgv_Customers.DataSource = db.Customer_And_Supplier.Where(p => !p.IsCutomer && p.Name.Contains(txt_CustomerSearchName.Text)).ToList();
            }
            else
            {
                dgv_Customers.DataSource = db.Customer_And_Supplier.Where(p => !p.IsCutomer && (p.Name.Contains(txt_CustomerSearchName.Text) || p.Phone.Contains(txt_CustomerSearchPhone.Text))).ToList();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgv_Customers.DataSource = db.Customer_And_Supplier.Where(c => !c.IsCutomer).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txt_CustomerName.Text == "" || txt_CustomerPhone.Text == "" || txt_CustomerEmail.Text == "")
            {
                MessageBox.Show("برجاء ملئ جميع البيانات المطلوبه اولا");
                return;
            }
            else if (db.Customer_And_Supplier.Any(s => s.Phone == txt_CustomerPhone.Text))
            {
                MessageBox.Show("رقم الهاتف مستخدم بالفعل برجاء ادخال رقم اخر");
                return;
            }
            else if (db.Customer_And_Supplier.Any(s => s.Email == txt_CustomerEmail.Text))
            {
                MessageBox.Show("البريد الألكتروني مستخدم بالفعل برجاء ادخال بريد الكتروني متاح");
                return;
            }
            
            Customer_And_Supplier customer = new Customer_And_Supplier()
            {
                Name = txt_CustomerName.Text,
                Address = txt_CustomerAddress.Text,
                Company = txt_CustomerCompany.Text,
                IsCutomer = false,
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
                string newImgPath = Environment.CurrentDirectory + $"\\Images\\Suppliers\\{customer.ID}.png";
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
            dgv_Customers.DataSource = db.Customer_And_Supplier.Where(c => !c.IsCutomer).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txt_CustomerName.Text == "" || txt_CustomerPhone.Text == "" || txt_CustomerEmail.Text == "")
            {
                MessageBox.Show("برجاء ملئ جميع البيانات المطلوبه اولا");
                return;
            }
            else if (db.Customer_And_Supplier.Any(s => s.Phone == txt_CustomerPhone.Text && s.ID != ID))
            {
                MessageBox.Show("رقم الهاتف مستخدم بالفعل برجاء ادخال رقم اخر");
                return;
            }
            else if (db.Customer_And_Supplier.Any(s => s.Email == txt_CustomerEmail.Text && s.ID != ID))
            {
                MessageBox.Show("البريد الألكتروني مستخدم بالفعل برجاء ادخال بريد الكتروني متاح");
                return;
            }
            SelectedSupplier.Name = txt_CustomerName.Text;
            SelectedSupplier.Email = txt_CustomerEmail.Text;
            SelectedSupplier.Notes = txt_CustomerNotes.Text;
            SelectedSupplier.Address = txt_CustomerAddress.Text;
            SelectedSupplier.Company = txt_CustomerCompany.Text;
            SelectedSupplier.Phone = txt_CustomerPhone.Text;
            SelectedSupplier.Image = imgPath;
            SelectedSupplier.IsActive = IsActive_.Checked;
            if (imgPath != "")
            {
                string newImagePath = Environment.CurrentDirectory + $"\\Images\\Suppliers\\{SelectedSupplier.ID}.png";
                File.Copy(imgPath, newImagePath, true);//true to remove old photo and replace the new one because they have the same name
                SelectedSupplier.Image = newImagePath;
                db.SaveChanges();
            }
            db.SaveChanges();
            MessageBox.Show("تم الحفظ بنجاح");
            dgv_Customers.DataSource = db.Customer_And_Supplier.Where(c => !c.IsCutomer).ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("تأكيد الحذف", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)//if the user is sure to delete and choose YES button
            {
                if (SelectedSupplier.Image != "")
                {
                    File.Delete(Environment.CurrentDirectory + $"\\Images\\Suppliers\\{SelectedSupplier.ID}.png");
                }
                db.Customer_And_Supplier.Remove(SelectedSupplier);
                db.SaveChanges();
                MessageBox.Show("تم الحذف");
                txt_CustomerName.Text = "";
                txt_CustomerAddress.Text = "";
                txt_CustomerCompany.Text = "";
                IsActive_.Checked = false;
                txt_CustomerPhone.Text = "";
                txt_CustomerNotes.Text = "";
                txt_CustomerEmail.Text = "";
                pictureBox.ImageLocation = "";
                dgv_Customers.DataSource = db.Customer_And_Supplier.Where(c => !c.IsCutomer).ToList();
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("هل تريد اضافة صورة؟", "", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.No)
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

        private void dgv_Customers_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ID = int.Parse(dgv_Customers.CurrentRow.Cells[0].Value.ToString());
                //Cells[0] => first column // value => ID
                SelectedSupplier = db.Customer_And_Supplier.SingleOrDefault(p => p.ID == ID);
                txt_CustomerName.Text = SelectedSupplier.Name;
                txt_CustomerEmail.Text = SelectedSupplier.Email;
                txt_CustomerPhone.Text = SelectedSupplier.Phone;
                txt_CustomerCompany.Text = SelectedSupplier.Company;
                txt_CustomerNotes.Text = SelectedSupplier.Notes;
                txt_CustomerAddress.Text = SelectedSupplier.Address;
                IsActive_.Checked = SelectedSupplier.IsActive;
                pictureBox.ImageLocation = SelectedSupplier.Image.ToString();
            }
            catch
            {
                //Nothing
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}

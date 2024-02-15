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
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace POS_Demo.Forms.Products
{
    public partial class frm_ProductList : DevExpress.XtraEditors.XtraForm
    {
        POSTutEntities db = new POSTutEntities();
        string imgPath = "";
        int ID = 0;
        Product SelectedProduct;
        public frm_ProductList()
        {
            InitializeComponent();

            comboBox1.DataSource = db.Categories.ToList();
            comboBox2.DataSource = db.Categories.ToList();

            dgv_Products.DataSource = db.Products.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if(txt_ProductSearchCode.Text == "")
           {
                dgv_Products.DataSource = db.Products.Where(p => p.Name.Contains(txt_ProductSearchName.Text)).ToList();
           }
           else
           {
               dgv_Products.DataSource = db.Products.Where(p => p.Code == txt_ProductSearchCode.Text).ToList();
           }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgv_Products.DataSource = db.Products.ToList();
            txt_ProductSearchCode.Text = "";
            txt_ProductSearchName.Text = "";
        }

        private void dgv_Products_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ID = int.Parse(dgv_Products.CurrentRow.Cells[0].Value.ToString());
                //Cells[0] => first column // value => ID
                SelectedProduct = db.Products.SingleOrDefault(p => p.ID == ID);
                txt_ProductName.Text = SelectedProduct.Name;
                txt_ParCode.Text = SelectedProduct.Code;
                txt_Price.Text = SelectedProduct.Price.ToString();
                txt_Quantity.Text = SelectedProduct.Quantity.ToString();
                txt_Notes.Text = SelectedProduct.Notes.ToString();
                pictureBox.ImageLocation = SelectedProduct.Image;
                comboBox1.SelectedValue =(int) SelectedProduct.CategoyID;
            }
            catch
            {
                //Nothing
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)//OK لو هو اختار صوره وضغط
            {
                imgPath = openFileDialog.FileName;
                pictureBox.ImageLocation = imgPath;//الصوره بتاع الفورم Box بيجيب الفايل لوكيشن اللي عملتها وبيخزناه في 

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool CheckProductCode = db.Products.Any(p => p.Code == txt_ParCode.Text && p.ID != SelectedProduct.ID);
            if (CheckProductCode)
            {
                MessageBox.Show("This product code already exists please enter another code!");
            }
            if (txt_ProductName.Text == "" || txt_ProductName.Text == "" || txt_Price.Text == "" || comboBox1.SelectedValue == null)
            {
                MessageBox.Show("برجاء ادخال البيانات المطلوبه اولا");
                return;
            }
            try
            {
                decimal.TryParse(txt_Price.Text, out decimal ParsedPrice);
                int.TryParse(txt_Quantity.Text, out int ParsedQuantity);
                SelectedProduct.Name = txt_ProductName.Text;
                SelectedProduct.Code = txt_ParCode.Text;
                SelectedProduct.Notes = txt_Notes.Text;
                SelectedProduct.Price = ParsedPrice;
                SelectedProduct.Quantity = ParsedQuantity;
                SelectedProduct.CategoyID =int.Parse(comboBox1.SelectedValue.ToString());
                if (imgPath != "")
                {
                    string newImagePath = Environment.CurrentDirectory + $"\\Images\\Products\\{SelectedProduct.ID}.png";
                    File.Copy(imgPath, newImagePath, true);//true to remove old photo and replace the new one because they have the same name
                    SelectedProduct.Image = newImagePath;
                    db.SaveChanges();
                }
                db.SaveChanges();
                MessageBox.Show("تم الحفظ بنجاح");
            }
            catch
            {
                //Nothing
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("تأكيد الحذف", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)//if the user is sure to delete and choose YES button
            {
                var SelectedProducToRemove = db.Products.Find(ID);
                if(SelectedProducToRemove.Image != "")
                {
                    File.Delete(Environment.CurrentDirectory + $"\\Images\\Products\\{SelectedProducToRemove.ID}.png");
                }
                db.Products.Remove(SelectedProducToRemove);
                db.SaveChanges();
                MessageBox.Show("تم الحذف");
                dgv_Products.DataSource = db.Products.ToList();
            }
            
        }

        private void label13_Click(object sender, EventArgs e)
        {
            frm_Product productForm = new frm_Product();
            productForm.Show();
        }

        private void frm_ProductList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSTutDataSet2.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.pOSTutDataSet2.Category);

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CatID = int.Parse(comboBox2.SelectedValue.ToString());
            dgv_Products.DataSource = db.Products.Where(p => p.CategoyID == CatID).ToList();
        }
    }
}

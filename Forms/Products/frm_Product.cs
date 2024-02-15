using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering;
using POS_Demo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace POS_Demo.Forms.Products
{
    public partial class frm_Product : DevExpress.XtraEditors.XtraForm
    {
        POSTutEntities db = new POSTutEntities();
        string imgPath;
        public frm_Product()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool CheckProductCode = db.Products.Where(p => p.Code == txt_ParCode.Text).Count() == 1;
            if (CheckProductCode)
            {
                MessageBox.Show("This product code already exists please enter another code!");
            }
            if (txt_ProductName .Text== ""|| txt_ProductName.Text == ""|| txt_Price.Text == "" || comboBox1.SelectedValue == null) 
            {
                MessageBox.Show("برجاء ادخال البيانات المطلوبه اولا");
                return;
            }

            decimal.TryParse(txt_Price.Text, out decimal ParsedPrice);
            int.TryParse(txt_Quantity.Text, out int ParsedQuantity);

            Product product = new Product()
            {
                Name = txt_ProductName.Text,
                Code = txt_ParCode.Text,
                Price = ParsedPrice,
                Notes = txt_Notes.Text,
                Quantity = ParsedQuantity,
                CategoyID = int.Parse(comboBox1.SelectedValue.ToString())
            };
            db.Products.Add(product);
            db.SaveChanges();
            if(imgPath != "")
            {
                string newImagePath = Environment.CurrentDirectory + $"\\Images\\Products\\{product.ID}.png";
                File.Copy(imgPath, newImagePath);
                product.Image = newImagePath;
                db.SaveChanges();
            }
            MessageBox.Show("تم حفظ المنتج");
            txt_Notes.Text = "";
            txt_ParCode.Text = "";
            txt_Price.Text = "";
            txt_ProductName.Text = "";
            txt_Quantity.Text = "";
            pictureBox.ImageLocation = "";
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

        private void label13_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(OpenForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        void OpenForm()
        {
            Application.Run(new frm_ProductList());
        }

        private void frm_Product_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSTutDataSet1.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter1.Fill(this.pOSTutDataSet1.Category);
            // TODO: This line of code loads data into the 'pOSTutDataSet.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.pOSTutDataSet.Category);

        }
    }
}

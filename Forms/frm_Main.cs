using DevExpress.XtraEditors;
using POS_Demo.Forms.Customers;
using POS_Demo.Forms.Products;
using POS_Demo.Forms.SalesBill;
using POS_Demo.Forms.Suppliers;
using POS_Demo.Forms.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Demo
{
    public partial class frm_Main : DevExpress.XtraEditors.XtraForm
    {
        public frm_Main()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_SignUp signUpForm = new frm_SignUp();
            signUpForm.Show();
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Product productForm = new frm_Product();
            productForm.Show();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            frm_Customers frm_Customers = new frm_Customers();
            frm_Customers.Show();
        }

        private void الموردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Supplier frm_Supplier = new frm_Supplier();
            frm_Supplier.Show();
        }

        private void ediToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ProductList productListForm = new frm_ProductList();
            productListForm.Show();
        }

        private void فاتورةمبيعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_SallesBills frm_SallesBills = new frm_SallesBills();
            frm_SallesBills.Show();
        }
    }
}

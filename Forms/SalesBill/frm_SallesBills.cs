using DevExpress.XtraEditors;
using POS_Demo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Demo.Forms.SalesBill
{
    public partial class frm_SallesBills : DevExpress.XtraEditors.XtraForm
    {
        POSTutEntities db = new POSTutEntities();
        public frm_SallesBills()
        {
            InitializeComponent();
            comboBox1.DataSource = db.Customer_And_Supplier.Where(c => c.IsActive && c.IsCutomer).ToList();
            comboBox3.DataSource = db.Categories.ToList();
        }

        private void frm_SallesBills_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSTutDataSet3.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.pOSTutDataSet3.Category);
            // TODO: This line of code loads data into the 'pOSTutDataSet4.Customer_And_Supplier' table. You can move, or remove it, as needed.
            this.customer_And_SupplierTableAdapter.Fill(this.pOSTutDataSet4.Customer_And_Supplier);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.customer_And_SupplierTableAdapter.FillBy(this.pOSTutDataSet4.Customer_And_Supplier);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int CatID = int.Parse(comboBox3.SelectedValue.ToString());
                dataGridView2.DataSource = db.Products.Where(p => p.CategoyID == CatID).ToList();
            }
            catch
            {

            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {

        }
    }
}

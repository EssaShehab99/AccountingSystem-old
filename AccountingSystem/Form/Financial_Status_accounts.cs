using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AccountingSystem
{
    public partial class Financial_Status_accounts : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQGE058\MSSQLSERVER,1433; Initial Catalog=accountingsystem;Integrated Security=false; User ID=sa;password=Essa771838957;");
        public Financial_Status_accounts()
        {
            InitializeComponent();
        }
        private void inserttoTreeView()
        {
            DataTable tb = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select level_name 'الحسابات' from accounts_levels", con);
            da.Fill(tb);
            treeView1.Nodes.Add("الحسابات");
            foreach (DataRow dr in tb.Rows)
            {
                TreeNode nod = new TreeNode(dr["الحسابات"].ToString());
                DataTable tb2 = new DataTable();
                treeView1.Nodes.Add(nod);
                SqlDataAdapter da2 = new SqlDataAdapter("select m.account_name'account_name' from main_accounts m join accounts_currency c on(m.currency_id=c.currency_id) join accounts_levels l on(c.level_id=l.level_id) where l.level_name=N'" + dr["الحسابات"].ToString() + "'", con);
                da2.Fill(tb2);
                foreach (DataRow dr2 in tb2.Rows)
                {
                    nod.Nodes.Add(dr2["account_name"].ToString());
                }
            }
        }
        private void selectTreeView()
        {
            try
            {
                if (treeView1.SelectedNode.Text == "الخزينة" || treeView1.SelectedNode.Text == "عام" || treeView1.SelectedNode.Text == "موردين" || treeView1.SelectedNode.Text == "عملاء")
                {
                    SqlDataAdapter da = new SqlDataAdapter("select m.account_id'رقم الحساب' ,m.account_name'الاسم' ,l.level_name'النوع' ,c.currency_name'العملة' ,(isnull(m.amount_creditor,0)-isnull(m.amount_debit,0)) as'رصيد الحساب',isnull(m.amount_creditor,0)'دائن',isnull(m.amount_debit,0)'مدين' ,m.description 'التفاصيل',m.phone_number'رقم الهاتف',m.account_status'حالة الحساب',m.number_edit'مرات التعديل',m.account_date'التاريخ' from main_accounts m join accounts_currency c on(m.currency_id =c.currency_id) join accounts_levels l on(c.level_id =l.level_id) where l.level_name=N'" + treeView1.SelectedNode.Text + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewX1.DataSource = dt;
                    
                    SqlDataAdapter da0 = new SqlDataAdapter("select l.level_name'التصنيف',c.currency_name as'العملة',isnull(c.amount_creditor,0)-isnull(c.amount_debit,0) as'الرصيد',isnull(c.amount_creditor,0) as'دائن',isnull(c.amount_debit,0) as'مدين' from accounts_currency c join accounts_levels l on(c.level_id=l.level_id) where l.level_name=N'" + treeView1.SelectedNode.Text + "'", con);
                    DataTable dt0 = new DataTable();
                    da0.Fill(dt0);
                    dataGridViewX2.DataSource = dt0;

                }
                else
                {
                    SqlDataAdapter da = new SqlDataAdapter("select m.account_id'رقم الحساب' ,m.account_name'الاسم' ,l.level_name'النوع' ,c.currency_name'العملة' ,(isnull(m.amount_creditor,0)-isnull(m.amount_debit,0)) as'رصيد الحساب',isnull(m.amount_creditor,0)'دائن',isnull(m.amount_debit,0)'مدين' ,m.description 'التفاصيل',m.phone_number'رقم الهاتف',m.account_status'حالة الحساب',m.number_edit'مرات التعديل',m.account_date'التاريخ' from main_accounts m join accounts_currency c on(m.currency_id =c.currency_id) join accounts_levels l on(c.level_id =l.level_id) where m.account_name=N'" + treeView1.SelectedNode.Text + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewX1.DataSource = dt;

                    SqlDataAdapter da0 = new SqlDataAdapter("select m.account_name'الاسم',c.currency_name'العملة',isnull(m.amount_creditor,0)-isnull(m.amount_debit,0) as'الرصيد',isnull(m.amount_creditor,0) as'دائن',isnull(m.amount_debit,0) as'مدين' from main_accounts m join accounts_currency c on(m.currency_id=c.currency_id) where m.account_name=N'" + treeView1.SelectedNode.Text + "'", con);
                    DataTable dt0 = new DataTable();
                    da0.Fill(dt0);
                    dataGridViewX2.DataSource = dt0;
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض الحسابات ", "1", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void Financial_Status_accounts_Load(object sender, EventArgs e)
        {
            inserttoTreeView();
        }

        private void dataGridViewX1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }

        private void buttonX5_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxEx3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                //message 1 -15
                SqlDataAdapter da = new SqlDataAdapter("select m.account_id'رقم الحساب' ,m.account_name'الاسم' ,l.level_name'النوع' ,c.currency_name'العملة' ,(isnull(m.amount_creditor,0)-isnull(m.amount_debit,0)) as'رصيد الحساب',isnull(m.amount_creditor,0)'دائن',isnull(m.amount_debit,0)'مدين' ,m.description 'التفاصيل',m.phone_number'رقم الهاتف',m.account_status'حالة الحساب',m.number_edit'مرات التعديل',m.account_date'التاريخ' from main_accounts m join accounts_currency c on(m.currency_id =c.currency_id) join accounts_levels l on(c.level_id =l.level_id)", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewX1.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض الحسابات ", "1", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void buttonX3_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            inserttoTreeView();
            dataGridViewX2.DataSource = null;
            dataGridViewX1.DataSource = null;
        }
        private void buttonX6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            //inserttodataGridView();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectTreeView();

        }
    }
}
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
    public partial class Restore_Backup : Form
    {
        //messagw 144
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQGE058\MSSQLSERVER,1433; Initial Catalog=accountingsystem;Integrated Security=false; User ID=sa;password=Essa771838957;");
        public Restore_Backup()
        {
            InitializeComponent();
        }

        private void Restore_Backup_Load(object sender, EventArgs e)
        {

        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL SERVER database backup files|*.bak";
            dlg.Title = "Database restore";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxX11.Text = dlg.FileName;
                textBoxX11.Enabled = true;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string database = con.Database.ToString();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                string sqlStmt2 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand bu2 = new SqlCommand(sqlStmt2, con);
                bu2.ExecuteNonQuery();

                string sqlStmt3 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + textBoxX11.Text + "'WITH REPLACE;";
                SqlCommand bu3 = new SqlCommand(sqlStmt3, con);
                bu3.ExecuteNonQuery();

                string sqlStmt4 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                SqlCommand bu4 = new SqlCommand(sqlStmt4, con);
                bu4.ExecuteNonQuery();

                MessageBox.Show("تم استعادة النسخة الاحتياطية بنجاح","144",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}

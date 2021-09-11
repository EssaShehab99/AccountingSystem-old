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
    public partial class Take_Backup : Form
    {
        //messagw 145-146
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQGE058\MSSQLSERVER,1433; Initial Catalog=accountingsystem;Integrated Security=false; User ID=sa;password=Essa771838957;");
        public Take_Backup()
        {
            InitializeComponent();
        }

        private void Take_Backup_Load(object sender, EventArgs e)
        {

        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxX11.Text = dlg.SelectedPath;
                buttonX1.Enabled = true;
            }

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string database = con.Database.ToString();
            try
            {
                if (textBoxX11.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء تحديد الملف لحفظ النسخة الاحتياطية","145",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                }
                else
                {
                    using (SqlCommand command = new SqlCommand("BACKUP DATABASE [" + database + "] TO DISK='" + textBoxX11.Text + "\\" + "database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'", con))
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        command.ExecuteNonQuery();
                        MessageBox.Show("تم أخذ النسخة الاحتياطية", "146", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        con.Close();
                        buttonX1.Enabled = false;
                    }
                }

            }
            catch
            {

            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

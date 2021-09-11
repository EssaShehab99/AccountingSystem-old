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
    public partial class Login : Form
    {
        //messagw 125-127

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQGE058\MSSQLSERVER,1433; Initial Catalog=accountingsystem;Integrated Security=false; User ID=sa;password=Essa771838957;");
        public Login()
        {
            InitializeComponent();

        }
        private void singin()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from users where user_name=N'" + comboBoxEx.Text + "'and user_password=N'" + textBoxX1.Text + "'", con);
                SqlDataReader re = cmd.ExecuteReader();
                if (re.Read())
                {
                    int user_id = Convert.ToInt32(re["user_id"].ToString());
                    string user_type = re["user_type"].ToString();
                    con.Close();
                    Main_Form fm2 = new Main_Form(user_id, user_type);
                    fm2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("اسم المستخدم أو كلمة المرور خاطئة", "125", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("الرجاء التأكد من ربط البرنامج على قواعد البيانات", "126", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                con.Close();
            }
        }
        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxEx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            
try
            {
                SqlCommand cmd;
                SqlDataReader dr;
                con.Open();
                cmd = new SqlCommand("select user_name from users", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBoxEx.Items.Add(dr[0]).ToString();
                }
            }
            catch
            {
                MessageBox.Show("الرجاء ربط النظام على قواعد البيانات", "127");
            }
            finally
            {
                con.Close();
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            singin();
        }

        private void comboBoxEx_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void comboBoxEx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                singin();
            }

        }

        private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                singin();
            }
        }
    }
}

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
    public partial class Manage_Users : Form
    {
        //messagw 128-143
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQGE058\MSSQLSERVER,1433; Initial Catalog=accountingsystem;Integrated Security=false; User ID=sa;password=Essa771838957;");
        int user_id, test_click = 0;
        string user_type;
        public Manage_Users(int user_id,string user_type)
        {
            InitializeComponent();
            this.user_id = user_id;
            this.user_type = user_type;

        }
        private void inserttodataGridView()
        {
            test_click = 0;
            try
            {

                if (user_type == "admin")
                {
                    SqlDataAdapter da = new SqlDataAdapter("select user_id as'رقم المستخدم',user_name as'اسم المستخدم',user_password as'كلمة المرور',user_type as'نوع المستخدم' from users", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewX1.DataSource = dt;

                }
                else if (user_type == "user")
                {
                    SqlDataAdapter da = new SqlDataAdapter("select user_id as'رقم المستخدم',user_name as'اسم المستخدم',user_password as'كلمة المرور',user_type as'نوع المستخدم' from users where user_id="+user_id+"", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewX1.DataSource = dt;

                }
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض الحسابات ", "128", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            comboBoxEx2.Text = "";
            textBoxX6.Clear();
            textBoxX11.Clear();
        }

        private void Manage_Users_Load(object sender, EventArgs e)
        {
            test_click = 0;
            inserttodataGridView();
            comboBoxEx2.Items.Add("admin");
            comboBoxEx2.Items.Add("user");
            if (user_type == "user")
            {
                textBoxX6.Enabled = false;
                buttonX1.Enabled = false;
                buttonX5.Enabled = false;
                buttonX2.Enabled = false;
                buttonX3.Enabled = false;
                comboBoxEx2.Enabled = false;
                test_click = 1;
                try
                {
                    textBoxX6.Text = dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString();
                    textBoxX11.Text = dataGridViewX1.SelectedRows[0].Cells[2].Value.ToString();
                    comboBoxEx2.Text = dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString();
                    con.Close();
                }
                catch
                {
                    MessageBox.Show("مشكلة في عرض بيانات العملية", "129", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                finally
                {
                    con.Close();
                }

            }
        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            test_click = 0;
            int test = 0;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select user_name from users", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (textBoxX6.Text == (dr[0]).ToString())
                    {
                        MessageBox.Show("اسم المستخدم موجود مسبقاً", "130", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        test = 1;
                    }
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في جلب بيانات الحسابات", "131", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }
            if (test == 0)
            {
                DialogResult result = MessageBox.Show("هل تريد الاستمرار في عملية الإضافة", "132", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("insert into users(user_name,user_password,user_type) values(N'" + textBoxX6.Text + "',N'" + textBoxX11.Text + "',N'" + comboBoxEx2.Text + "')", con);
                    try
                    {
                        con.Open();

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("تم إضافة الحساب بنجاح", "133", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        inserttodataGridView();
                    }
                    catch
                    {
                        MessageBox.Show("لم تتم العملية بنجاح", "134", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                        inserttodataGridView();
                    }
                }
            }

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            inserttodataGridView();
            test_click = 0;

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            comboBoxEx2.Text = "";
            textBoxX6.Clear();
            textBoxX11.Clear();
            test_click = 0;
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (test_click == 1 ||user_type!="admin")
            {
                DialogResult result = MessageBox.Show("هل تريد الاستمرار في عملية التعديل", "135", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Update users set user_name=N'" + textBoxX6.Text + "',user_type=N'" + comboBoxEx2.Text + "',user_password=N'" + textBoxX11.Text + "' where user_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString() + "", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("تم تعديل بيانات المستخدم بنجاح", "136", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("لم تتم العملية بنجاح", "137", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                        inserttodataGridView();
                    }

                }
                test_click = 0;
            }
            else
            {
                MessageBox.Show("الرجاء تحديد العملية المراد تعديلها", "138", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridViewX1_MouseClick(object sender, MouseEventArgs e)
        {
            test_click = 1;
            try
            {
                textBoxX6.Text = dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString();
                textBoxX11.Text = dataGridViewX1.SelectedRows[0].Cells[2].Value.ToString();
                comboBoxEx2.Text = dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString();
                con.Close();
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض بيانات العملية", "139", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
            timer1.Start();

        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (test_click == 1 || user_type != "admin")
            {
                DialogResult result = MessageBox.Show("هل تريد الاستمرار في عملية الحذف", "140", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("delete users where user_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString() + "", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("تم حذف المستخدم بنجاح", "141", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("لم تتم العملية بنجاح", "142", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                        inserttodataGridView();
                    }

                }
                test_click = 0;
            }
            else
            {
                MessageBox.Show("الرجاء تحديد العملية المراد حذفها", "143", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}

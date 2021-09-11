using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingSystem
{
    public partial class Main_Form : Form
    {
        int user_id;
        string user_type;
        public Main_Form(int user_id,string user_type)
        {
            InitializeComponent();
            this.user_id = user_id;
            this.user_type = user_type;

        }

        private void Main_Form_Load(object sender, EventArgs e)
        {

            if (user_type != "admin")
            {
                كشفحسابToolStripMenuItem.Enabled = false;
                الحساباتToolStripMenuItem.Enabled = false;
                النظامToolStripMenuItem.Enabled = false;
            }
        }

        private void Main_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "Menu")
                    Application.OpenForms[i].Close();
            }

        }

        private void سحبنقدToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Cash_Pull cp = new Cash_Pull(user_id, user_type);

            cp.MdiParent = this;
            cp.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            cp.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            cp.Dock = DockStyle.Fill;
            cp.Show();

        }

        private void Main_Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.Q)
            {
                Cash_Pull cp = new Cash_Pull(user_id, user_type);

                cp.MdiParent = this;
                cp.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                cp.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                cp.Dock = DockStyle.Fill;
                cp.Show();
            }
            if (e.Control == true && e.KeyCode == Keys.E)
            {
                exchange_between_account ex_be_acc = new exchange_between_account(user_id, user_type);

                ex_be_acc.MdiParent = this;
                ex_be_acc.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                ex_be_acc.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                ex_be_acc.Dock = DockStyle.Fill;
                ex_be_acc.Show();
            }
            if (e.Control == true && e.KeyCode == Keys.W)
            {
                Cash_Deposit cp = new Cash_Deposit(user_id, user_type);


                cp.MdiParent = this;
                cp.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                cp.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                cp.Dock = DockStyle.Fill;
                cp.Show();
            }
            if (e.Control == true && e.KeyCode == Keys.T)
            {
                Exchange_Currency ex_cu = new Exchange_Currency(user_id, user_type);

                ex_cu.MdiParent = this;
                ex_cu.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                ex_cu.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                ex_cu.Dock = DockStyle.Fill;
                ex_cu.Show();
            }
            if (e.Control == true && e.KeyCode == Keys.G)
            {
                Add_Account add_acc = new Add_Account(user_id);

                add_acc.MdiParent = this;
                add_acc.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                add_acc.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                add_acc.Dock = DockStyle.Fill;
                add_acc.Show();
            }
            if (e.Control == true && e.KeyCode == Keys.S)
            {
                Financial_Status_accounts fin_st_acc = new Financial_Status_accounts();

                fin_st_acc.MdiParent = this;
                fin_st_acc.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                fin_st_acc.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                fin_st_acc.Dock = DockStyle.Fill;
                fin_st_acc.Show();
            }
            if (e.Control == true && e.KeyCode == Keys.U)
            {
                Manage_Users ma_us = new Manage_Users(user_id, user_type);

                ma_us.MdiParent = this;
                ma_us.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                ma_us.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                ma_us.Dock = DockStyle.Fill;
                ma_us.Show();
            }
            if ((e.Control == true && e.KeyCode == Keys.B) && user_type == "admin")
            {
                Take_Backup t_b = new Take_Backup();
                t_b.MdiParent = this;
                t_b.Show();
            }
            if ((e.Control == true && e.KeyCode == Keys.K) && user_type == "admin")
            {
                Restore_Backup re_b = new Restore_Backup();
                re_b.MdiParent = this;
                re_b.Show();


            }
            if ((e.Control == true && e.KeyCode == Keys.Y) && user_type == "admin")
            {
                Full_Accounts fll_amunt = new Full_Accounts(user_id);


                fll_amunt.MdiParent = this;
                fll_amunt.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                fll_amunt.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                fll_amunt.Dock = DockStyle.Fill;
                fll_amunt.Show();
            }
            if ((e.Control == true && e.KeyCode == Keys.I))
            {
                Manage_Items ma_it = new Manage_Items(user_type);

                ma_it.MdiParent = this;
                ma_it.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                ma_it.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                ma_it.Dock = DockStyle.Fill;
                ma_it.Show();


            }
            if ((e.Control == true && e.KeyCode == Keys.F))
            {

                Found_Operation f_op = new Found_Operation();

                f_op.MdiParent = this;
                f_op.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                f_op.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                f_op.Dock = DockStyle.Fill;
                f_op.Show();


            }
            if ((e.Control == true && e.KeyCode == Keys.R)) 
            {
                Export_Report e_re = new Export_Report();


                e_re.MdiParent = this;
                e_re.ClientSize = new System.Drawing.Size(2000, 800);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                e_re.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                e_re.Dock = DockStyle.Fill;
                e_re.Show();

            }
        }

        private void ايداعنقدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cash_Deposit cp = new Cash_Deposit(user_id, user_type);


            cp.MdiParent = this;
            cp.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            cp.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            cp.Dock = DockStyle.Fill;
            cp.Show();
 
        }

        private void التحويلبينالحساباتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exchange_between_account ex_be_acc = new exchange_between_account(user_id, user_type);

            ex_be_acc.MdiParent = this;
            ex_be_acc.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ex_be_acc.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ex_be_acc.Dock = DockStyle.Fill;
            ex_be_acc.Show();

        }

        private void المصارفةبينالحساباتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exchange_Currency ex_cu = new Exchange_Currency(user_id, user_type);

            ex_cu.MdiParent = this;
            ex_cu.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ex_cu.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ex_cu.Dock = DockStyle.Fill;
            ex_cu.Show();
        }

        private void إضافةحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Account add_acc = new Add_Account(user_id);

            add_acc.MdiParent = this;
            add_acc.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            add_acc.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            add_acc.Dock = DockStyle.Fill;
            add_acc.Show();

        }

        private void الحالةالماليةللحساباتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Financial_Status_accounts fin_st_acc = new Financial_Status_accounts();

            fin_st_acc.MdiParent = this;
            fin_st_acc.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            fin_st_acc.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            fin_st_acc.Dock = DockStyle.Fill;
            fin_st_acc.Show();
        }

        private void إضافةمستخدمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Users ma_us = new Manage_Users(user_id, user_type);

            ma_us.MdiParent = this;
            ma_us.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ma_us.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ma_us.Dock = DockStyle.Fill;
            ma_us.Show();
        }

        private void إنشاءنسخةاحتياطيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Take_Backup t_b = new Take_Backup();
            t_b.MdiParent = this;
            t_b.Show();
        }

        private void استعادةنسخةمحفوظةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restore_Backup re_b = new Restore_Backup();
            re_b.MdiParent = this;
            re_b.Show();

        }

        private void البحثToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Found_Operation f_op = new Found_Operation();

            f_op.MdiParent = this;
            f_op.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            f_op.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            f_op.Dock = DockStyle.Fill;
            f_op.Show();
        }

        private void كشفحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export_Report e_re = new Export_Report();


            e_re.MdiParent = this;
            e_re.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            e_re.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            e_re.Dock = DockStyle.Fill;
            e_re.Show();
        }

        private void الأصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Items ma_it = new Manage_Items(user_type);


            ma_it.MdiParent = this;
            ma_it.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ma_it.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ma_it.Dock = DockStyle.Fill;
            ma_it.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void مطابقةالأرصدةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Full_Accounts fll_amunt = new Full_Accounts(user_id);


            fll_amunt.MdiParent = this;
            fll_amunt.ClientSize = new System.Drawing.Size(2000, 800);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            fll_amunt.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            fll_amunt.Dock = DockStyle.Fill;
            fll_amunt.Show();
        }
    }
}

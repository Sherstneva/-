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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Аптека
{
    public partial class LoginForm : Form
    {
        DataBase database = new DataBase();

        public LoginForm()
        {
            InitializeComponent();
            txtPass.UseSystemPasswordChar = true;
        }

        private void btnEntrance_Click(object sender, EventArgs e)
        {
            var loginUser = txtLogin.Text;
            var passUser = txtPass.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();    

            string querystring = $"select login_user, password_user, Role From Login where login_user = '{loginUser}' and password_user = '{passUser}' ";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Твкого аккаунта не сущесвует!", "Аккаунта не существеут!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                if (table.Rows[0]["Role"].ToString() == "Заведующий-провизор")
                {
                   DataForm dataForm  = new DataForm();
                    this.Hide();
                    dataForm.ShowDialog();
                }
                else if (table.Rows[0]["Role"].ToString() == "Бухгалтер-фармацевт")
                {
                    DataForm dataForm = new DataForm();
                    this.Hide();
                    dataForm.ShowDialog();
                }
            }     
        }

        private void panLog_in_Paint(object sender, PaintEventArgs e)
        {
            txtLogin.MaxLength = 50;
            txtPass.MaxLength = 50;
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPass.Checked)
            {
                txtPass.UseSystemPasswordChar = false;

            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.Show();
            this.Show();
        }
    }
}

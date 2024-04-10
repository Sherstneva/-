using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Аптека
{
    public partial class RegisterForm : Form
    {
        DataBase database = new DataBase();

        public RegisterForm()
        {
            InitializeComponent();
            txtPass.UseSystemPasswordChar = false;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            var login = txtLogin.Text;
            var password = txtPass.Text;
            var Role = txtRole.Text;
            var RealName = txtRealName.Text;

            if (checkuser()) 
            {
                return;
            }

            string querystring = $"insert into Login (login_user, password_user, Role, Real_name) values('{login}', '{password}', '{Role}', '{RealName}') ";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            database.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успшно создан!", "Успех!");
                LoginForm frm_login = new LoginForm();
                this.Hide();
                frm_login.ShowDialog();
                this.Show();
            }
            else 
            {
                MessageBox.Show("Аккаунт не создан!");
            }
            database.closeConnection();
        }

        private Boolean checkuser() 
        { 
            var loginUser = txtLogin.Text;
            var passUser = txtPass.Text;
            var Role = txtRole.Text;
            var RealName = txtRealName.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();  
            DataTable table = new DataTable();
            string querystring = $"select ID_user, login_user, password_user, Role, Real_name  from Login where login_user = '{loginUser}' and password_user = '{passUser}' and Role = '{Role}' and Real_name = '{RealName}' ";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0) 
            {
                MessageBox.Show("Пользователь уже сществует!");
                return true;
            }
            else 
            {
                return false;
            }
        }

        private void checkPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPass.Checked)
            {
                txtPass.UseSystemPasswordChar = true;

            }
            else
            {
                txtPass.UseSystemPasswordChar = false;
            }
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panCreate_Paint(object sender, PaintEventArgs e)
        {
            txtPass.PasswordChar = '●';
            txtLogin.MaxLength = 50;
            txtPass.MaxLength = 50;
        }
    }
}

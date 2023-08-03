using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exrate
{
    public partial class LoginForm : Form
    {
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");
        public LoginForm()
        {

            InitializeComponent();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/yusufkaradeniz?tab=repositories";

            try
            {
                // Belirtilen URL'yi varsayılan web tarayıcınızda açmak için Process.Start kullanın.
                Process.Start(url);
            }
            catch (Exception ex)
            {
                // URL açma işlemi başarısız olduysa hata mesajı gösterin.
                MessageBox.Show("An error occurred while opening the URL: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void visible_Click(object sender, EventArgs e)
        {
            visible.Visible = false;
            hide.Visible = true;

            txtPassword.PasswordChar = '\0';
            txtPassword.BorderStyle = BorderStyle.Fixed3D;
            txtPassword.BorderStyle = BorderStyle.None;
        }

        private void hide_Click(object sender, EventArgs e)
        {
            visible.Visible = true;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.PasswordChar = '*';
            hide.Visible = false;
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (AuthenticateUser(username, password))
            {
                MainForm mainForm = new MainForm(username);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password.");
            }
        }
        private bool AuthenticateUser(string username, string password)
        {
            connection.Open();
            string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            OleDbDataReader reader = command.ExecuteReader();

            bool result = reader.HasRows;

            connection.Close();
            return result;
        }
    }
}

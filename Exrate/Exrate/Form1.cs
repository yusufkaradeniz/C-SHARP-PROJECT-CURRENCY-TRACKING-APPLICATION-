using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exrate
{
    public partial class Form1 : Form
    {
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");
        public Form1()
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
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void panel3_Click(object sender, EventArgs e)
        {
          
            string newUsername = txtNewUsername.Text;
            string newPassword = txtNewPassword.Text;

            // Kullanıcı adının aynı olup olmadığını kontrol ediyoruz
            if (!IsUsernameAvailable(newUsername))
            {
                MessageBox.Show("This username is already taken.");
                return;
            }

            // Şifre doğrulama kontrolleri
            if (!IsValidPassword(newPassword))
            {
                MessageBox.Show("The password must contain at least 13 characters, one uppercase letter, one lowercase letter, and one number.");
                return;
            }

            // Yeni kullanıcıyı veritabanına ekleyelim
            if (AddUserToDatabase(newUsername, newPassword))
            {
                // Kayıt işlemi başarılı olduysa ana ekrana geçiş yapalım
                MessageBox.Show("Successfully completed the registration");
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
               
            }
            else
            {
                MessageBox.Show("An error occurred when the user registration was received. Please try again.");
            }
           
        }

        private bool IsUsernameAvailable(string username)
        {
            connection.Open();
            string query = "SELECT * FROM Users WHERE Username = @username";
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            OleDbDataReader reader = command.ExecuteReader();

            bool result = !reader.HasRows;

            connection.Close();
            return result;
        }

        private bool IsValidPassword(string password)
        {
            // Şifre en az 13 en fazla 50 karakter uzunluğunda olmalı, bir büyük harf, bir küçük harf, bir sayı ve bir özel karakter içermelidir.
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_])[a-zA-Z\d\W_]{13,50}$";
            return Regex.IsMatch(password, pattern);
        }


        private bool AddUserToDatabase(string username, string password)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO Users (Username, [Password]) VALUES (@username, @password)";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
                connection.Close();
                return false;
            }
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            anaform anaform = new anaform();
            anaform.Show();
            this.Hide();
        }
        


        private void visible_Click(object sender, EventArgs e)
        {
            
            visible.Visible = false;
            hide.Visible = true;
            
            txtNewPassword.PasswordChar = '\0';
            txtNewPassword.BorderStyle = BorderStyle.Fixed3D;
            txtNewPassword.BorderStyle = BorderStyle.None;

        }

        private void hide_Click(object sender, EventArgs e)
        {
            visible.Visible = true;
            txtNewPassword.BorderStyle = BorderStyle.None;
            txtNewPassword.PasswordChar = '*';
            hide.Visible = false;
        }
        
    }
}

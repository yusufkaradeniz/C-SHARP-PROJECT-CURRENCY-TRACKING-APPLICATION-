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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Exrate
{
    public partial class EditUserForm : Form
    {
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");
        private string loggedInUsername;
        public EditUserForm(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
            LoadUserData();
        }

        private void LoadUserData()
        {
            connection.Open();
            string query = "SELECT * FROM Users WHERE Username = @username";
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@username", loggedInUsername);
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string username = reader["Username"].ToString();
                string password = reader["Password"].ToString();

                txtUsername.Text = username;
                txtPassword.Text = password;
            }

            connection.Close();
        }

        private bool IsValidPassword(string password)
        {
            // Şifre en az 13 en fazla 50 karakter uzunluğunda olmalı, bir büyük harf, bir küçük harf, bir sayı ve bir özel karakter içermelidir.
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_])[a-zA-Z\d\W_]{13,50}$";
            return Regex.IsMatch(password, pattern);
        }
        private bool IsUsernameAvailable(string username)
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND NOT Username = @loggedInUsername";
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@loggedInUsername", loggedInUsername);
            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count == 0;
        }

        private void UpdateUserInDatabase(string username, string password)
        {
            connection.Open();
            string query = "UPDATE Users SET Username = @newUsername, [Password] = @newPassword WHERE Username = @oldUsername"; // Password, rezerve edilmiş bir kelime olduğu için köşeli parantez içinde yazılır.
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@newUsername", username);
            command.Parameters.AddWithValue("@newPassword", password);
            command.Parameters.AddWithValue("@oldUsername", loggedInUsername);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            string newUsername = txtUsername.Text;
            string newPassword = txtPassword.Text;

            // Şifre doğrulama kontrolleri
            if (!IsValidPassword(newPassword))
            {
                MessageBox.Show("The password must be at least 13 and at most 50 characters long, contain one uppercase letter, one lowercase letter, one digit, and one special character.");
                return;
            }

            // Kullanıcı bilgilerini veritabanında güncellemek için önce yeni kullanıcı adının uygun olup olmadığını kontrol edelim
            if (IsUsernameAvailable(newUsername))
            {
                UpdateUserInDatabase(newUsername, newPassword);

                // Güncelleme işlemi başarılı olduysa ana ekrana geçiş yapalım
                MessageBox.Show("The information has been successfully updated.");
                MainForm mainForm = new MainForm(newUsername);
                mainForm.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("This username is already in use, please choose another username.");
            }
        }

         private void panel4_Click(object sender, EventArgs e)
         {
            MainForm mainForm = new MainForm(loggedInUsername);
            mainForm.Show();
            this.Hide();
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
        

       
    }



}

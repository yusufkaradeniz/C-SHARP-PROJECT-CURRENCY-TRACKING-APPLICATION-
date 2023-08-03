using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exrate
{
    public partial class anaform : Form
    {
        public anaform()
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
                MessageBox.Show("URL açılırken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
              loginForm.Show();
             this.Hide();
        }

        private void panel4_Click(object sender, EventArgs e)
        {
             Form1 form1 = new Form1();
             form1.Show();
             this.Hide();
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            string url = "https://en.wikipedia.org/wiki/Mustafa_Kemal_Atat%C3%BCrk";

            try
            {
                // Belirtilen URL'yi varsayılan web tarayıcınızda açmak için Process.Start kullanın.
                Process.Start(url);
            }
            catch (Exception ex)
            {
                // URL açma işlemi başarısız olduysa hata mesajı gösterin.
                MessageBox.Show("URL açılırken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

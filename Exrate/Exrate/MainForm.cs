using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exrate
{
    public partial class MainForm : Form
    {
        private string loggedInUsername;
        public MainForm(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
            LoadUserData();
        }
        private void LoadUserData()
        {
            // Ana ekrana giriş yapan kullanıcının adını gösteriyoruz
            lblUsername.Text = "Welcome, dear user; " + loggedInUsername;
        }
      public void DovizGoster()
        {
            try
           {
             XmlDocument xmlVerisi = new XmlDocument();
                xmlVerisi.Load("http://www.tcmb.gov.tr/kurlar/today.xml");
                decimal norikroni = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "NOK")).InnerText.Replace('.', ','));
                decimal arapdinari = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "KWD")).InnerText.Replace('.', ','));
                decimal arabriyalisi = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "SAR")).InnerText.Replace('.', ','));
                decimal cekikyen = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "JPY")).InnerText.Replace('.', ','));
                decimal rusarubles = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "RUB")).InnerText.Replace('.', ','));
                decimal kandoları = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "CAD")).InnerText.Replace('.', ','));
                decimal isokron = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "SEK")).InnerText.Replace('.', ','));
                decimal FRANGA = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "CHF")).InnerText.Replace('.', ','));
                decimal DKKRON = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "DKK")).InnerText.Replace('.', ','));
                decimal avdolar = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "AUD")).InnerText.Replace('.', ','));
                decimal dolar = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "USD")).InnerText.Replace('.', ','));
                decimal euro = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "EUR")).InnerText.Replace('.', ','));
                decimal sterlin = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "GBP")).InnerText.Replace('.', ','));
                decimal leva = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "BGN")).InnerText.Replace('.', ','));

                lblDolar.Text = dolar.ToString();
                lblEuro.Text = euro.ToString();
                lblSterlin.Text = sterlin.ToString();
                lblLeva.Text = leva.ToString();
                adolar.Text = avdolar.ToString();
                dkron.Text = DKKRON.ToString();
                frangas.Text = FRANGA.ToString();
                swedenkronas.Text = isokron.ToString();
                canadadolar.Text = kandoları.ToString();
                kuveytdinar.Text = arapdinari.ToString();
                arapriyal.Text = arabriyalisi.ToString();
                japyen.Text = cekikyen.ToString();
                rusaruble.Text = rusarubles.ToString();
                norkron.Text=   norikroni.ToString();
                
            }
            catch (XmlException xml)
            {
               timer2.Stop();
                MessageBox.Show(xml.ToString());
            }

        }


        // kuveyt dinarı yukarıda



        private void panel2_Click(object sender, EventArgs e)
        {
            EditUserForm editUserForm = new EditUserForm(loggedInUsername);
            editUserForm.Show();
            this.Hide();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void panel1_Click(object sender, EventArgs e)
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
                MessageBox.Show("An error occurred while opening the URL: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer2.Start();
            timer2.Interval = 5000;
            DovizGoster();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 5000;
            DovizGoster();
        }

        private void swedenkronas_Click(object sender, EventArgs e)
        {

        }
    }
}

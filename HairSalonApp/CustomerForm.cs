/****************************************************************************
**					SAKARYA ÜNİVERSİTESİ
**				BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**				    BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
**				   NESNEYE DAYALI PROGRAMLAMA DERSİ
**					2023-2024 BAHAR DÖNEMİ
**	
**				ÖDEV NUMARASI..........:proje
**				ÖĞRENCİ ADI............:ahmet cebeci
**				ÖĞRENCİ NUMARASI.......:b231210077
**                         DERSİN ALINDIĞI GRUP...:c grubu
****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HairSalonApp
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();

            timer1.Tick += new EventHandler(timer1_Tick);

            // Timer'ın Interval özelliğini 1000 milisaniye (1 saniye) olarak ayarla
            timer1.Interval = 1000;

            // Form yüklendiğinde saat ve tarih gösterimini başlat
            timer1.Enabled = true;
            timer1_Tick(null, null);
            timer1.Start();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
            // Her saniyede bir saat ve tarihi güncelle
            lblSaat.Text = DateTime.Now.ToString("HH:mm:ss");
            lblTarih.Text = DateTime.Now.ToString("dd.MM.yyyy");

            // Formun yeniden çizilmesini sağla
            this.Invalidate();
        }


        private void returnToMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 menu = new Form1();
            menu.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text)&&!string.IsNullOrEmpty(maskedTextBox1.Text))
            {
                // Yeni bir Customer nesnesi oluştur ve listeye ekle
                Customer cust=new Customer(textBox1.Text,textBox2.Text,maskedTextBox1.Text);
                Customer.printAllCustomers(customerListbox);

                // TextBox'ları sıfırla
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                maskedTextBox1.Text=string.Empty;
            }
            else
            {
                MessageBox.Show("Please enter customer's name , surname and phone number!");
                textBox1.Text = null;
                textBox2.Text = null;
                maskedTextBox1.Text = string.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Eğer ListBox'ta seçili bir öğe varsa
            if (customerListbox.SelectedItem != null)
            {
                // Seçilen öğeyi al
                object selectedCustomer = customerListbox.SelectedItem;

                // Eğer seçili bir öğe varsa devam et
                if (selectedCustomer != null)
                {
                    // Seçilen öğeyi listbox'tan sil
                    customerListbox.Items.Remove(selectedCustomer);

                    //customer sınıfındaki list'den de sil
                    string customerTmp = Convert.ToString(selectedCustomer);

                    string[] parts = customerTmp.Split(':');

                    if (parts.Length == 2)
                    {
                        string nameSurname = parts[0];
                        string phoneNumber = parts[1];
                        // Ad ve soyadı ayrı ayrı almak için tekrar bölme işlemi yapabiliriz
                        string[] nameSurnameParts = nameSurname.Split('/');
                        if (nameSurnameParts.Length == 2)
                        {
                            string name = nameSurnameParts[0];
                            string surname = nameSurnameParts[1];
                            //bu ad soyad ve numaradaki müşteriyi sil
                            Customer.removeCustomer(Customer.FindCustomer(name,surname,phoneNumber));
                            Customer.printAllCustomers(customerListbox);

                        }
                    }
                }
            }
        }
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            Customer.LoadCustomersFromFile();
            Customer.printAllCustomers(customerListbox);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

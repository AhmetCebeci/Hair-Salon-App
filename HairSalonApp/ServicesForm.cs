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
    public partial class ServicesForm : Form
    {
        public ServicesForm()
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


        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(maskedTextBox1.Text))
            {
                // textBox2 ve maskedTextBox1'in Text özelliklerini kullanarak değerlerini alıyoruz
                ServicesAndPrices.Items.Add(textBox2.Text + "---" + maskedTextBox1.Text+"$");
                //servis sınıfındaki dictionary'ye servisi ve fiyatını ekle
                Services.addService(Convert.ToString(textBox2.Text),Convert.ToInt32(maskedTextBox1.Text));

                //textbox'ları sıfırla
                textBox2.Text=null;
                maskedTextBox1.Text=null;
            }
            else
            {
                MessageBox.Show("Please enter service name and price!");
                textBox2.Text = null;
                maskedTextBox1.Text = null;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Eğer ListBox'ta seçili bir öğe varsa
            if (ServicesAndPrices.SelectedItem != null)
            {
                // Seçilen öğeyi al
                object selectedService = ServicesAndPrices.SelectedItem;

                // Eğer seçili bir öğe varsa devam et
                if (selectedService != null)
                {
                    // Seçilen öğeyi listbox'tan sil
                    ServicesAndPrices.Items.Remove(selectedService);

                    //service sınıfındaki dictionary'den silme
                    string serviceTemp = Convert.ToString(selectedService);

                    // --- işaretinden önceki kısmı "DeletingService" adlı bir stringe ata
                    int indexOfSeparator = serviceTemp.IndexOf("---");
                    if (indexOfSeparator != -1)
                    {
                        string DeletingService = serviceTemp.Substring(0, indexOfSeparator);

                        //bu isimdeki servisi dictionary'den ve dosyadan sil
                        Services.removeService(DeletingService);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a service to delete!");
            }
        }

        private void returTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 menu= new Form1();
            menu.ShowDialog();
            this.Close();
        }
        private void ServicesForm_Load(object sender, EventArgs e)
        {
            Services.PrintAllServices(ServicesAndPrices);

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ServicesAndPrices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lblTarih_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}

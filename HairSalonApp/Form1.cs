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
    public partial class Form1 : Form
    {
        public Form1()
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
            this.Hide();
            ServicesForm servicesForm = new ServicesForm();
            servicesForm.ShowDialog();
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            PersonelListForm personelList=new PersonelListForm();
            personelList.ShowDialog();
            this.Close();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            CustomerForm customerForm = new CustomerForm();
            customerForm.ShowDialog();
            this.Close();
        }
        private void AppointmentButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            AppointmentForm appointment=new AppointmentForm();
            appointment.ShowDialog();
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class PersonelListForm : Form
    {
        public PersonelListForm()
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
        private void PersonelListForm_Load(object sender, EventArgs e)
        {
            Employee.LoadEmployeesFromFile();
            Employee.printAllEmployees(employeeListbox);
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
            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox1.Text))
            {
                // Yeni bir Employee nesnesi oluştur ve listbox'a ekle
                Employee emp = new Employee(textBox1.Text, textBox2.Text);
                Employee.printAllEmployees(employeeListbox);

                // TextBox'ları sıfırla
                textBox2.Text = string.Empty;
                textBox1.Text = string.Empty;
            }

            else
            {
                MessageBox.Show("Please enter employee's name and surname!");
                textBox2.Text = null;
                textBox1.Text = null;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Eğer ListBox'ta seçili bir öğe varsa
            if (employeeListbox.SelectedItem != null)
            {
                // Seçilen öğeyi al
                object selectedEmployee =employeeListbox.SelectedItem;

                // Eğer seçili bir öğe varsa devam et
                if (selectedEmployee != null)
                {
                    // Seçilen öğeyi listbox'tan sil
                    employeeListbox.Items.Remove(selectedEmployee);

                    //employee sınıfındaki list'den de sil
                    string employeeTemp=Convert.ToString(selectedEmployee);

                    // null işaretinden önceki kısmı "DeletingService" adlı bir stringe ata
                    int indexOfSeparator = employeeTemp.IndexOf(" ");
                    if (indexOfSeparator != -1)
                    {
                        string empNameTemp = employeeTemp.Substring(0, indexOfSeparator);
                        string empSurnameTemp=employeeTemp.Substring(indexOfSeparator + 1);

                        //bu isimdeki employee'yi list'den sil
                        Employee.removeEmployee(Employee.FindEmployee(empNameTemp, empSurnameTemp));
                        Employee.printAllEmployees(employeeListbox);
                    }
                }
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

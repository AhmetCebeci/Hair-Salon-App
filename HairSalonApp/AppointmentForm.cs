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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HairSalonApp
{
    public partial class AppointmentForm : Form
    {
        private static string filePath = "Appointments.txt";
        public AppointmentForm()
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


            // Kontrolün Format özelliğini tarih ve saat olarak ayarlayın
            dateTimePicker1.Format = DateTimePickerFormat.Custom;

            // Özel format olarak tarih ve saat formatını belirtin
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:00";

            // Forma kontrolü ekleyin
            this.Controls.Add(dateTimePicker1);
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {

            LoadAppointmentsFromFileToListbox(AppointmentsListbox);
            
            Services.PrintAllServices(ServicesAppListbox);
            
            Employee.LoadEmployeesFromFile();
            Employee.printAllEmployees(EmployeeAppListbox);
            
            Customer.LoadCustomersFromFile();
            Customer.printAllCustomers(CustomersAppListbox);
        }

        private void returnToMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 menu = new Form1();
            menu.ShowDialog();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((CustomersAppListbox.SelectedItem != null) && (ServicesAppListbox.SelectedItem != null) && (EmployeeAppListbox.SelectedItem != null))
            {
                ///çalışanı al
                string employeeFull = EmployeeAppListbox.SelectedItem.ToString();
                string[] employeeParts = employeeFull.Split(' ');
                string empName = employeeParts[0];///çalışan adı
                string empSurname = employeeParts[1];///çalışan soyadı

                ///servis ve fiyatını al
                string serviceFull = ServicesAppListbox.SelectedItem.ToString();
                string[] partsService = serviceFull.Split(new string[] { "---" }, StringSplitOptions.None);
                string serviceName = partsService[0];///servis adı
                int servicePrice = DataAccess.ServicePrices[serviceName];///servis fiyatı(dictionaryden ulaştık)

                ///müşteriyi al
                string customerFull = CustomersAppListbox.SelectedItem.ToString();
                string[] customerParts = customerFull.Split(':');
                // Ad ve soyadı içeren kısmı alırız
                string custNameSurname = customerParts[0];
                // Telefon numarasını içeren parçayı alırız
                string custPhoneNumber = customerParts[1];///müşteri tel no
                // Ad ve soyadı ayrı ayrı almak için tekrar bölme işlemi yaparız
                string[] custNameSurnameParts = custNameSurname.Split('/');
                string custName = custNameSurnameParts[0];///müşteri ad
                string custSurname = custNameSurnameParts[1];///müşteri soyad 

                ///Seçilen tarih ve saati al
                DateTime selectedDateTime = dateTimePicker1.Value;

                ///Seçilen tarih ve saatin geçmişte olup olmadığını kontrol et
                if (selectedDateTime < DateTime.Now)
                {
                    MessageBox.Show("You cannot select a past date and time for appointment!");
                    return;
                }

                ///Seçilen tarih ve saatin müşteri ve çalışan randevu listelerinde olup olmadığını kontrol et
                if (Customer.FindCustomer(custName, custSurname, custPhoneNumber).dates.Contains(selectedDateTime) ||
                    Employee.FindEmployee(empName, empSurname).dates.Contains(selectedDateTime))
                {
                    MessageBox.Show("This date and time is already booked for either the customer or the employee!");
                    return;
                }

                ///Randevu bilgisini oluştur
                string appointment = $"{custName},{custSurname},{custPhoneNumber},{serviceName},{servicePrice},{empName},{empSurname},{selectedDateTime.ToString()}";

                ///listboxa randevuyu yaz                
                AppointmentsListbox.Items.Add(appointment);
                ///dosyaya randevuyu yaz
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(appointment);
                }

                CustomersAppListbox.ClearSelected();
                EmployeeAppListbox.ClearSelected();
                ServicesAppListbox.ClearSelected();
            }
            else
            {
                MessageBox.Show("Please select service,employee and customer!");
                CustomersAppListbox.ClearSelected();
                EmployeeAppListbox.ClearSelected();
                ServicesAppListbox.ClearSelected();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Eğer ListBox'ta seçili bir öğe varsa
            if (AppointmentsListbox.SelectedItem != null)
            {
                // Seçilen öğeyi al
                object selectedAppointment = AppointmentsListbox.SelectedItem;

                // Eğer seçili bir öğe varsa devam et
                if (selectedAppointment != null)
                {
                    // Seçilen randevuyu listbox'tan sil
                    AppointmentsListbox.Items.Remove(selectedAppointment);

                    // Randevu bilgilerini elde et
                    string appointmentInfo = selectedAppointment.ToString();
                    string[] parts = appointmentInfo.Split(',');

                    // Gerekli bilgileri al
                    string customerName = parts[0];
                    string customerSurname = parts[1];
                    string phoneNumber = parts[2];
                    string serviceName = parts[3];
                    string servicePrice = parts[4];
                    string employeeName = parts[5];
                    string employeeSurname = parts[6];
                    string appointmentDateTime = parts[7];
                    DateTime appDateTime;

                    if (!DateTime.TryParse(appointmentDateTime, out appDateTime))
                    {
                        MessageBox.Show("Invalid date and time format in the appointment data.");
                        return;
                    }

                    //çalışanın ve müşterinin randevu tarihini dates listesindne sil
                    Employee.FindEmployee(employeeName, employeeSurname).dates.Remove(appDateTime);
                    Customer.FindCustomer(customerName, customerSurname, phoneNumber).dates.Remove(appDateTime);

                    // Randevuyu dosyadan sil
                    RemoveAppointmentFromFile(customerName, customerSurname, phoneNumber, serviceName, servicePrice, employeeName, employeeSurname, appointmentDateTime);
                }
                AppointmentsListbox.ClearSelected();
            }
            else
            {
                MessageBox.Show("Please select an appointment to delete.");
                
            }
        }
        private void RemoveAppointmentFromFile(string customerName, string customerSurname, string phoneNumber, string serviceName, string servicePrice, string employeeName, string employeeSurname, string appointmentDateTime)
        {
            // Dosyadaki tüm satırları oku
            string[] lines = File.ReadAllLines(filePath);
            List<string> updatedLines = new List<string>();

            foreach (string line in lines)
            {
                // Satırı parçalara ayır
                string[] parts = line.Split(',');

                // Eğer satırda silinecek randevuyu içeren veriler varsa, bu satırı güncellenmiş satırlara ekleme
                if (parts.Length == 8 && parts[0] == customerName && parts[1] == customerSurname && parts[2] == phoneNumber && parts[3] == serviceName && parts[4] == servicePrice && parts[5] == employeeName && parts[6] == employeeSurname && parts[7] == appointmentDateTime)
                {
                    continue; // Bu satırı es geç
                }

                // Güncellenmiş satırlara ekle
                updatedLines.Add(line);
            }

            // Dosyayı yeniden yaz
            File.WriteAllLines(filePath, updatedLines.ToArray());
        }
        private void LoadAppointmentsFromFileToListbox(ListBox listBox)
        {
            // Dosyadaki tüm satırları oku
            string[] lines = File.ReadAllLines(filePath);

            // Her bir satırı listbox'a ekle
            foreach (string line in lines)
            {
                listBox.Items.Add(line);
            }
        }







    }
}
    



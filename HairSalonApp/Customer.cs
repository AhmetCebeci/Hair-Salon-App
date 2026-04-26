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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HairSalonApp
{
    internal class Customer:Person
    {
        private static string filePath = "Customers.txt";
        public string phoneNumber { get; set; }
       
        public Customer(string name, string surname,string phoneNumber) : base(name, surname)
        {
            this.phoneNumber = phoneNumber;
            if (!DataAccess.customerList.Any(cust => cust.name == name && cust.surname == surname&&cust.phoneNumber==phoneNumber))
            {
                DataAccess.customerList.Add(this);
            }
            else if (DataAccess.customerList.Any(cust => cust.name == name && cust.surname == surname && cust.phoneNumber == phoneNumber))
            {
                MessageBox.Show("This customer already exists!");
            }
            if (!File.ReadAllLines(filePath).Any(line => line.Equals($"{name}/{surname}:{phoneNumber}")))
            {
                WriteToFile(this);
            }   
        }
        public static void printAllCustomers(ListBox listbox)
        {
            listbox.Items.Clear();
            foreach (var customer in DataAccess.customerList)
            {
                listbox.Items.Add($"{customer.name}/{customer.surname}:{customer.phoneNumber}");
            }
        }
        public static Customer FindCustomer(string name, string surname,string phoneNumber)
        {
            return DataAccess.customerList.FirstOrDefault(cust => cust.name == name && cust.surname == surname&&cust.phoneNumber==phoneNumber);
        }
        public static void LoadCustomersFromFile()
        {
            // Dosya yoksa veya boşsa işlem yapma
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
                return;

            // Dosyadan müşterileri oku ve listeye ekle
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                // Satırı ":" karakterinden böleriz
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    // Ad ve soyadı içeren kısmı alırız
                    string nameSurname = parts[0];

                    // Telefon numarasını içeren parçayı alırız
                    string phoneNumber = parts[1];

                    // Ad ve soyadı ayrı ayrı almak için tekrar bölme işlemi yaparız
                    string[] nameSurnameParts = nameSurname.Split('/');
                    if (nameSurnameParts.Length == 2)
                    {
                        string name = nameSurnameParts[0];
                        string surname = nameSurnameParts[1];
                        if(!DataAccess.customerList.Any(cust => cust.name == name && cust.surname == surname && cust.phoneNumber == phoneNumber))
                        {
                            Customer cust=new Customer(name,surname,phoneNumber);
                        }
                        
                    }
                }
            }
        }

        public static void removeCustomer(Customer customer)
        {
            RemoveCustomerFromFile(customer);

            DataAccess.customerList.Remove(customer);
        }
        private static void WriteToFile(Customer customer)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{customer.name}/{customer.surname}:{customer.phoneNumber}");
            }
        }
        private static void RemoveCustomerFromFile(Customer customer)
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string> updatedLines = new List<string>();

            foreach (string line in lines)
            {
                if (!line.Equals($"{customer.name}/{customer.surname}:{customer.phoneNumber}"))
                {
                    updatedLines.Add(line);
                }
            }

            // Dosya yeniden yazılacak satırlar varsa, dosyayı güncelle
            if (updatedLines.Count > 0)
            {
                File.WriteAllLines(filePath, updatedLines);
            }
            else
            {
                // Eğer güncellenecek satır yoksa dosyayı temizle
                File.WriteAllText(filePath, string.Empty);
            }
        }
    }
}

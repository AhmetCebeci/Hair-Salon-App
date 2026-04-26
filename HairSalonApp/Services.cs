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
    public static class Services
    {
        


        private static string filePath = "Services&prices.txt";
        static Services() 
        {          
            LoadServicesFromFile();
        }
        private static void LoadServicesFromFile()
        {            

            // Dosya yoksa veya boşsa işlem yapma
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
                return;

            // Dosyadan servisleri oku ve sözlüğe ekle
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(new[] { "---" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    string serviceName = parts[0];
                    int servicePrice;
                    if (int.TryParse(parts[1].TrimEnd('$'), out servicePrice))
                    {
                        DataAccess.servicePrices.Add(serviceName, servicePrice);
                    }
                }
            }
        }
        private static void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var kvp in DataAccess.servicePrices)
                {
                    writer.WriteLine($"{kvp.Key}---{kvp.Value}$");
                }
            }
        }
        public static void addService(string service,int price)
        {
            DataAccess.servicePrices.Add(service, price);
            WriteToFile();
        }
        public static void removeService(string service) 
        {
            DataAccess.servicePrices.Remove(service);
            // Servisi dosyadan da sil
            RemoveServiceFromFile(service);
        }
        private static void RemoveServiceFromFile(string service)
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string> updatedLines = new List<string>();

            foreach (string line in lines)
            {
                if (!line.StartsWith(service)) // Hizmet adına sahip olmayan satırları yeni dosyaya ekleyin
                {
                    updatedLines.Add(line);
                }
            }

            // Dosyayı yeniden yazın
            File.WriteAllLines(filePath, updatedLines.ToArray());
        }
        public static void PrintAllServices(ListBox listBox)
        {
            listBox.Items.Clear();

            foreach (var kvp in DataAccess.servicePrices)
            {
                listBox.Items.Add($"{kvp.Key}---{kvp.Value}$");
            }
        }
    }
}

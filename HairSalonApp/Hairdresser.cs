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
    internal class Employee:Person
    {
        private static string filePath = "Employees.txt";
        public Employee(string name, string surname) : base(name, surname)
        {
            if (!DataAccess.employeeList.Any(emp => emp.name == name && emp.surname == surname))
            {
                DataAccess.employeeList.Add(this);
            }
            else if (DataAccess.employeeList.Any(emp => emp.name == name && emp.surname == surname))
            {
                MessageBox.Show("This employee already exists!");
            }
            if (!File.ReadAllLines(filePath).Any(line => line.Equals($"{name} {surname}")))
            {
                WriteToFile(this);
            }    
        }
        public static void printAllEmployees(ListBox listbox)
        {
            listbox.Items.Clear();
            foreach (var employee in DataAccess.employeeList)
            {
                listbox.Items.Add($"{employee.name} {employee.surname}");
            }
        }
        public static Employee FindEmployee(string name, string surname)
        {
            return DataAccess.employeeList.FirstOrDefault(emp => emp.name == name && emp.surname == surname);
        }
        public static void LoadEmployeesFromFile()
        {
            // Dosya yoksa veya boşsa işlem yapma
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
                return;

            // Dosyadan çalışanları oku ve listeye ekle
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length == 2)
                {
                    string name = parts[0];
                    string surname = parts[1];
                    // Eğer aynı isim ve soyisimde bir çalışan yoksa, oluştur ve SADECE listeye ekle dosyaya ekleme
                    if (!DataAccess.employeeList.Any(emp => emp.name == name && emp.surname == surname))
                    {
                        Employee emp= new Employee(name, surname);
                    }
                }
            }
        }
        public static void removeEmployee(Employee employee)
        {
            RemoveEmployeeFromFile(employee);

            DataAccess.employeeList.Remove(employee);
        }
        private static void WriteToFile(Employee employee)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{employee.name} {employee.surname}");
            }
        }
        private static void RemoveEmployeeFromFile(Employee employee)
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string> updatedLines = new List<string>();

            foreach (string line in lines)
            {
                if (!line.Equals($"{employee.name} {employee.surname}"))
                {
                    updatedLines.Add(line);
                }
            }

            // Dosya yeniden yazılacak satırlar varsa, dosyayı güncelle
            if (updatedLines.Count > 0)
            {
                File.WriteAllLines(filePath, updatedLines);
            }
            else // Eğer dosyada satır yoksa, dosyayı boşalt
            {
                File.WriteAllText(filePath, string.Empty);
            }
        }

    }
}

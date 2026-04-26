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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonApp
{
    internal class DataAccess
    {
        // Dictionary'yi genel bir erişim için static olarak tanımlayın
        public static Dictionary<string, int> servicePrices = new Dictionary<string, int>();

        // ServicePrices Dictionary'sine erişim sağlayacak property
        public static Dictionary<string, int> ServicePrices
        {
            get { return servicePrices; }
            set { servicePrices = value; }
        }

        public static List<Customer> customerList = new List<Customer>();

        public static List<Customer> CustomerList
        {
            get { return customerList; }
            set { customerList = value; }
        }

        public static List<Employee> employeeList = new List<Employee>();

        public static List<Employee> EmployeeList
        {
            get { return employeeList; }
            set{ employeeList = value; }
        }
    }
}

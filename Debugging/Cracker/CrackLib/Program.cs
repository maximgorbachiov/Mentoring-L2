using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace CrackLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CrackMe KeyGen programm:");
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            NetworkInterface networkInterface = networkInterfaces.FirstOrDefault();
            PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
            byte[] adressBytes = physicalAddress.GetAddressBytes();

            DateTime date = DateTime.Now.Date;
            long dateBits = date.ToBinary();
            byte[] dateBytes = BitConverter.GetBytes(dateBits);

            int[] key = adressBytes.Select((adressByte, index) => (adressByte ^ dateBytes[index]) * 10).ToArray();

            string displayKey = string.Join("-", key.Select(part => part.ToString()).ToArray());

            Console.WriteLine("The key is " + displayKey);
            Console.ReadLine();
        }
    }
}

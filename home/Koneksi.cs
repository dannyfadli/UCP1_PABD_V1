using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace home
{
    internal class Koneksi
    {
        public string connectionString()
        {
            try
            {
                string serverIP = "192.168.1.16"; // IP kamu
                string connectStr = $"Server={serverIP},1433;" +
                                    $"Initial Catalog=layananPengaduan;" +
                                    $"User ID=SA;" +
                                    $"Password=15975321dny;" +
                                    $"TrustServerCertificate=True;";
                return connectStr;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Koneksi gagal: " + ex.Message);
                return string.Empty;
            }
        }

    }
}

using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLibrary1;

namespace ConsoleApp1
{
    class Program
    {
        // адрес та порт сервера, до якого відбувається підключення
        static int port = 8080;              // порт сервера
        static string address = "127.0.0.1"; // адреса сервера
        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            TcpClient client = new TcpClient();

            // підключення до віддаленого хоста
            client.Connect(ipPoint);

            try
            {
                Car car = new Car();
                do
                {
                    Console.Write("Enter VINCode:");
                    car.VINcode = Console.ReadLine();
                    
                    NetworkStream ns = client.GetStream();

                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ns, car);

                    // отримуємо відповідь
                    StreamReader sr = new StreamReader(ns);
                    string response = sr.ReadLine();

                    Console.WriteLine("Server response: " + response);
                } while (car.VINcode != "stop");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // закриваємо підключення
                client.Close();
            }
        }
    }
}
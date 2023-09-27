using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLibrary1;

namespace ConsoleApp5
{
    class Program
    {
        static int port = 8080;
        static string address = "127.0.0.1";
        static List<Car> cars = new List<Car>();
        static void Main(string[] args)
        {
            cars.Add(new Car(){ VINcode = "2g5235gr56236233",Model = "BMW",Color = "Black",Year = 2013});
            cars.Add(new Car(){ VINcode = "31f43tg365479fgr",Model = "Audi",Color = "Blue",Year = 2003});
            cars.Add(new Car(){ VINcode = "43259fgrejh3599f",Model = "Mercedes", Color = "Metalic",Year = 2007});
            cars.Add(new Car(){ VINcode = "ropeg90435834523",Model = "Toyota",Color = "Red",Year = 2020});
            cars.Add(new Car(){ VINcode = "foi4wej345235764",Model = "Honda",Color = "Grey",Year = 2017});
            cars.Add(new Car(){ VINcode = "43403gerg68fretj",Model = "Skoda",Color = "White",Year = 2013});
            cars.Add(new Car(){ VINcode = "4gre50f68ewjt435",Model = "Mazda",Color = "Blue",Year = 2019});
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            TcpListener listener = new TcpListener(ipPoint);

            listener.Start(10);

            while (true)
            {
                Console.WriteLine("Server started! Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient();

                try
                {
                    while (client.Connected)
                    {
                        NetworkStream ns = client.GetStream();

                        BinaryFormatter formatter = new BinaryFormatter();
                        var car = (Car)formatter.Deserialize(ns);

                        Console.WriteLine($"Vincode: {car.VINcode} from {client.Client.RemoteEndPoint}");

                        string response = "Car not found!";
                        for (int i = 0; i < cars.Count; i++)
                        {
                            if(car.VINcode == cars[i].VINcode)
                            {
                                response = $"Vincode = {cars[i].VINcode}, Model = {cars[i].Model}, Year = {cars[i].Year}, Color = {cars[i].Color}";
                                break;
                            }
                        }
                        StreamWriter sw = new StreamWriter(ns);
                        sw.WriteLine(response);
                        sw.Flush();
                    }

                    client.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            listener.Stop();
        }
    }
}
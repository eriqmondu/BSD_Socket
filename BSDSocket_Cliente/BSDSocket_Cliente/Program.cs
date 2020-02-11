using System;
using System.Net;
using System.Net.Sockets;

//Cliente
namespace BSDSocket_Cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            connect_client();
        }

        private static void connect_client()
        {
            //Creando un Socket IPv4 TCP
            Socket BSDCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Dirección IP del servidor que estará a la escucha de las peticiones del cliente
            //Será la dirección IP de cualquier adaptador de red de Windows
            IPEndPoint direccionCliente = new IPEndPoint(IPAddress.Parse("192.168.100.1"), 1234);

            try
            {
                BSDCliente.Connect(direccionCliente); // Conectamos                
                Console.WriteLine("Conectado con exito");
                BSDCliente.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString());
            }
            Console.WriteLine("Presione cualquier tecla para terminar");
            Console.ReadLine();

        }
    }
}

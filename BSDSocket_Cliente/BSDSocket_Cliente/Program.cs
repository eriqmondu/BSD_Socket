using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
            byte[] bytes = new byte[1024];

            //Creando un Socket IPv4 TCP
            Socket BSDCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Dirección IP del servidor que estará a la escucha de las peticiones del cliente
            //Será la dirección IP de cualquier adaptador de red de Windows
            IPEndPoint direccionCliente = new IPEndPoint(IPAddress.Parse("192.168.100.1"), 1234);

            try
            {
                BSDCliente.Connect(direccionCliente); // Conectamos                
                Console.WriteLine("Conectado con exito");

                Console.WriteLine("Socket conectado a {0}",
                    BSDCliente.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.    
                byte[] msg = Encoding.ASCII.GetBytes("Saaaaaaaaaaadisiano ");

                // Send the data through the socket.    
                int bytesSent = BSDCliente.Send(msg);

                // Receive the response from the remote device.    
                int bytesRec = BSDCliente.Receive(bytes);
                Console.WriteLine("Mensaje de prueba = {0}",
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Release the socket.    
                BSDCliente.Shutdown(SocketShutdown.Both);
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

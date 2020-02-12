using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Linq;



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
            string texto= null;
            string ip = null;
            int puerto = 0;
            byte[] bytes = new byte[1024];
            Console.WriteLine("Creando Socket TCP");
            //Creando un Socket IPv4 TCP
            Socket BSDCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Dirección IP del servidor que estará a la escucha de las peticiones del cliente
            //Será la dirección IP de cualquier adaptador de red de Windows
            Console.WriteLine("Escriba la direccion IP del servidor:");
            ip = Console.ReadLine();
            Console.WriteLine("Escriba el puerto del servidor:");
            puerto = Convert.ToInt32( Console.ReadLine());
            IPEndPoint direccionCliente = new IPEndPoint(IPAddress.Parse(ip),puerto);

            try
            {
                BSDCliente.Connect(direccionCliente); // Conectamos                
                

                Console.WriteLine("Socket conectado al servidor con la siguiente direccion y puerto {0}",
                    BSDCliente.RemoteEndPoint.ToString());
                Console.WriteLine("Connect [OK]");
                // Encode the data string into a byte array. 
                Console.Write("Escribe el texto a continuacion (Para terminar la conexion, finaliza la cadena con un '.')\n>");
               // Console.WriteLine("Para terminar la conexion, finaliza la cadena con un '.'");
                texto =Console.ReadLine();
                byte[] msg = Encoding.ASCII.GetBytes(texto);
                // Send the data through the socket.    
                Console.WriteLine("Send [OK]");
                int bytesSent = BSDCliente.Send(msg);

                // Receive the response from the remote device.    
                int bytesRec = BSDCliente.Receive(bytes);
                Console.WriteLine("Receive [OK]");
                Console.WriteLine("Mensaje enviado al servidor:  = {0}",
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));
                Console.WriteLine("Close [OK]");
                // Release the socket.    
                BSDCliente.Shutdown(SocketShutdown.Both);
                BSDCliente.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString());
            }
            Console.WriteLine("Presione ENTER para finalizar la app.");
            Console.ReadLine();

        }
    }
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

//Servidor
namespace BSDSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            start_server();
        }

        private static void start_server()
        {
            //Creación del socket IPv4 TCP
            Socket BSDSocket = new Socket(AddressFamily.InterNetwork /*IPv4*/, SocketType.Stream, ProtocolType.Tcp);
            
            //Dirección IP y puerto
            //Escuchará desde cualquier dirección IP asignada en los adaptadores de red de la PC.
            //Poner una IPv4 manual al adaptador Ethernet o WLAN en Windows. Esta será la IP del servidor
            IPEndPoint direccion = new IPEndPoint(IPAddress.Any, 1234);


            try
            {
                //Asociar dirección IP y puerto al socket
                BSDSocket.Bind(direccion);

                //Poner socket en modo de escucha (solo un cliente)
                BSDSocket.Listen(1);

                Console.WriteLine("Socket Berkeley\nEn espera de un cliente (Puerto 1234)...");

                //Creación de nuevo socket utilizando el anterior
                Socket escuchar = BSDSocket.Accept();

                /*
                 * 
                 * Aplicación en espera de una conexión
                 * 
                 */

                //Si la conexión es exitosa, mostrar mensaje
                Console.WriteLine("Conectado con éxito");


                // Variables para los datos entrantes del cliente
                string data = null;
                byte[] bytes = null;

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = escuchar.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                Console.WriteLine("Texto recibido : {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                escuchar.Send(msg);
                escuchar.Shutdown(SocketShutdown.Both);

                BSDSocket.Close(); //Cerrar el socket
            }
            catch(Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString()); //Mostrar mensaje de error
            }
            Console.WriteLine("Presione cualquier tecla para finalizar la app.");
            Console.ReadLine();
        }
    }
}

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
            int puerto = 0;
            Console.WriteLine("BSD Socket (Servidor) en C#\n");
            //Creación del socket IPv4 TCP
            Socket BSDSocket = new Socket(AddressFamily.InterNetwork /*IPv4*/, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Creando socket IPv4 TCP [OK]");
            //Dirección IP y puerto
            //Escuchará desde cualquier dirección IP asignada en los adaptadores de red de la PC.
            //Poner una IPv4 manual al adaptador Ethernet o WLAN en Windows. Esta será la IP del servidor
            Console.Write("Puerto que aceptará conexiones a este servidor: ");
            puerto = Convert.ToInt32(Console.ReadLine());
            IPEndPoint direccion = new IPEndPoint(IPAddress.Any, puerto);

            try
            {
                //Asociar dirección IP y puerto al socket
                BSDSocket.Bind(direccion);
                Console.WriteLine("Binding [OK]");
                //Poner socket en modo de escucha (solo un cliente)
                BSDSocket.Listen(1);
                Console.WriteLine("Listening [...]");

                Console.WriteLine("En espera de un cliente...");

                //Creación de nuevo socket utilizando el anterior
                Socket escuchar = BSDSocket.Accept();
                Console.WriteLine("Accept [OK]");

                // Variables para los datos entrantes del cliente
                string data = null;
                byte[] bytes = null;


                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = escuchar.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    //Recibe cualquier mensaje y finaliza al encontrar un punto "."
                    if (data.IndexOf(".") > -1)
                    {
                        break;
                    }
                }

                Console.WriteLine("Texto recibido desde el cliente, devolviéndolo: {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                Console.WriteLine("Send [OK]");
                escuchar.Send(msg);
                escuchar.Shutdown(SocketShutdown.Both);

                BSDSocket.Close(); //Cerrar el socket
                Console.WriteLine("Close [OK]");
            }
            catch(Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString()); //Mostrar mensaje de error
            }
            Console.WriteLine("\nPresione ENTER para finalizar la app.");
            Console.ReadLine();
        }
    }
}

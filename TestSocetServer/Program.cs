using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace TestSocetServer
{
    class Program
    {
        static Manager man = new Manager();
        static void Main(string[] args)
        {
            
            // Устанавливаем для сокета локальную конечную точку
            Server serv = new Server();
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            foreach (IPAddress ip in ipHost.AddressList) {
                Console.WriteLine(ip);
            }
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 8800);

            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);

                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;

                    // Мы дождались клиента, пытающегося с нами соединиться

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    // Показываем данные на консоли
                    Console.Write("Полученный текст: " + data + "\n\n");

                    // Отправляем ответ клиенту\
                    string reply = "";


                    if (data.IndexOf("Register: ") > -1)
                    {
                        reply = registration(data);
                    }
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        Console.WriteLine("Сервер завершил соединение с клиентом.");
                        break;
                    }
                    

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                        
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static string registration(string data) {
            string reply = "";
            string[] pData = data.Split(" ");
            if (pData.Length != 3)
            {
                reply = "Please write correct (Registe <login> <password>)";
            }
            else
            {
                int tmp1;
                int tmp2;
                bool isNum1 = int.TryParse(pData[2], out tmp1);
                bool isNum2 = int.TryParse(pData[1], out tmp2);
                if (isNum1 || isNum2)
                {
                    if (isNum1)
                    {
                        reply = "the password must contain at least one letter";
                    }
                    if (isNum2)
                    {
                        reply = "the login must contain at least one letter";
                    }
                    if (isNum2 && isNum1)
                    {
                        reply = "login and password must contain at least one letter";
                    }
                }
                else
                {
                    int userID = man.register(pData[1], pData[2]);
                    reply = String.Format("Registration correct ({0})", userID);
                }
            }

            return reply;
        }



    }
}

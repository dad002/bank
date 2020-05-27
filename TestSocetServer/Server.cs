using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TestSocetServer
{
    class Server
    {
        public Server() {
            GetIpAddressList();
        }

        public void GetIpAddressList()
        {
            //try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(Dns.GetHostName());
                Console.WriteLine("Host name : " + hostInfo.HostName);
                Console.WriteLine("IP address List : ");
                for (int index = 0; index < hostInfo.AddressList.Length; index++)
                {
                    Console.WriteLine(hostInfo.AddressList[index]);
                }
            }
            /*catch (SocketException e)
            {
                Console.WriteLine("SocketException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }*/
        }
    }
}

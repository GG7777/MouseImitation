using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MouseImitation
{
    static class View
    {
        static void Show(string str)
        {
            Console.WriteLine(str);
        }

        public static void WaitingForConnecting()
        {
            Show("Waiting for client connecting");
        }

        public static void Connected()
        {
            Show("Client connected");
        }

        public static void ShowIP(IPAddress address)
        {
            Show(String.Format("Enter this IP: {0}", address.ToString()));
        }

        public static void ShowPort(int port)
        {
            Show(String.Format("Enter this Port: {0}", port.ToString()));
        }

        public static void Closed()
        {
            Show("Connection is closed");
        }

        public static void ClientDisconnected()
        {
            Show("Client disconnected");
        }

        public static void Clear()
        {
            Console.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MouseImitation
{
    class Connection : IConnection
    {
        public int DefaultPort => 11000;
        public IPAddress DefaultIPV4IpAddress { get; }
        public IPAddress DefaultIPV6IpAddress { get; }

        public int Port { get; }
        public IPAddress IPV4IpAddress { get; }
        public IPAddress IPV6IpAddress { get; }

        Socket Client { get; set; }
        Socket Listener { get; set; }

        public Connection()
        {
            Port = DefaultPort;
            var addresses = Dns.GetHostAddresses(Dns.GetHostName());
            DefaultIPV6IpAddress = addresses[0];
            DefaultIPV4IpAddress = addresses[1];
            IPV4IpAddress = DefaultIPV4IpAddress;
            IPV6IpAddress = DefaultIPV6IpAddress;
        }

        public void Start(IClientCommandsHandler handler)
        {
            while (true)
            {
                View.Clear();
                View.ShowIP(IPV4IpAddress);
                View.ShowPort(Port);
                View.WaitingForConnecting();
                ListenForClient();
                View.Connected();
                while (true)
                {
                    var data = ReceiveDataByClient();
                    if (data.Length != 0)
                        handler.Handle(data);
                    else
                        break;
                }
                CloseConnection();
                View.ClientDisconnected();
                View.Closed();
            }
        }

        void ListenForClient()
        {
            if (Listener == null)
            {
                var ipEndPoint = new IPEndPoint(IPV4IpAddress, Port);
                Listener = new Socket(IPV4IpAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Listener.Bind(ipEndPoint);
            }
            Listener.Listen(1);
            Client = Listener.Accept();
        }

        byte[] ReceiveDataByClient()
        {
            var buffer = new byte[1024];
            var read = 0;
            try
            {
                read = Client.Receive(buffer);
            }
            catch
            {
            }
            return read == 1024 ? buffer : buffer.Take(read).ToArray();
        }

        void CloseConnection()
        {
            Client.Shutdown(SocketShutdown.Both);
            Client.Close();
        }

        //public static IPAddress GetLocalIpAddress()
        //{
        //    return Dns.GetHostAddresses(Dns.GetHostName())[1];
        //}



        //public void asd()
        //{
        //    var ipHostEntry = Dns.GetHostEntry("localhost");
        //    //var ipAddr = ipHostEntry.AddressList[0];
        //    var ipAddr = IPAddress.Parse("10.97.163.117");
        //    var ipEndPoint = new IPEndPoint(ipAddr, 11000);
        //    var sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //    sListener.Bind(ipEndPoint);
        //    sListener.Listen(1);

        //    var handler = sListener.Accept();
        //    var arr = new byte[1024];
        //    int bytesRec = handler.Receive(arr);
        //    Console.WriteLine(Encoding.UTF8.GetString(arr.Take(bytesRec).ToArray()));
        //    handler.Shutdown(SocketShutdown.Both);
        //    handler.Close();
        //}

        //public void asdCl(string ip, int port)
        //{
        //    //var ipHostEntry = Dns.GetHostEntry("localhost");
        //    //var ipAddr = ipHostEntry.AddressList[0];
        //    var ipAddr = IPAddress.Parse(ip);
        //    var ipEndPoint = new IPEndPoint(ipAddr, port);

        //    var sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //    sender.Connect(ipEndPoint);

        //    var rand = -1;
        //    Console.ReadKey();
        //    //while (true)
        //    //{
        //    //    rand = -rand;
        //    //    //var p = MouseAPI.GetCursorPosition();
        //    //    //var b = Encoding.UTF8.GetBytes(rand * 50 + " " + (-rand) * 50);
        //    //    //new ClientCommandsHandler().Handle(new byte[2] { 1, 32 }.Concat(b).ToArray());

        //    //    var bytesSend = sender.Send(new byte[2] { 1, 32 }.Concat(b).Concat(new byte[1] { 0 }).ToArray());
        //    //}

        //    sender.Shutdown(SocketShutdown.Both);
        //    sender.Close();
        //}
    }
}

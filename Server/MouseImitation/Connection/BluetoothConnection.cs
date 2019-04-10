//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using InTheHand.Net;
//using InTheHand.Net.Bluetooth;
//using InTheHand.Net.Sockets;

//namespace MouseImitation
//{
//    class BluetoothConnection
//    {
//        public static void asd()
//        {
//            var bc = new BluetoothClient();
//            var ma = bc.DiscoverDevices(999);
//            while (true)
//            {
//                foreach (var m in ma)
//                {
//                    if (m.Authenticated)
//                    {
//                        bc.Connect(new BluetoothEndPoint(m.DeviceAddress, Guid.Empty));
//                        break;
//                    }
//                    Console.WriteLine(m.DeviceName + " " + m.Connected + " " + m.Authenticated);
//                }
//                if (bc.Connected)
//                    break;
//                Thread.Sleep(1000);
//                Console.Clear();
//            }
//        }
//    }
//}

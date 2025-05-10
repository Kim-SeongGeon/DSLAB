using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using static xplane_data_test.XPlaneServer;

namespace xplane_data_test
{
    class DataLink
    {
        private Thread DownLink; //수신을 위한 링크
        private static UdpClient client;
        private static IPEndPoint endPoint;
        private static int UpLinkPort = 40000;
        private static int DownLinkPort = 30000;
        private static byte[] revBuf;

        public DataLink()
        {
            Thread DownLink = new Thread(Recive);
            //DownLink.Start();
            int[] test = { 0xFD, 9, 0x00, 0x00, 0, 255, 1, 0, 0 };
            revBuf = new byte[test.Length * sizeof(int)];
            for(int i = 0; i < test.Length; ++i)
            {
                byte[] bytes = BitConverter.GetBytes(test[i]);
                Array.Copy(bytes, 0, revBuf, i * sizeof(int), bytes.Length);
            }
        }

        public static void Send(byte[] strb)
        {

            Console.WriteLine("send button\n");
            try
            {
                using (UdpClient Server = new UdpClient())
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, DownLinkPort);
                    Server.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    Server.Send(strb, strb.Length, endPoint);

                    Console.WriteLine("send 성공");
                    Server.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }

        private void Recive()
        {
            Console.WriteLine("recv");
            if (client != null)
            {
                Debug.WriteLine("이미 UDP 소켓이 생성되어있음..");
            }

            client = new UdpClient();
            endPoint = new IPEndPoint(IPAddress.Any, UpLinkPort);

            client.Client.Bind(endPoint); //특정 IP에게 수신시 bind 사용

            while (true)
            {
                if (client == null) { return; }

                try
                {

                    revBuf = client.Receive(ref endPoint);

                    Console.WriteLine("Receive\n");

                    foreach (byte data in revBuf)
                    {
                        Console.WriteLine("-----------" + data + "-----------------");
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        public byte[] getRevBuf()
        {
            return revBuf;
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace InvokeServer
{
    class Program
    {
        // Referred to the codes on the following site:
        // http://sabulinsprog.seesaa.net/article/130925253.html
        public static void Main()
        {
            IPAddress localIP = IPAddress.Parse("127.0.0.1");
            int portNo = 50000;

            Console.WriteLine("---- Listener ----");
            Console.WriteLine("Local IP = {0}", localIP);
            Console.WriteLine("Port No. = {0}", portNo);

            IPEndPoint ep = new IPEndPoint(localIP, portNo);

            Socket listener = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Stream,
                                         ProtocolType.Tcp);
            listener.Bind(ep);
            listener.Listen(1);

            while (true)
            {
                Console.WriteLine("waiting..");
                Socket connection = listener.Accept();

                Console.WriteLine("Accepted!!\n");
                IPEndPoint remoteEP = (IPEndPoint)connection.RemoteEndPoint;
                Console.WriteLine("Remote IP = {0}", remoteEP.Address);
                Console.WriteLine("Remote Port = {0}", remoteEP.Port);

                Console.WriteLine("receiving data...");
                byte[] receiveData = new byte[1000];
                int size = connection.Receive(receiveData);
                string receiveString = Encoding.UTF8.GetString(receiveData, 0, size);
                Console.WriteLine("received!");
                Console.WriteLine("raceived data = {0}\n", receiveString);

                var p = System.Diagnostics.Process.Start("notepad.exe");
            }
        }
    }
}

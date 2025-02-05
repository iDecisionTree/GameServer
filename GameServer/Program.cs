using GameServer.Database;
using GameServer.Network;
using GameServer.Network.Server;
using GameServer.Player;
using GameServer.Protocol;
using GameServer.Protocol.Login;
using GameServer.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Logger.Initialize(LogLevel.Debug, "Log");

            UdpServer server = new UdpServer();
            server.Start();

            while (true)
            {
                if(Console.ReadLine() == "stop")
                {
                    server.Stop();

                    return;
                }
            }
        }
    }
}

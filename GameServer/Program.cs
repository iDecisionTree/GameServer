using GameServer.Network;
using GameServer.Protocol;
using GameServer.Protocol.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (MemoryStream ms = new MemoryStream()) 
            {
                using (BinaryWriter bw = new BinaryWriter(ms)) 
                {
                    bw.Write("DecisionTree");
                    bw.Write("12345678");
                }

                byte[] payload = ms.ToArray();
                byte[] packet = PacketBuilder.BuildPacket(OpCode.Login, 0u, payload);

                string userId;
                string password;
                Login.TryParseLoginPacket(packet, out userId, out password);
            }
        }
    }
}

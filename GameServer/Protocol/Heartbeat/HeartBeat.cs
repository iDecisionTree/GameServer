using GameServer.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Protocol.Heartbeat
{
    /// <summary>
    /// 心跳包流程
    /// s -> c: 0x00心跳包
    /// c -> s: 0x00心跳包 (string)SessionToken + (long)毫秒时间戳
    /// </summary>

    public class Heartbeat
    {
        public static byte[] BuildHeartbeatPacket(uint sequence)
        {
            byte[] packet = PacketBuilder.BuildPacket(OpCode.Heartbeat, sequence, null);

            return packet;
        }

        public static bool TryParseHeartbeatPacket(byte[] packet, out string sessionToken, out long timestamp)
        {
            sessionToken = string.Empty;
            timestamp = 0L;

            using(MemoryStream ms = new MemoryStream(packet))
            {
                using(BinaryReader br = new BinaryReader(ms))
                {
                    byte opCode = br.ReadByte();
                    uint sequence = br.ReadUInt32();
                    int payloadLength = br.ReadInt32();

                    if(opCode != (byte)OpCode.Heartbeat || payloadLength == 0)
                    {
                        return false;
                    }

                    sessionToken = br.ReadString();
                    timestamp = br.ReadInt64();

                    return true;
                }
            }
        }
    }
}

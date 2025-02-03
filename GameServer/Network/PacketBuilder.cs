using GameServer.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network
{
    /// <summary>
    /// 数据包结构
    /// (byte)操作码 + (uint)序列号 + (int)负载长度 + 负载
    /// </summary>

    public static class PacketBuilder
    {
        public static byte[] BuildHeader(byte opCode, uint sequence, int payloadLength)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                using(BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(opCode);
                    bw.Write(sequence);
                    bw.Write(payloadLength);

                    return ms.ToArray();
                }
            }
        }

        public static byte[] BuildPacket(OpCode opCode, uint sequence, byte[] payload)
        {
            byte[] header = BuildHeader((byte)opCode, sequence, payload.Length);
            if(payload == null)
            {
                return header;
            }

            byte[] packet = new byte[header.Length + payload.Length];

            Array.Copy(header, 0, packet, 0, header.Length);
            Array.Copy(payload, 0, packet, header.Length, payload.Length);

            return packet;
        }
    }
}

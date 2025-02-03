using GameServer.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Protocol.Login
{
    /// <summary>
    /// 登录流程
    /// c -> s: 0x01登录 (string)UserId + (string)(md5)Password
    /// s -> c: 0x01登录 (ushort)StatusCode + (string)SessionToken(状态码为0时)
    /// c -> s: 0x02登陆状态 (string)SessionToken
    /// s -> c: 0x02登陆状态 (bool)IsLogin
    /// </summary>

    public class Login
    {
        public static byte[] BuildLoginPacket(uint sequence, ushort statusCode, string sessionToken)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(statusCode);

                    if (sessionToken != null)
                    {
                        bw.Write(sessionToken);
                    }
                }

                byte[] payload = ms.ToArray();
                byte[] packet = PacketBuilder.BuildPacket(OpCode.Login, sequence, payload);

                return packet;
            }
        }

        public static byte[] BuildLoginStatusPacket(uint sequence, bool isLogin)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(isLogin);
                }

                byte[] payload = ms.ToArray();
                byte[] packet = PacketBuilder.BuildPacket(OpCode.LoginStatus, sequence, payload);

                return packet;
            }
        }

        public static bool TryParseLoginPacket(byte[] packet, out string userId, out string password)
        {
            userId = string.Empty;
            password = string.Empty;

            using (MemoryStream ms = new MemoryStream(packet))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    byte opCode = br.ReadByte();
                    uint sequence = br.ReadUInt32();
                    int payloadLength = br.ReadInt32();

                    if (opCode != (byte)OpCode.Login || payloadLength == 0)
                    {
                        return false;
                    }

                    userId = br.ReadString();
                    password = br.ReadString();

                    return true;
                }
            }
        }

        public static bool TryParseLoginStatusPacket(byte[] packet, out string sessionToken)
        {
            sessionToken = string.Empty;

            using (MemoryStream ms = new MemoryStream(packet))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    byte opCode = br.ReadByte();
                    uint sequence = br.ReadUInt32();
                    int payloadLength = br.ReadInt32();

                    if (opCode != (byte)OpCode.LoginStatus || payloadLength == 0)
                    {
                        return false;
                    }

                    sessionToken = br.ReadString();

                    return true;
                }
            }
        }
    }
}

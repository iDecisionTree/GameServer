using GameServer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Player
{
    public class PlayerData : ISerializable
    {
        public uint Uid;  // 唯一标识符
        public string Name;  // 用户名
        public string Account;  // 账号
        public string Password;  // 密码
        public long RegisterTime;  // 注册时间

        public byte[] Serialize()
        {
            using(MemoryStream ms = new MemoryStream())
            {
                using(BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(Uid);
                    bw.Write(Name);
                    bw.Write(Account);
                    bw.Write(Password);
                    bw.Write(RegisterTime);
                }

                return ms.ToArray();
            }
        }

        public void Deserialize(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    Uid = br.ReadUInt32();
                    Name = br.ReadString();
                    Account = br.ReadString();
                    Password = br.ReadString();
                    RegisterTime = br.ReadInt64();
                }
            }
        }
    }
}

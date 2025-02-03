using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Player
{
    public struct PlayerData
    {
        public int Uid;  // 唯一标识符
        public string Name;  // 用户名
        public string Account;  // 账号
        public string Password;  // 密码
        public long RegisterTime;  // 注册时间

        public static byte[] Serialize(PlayerData playerData)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                using(BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(playerData.Uid);
                    bw.Write(playerData.Name);
                    bw.Write(playerData.Account);
                    bw.Write(playerData.Password);
                    bw.Write(playerData.RegisterTime);
                }

                return ms.ToArray();
            }
        }

        public static PlayerData Deserialize(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    PlayerData playerData = new PlayerData()
                    {
                        Uid = br.ReadUInt16(),
                        Name = br.ReadString(),
                        Account = br.ReadString(),
                        Password = br.ReadString(),
                        RegisterTime = br.ReadInt64(),
                    };

                    return playerData;
                }
            }
        }
    }
}

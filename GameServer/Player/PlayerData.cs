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
        public string UserId;  // 账号
        public string Password;  // 密码
        public DateTime RegisterTime;  // 注册时间
    }
}

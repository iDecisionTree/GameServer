using GameServer.Database;
using GameServer.Network;
using GameServer.Player;
using GameServer.Protocol;
using GameServer.Protocol.Login;
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
            Database<PlayerData> database = new Database<PlayerData>("Database\\Player.db");
            database.Load();

            
            for (int i = 0; i < 100; i++)
            {
                PlayerData player = new PlayerData()
                {
                    Uid = (uint)i,
                    Name = Random.Shared.Next().ToString(),
                    Account = Random.Shared.Next().ToString(),
                    Password = Random.Shared.Next().ToString(),
                    RegisterTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };
                database.Add(player);
            }
            

            PlayerData test = database.FindFirst(x => x.Uid == 1);
            database.Update(x => x.Uid == 2, p => { p.Name = "Alice2"; });

           // test = database.FindFirst(x => x.Name == "DecisionTree");

            database.Save();    
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Database
{
    public interface ISerializable
    {
        public byte[] Serialize();
        public void Deserialize(byte[] data);
    }
}

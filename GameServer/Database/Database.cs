using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Database
{
    public class Database<T> where T : class, ISerializable, new()
    {
        public int Count => datas.Count;

        private string filePath;
        private List<T> datas;

        public Database(string filePath)
        {
            this.filePath = Path.GetFullPath(filePath);

            datas = new List<T>();
        }

        public void Add(T item)
        {
            if(!datas.Contains(item))
            {
                datas.Add(item);
            }
        }

        public void Remove(T item)
        {
            if(datas.Contains(item))
            {
                datas.Remove(item);
            }
        }

        public int Remove(Func<T, bool> predicate)
        {
            return datas.RemoveAll(new Predicate<T>(predicate));
        }

        public void Update(Func<T, bool> predicate, Action<T> updateAction)
        {
            for (int i = 0; i < datas.Count; i++)
            {
                if (predicate(datas[i]))
                {
                    updateAction(datas[i]);
                }
            }
        }

        public bool Contains(Func<T, bool> predicate)
        {
            return datas.Any(predicate);
        }

        public T FindFirst(Func<T, bool> predicate)
        {
            return datas.First(predicate);
        }

        public List<T> Find(Func<T, bool> predicate)
        {
            return datas.Where(predicate).ToList();
        }

        public List<T> GetAll()
        {
            return datas;
        }

        public void Load()
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (fs.Length == 0)
                {
                    return;
                }

                using(BinaryReader br = new BinaryReader(fs))
                {
                    int length = br.ReadInt32();
                    for (int i = 0; i < length; i++) 
                    {
                        int l = br.ReadInt32();
                        byte[] buffer = br.ReadBytes(l);

                        T data = new T();
                        data.Deserialize(buffer);

                        datas.Add(data);
                    }
                }
            }
        }

        public void Save()
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(datas.Count);
                    for (int i = 0; i < datas.Count; i++)
                    {
                        byte[] buffer = datas[i].Serialize();
                        bw.Write(buffer.Length);
                        bw.Write(buffer);
                    }
                }
            }
        }
    }
}
using GameServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Database
{
    public class Database<T> where T : class, ISerializable, new()
    {
        public int Count => _datas.Count;

        private string _filePath;
        private List<T> _datas;

        public Database(string filePath)
        {
            _filePath = filePath;
            DirectoryUtils.EnsureFileDirectoryExists(_filePath);

            _datas = new List<T>();
        }

        public void Add(T item)
        {
            if(!_datas.Contains(item))
            {
                _datas.Add(item);
            }
        }

        public void Remove(T item)
        {
            if(_datas.Contains(item))
            {
                _datas.Remove(item);
            }
        }

        public int Remove(Func<T, bool> predicate)
        {
            return _datas.RemoveAll(new Predicate<T>(predicate));
        }

        public void Update(Func<T, bool> predicate, Action<T> updateAction)
        {
            for (int i = 0; i < _datas.Count; i++)
            {
                if (predicate(_datas[i]))
                {
                    updateAction(_datas[i]);
                }
            }
        }

        public bool Contains(Func<T, bool> predicate)
        {
            return _datas.Any(predicate);
        }

        public T FindFirst(Func<T, bool> predicate)
        {
            return _datas.First(predicate);
        }

        public List<T> Find(Func<T, bool> predicate)
        {
            return _datas.Where(predicate).ToList();
        }

        public List<T> GetAll()
        {
            return _datas;
        }

        public void Load()
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read))
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

                        _datas.Add(data);
                    }
                }
            }
        }

        public void Save()
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(_datas.Count);
                    for (int i = 0; i < _datas.Count; i++)
                    {
                        byte[] buffer = _datas[i].Serialize();
                        bw.Write(buffer.Length);
                        bw.Write(buffer);
                    }
                }
            }
        }
    }
}
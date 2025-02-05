using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utils
{
    public class DirectoryUtils
    {
        public static void EnsureDirectoryExists(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
        }

        public static void EnsureFileDirectoryExists(string filePath)
        {
            string? directory = Path.GetDirectoryName(filePath);
            if (directory != null)
            {
                EnsureDirectoryExists(directory);
            }
        }
    }
}

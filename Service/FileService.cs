using System.Collections.Generic;
using System.IO;

namespace BluePrism.Service
{
    public class FileService : IFileservice
    {
        public string[] Read(string filePath)
        {
            return File.ReadAllText(filePath).Split("\r\n");
        }

        public bool Save(string filePath, IReadOnlyList<string> results)
        {
            File.WriteAllLines(filePath, results);

            return true;
        }
    }
}
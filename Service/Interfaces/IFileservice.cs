
using System.Collections.Generic;

namespace BluePrism.Service
{
    public interface IFileservice
    {
        string[] Read(string filePath);
        bool Save(string filePath, IReadOnlyList<string> results);
    }
}

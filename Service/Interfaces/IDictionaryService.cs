using System.Collections.Generic;

namespace BluePrism.Service
{
    public interface IDictionaryService
    {
        IReadOnlyList<string> CreateResult(string dictionaryFile,
            string startWord, string endWord, string resultFile);
        bool Exists(string word, string filePath);

    }
}
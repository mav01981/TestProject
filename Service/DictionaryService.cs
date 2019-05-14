using System.Collections.Generic;
using System.Linq;

namespace BluePrism.Service
{
    public class DictionaryService : IDictionaryService
    {
        public readonly IFileservice _fileService;

        public DictionaryService(IFileservice fileService)
        {
            _fileService = fileService;
        }

        public IReadOnlyList<string> CreateResult(string dictionaryFile,
            string startWord, string endWord, string resultFile)
        {
            var dictionary = _fileService.Read(dictionaryFile)
                .OrderBy(x => x).ToList();

            var results = BuildList(dictionary, startWord, endWord);

            _fileService.Save(resultFile, results);

            return results;
        }

        public bool Exists(string word, string filePath)
        {
            return _fileService.Read(filePath)
                   .Where(x => x == word).Any();
        }

        private List<string> BuildList(IReadOnlyList<string> dictionary, string startWord,
            string endWord)
        {
            var results = new List<string>();
            int startPoint = 0;

            for (int i = 0; i < dictionary.Count; i++)
            {
                if (dictionary[i] == endWord)
                {
                    results.Add(dictionary[i]);
                    break;
                }
                else if (dictionary[i] == startWord || startPoint > 0)
                {
                    startPoint = startPoint > 0 ? startPoint : i;

                    if (startPoint == i)
                    {
                        results.Add(dictionary[i]);
                    }
                    else if (HasOneCharDifference(results.LastOrDefault(), dictionary[i]))
                        results.Add(dictionary[i]);
                }
            }
            return results;
        }
        private bool HasOneCharDifference(string word1, string word2)
        {
            int charDiff = 0;

            foreach (var character in word1.ToCharArray())
            {
                charDiff += (word2.IndexOf(character) == -1) ? 1 : 0;
            }

            return charDiff == 1 && word1.Length == word2.Length;
        }
    }
}
using BluePrism.Service.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BluePrism.Service
{
    public class DictionaryService : IDictionaryService
    {
        public readonly IFileservice _fileService;
        public readonly IValidationService _validationService;

        public DictionaryService(IFileservice fileService,
            IValidationService validationService)
        {
            _fileService = fileService;
            _validationService = validationService;
        }

        public IReadOnlyList<string> CreateResult(string dictionaryFile,
            string startWord, string endWord, string resultFile)
        {
            var source = _fileService.Read(dictionaryFile)
                .OrderBy(x => x).Distinct().ToList();

            if (source.Any())
            {
                var results = CreateWordSet(source, startWord, endWord);

                _fileService.Save(resultFile, results);

                return results;
            }

            return source;

        }

        public bool Exists(string word, string filePath)
        {
            return _fileService.Read(filePath)
                   .Where(x => x == word).Any();
        }

        private List<string> CreateWordSet(List<string> dictionary, string startWord,
            string endWord)
        {
            var results = new List<string>();

            int startIndex = dictionary.IndexOf(startWord);
            int endIndex = dictionary.IndexOf(endWord);

            for (int i = startIndex; i <= endIndex; i++)
            {
                if (i == startIndex)
                {
                    results.Add(dictionary[i]);
                }
                else
                {
                    var word = GetNextWord(results.LastOrDefault(), dictionary[i], endWord);

                    if (word.Length > 0)
                        results.Add(word);

                    if (word == endWord)
                        break;
                }
            }
            return results;
        }

        private string GetNextWord(string lastword, string nextword, string endWord)
        {
            if (_validationService.ISvalidNextWord(lastword, endWord))
            {
                return endWord;
            }
            else if (_validationService.ISvalidNextWord(lastword, nextword))
            {
                return nextword;
            }
            return string.Empty;
        }
    }
}
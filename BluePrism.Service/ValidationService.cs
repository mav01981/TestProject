using BluePrism.Service.Interfaces;
using System.Linq;

namespace BluePrism.Service
{
    public class ValidationService : IValidationService
    {
        public bool IsValidNextWord(string previousWord, string nextWord)
        {
            if (nextWord.Length != 4) return false;

            var counter = previousWord.Where((t, i) => nextWord[i] != t).Count();

            return counter == 1;
        }
    }
}
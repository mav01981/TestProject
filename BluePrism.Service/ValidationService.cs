using BluePrism.Service.Interfaces;
using System.Linq;

namespace BluePrism.Service
{
    public class ValidationService : IValidationService
    {
        public bool ISvalidNextWord(string previousWord, string nextWord)
        {
            if (nextWord.Length != 4) return false;

            return previousWord.Except(nextWord).Count() == 1;
        }
    }
}
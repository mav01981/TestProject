namespace BluePrism.Service.Interfaces
{
    public interface IValidationService
    {
        bool IsValidNextWord(string previousWord, string nextWord);
    }
}

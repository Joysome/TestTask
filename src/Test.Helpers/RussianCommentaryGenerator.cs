using Test.Helpers.Interfaces;

namespace Test.Helpers
{
    class RussianCommentaryGenerator : ICommentaryGenerator
    {
        public string Generate(string originalWord, string[] substrings)
        {
            if(substrings == null)
            {
                return "Невозможно разбить.";
            }
            return $"Разбили на {substrings.Length} части.";
        }
    }
}

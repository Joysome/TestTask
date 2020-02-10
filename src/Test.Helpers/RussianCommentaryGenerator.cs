using Test.Helpers.Interfaces;

namespace Test.Helpers
{
    public class RussianCommentaryGenerator : ICommentaryGenerator
    {
        public string Generate(int substringsCount)
        {
            if(substringsCount <= 0)
            {
                return "Невозможно разбить.";
            }
            return $"Разбили на {substringsCount} части.";
        }
    }
}

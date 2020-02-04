using System;

namespace Test.Helpers
{
    public interface ICommentaryGenerator
    {
        string Generate(string originalWord, string[] substrings);
    }
}

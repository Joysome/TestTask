using System;

namespace Test.Helpers.Interfaces
{
    public interface ICommentaryGenerator
    {
        string Generate(string originalWord, string[] substrings);
    }
}

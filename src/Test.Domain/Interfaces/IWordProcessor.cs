using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Domain.Interfaces
{
    public interface IWordProcessor
    {
        IEnumerable<(string, string[])> ProcessWords(string[] inputWordsArray);
    }
}

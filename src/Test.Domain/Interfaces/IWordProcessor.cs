using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Domain.Interfaces
{
    public interface IWordProcessor
    {
        List<(string, string[])> ProcessWords(string[] inputWordsArray);
    }
}

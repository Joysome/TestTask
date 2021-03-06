﻿using System;
using System.Collections.Generic;

namespace Test.Output.Interfaces
{
    public interface IOutputWriter
    {
        void WriteWords(IEnumerable<(string, string[])> wordsWithSubstrings);
    }
}

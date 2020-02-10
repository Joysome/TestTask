using System;
using System.Collections.Generic;
using System.Text;
using Test.Output.Interfaces;

namespace Test.Output
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        public void WriteWords(IEnumerable<(string, string[])> wordsWithSubstrings)
        {
            var sb = new StringBuilder();

            foreach (var (originalString, substringsEntry) in wordsWithSubstrings)
            {
                if (substringsEntry == null)
                {
                    sb.Append($"Can't break word \"{originalString}\"\n");
                }
                else
                {
                    sb.Append($"{originalString}: ");
                    foreach (var part in substringsEntry)
                    {
                        sb.Append(part + " ");
                    }
                    sb.Append("\n");
                }
            }

            Console.WriteLine(sb.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Test.Domain.Interfaces;

namespace Test.Domain.WordProcessors
{
    public class SerialWordProcessor : IWordProcessor
    {
        private readonly IWordBreaker _wordBreaker;

        public SerialWordProcessor(IWordBreaker wordBreaker)
        {
            _wordBreaker = wordBreaker;
        }

        public IEnumerable<(string, string[])> ProcessWords(string[] inputWords)
        {
            List<(string, string[])> resultingSubstrings = new List<(string, string[])>();

            foreach (var word in inputWords)
            {
                var processedWordParts = _wordBreaker.BreakWord(word);

                resultingSubstrings.Add((word, processedWordParts));
            }

            return resultingSubstrings;
        }
    }
}

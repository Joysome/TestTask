﻿using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Test.Domain.Interfaces;

namespace Test.Domain.WordProcessors
{
    public class ParallelWordProcessor : IWordProcessor
    {
        private readonly IWordBreaker _wordBreaker;

        public ParallelWordProcessor(IWordBreaker wordBreaker)
        {
            _wordBreaker = wordBreaker;
        }

        public IEnumerable<(string, string[])> ProcessWords(string[] inputWords)
        {
            var resultingSubstrings = new ConcurrentBag<(string, string[])>();

            Parallel.For(0, inputWords.Length, i =>
            {
                var processedWordParts = _wordBreaker.BreakWord(inputWords[i]);

                resultingSubstrings.Add((inputWords[i], processedWordParts));
            });

            return resultingSubstrings;
        }
    }
}

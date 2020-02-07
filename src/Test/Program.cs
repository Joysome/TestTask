﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain;
using Test.Repository;

namespace Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            var dictionaryRepo = new DictionaryFileRepository("C:\\test\\dict", Encoding.UTF8);
            var wordRepo = new WordFileRepository("C:\\test\\de-test-words.tsv", Encoding.UTF8);

            var dictionary = await dictionaryRepo.GetDictionaryWords();
            var inputWords = await wordRepo.GetWords();

            //var processor = new DynamicWordBreaker(dictionary); // approach for small dictionaries & input words ammount
            var processor = new DepthFirstSearchWordBreaker(dictionary);

            var inputWordsArray = inputWords.Values.First().ToArray();//TODO: fix this for different languages

            List<(string, string[])> resultingSubstrings = new List<(string, string[])>();

            stopwatch.Start(); // start measuring

            Parallel.For(0, inputWordsArray.Length, i =>
            {
                var processedWordParts = processor.ProcessWord(inputWordsArray[i]);

                resultingSubstrings.Add((inputWordsArray[i], processedWordParts));
            });

            stopwatch.Stop();// stop measuring


            var sb = new StringBuilder();

            foreach (var (originalString, substringsEntry) in resultingSubstrings)
            {
                if (substringsEntry == null)
                {
                    sb.Append($"Can't break word \"{originalString}\"\n");
                }
                else
                {
                    foreach (var part in substringsEntry)
                    {
                        sb.Append(part + " ");
                    }
                    sb.Append("\n");
                }
            }

            Console.WriteLine(sb.ToString());

            Console.WriteLine(new string('=', 50)); 
            Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}

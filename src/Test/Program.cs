using System;
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
            var stopwatch = new Stopwatch();

            var dictionaryRepo = new DictionaryFileRepository("C:\\test\\dict", Encoding.UTF8);
            var wordRepo = new WordFileRepository("C:\\test\\de-test-words.tsv", Encoding.UTF8);

            var dictionary = await dictionaryRepo.GetDictionaryWords();
            var inputWords = await wordRepo.GetWords();

            //var processor = new DynamicWordBreaker(dictionary); // approach for small dictionaries & input words ammount
            var processor = new DepthFirstSearchWordBreaker(dictionary);

            var inputWordsArray = inputWords.Values.First().ToArray();// should write this properly for different languages support

            List<(string, string[])> resultingSubstrings = new List<(string, string[])>();

            ///////////////////
            var processor2 = new DynamicWordBreaker(dictionary);
            List<(string, string[])> resultingSubstrings2 = new List<(string, string[])>();


            var stopwatch2 = new Stopwatch();
            stopwatch2.Start(); // start measuring

            //Parallel.For(0, inputWordsArray.Length, i =>
            //{
            //    var processedWordParts = processor2.ProcessWord(inputWordsArray[i]);

            //    resultingSubstrings2.Add((inputWordsArray[i], processedWordParts));
            //});

            foreach(var word in inputWordsArray)
            {
                var processedWordParts = processor2.ProcessWord(word);

                resultingSubstrings2.Add((word, processedWordParts));
            }

            stopwatch2.Stop();

            var sb2 = new StringBuilder();

            foreach (var (originalString, substringsEntry) in resultingSubstrings2)
            {
                if (substringsEntry == null)
                {
                    sb2.Append($"Can't break word \"{originalString}\"\n");
                }
                else
                {
                    foreach (var part in substringsEntry)
                    {
                        sb2.Append(part + " ");
                    }
                    sb2.Append("\n");
                }
            }

            Console.WriteLine(sb2.ToString());

            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"Time elapsed: {stopwatch2.ElapsedMilliseconds} ms");

            ///////////////////

            stopwatch.Start(); // start measuring

            //Parallel.For(0, inputWordsArray.Length, i =>
            //{
            //    var processedWordParts = processor.ProcessWord(inputWordsArray[i]);

            //    resultingSubstrings.Add((inputWordsArray[i], processedWordParts));
            //});

            foreach (var word in inputWordsArray)
            {
                var processedWordParts = processor.ProcessWord(word);

                resultingSubstrings.Add((word, processedWordParts));
            }

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

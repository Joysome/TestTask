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
            Stopwatch stopwatch = new Stopwatch();

            var dictionaryRepo = new DictionaryFileRepository("C:\\test\\dict", Encoding.UTF8);
            var wordRepo = new WordFileRepository("C:\\test\\de-test-words.tsv", Encoding.UTF8);

            var dictionary = await dictionaryRepo.GetDictionaryWords();
            var inputWords = await wordRepo.GetWords();

            var processor = new WordBreaker(dictionary);

            stopwatch.Start();

            var inputWordsArray = inputWords.Values.First().ToArray();

            Parallel.For(0, inputWordsArray.Length, i =>
            {
                var processedWordParts = processor.ProcessWord(inputWordsArray[i]);
                if (processedWordParts == null)
                {
                    Console.WriteLine($"Can't break word \"{inputWordsArray[i]}\"");
                }
                else
                {

                    var sb = new StringBuilder();
                    foreach (var part in processedWordParts)
                    {
                        sb.Append(part + " ");
                    }
                    Console.WriteLine(sb.ToString());
                }
            });

            stopwatch.Stop();

            Console.WriteLine(new string('=', 50)); 
            Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}

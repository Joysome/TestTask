﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain;
using Test.Domain.Interfaces;
using Test.Domain.WordBreakers;
using Test.Domain.WordProcessors;
using Test.Helpers;
using Test.Output;
using Test.Repository;

namespace Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var dictionaryRepo = new DictionaryFileRepository("C:\\test\\dict", Encoding.UTF8);
            var wordRepo = new WordFileRepository("C:\\test\\de-test-words.tsv", Encoding.UTF8);

            var dictionary = await dictionaryRepo.GetDictionaryWords();
            var inputWords = await wordRepo.GetWords();

            var inputWordsArray = inputWords.Values.First().ToArray(); // TODO: do this properly for different languages support if needed

            //var breaker = new DepthFirstSearchWordBreaker(dictionary); // approach for small dictionaries. Ineffective for the task.
            var breaker = new DynamicWordBreaker(dictionary); // approach for small input words ammount

            var processor = new SerialWordProcessor(breaker); // will work faster than parallel for dynamic breaker and small ammount of words
            //var processor = new ParallelWordProcessor(breaker); // for large ammount of input words only due to concurrent collections usage

            var stopwatch = new Stopwatch();
            stopwatch.Start(); // start measuring
             
            var resultingSubstrings = processor.ProcessWords(inputWordsArray);

            stopwatch.Stop(); // stop measuring

            WriteDiagnosticInfo(dictionary, inputWordsArray, breaker, processor, stopwatch);

            //var outputWriter = new ConsoleOutputWriter(); // for test purposes only
            var outputWriter = new TextFileOutputWriter("C:\\test\\res.tsv", "de", new RussianCommentaryGenerator());

            outputWriter.WriteWords(resultingSubstrings);

            Console.ReadLine();
        }

        private static void WriteDiagnosticInfo(
            string[] dictionary, 
            string[] inputWordsArray, 
            IWordBreaker breaker, 
            IWordProcessor processor, 
            Stopwatch stopwatch)
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"Breaker: {breaker.GetType().Name}");
            Console.WriteLine($"Processor: {processor.GetType().Name}");
            Console.WriteLine($"Dictionary words count: {dictionary.Length}");
            Console.WriteLine($"Input words count: {inputWordsArray.Length}");
            Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine(new string('=', 50));
        }
    }
}

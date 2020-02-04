using System;
using System.Collections.Generic;
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
            var dictionaryRepo = new DictionaryFileRepository("C:\\test\\dict", Encoding.UTF8);
            var wordRepo = new WordFileRepository("C:\\test\\de-test-words.tsv", Encoding.UTF8);

            var dictionary = await dictionaryRepo.GetDictionaryWords();
            var inputWords = await wordRepo.GetWords();

            var processor = new WordProcessor(dictionary);

            //TODO: parallel for
            foreach (var word in inputWords.Values.First())
            {
                var processedWordParts = await processor.ProcessWord(word);
                if(processedWordParts == null)
                {
                    Console.WriteLine($"Can't break word \"{word}\"");
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
            }
        }
    }
}

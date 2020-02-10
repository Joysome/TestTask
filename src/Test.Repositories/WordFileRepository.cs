using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain;
using Test.Repository.Interfaces;

namespace Test.Repository
{
    public class WordFileRepository : IWordRepository
    {
        private readonly string _filePath;
        private readonly Encoding _encoding;

        public WordFileRepository(string filePath, Encoding encoding)
        {
            _filePath = filePath;
            _encoding = encoding;
        }

        public async Task<Dictionary<Language, string[]>> GetWords()
        {
            var dictionary = new Dictionary<Language, string[]>();


            string[] stringSeparators = new string[] { "\r\n" };
            //TODO: use Stream to read from file for more efficien reading
            var strings = File.ReadAllText(_filePath, _encoding)
                .Trim()
                .Split(stringSeparators, StringSplitOptions.None);

            if (strings.Length <= 0)
            {
                return dictionary;
            }

            //var template = strings.First();
            var wordStrings = strings.Skip(1).ToArray(); //TODO: add different templates support

            dictionary = await SplitWords(wordStrings, new char[] { ' ', '\t' });

            return dictionary;
        }

        private async Task<Dictionary<Language, string[]>> SplitWords(string[] wordStrings, char[] splitters)
        {
            var dictionary = new Dictionary<Language, string[]>();
        
            var splittedStrings = wordStrings
                .Select(x => x.Split(splitters))
                .ToList();

            //TODO: check if there is 2 strings ("de", "word") in each entry

            var groupedStrings = splittedStrings
                .GroupBy(x => DefineLanguage(x[0]));

            dictionary = groupedStrings
                .ToDictionary(k => k.Key, v => v.Select(x => x[1]).ToArray());

            return dictionary;
        }

        //TODO: to extension method in Repositories project
        private Language DefineLanguage(string languageString)
        {
            return languageString == "de" ? Language.German : Language.Undefined;
        }
    }
}

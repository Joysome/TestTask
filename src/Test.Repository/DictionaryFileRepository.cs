using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class DictionaryFileRepository : IDictionaryRepository
    {
        private readonly string _filePath;
        private readonly Encoding _encoding;

        public DictionaryFileRepository(string filePath, Encoding encoding)
        {
            _filePath = filePath;
            _encoding = encoding;
        }

        public async Task<string[]> GetDictionaryWords()
        {
            var words = File
                .ReadAllText(_filePath, _encoding)
                .ToLower()
                .Split(new[] { '\n' });

            return words;
        }
    }
}

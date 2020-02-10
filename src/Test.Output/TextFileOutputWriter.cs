using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Test.Helpers.Interfaces;
using Test.Output.Interfaces;

namespace Test.Output
{
    public class TextFileOutputWriter : IOutputWriter
    {
        private readonly string _filePath;
        private readonly string _languageIdentifier;
        private readonly ICommentaryGenerator _commentaryGenerator;

        public TextFileOutputWriter(string filePath, string languageIdentifier,  ICommentaryGenerator commentaryGenerator)
        {
            _filePath = filePath;
            _languageIdentifier = languageIdentifier;
            _commentaryGenerator = commentaryGenerator;
        }
        public void WriteWords(List<(string, string[])> wordsWithSubstrings)
        {
            var text = GetFileText(wordsWithSubstrings);
            File.WriteAllText(_filePath, text);
        }

        private string GetFileText(List<(string, string[])> wordsWithSubstrings)
        {
            var sb = new StringBuilder();

            sb.Append("country\tss\n");

            foreach (var (originalString, substringsEntry) in wordsWithSubstrings)
            {
                var resultingValue = GetResultingValueString(originalString, substringsEntry);
                var comment = _commentaryGenerator.Generate(substringsEntry?.Length ?? 0);
                var outputString = $"{_languageIdentifier}\t{resultingValue} // {comment}\n";

                sb.Append(outputString);
            }

            return sb.ToString();
        }

        private string GetResultingValueString(string originalString, string[] substringsEntry)
        {
            if (substringsEntry == null)
                return originalString;

            var sb = new StringBuilder();

            for(var i = 0; i < substringsEntry.Length; i++)
            {
                sb.Append(substringsEntry[i]);

                if (i != originalString.Length - 1)
                    sb.Append(", ");
            }

            return sb.ToString();
        }
    }
}

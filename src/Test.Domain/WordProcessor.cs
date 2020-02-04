using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Domain
{
    public class WordProcessor : IWordProcessor
    {
        private readonly string[] _dictionary;

        public WordProcessor(string[] dictionary)
        {
            _dictionary = dictionary;
        }

        public async Task<string[]> ProcessWord(string word)
        {
            var possibleSubstrings = GetAllPossibleSubstrings(word);

            var mostSubstringsEntry = possibleSubstrings
                .OrderBy(x => x.Length)
                .FirstOrDefault();

            return mostSubstringsEntry;
        }

        private List<string[]> GetAllPossibleSubstrings(string inputString)
        {
            var accumulatorStack = new Stack<string>();
            var resultingSubstrings = new List<string[]>();

            GetSubstrings(inputString, inputString.Length, accumulatorStack, resultingSubstrings);

            return resultingSubstrings;
        }

        void GetSubstrings(string checkedString, int lastChar, Stack<string> accumulatorStack, List<string[]> resultingSubstrings)
        {

            for (int i = 1; i <= lastChar; i++)
            {
                var substringToCheck = checkedString.Substring(0, i);

                if (_dictionary.Any(x => x == substringToCheck))
                {
                    if (i == lastChar)
                    {
                        accumulatorStack.Push(substringToCheck);

                        resultingSubstrings.Add(accumulatorStack.ToArray());
                        accumulatorStack = new Stack<string>();

                        return;
                    }

                    var newSubstringLength = lastChar - i;
                    var newSubstring = checkedString.Substring(i, newSubstringLength);
                    accumulatorStack.Push(substringToCheck);

                    GetSubstrings(newSubstring, newSubstringLength, accumulatorStack, resultingSubstrings);
                }
                else if (i == lastChar && accumulatorStack.Count != 0)
                {
                    // if the loop reaches the end of the word 
                    // and there is no  dictionary entry for checked substring, 
                    // then remove last valid entry and try to find new one
                    accumulatorStack.Pop();
                }
            }
        }
    }
}

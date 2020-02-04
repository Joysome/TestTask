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
            var stringResult = "";
            var accumulatorStack = new Stack<string>();
            var resultingSubstrings = new List<string[]>();

            GetSubstrings(inputString, inputString.Length, stringResult, accumulatorStack, resultingSubstrings);

            return resultingSubstrings;
        }

        //parallel for
        void GetSubstrings(string @string, int lastChar, string accumulator /*= ""*/, Stack<string> accumulatorStack, List<string[]> resultingSubstrings)
        {

            for (int i = 1; i <= lastChar; i++)
            {
                var substringToCheck = @string.Substring(0, i);

                // if dictionary conatins this prefix, then 
                // we check for remaining string. Otherwise 
                // we ignore this prefix (there is no else for 
                // this if) and try next 
                if (_dictionary.Any(x => x == substringToCheck))
                {
                    if (i == lastChar)
                    {
                        accumulator += substringToCheck;
                        accumulatorStack.Push(substringToCheck);

                        resultingSubstrings.Add(accumulatorStack.ToArray());
                        accumulatorStack = new Stack<string>();

                        return;
                    }

                    var newSubstringLength = lastChar - i;
                    var newAccumulator = accumulator + substringToCheck + " ";
                    accumulatorStack.Push(substringToCheck);

                    GetSubstrings(@string.Substring(i, newSubstringLength), newSubstringLength, newAccumulator, accumulatorStack, resultingSubstrings);
                }
                if(i == lastChar && accumulatorStack.Count != 0)
                {
                    accumulatorStack.Pop();
                }
            }
        }
    }
}

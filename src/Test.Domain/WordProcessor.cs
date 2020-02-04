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
            return GetSubstrings(word).ToArray();
        }

        private List<string> GetSubstrings(string inputString)
        {
            var stringResult = "";
            var accumulatorStack = new Stack<string>();//todo: use string [][]

            GetSubstrings(inputString, inputString.Length, stringResult, accumulatorStack);

            var resultingSubstrings = stringResult.Split(new char[] { ' ' }).ToList();
            return resultingSubstrings;
        }

        //parallel foreach
        void GetSubstrings(string @string, int lastChar, string accumulator /*= ""*/, Stack<string> accumulatorStack)
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
                        return;
                    }

                    var newSubstringLength = lastChar - i;
                    var newAccumulator = accumulator + substringToCheck + " ";
                    accumulatorStack.Push(substringToCheck);
                    GetSubstrings(@string.Substring(i, newSubstringLength), newSubstringLength, newAccumulator, accumulatorStack);
                }
                if(i == lastChar && accumulatorStack.Count != 0)
                {
                    accumulatorStack.Pop();
                }
            }
        }
    }
}

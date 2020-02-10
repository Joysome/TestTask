using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Interfaces;

namespace Test.Domain.WordBreakers
{
    public class DynamicWordBreaker : IWordBreaker
    {
        private readonly HashSet<string> _dictionary;

        public DynamicWordBreaker(string[] dictionary)
        {
            //Array.Sort(dictionary);
            //_dictionary = dictionary;
            _dictionary = new HashSet<string>(dictionary);
        }

        public string[] BreakWord(string word)
        {
            var possibleBreaks = GetAllPossibleBreaks(word);

            var mostSubstringsEntry = GetBreakWithMostSubstrings(possibleBreaks);

            return mostSubstringsEntry?.ToArray();
        }

        private string[] GetBreakWithMostSubstrings(List<Stack<string>> possibleBreaks)
        {
            var maxIndex = -1;
            var maxCount = 0;
            for(int i = 0; i < possibleBreaks.Count; i++) {
                if(possibleBreaks[i].Count > maxCount)
                {
                    maxIndex = i;
                    maxCount = possibleBreaks[i].Count;
                }
            }

            if(maxIndex < 0 || maxCount < 2)
            {
                return null;
            }

            return possibleBreaks[maxIndex].ToArray();
        }

        private List<Stack<string>> GetAllPossibleBreaks(string inputString)
        {
            var accumulatorStack = new Stack<string>();
            var possibleBreaks = new List<Stack<string>>();

            GetSubstrings(inputString, inputString.Length, accumulatorStack, possibleBreaks);

            return possibleBreaks;
        }


        private bool DictionaryHasSubstring(string substringToCheck) =>
            _dictionary.Contains(substringToCheck);//Array.BinarySearch(_dictionary, substringToCheck, StringComparer.OrdinalIgnoreCase) > 0;

        private void GetSubstrings(string checkedString, int lastChar, Stack<string> accumulatorStack, List<Stack<string>> resultingBreaks)
        {
            for (int i = 1; i <= lastChar; i++)
            {
                var substringToCheck = checkedString.Substring(0, i);

                if (DictionaryHasSubstring(substringToCheck))
                {
                    if (i == lastChar)
                    {
                        accumulatorStack.Push(substringToCheck);

                        resultingBreaks.Add(new Stack<string>(accumulatorStack));
                        accumulatorStack.Pop();

                        if (accumulatorStack.Count != 0)
                            accumulatorStack.Pop();

                        return;
                    }

                    var newSubstringLength = lastChar - i;
                    var newSubstring = checkedString.Substring(i, newSubstringLength);

                    accumulatorStack.Push(substringToCheck);

                    GetSubstrings(newSubstring, newSubstringLength, accumulatorStack, resultingBreaks);
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

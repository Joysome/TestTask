using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Domain
{
    public class DynamicWordBreaker : IWordBreaker
    {
        private readonly string[] _dictionary;

        public DynamicWordBreaker(string[] dictionary)
        {
            _dictionary = dictionary;
        }

        public string[] ProcessWord(string word)
        {
            var possibleSubstrings = GetAllPossibleSubstrings(word);

            var mostSubstringsEntry = possibleSubstrings
                .OrderBy(x => x.Length)
                .FirstOrDefault(); // TODO: rewrite as private method;

            return mostSubstringsEntry;
        }

        private List<string[]> GetAllPossibleSubstrings(string inputString)
        {
            var accumulatorStack = new Stack<string>();
            var resultingSubstrings = new List<string[]>();

            GetSubstrings(inputString, inputString.Length, accumulatorStack, resultingSubstrings);

            return resultingSubstrings;
        }


        private bool DictionaryHasSubstring(string substringToCheck)
        {
            for (int j = 0; j < _dictionary.Length; j++)
            {
                //if (_dictionary[j].Length != substringToCheck.Length)
                //    continue;

                if (_dictionary[j] == substringToCheck)
                {
                    return true;
                }
            }

            return false;
        }

        private void GetSubstrings(string checkedString, int lastChar, Stack<string> accumulatorStack, List<string[]> resultingSubstrings)
        {
            for (int i = 1; i <= lastChar; i++)
            {
                var substringToCheck = checkedString.Substring(0, i);

                if (DictionaryHasSubstring(substringToCheck))
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


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Test.Domain
//{
//    public class WordBreaker : IWordBreaker
//    {
//        private readonly string[] _dictionary;

//        public WordBreaker(string[] dictionary)
//        {
//            _dictionary = dictionary;
//        }

//        public string[] ProcessWord(string word)
//        {
//            var possibleSubstrings = GetAllPossibleSubstrings(word);

//            string[] mostSubstringsEntry = GetMostSubstringsEntry(possibleSubstrings);

//            return mostSubstringsEntry;
//        }

//        private string[] GetMostSubstringsEntry(List<Stack<string>> possibleSubstrings)
//        {
//            var maxCountEntry = possibleSubstrings.OrderBy(x => x.Count()).FirstOrDefault();
//            return maxCountEntry?.ToArray() ?? null;

//            string[] mostSubstringsEntry = null;

//            var maxSubstringsCountEntryIndex = 0;
//            var maxSubstringsCount = 0;
//            for (var i = 0; i < possibleSubstrings.Count; i++)
//            {
//                var entrySubstringsCount = possibleSubstrings[i].Count();
//                if (mostSubstringsEntry == null || entrySubstringsCount > mostSubstringsEntry.Length)
//                {
//                    maxSubstringsCountEntryIndex = i;
//                    maxSubstringsCount = entrySubstringsCount;
//                }
//            }
//            if(maxSubstringsCount < 2)
//            {
//                return null;
//            }

//            mostSubstringsEntry = possibleSubstrings[maxSubstringsCountEntryIndex].ToArray();

//            return mostSubstringsEntry;
//        }

//        private List<Stack<string>> GetAllPossibleSubstrings(string inputString)
//        {
//            var accumulatorStack = new Stack<string>();
//            var resultingSubstrings = new List<Stack<string>>();

//            GetSubstrings(inputString, inputString.Length, accumulatorStack, resultingSubstrings);

//            return resultingSubstrings;
//        }

//        private bool DictionaryHasSubstring(string substringToCheck)
//        {
//            for (int j = 0; j < _dictionary.Length; j++)
//            {
//                if (_dictionary[j] == substringToCheck)
//                {
//                    return true;
//                }
//            }

//            return false;
//        }

//        private void GetSubstrings(
//            string checkedString, 
//            int lastChar, 
//            Stack<string> accumulatorStack, 
//            List<Stack<string>> resultingSubstrings)
//        {
//            for (int i = 1; i <= lastChar; i++)
//            {
//                var substringToCheck = checkedString.Substring(0, i);

//                if (DictionaryHasSubstring(substringToCheck))
//                {
//                    if (i == lastChar)
//                    {
//                        resultingSubstrings.Add(accumulatorStack);
//                        accumulatorStack = new Stack<string>();

//                        return;
//                    }

//                    var newSubstringLength = lastChar - i;
//                    var newSubstring = checkedString.Substring(i, newSubstringLength);
//                    accumulatorStack.Push(substringToCheck);

//                    GetSubstrings(newSubstring, newSubstringLength, accumulatorStack, resultingSubstrings);
//                }
//                else if (i == lastChar && accumulatorStack.Count != 0)
//                {
//                    // if the loop reaches the end of the word 
//                    // and there is no  dictionary entry for checked substring, 
//                    // then remove last valid entry and try to find new one
//                    accumulatorStack.Pop();
//                }
//            }
//        }
//    }
//}

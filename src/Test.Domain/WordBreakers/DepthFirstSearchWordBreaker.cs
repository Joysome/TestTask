using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.Domain.Interfaces;

namespace Test.Domain.WordBreakers
{
    public class DepthFirstSearchWordBreaker : IWordBreaker
    {
        private readonly string[] _dictionary;

        public DepthFirstSearchWordBreaker(string[] dictionary)
        {
            _dictionary = dictionary;
        }

        public string[] BreakWord(string word)
        {
            var wordBreakVariants = GetAllPossibleBreaks(word, _dictionary);

            var mostWordsArray = GetBreakWithMostSubstrings(wordBreakVariants);

            return mostWordsArray;
        }

        private string[] GetBreakWithMostSubstrings(List<Stack<string>> possibleBreaks)
        {
            var maxIndex = -1;
            var maxCount = 0;
            for (int i = 0; i < possibleBreaks.Count; i++)
            {
                if (possibleBreaks[i].Count > maxCount)
                {
                    maxIndex = i;
                    maxCount = possibleBreaks[i].Count;
                }
            }

            if (maxIndex < 0 || maxCount < 2)
            {
                return null;
            }

            return possibleBreaks[maxIndex].ToArray();
        }

        public static List<Stack<string>> GetAllPossibleBreaks(string @string, string[] dictionary)
        {
            // an array to track words. Index of the word is the position in string where the word was found. 
            // There can be multiple words in one pointer, so it is an array of lists.
            var wordsTrackingArray = new List<string>[@string.Length + 1];
            wordsTrackingArray[0] = new List<string>();

            for (int currentPosition = 0; currentPosition < @string.Length; currentPosition++)
            {
                if (wordsTrackingArray[currentPosition] == null)
                    continue;

                for (var dictionaryWordIndex = 0; dictionaryWordIndex < dictionary.Length; dictionaryWordIndex++)
                {
                    var dictionaryWord = dictionary[dictionaryWordIndex];

                    int wordSubstringEndPosition = currentPosition + dictionaryWord.Length;
                    
                    if (wordSubstringEndPosition > @string.Length || (currentPosition == 0 && wordSubstringEndPosition == @string.Length))
                    {
                        continue;
                    }

                    if (@string.Substring(currentPosition, dictionaryWord.Length) == dictionaryWord)
                    {
                        if (wordsTrackingArray[wordSubstringEndPosition] == null)
                        {
                            wordsTrackingArray[wordSubstringEndPosition] = new List<string>();
                        }
                        wordsTrackingArray[wordSubstringEndPosition].Add(dictionaryWord);
                    }
                }
            }

            var result = new List<Stack<string>>();

            if (wordsTrackingArray[@string.Length] == null)
                return result;

            var accumulator = new Stack<string>();
            DepthFirstSearch(wordsTrackingArray, @string.Length, result, accumulator);

            return result;
        }

        public static void DepthFirstSearch(List<string>[] wordsTrackingArray, int endPosition, List<Stack<string>> result, Stack<string> accumulator)
        {
            if (endPosition <= 0)
            {
                result.Add(new Stack<string>(accumulator));
                return;
            }

            for (var i = 0; i < wordsTrackingArray[endPosition].Count; i++)
            {
                var substring = wordsTrackingArray[endPosition][i];
                accumulator.Push(substring);

                var newEndPosition = endPosition - wordsTrackingArray[endPosition][i].Length;
                DepthFirstSearch(wordsTrackingArray, newEndPosition, result, accumulator);
                accumulator.Pop();
            }
        }
    }
}

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

            var mostWordsArray = wordBreakVariants
                .OrderBy(x => x.Count())
                .FirstOrDefault();

            return mostWordsArray;
        }

        public static List<string[]> GetAllPossibleBreaks(string @string, string[] dictionary)
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

            var result = new List<string[]>();

            if (wordsTrackingArray[@string.Length] == null)
                return result;

            var accumulator = new Stack<string>();
            DepthFirstSearch(wordsTrackingArray, @string.Length, result, accumulator);

            return result;
        }

        public static void DepthFirstSearch(List<string>[] wordsTrackingArray, int endPosition, List<string[]> result, Stack<string> accumulator)
        {
            if (endPosition <= 0)
            {
                result.Add(accumulator.ToArray()); // TODO: use properly
                return;
            }

            for (var i = 0; i < wordsTrackingArray[endPosition].Count; i++)
            {
                accumulator.Push(wordsTrackingArray[endPosition][i]);

                var newEndPosition = endPosition - wordsTrackingArray[endPosition][i].Length;
                DepthFirstSearch(wordsTrackingArray, newEndPosition, result, accumulator);
                accumulator.Pop();
            }
        }
    }
}

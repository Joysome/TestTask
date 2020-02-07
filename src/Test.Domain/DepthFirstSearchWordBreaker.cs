using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Domain
{
    public class DepthFirstSearchWordBreaker : IWordBreaker
    {
        private readonly string[] _dictionary;

        public DepthFirstSearchWordBreaker(string[] dictionary)
        {
            _dictionary = dictionary;
        }

        public string[] ProcessWord(string word)
        {
            var wordBreakVariants = WordBreak(word, _dictionary);

            var mostWordsArray = wordBreakVariants.OrderBy(x => x.Count()).FirstOrDefault();

            return mostWordsArray;
        }

        public static List<string[]> WordBreak(string @string, string[] dictionary)
        {
            var wordsTrackingArray = new List<string>[@string.Length + 1];
            wordsTrackingArray[0] = new List<string>();

            for (int pos = 0; pos < @string.Length; pos++)
            {
                if (wordsTrackingArray[pos] == null)
                    continue;

                for (var dictionaryWordIndex = 0; dictionaryWordIndex < dictionary.Length; dictionaryWordIndex++)
                {
                    var dictionaryWord = dictionary[dictionaryWordIndex];

                    int dictionaryWordLength = dictionaryWord.Length;
                    int wordSubstringEndPosition = pos + dictionaryWordLength;
                    
                    if (wordSubstringEndPosition > @string.Length || (pos == 0 && wordSubstringEndPosition == @string.Length))
                    {
                        continue;
                    }

                    if (@string.Substring(pos, dictionaryWordLength) == dictionaryWord)
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

            List<string> temp = new List<string>();
            DepthFirstSearch(wordsTrackingArray, @string.Length, result, temp);

            return result;
        }

        public static void DepthFirstSearch(List<string>[] wordsTrackingArray, int endPosition, List<string[]> result, List<string> tmp)
        {
            if (endPosition <= 0)
            {
                //tmp.RemoveAt(tmp.Count - 1);
                //result.Add(tmp.ToArray());

                //todo: tmp.removeat(tmp.Count); return tmp
                string path = tmp[tmp.Count - 1];
                for (int i = tmp.Count - 2; i >= 0; i--)
                {
                    path += " " + tmp[i];
                }

                result.Add(/*tmp.Take(tmp.Count - 2).ToArray()*/tmp.ToArray());
                return;
            }

            for (var i = 0; i < wordsTrackingArray[endPosition].Count; i++)
            {
                tmp.Add(wordsTrackingArray[endPosition][i]);
                DepthFirstSearch(wordsTrackingArray, endPosition - wordsTrackingArray[endPosition][i].Length, result, tmp);
                tmp.RemoveAt(tmp.Count - 1);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Domain
{
    public class DeepFirstSearchWordBreaker : IWordBreaker
    {
        private readonly string[] _dictionary;

        public DeepFirstSearchWordBreaker(string[] dictionary)
        {
            _dictionary = dictionary;
        }

        public string[] ProcessWord(string word)
        {
            return WordBreak(word, _dictionary).ToArray();
        }

        public static List<String> WordBreak(String @string, string[] dictionary)
        {
            var wordsTrackingArray = new List<String>[@string.Length + 1];
            wordsTrackingArray[0] = new List<String>();

            for (int pos = 0; pos < @string.Length; pos++)
            {
                if (wordsTrackingArray[pos] == null)
                    continue;

                for (var dictionaryWordIndex = 0; dictionaryWordIndex < dictionary.Length; dictionaryWordIndex++)
                {
                    var dictionaryWord = dictionary[dictionaryWordIndex];

                    int dictionaryWordLength = dictionaryWord.Length;
                    int wordSubstringEndPosition = pos + dictionaryWordLength;
                    
                    if (wordSubstringEndPosition > @string.Length)
                        continue;

                    if (@string.Substring(pos, dictionaryWordLength) == dictionaryWord)
                    {
                        if (wordsTrackingArray[wordSubstringEndPosition] == null)
                        {
                            wordsTrackingArray[wordSubstringEndPosition] = new List<String>();
                        }
                        wordsTrackingArray[wordSubstringEndPosition].Add(dictionaryWord);
                    }
                }
            }

            List<String> result = new List<String>();
            if (wordsTrackingArray[@string.Length] == null)
                return result;

            List<String> temp = new List<String>();
            dfs(wordsTrackingArray, @string.Length, result, temp);

            return result;
        }

        public static void dfs(List<String>[] dp, int end, List<String> result, List<String> tmp)
        {
            if (end <= 0)
            {
                String path = tmp[tmp.Count - 1];
                for (int i = tmp.Count - 2; i >= 0; i--)
                {
                    path += " " + tmp[i];
                }

                result.Add(path);
                return;
            }

            for (var i = 0; i < dp[end].Count; i++)
            {
                tmp.Add(dp[end][i]);
                dfs(dp, end - dp[end][i].Length, result, tmp);
                tmp.RemoveAt(tmp.Count - 1);
            }
        }
    }
}

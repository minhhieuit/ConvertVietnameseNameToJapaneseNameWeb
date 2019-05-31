using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConvertVietnameseToJapanese
{
    public static class Katankana
    {
        public static bool IsHeadConsonant(char character, string word, int index)
        {
            int wLenght = word.Length;
            bool result = false;
            List<int> cLocation = new List<int>();
            int numChar = 0;


            if (character == word[index] && index < wLenght - 1)
            {
                numChar++;
            }

            if (numChar > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static bool IsFinalConsonant(char character, string word, int index)
        {
            int wLenght = word.Length;
            bool result = false;
            List<int> cLocation = new List<int>();
            int numChar = 0;

            if (character == word[index] && index == wLenght - 1)
            {
                numChar++;
                //cLocation.Add(i);
            }


            if (numChar > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static bool Is2HeadConsonant(string s, string word)
        {
            bool result = false;
            //int wLenght = word.Length;
            //List<int> cLocation = new List<int>();
            //int numChar = 0;


            //    if (IsLastWord(s,word))
            //    {
            //        numChar++;
            //        cLocation.Add(i);
            //    }


            //if (numChar > 0 && cLocation.Any(w => w <= wLenght - 2))
            //{
            //    result = true;
            //}
            //else
            //{
            //    result = false;
            //}

            switch (s)
            {
                case "ph":
                    result = true;
                    break;
                case "qu":
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        //private static int IndexOfCustom(string s, string word)
        //{
        //    int result = 0;
        //    for(int i = 0; i < word.Length; i++)
        //    {
        //        if()
        //    }
        //    return result;
        //}

        private static bool IsLastWord(string s, string word)
        {
            var str = word.SplitInParts(2).ToArray();
            bool result = false;
            for (int i = 0; i < word.Length; i++)
            {
                if (s[1] == word[i] && i == word.Length - 1)
                {
                    result = true;
                }
            }
            //for(int i = 0; i < str.Count(); i++)
            //{
            //    if(s[2] == str[i] && i == word.Length - 1)
            //    {
            //        result = true;
            //    }
            //}
            return result;
        }
        public static bool Is2FinalConsonant(string s, string word)
        {
            bool result = false;
            switch (s)
            {
                case "ch":
                    result = true;
                    break;
                case "nh":
                    result = true;
                    break;
                case "ng":
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }
        public static bool IsContainGraftConsonant(string word, out int count)
        {
            count = 0;
            var regex = new Regex(@"[ph|qu|ch|nh|ng]");
            count = Regex.Matches(word, "ph|qu|ch|nh|ng").Count;
            return count > 0;
        }

        public static bool IsFinalWord(char character, string word, out string output)
        {
            bool result = false;
            int wLenght = word.Length - 1;
            output = "";
            if (word.IndexOfAny(new char[] { character }) == wLenght)
            {
                RomajiToKana.TableFinalConsonant.TryGetValue(character + "", out output);
            }


            return result;
        }

        public static bool IsVowel(char character)
        {
            return "aàáảãạăằắẳẵặâầấẩẫậeèéẻẽẹêềếểễệuùúủũụưừứửữựiìíỉĩịoòóỏõọôồốổỗộơờớởỡợ".Contains(character);
        }

        public static bool IsSpecialChar(string character)
        {
            throw new NotImplementedException();
        }
        public static string ToKatankana(string word, bool isLowercase = true)
        {
            throw new NotImplementedException();
        }

        public static string ConvertVowel(string character)
        {
            return RomajiToKana.TableVowel[character];
        }

        public static string ConvertHeadConsonant(string character)
        {
            string result = "";

            RomajiToKana.TableHeadConsonant.TryGetValue(character, out result);
            return result;
        }

        public static string ConvertFinalConsonant(string character)
        {
            return RomajiToKana.TableFinalConsonant[character];
        }


        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }
    }
}

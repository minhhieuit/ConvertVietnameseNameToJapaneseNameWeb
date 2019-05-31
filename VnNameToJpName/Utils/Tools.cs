using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConvertVietnameseToJapanese
{
    public static class Tools
    {
        #region Get list first name and add to list dictionary
        static Dictionary<string, string> ListDictionary = new Dictionary<string, string>();

        //Get list first name from file
        public static void ReadFile(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

            List<string> arrFirstName = new List<string>();
            List<string> arrLastName = new List<string>();

            foreach (string line in lines)
            {
                List<string> listMultiWord = new List<string>();
                List<string> listWord = new List<string>();
                listWord = line.Split(new string[] { "->" }, StringSplitOptions.None).ToList();

                arrFirstName.Add(listWord.FirstOrDefault());
                arrLastName.Add(listWord.LastOrDefault());

                //if (!arrLastName.Contains("/"))
                //{
                for (int i = 0; i < arrFirstName.Count(); i++)
                {
                    if (arrFirstName[i].Contains("/"))
                    {
                        listMultiWord = arrFirstName[i].Split(new string[] { "/" }, StringSplitOptions.None).ToList();
                        foreach (string itemSub in listMultiWord)
                        {
                            if (!ListDictionary.ContainsKey(itemSub.ToLower().Trim()))
                            {
                                if (arrLastName[i].Contains("/"))
                                {
                                    ListDictionary.Add(itemSub.ToLower().Trim(), arrLastName[i].Split(new string[] { "/" }, StringSplitOptions.None).FirstOrDefault());
                                }
                                else
                                {
                                    ListDictionary.Add(itemSub.ToLower().Trim(), arrLastName[i]);
                                }
                            }
                        }
                    }
                    else
                    {
                        //listMultiWord = arrFirstName[i].Split(new string[] { "/" }, StringSplitOptions.None).ToList();
                        //foreach (string itemSub in listMultiWord)
                        //{
                        if (!ListDictionary.ContainsKey(arrFirstName[i].ToLower().Trim()))
                        {
                            if (arrLastName[i].Contains("/"))
                            {
                                ListDictionary.Add(arrFirstName[i].ToLower().Trim(), arrLastName[i].Split(new string[] { "/" }, StringSplitOptions.None).FirstOrDefault());
                            }
                            else
                            {
                                ListDictionary.Add(arrFirstName[i].ToLower().Trim(), arrLastName[i]);
                            }
                        }
                        //}
                    }
                }
                //}
                //else
                //{
                //    foreach (string item in arrFirstName)
                //    {
                //        if (item.Contains("/"))
                //        {
                //            listMultiWord = item.Split(new string[] { "/" }, StringSplitOptions.None).ToList();
                //            foreach (string itemSub in listMultiWord)
                //            {
                //                ListDictionary.Add(itemSub, arrLastName.FirstOrDefault());
                //            }
                //        }
                //    }
                //}
            }
        }

        #endregion

        #region Compare word with dictionary
        private static readonly char[] Vowels = "aeiouyAEIOUY".ToCharArray();
        public static void CompareWithDictionary(this string word, ref string result, out string dissection, out List<string> listDissection)
        {

            //var VowelWords = word.Where(c => "aàáảãạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệyỳýỷỹỵuùúủũụưừứửữựiìíỉĩịoòóỏõọôồốổỗộơờớởỡợ".Contains(c));
            dissection = "";
            listDissection = new List<string>();
            List<string> listChars = new List<string>();
            if (ListDictionary.ContainsKey(word))
            {
                result = ListDictionary[word];
                dissection = word + " => " + result;
            }
            else
            {
                //var arrConsonant = new string[] { "ng", "ph", "nh", "ch", "qu", "b", "v", "c", "k", "d", "đ", "m", "r", "l", "s", "x", "t", "y", "p", "t", };
                //var arrChars = new char[] { 'a', 'à', 'á', 'ả', 'ã', 'ạ', 'ă', 'ằ', 'ắ', 'ẳ', 'ẵ', 'ặ', 'â', 'ầ', 'ấ', 'ẩ', 'ẫ', 'ậ', 'è', 'é', 'ẻ', 'ẽ', 'ẹ', 'ê', 'ề', 'ế', 'ể', 'ễ', 'ệ', 'y', 'ỳ', 'ý', 'ỷ', 'ỹ', 'ỵ', 'u', 'ù', 'ú', 'ủ', 'ũ', 'ụ', 'ư', 'ừ', 'ứ', 'ử', 'ữ', 'ự', 'i', 'ì', 'í', 'ỉ', 'ĩ', 'ị', 'o', 'ò', 'ó', 'ỏ', 'õ', 'ọ', 'ô', 'ồ', 'ố', 'ổ', 'ỗ', 'ộ', 'ơ', 'ờ', 'ớ', 'ở', 'ỡ', 'ợ' };
                //var listChars2 = word.Where(c => arrConsonant.Contains(c));
                //listChars = word.Split(new string[] { "b", "v", "c", "k", "d", "đ", "g", "h", "m", "n", "r", "l", "s", "x", "t", "y", "p", "t", "ng", "ph", "nh", "ch", "qu", }, StringSplitOptions.).ToList();
                //var a = word.Split(arrChars);

                List<string> resultVn;
                listChars = word.GetListCharacters(out resultVn);

                result = AttachCharToWord(listChars);
                listDissection = resultVn;
                //Check vowel and consonant

            }
        }

        private static string AttachCharToWord(List<string> listChars)
        {
            string result = "";
            foreach (string item in listChars)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    result += item;
                }
            }
            return result;
        }

        public static List<string> GetListCharacters(this string word, out List<string> resultVn)
        {
            List<string> result = new List<string>();
            resultVn = new List<string>();
            List<string> lsOutput = new List<string>();
            string output = "";
            int numCononant = 0;
            if (Katankana.IsContainGraftConsonant(word, out numCononant))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (Katankana.IsVowel(word[i]))
                    {
                        result.Add(Katankana.ConvertVowel(word[i] + ""));
                        resultVn.Add(word[i] + " => " + Katankana.ConvertVowel(word[i] + ""));
                    }
                    else
                    {
                        //if (Katankana.IsFinalWord(item, word, out output))
                        //{
                        //    result.Add(output);
                        //}
                        //else
                        //{
                        //    result.Add(Katankana.ConvertHeadConsonant(item + ""));
                        //}

                        if (Katankana.IsHeadConsonant(word[i], word, i))
                        {

                            if ((i + 1) < word.Length)
                            {
                                if (Katankana.Is2HeadConsonant(word[i] + "" + word[i + 1], word))
                                {
                                    result.Add(Katankana.ConvertHeadConsonant(word[i] + "" + word[i + 1]));
                                    resultVn.Add(word[i] + "" + word[i + 1] + " => " + Katankana.ConvertHeadConsonant(word[i] + "" + word[i + 1]));
                                    i++;
                                }
                                else
                                {
                                    if (Katankana.Is2FinalConsonant(word[i] + "" + word[i + 1], word) && (i + 1) == word.Length - 1)
                                    {
                                        result.Add(Katankana.ConvertFinalConsonant(word[i] + "" + word[i + 1]));
                                        resultVn.Add(word[i] + "" + word[i + 1] + " => " + Katankana.ConvertFinalConsonant(word[i] + "" + word[i + 1]));
                                        i++;
                                    }
                                    else
                                    {
                                        if (Katankana.IsFinalConsonant(word[i], word, i))
                                        {
                                            result.Add(Katankana.ConvertFinalConsonant(word[i] + ""));
                                            resultVn.Add(word[i] + " => " + Katankana.ConvertFinalConsonant(word[i] + ""));
                                        }
                                        else
                                        {
                                            result.Add(Katankana.ConvertHeadConsonant(word[i] + ""));
                                            resultVn.Add(word[i] + " => " + Katankana.ConvertHeadConsonant(word[i] + ""));
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            result.Add(Katankana.ConvertFinalConsonant(word[i] + ""));
                            resultVn.Add(word[i] + " => " + Katankana.ConvertFinalConsonant(word[i] + ""));
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (Katankana.IsVowel(word[i]))
                    {
                        result.Add(Katankana.ConvertVowel(word[i].ToString()));
                        resultVn.Add(word[i] + " => " + Katankana.ConvertVowel(word[i] + ""));
                    }
                    else
                    {
                        if (Katankana.IsHeadConsonant(word[i], word, i))
                        {
                            result.Add(Katankana.ConvertHeadConsonant(word[i] + ""));
                            resultVn.Add(word[i] + " => " + Katankana.ConvertHeadConsonant(word[i] + ""));
                        }
                        else
                        {
                            if (Katankana.IsFinalConsonant(word[i], word, i))
                            {
                                result.Add(Katankana.ConvertFinalConsonant(word[i] + ""));
                                resultVn.Add(word[i] + " => " + Katankana.ConvertFinalConsonant(word[i] + ""));
                            }
                            else
                            {
                                result.Add(Katankana.ConvertHeadConsonant(word[i] + ""));
                                resultVn.Add(word[i] + " => " + Katankana.ConvertHeadConsonant(word[i] + ""));
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static bool WordContainWord(this string word, out string output)
        {
            bool result = false;
            bool isHead = false;
            int wLength = 0;
            output = "";

            wLength = word.Length;

            var regex = new Regex(@"[ph+qu+ch+nh+ng]");
            result = regex.IsMatch(word);
            string[] listCheck = new string[] { "ph", "qu", "ch", "nh", "ng" };
            List<string> listResult = new List<string>();

            foreach (string item in listCheck)
            {
                if (word.Contains(item))
                {
                    result = true;
                    listResult.Add(item);
                }
            }

            return result;
        }

        #endregion
    }
}

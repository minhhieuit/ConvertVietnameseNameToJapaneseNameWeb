using ConvertVietnameseToJapanese;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VnNameToJpName.Models;
using VnNameToJpName.Utils;

namespace VnNameToJpName.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Word model = new Word();

            List<Word> words = new List<Word>();
            List<string> listName = new List<string>();
            List<string> listDissection;
            List<string> listDissectionChar;
            int count = 0;

            // Read file
            Tools.ReadFile(Constant.PATH_FIRST_NAME);
            listName = FileHandler.ReadListName(Constant.PATH_LIST_NAME);
            
            foreach(var item in listName)
            {
                listDissection = new List<string>();
                listDissectionChar = new List<string>();
                string result = "";
                string output = "";
                string dissection = "";
                count++;
                foreach (string word in item.Split(new string[] { " " }, StringSplitOptions.None))
                {
                    Tools.CompareWithDictionary(word.ToLower().Trim(), ref output, out dissection, out listDissectionChar);
                    result +=  output + " | ";
                    if(!string.IsNullOrEmpty(dissection))
                        listDissection.Add(dissection);

                    if (listDissectionChar.Count() > 0)
                    {
                        foreach(var c in listDissectionChar)
                        {
                            listDissection.Add(c);
                        }
                    }
                }

                words.Add(new Word()
                {
                    STT = count,
                    Vietnamese = item,
                    Japanese = result.Substring(0,(result.Length - 3)),
                    ListWord = listDissection
                });
            }
            return View(words);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
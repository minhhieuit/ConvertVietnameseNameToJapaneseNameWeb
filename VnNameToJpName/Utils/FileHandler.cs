using ConvertVietnameseToJapanese;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VnNameToJpName.Utils
{
    public class FileHandler
    {
        public static List<string> ReadListName(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            List<string> result = new List<string>();
            result.AddRange(lines);
            return result;
        }
    }
}
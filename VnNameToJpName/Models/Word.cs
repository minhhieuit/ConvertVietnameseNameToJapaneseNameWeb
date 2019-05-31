using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VnNameToJpName.Models
{
    public class Word
    {
        public int STT { get; set; }
        public string Vietnamese { get; set; }
        public string Japanese { get; set; }
        public List<string> ListWord { get; set; }
    }
}
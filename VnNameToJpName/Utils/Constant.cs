using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace ConvertVietnameseToJapanese
{
    public class Constant
    {
        public static string PATH_FIRST_NAME = HostingEnvironment.MapPath("~/App_Data/first-name-list.txt"); /*@"C:\Users\Admin\source\repos\VnNameToJpName\VnNameToJpName\App_Data\first-name-list.txt";*/
        public static string PATH_LIST_NAME = HostingEnvironment.MapPath("~/App_Data/list-name-vn.txt");  /*@"C:\Users\Admin\source\repos\VnNameToJpName\VnNameToJpName\App_Data\list-name-vn.txt";*/
        public static string VOWEL_LIST = @"C:\Users\Admin\source\repos\ConvertVietnameseToJapanese\ConvertVietnameseToJapanese\Dictionary\vowel-list.txt";
    }
}

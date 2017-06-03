using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageService
{
    public static class Extensions
    {
        public static string Or(this string value, string alternative)
        {
            return string.IsNullOrEmpty(value) ? alternative : value;
        }
    }
}

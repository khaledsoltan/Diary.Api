using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public class SpecialCharReplace
    {
        public static string Replacecomma(string Text)
        {
            return Text.ToString().Replace(",", "-").Replace("\\", " ").Replace("\n", " ").Replace('"', ' ');
        }
        public static string TextValidation(string Text)
        {
            var validtext = Text.ToString().Replace("\\", " ").Replace("\n", " ");
            return Regex.Replace(validtext.Trim(), "[^a-zA-Z0-9.-]+", "", RegexOptions.Compiled);
        }
    }
}

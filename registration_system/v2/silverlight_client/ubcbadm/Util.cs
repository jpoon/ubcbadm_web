using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ubcbadm
{
    public static class Util
    {
        public static string TitleCase(string str)
        {
            return Regex.Replace(str, @"\w+", (m) =>
            {
                string tmp = m.Value;
                return char.ToUpper(tmp[0]) + tmp.Substring(1, tmp.Length - 1).ToLower();
            });
        }

    }
}

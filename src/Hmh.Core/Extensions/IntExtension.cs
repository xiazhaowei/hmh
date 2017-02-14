using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 将字符串转换为整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToIntDefault(this string value)
        {
            if (value == null)
            {
                return 0;
            }
            int result;
            return int.TryParse(value, out result) ? result : 0;
        }

        public static bool ContainsAny(this string s, params string[] tokens)
        {
            foreach (string token in tokens)
                if (s.Contains(token)) return true;
            return false;
        }

        public static bool ContainsAll(this string s, params string[] tokens)
        {
            foreach (string token in tokens)
                if (!s.Contains(token)) return false;
            return true;
        }
    }
}

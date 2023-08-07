using System;
using System.Collections.Generic;
using System.Text;

namespace MyCodeBase.Library.Extensions
{
    public static class MathExtensions
    {
        /// <summary>
        /// 字串轉數字，失敗給0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int IntSafeParse(this string value)
        {
            var output = 0;
            int.TryParse(value, out output);
            return output;
        }
        /// <summary>
        /// 字串轉double，失敗給0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double DoubleSafeParse(this string value)
        {
            double output = 0;
            double.TryParse(value, out output);
            return output;
        }
    }
}

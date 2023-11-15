using MyCodeBase.Library.Enum;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MyCodeBase.Library.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// TripleDes_KEY_192
        /// </summary>
        private static readonly byte[] TripesDes_KEY_192 = new byte[] { 42, 16, 93, 156, 78, 4, 218, 32, 15, 167, 44, 80, 26, 250, 155, 112, 2, 94, 11, 204, 119, 35, 184, 197 };
        /// <summary>
        /// TripleDes_IV_192
        /// </summary>
        private static readonly byte[] TripesDes_IV_192 = new byte[] { 55, 103, 246, 79, 36, 99, 167, 3, 42, 5, 62, 83, 184, 7, 209, 13, 145, 23, 200, 58, 173, 10, 121, 222 };
        /// <summary>
        /// AES_KEY_128
        /// </summary>
        private static readonly byte[] AES_KEY_128 = new byte[] { 43, 122, 157, 156, 78, 4, 218, 32, 15, 167, 44, 80, 26, 250, 155, 112 };
        /// <summary>
        /// AES_IV_128
        /// </summary>
        private static readonly byte[] AES_IV_128 = new byte[] { 55, 103, 246, 79, 36, 99, 167, 3, 42, 5, 62, 83, 184, 7, 209, 145 };

        #region 字串加密
        /// <summary>
        /// TripleDes加密
        /// </summary>
        /// <param name="plainText">原始字串</param>
        /// <returns></returns>
        public static string TripleDesEncrypt(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;

            var des = new TripleDESCryptoServiceProvider();
            var ct = des.CreateEncryptor(TripesDes_KEY_192, TripesDes_IV_192);
            var input = Encoding.UTF8.GetBytes(plainText);
            var output = ct.TransformFinalBlock(input, 0, input.Length);
            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// TripleDes解密
        /// </summary>
        /// <param name="cypherText">加密字串</param>
        /// <returns></returns>
        public static string TripleDesDecrypt(this string cypherText)
        {
            var des = new TripleDESCryptoServiceProvider();
            var ct = des.CreateDecryptor(TripesDes_KEY_192, TripesDes_IV_192);
            var input = Convert.FromBase64String(cypherText);
            var output = ct.TransformFinalBlock(input, 0, input.Length);
            return Encoding.UTF8.GetString(output);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string AESEncrypt(this string plainText)
        {
            plainText = plainText.Trim();
            var aesAlg = Aes.Create();
            var encryptor = aesAlg.CreateEncryptor(AES_KEY_128, AES_IV_128);
            var input = Encoding.UTF8.GetBytes(plainText);
            var output = encryptor.TransformFinalBlock(input, 0, input.Length);
            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cypherText"></param>
        /// <returns></returns>
        public static string AESDecrypt(this string cypherText)
        {
            cypherText = cypherText.Trim();
            var des = Aes.Create();
            var ct = des.CreateDecryptor(AES_KEY_128, AES_IV_128);
            var input = Convert.FromBase64String(cypherText);
            var output = ct.TransformFinalBlock(input, 0, input.Length);
            return Encoding.UTF8.GetString(output);
        }

        /// <summary>
        /// 將字串轉換成SHA256
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string ToSha256(this string str)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] source = Encoding.Default.GetBytes(str);
            byte[] crypto = sha256.ComputeHash(source);
            string result = Convert.ToBase64String(crypto);

            return result;
        }

        /// <summary>
        /// 將字串轉換成MD5
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string ToMd5(this string str)
        {
            var md5 = MD5.Create();
            var hashData = md5.ComputeHash(Encoding.Default.GetBytes(str));
            var returnValue = new StringBuilder();

            foreach (var t in hashData)
            {
                returnValue.Append(t.ToString());
            }

            return returnValue.ToString();
        }

        #endregion

        #region 字串轉換
        /// <summary>
        /// 阿拉伯數字轉中文數字
        /// </summary>
        /// <param name="str">The string type [0-9] input.</param>
        /// <returns></returns>
        public static string ToChineseNumber(this string str)
        {
            var chineseNumbers = new Dictionary<char, string>
            {
                {'0', "零"},
                {'1', "壹"},
                {'2', "貳"},
                {'3', "參"},
                {'4', "肆"},
                {'5', "伍"},
                {'6', "陸"},
                {'7', "柒"},
                {'8', "捌"},
                {'9', "玖"}
            };

            var result = new StringBuilder();

            foreach (var c in str)
            {
                result.Append(chineseNumbers.ContainsKey(c) ? chineseNumbers[c] : "零");
            }

            return result.ToString();
        }

        /// <summary>
        /// 數字月份轉英文縮寫
        /// </summary>
        /// <param name="str">The int type [0-12] input.</param>
        /// <returns></returns>
        public static string ToMonthsName(this int month)
        {
            string[] monthAbbreviations = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            if (month >= 1 && month <= 12)
            {
                return monthAbbreviations[month];
            }

            return "";
        }
        #endregion

        #region 字串移除
        /// <summary>
        /// 移除html中的href標籤
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string RemoveHtmlHref(this string url)
        {
            string HRefPattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";
            string result = Regex.Replace(url, HRefPattern, "");
            return result;
        }
        #endregion

        #region 把分鐘數轉換成日時數
        /// <summary>
        /// 把分鐘數轉換成日時數(480分鐘=>1日0時0分)
        /// </summary>
        /// <param name="totalmins"></param>
        /// <param name="inps"></param>
        /// <returns></returns>
        public static string ToDayHour(this int totalmins, int inps)
        {
            return totalmins.ToDayHour(EDayHourType.日時分, inps);
        }
        /// <summary>
        /// 把分鐘數轉換成日時數(480分鐘=>1日0時0分)
        /// </summary>
        /// <param name="totalmins"></param>
        /// <param name="inps"></param>
        /// <returns></returns>
        public static string ToDayHour(this int totalmins, EDayHourType dayHourType = EDayHourType.日時分, int inps = 480)
        {
            var resultStr = "";
            switch (dayHourType)
            {
                case EDayHourType.日時分:
                    {
                        if (totalmins == 0)
                            resultStr = "0日0時";
                        else
                        {
                            var days = totalmins / inps; //日數
                            var hours = (totalmins - (days * inps)) / 60; // 時數
                            var mins = (totalmins - (days * inps)) % 60; //分鐘數
                            resultStr = $"{days}日{hours}時";

                            if (mins > 0)
                                resultStr += $"{mins}分";
                        }
                    }
                    break;
                case EDayHourType.時分:
                    {
                        if (totalmins == 0)
                            resultStr = "0時";
                        else
                        {
                            var hours = totalmins / 60; //時數
                            var mins = totalmins % 60; //分鐘數
                            resultStr = $"{hours}時";

                            if (mins > 0)
                                resultStr += $"{mins}分";
                        }
                    }
                    break;
                case EDayHourType.分:
                    resultStr = $"{totalmins}分";
                    break;
                default:
                    break;
            }

            return resultStr;
        }
        #endregion

        #region 把分鐘數轉換成時數
        /// <summary>
        /// 把分鐘數轉換成時數
        /// </summary>
        /// <param name="totalmins"></param>
        /// <param name="inps"></param>
        /// <returns></returns>
        public static string ToHourStr(this int totalmins)
        {
            var hours = "";
            if (totalmins == 0)
            {
                hours = "0";
            }
            else
            {
                hours = (totalmins / 60.0).ToString(); //總時數

            }
            return hours;
        }
        #endregion

        #region 把分鐘數轉換成時分
        /// <summary>
        /// 把分鐘數轉換成時分數(60分鐘=>1時0分)
        /// </summary>
        /// <param name="totalmins"></param>
        /// <returns></returns>
        public static string ToHourMins(this int totalmins)
        {
            var HourMinstr = "";
            if (totalmins == 0)
            {
                HourMinstr = "0時0分";
            }
            else
            {
                var hours = (totalmins) / 60; // 時數
                var mins = totalmins - (hours * 60); //分鐘數
                HourMinstr = $"{hours}時{mins}分";
            }
            return HourMinstr;
        }
        #endregion


        /// <summary>
        /// 現在語系
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CultureName(this string value)
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
        }

        /// <summary>
        /// 是否有此值
        /// </summary>
        /// <param name="sourceValue">來源值</param>
        /// <param name="value">檢查值</param>
        /// <returns></returns>
        public static bool HasValue(this int sourceValue, int value)
        {
            return (value & sourceValue) == value;
        }
        /// <summary>
        /// 移除url中的指定參數
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string RemoveUrlParameter(this string url, string parameter)
        {
            string[] separateUrl = url.Split('?');
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(separateUrl[1]);
            queryString.Remove(parameter);

            return separateUrl[0] + "?" + queryString;
        }

        /// <summary>
        /// 西元年轉換民國年
        /// </summary>
        /// <returns></returns>
        public static string ToTaiwanDate(this DateTime dt)
        {
            var taiwanCalendar = new TaiwanCalendar();
            return $"民國 {taiwanCalendar.GetYear(dt):000} 年 {dt.Month:00} 月 {dt.Day:00} 日";
        }

        /// <summary>
        /// 取得民國年
        /// </summary>
        /// <returns></returns>
        public static string GetTaiwanYear(this DateTime dt)
        {
            var taiwanCalendar = new TaiwanCalendar();
            return $"{taiwanCalendar.GetYear(dt):000}";
        }

        /// <summary>
        /// 移除html tag
        /// </summary>
        /// <param name="htmlSource">The HTML source.</param>
        /// <param name="removeLineBreak">if set to <c>true</c> [remove line break].</param>
        /// <returns></returns>
        public static string RemoveHtmlTag(this string htmlSource, bool removeLineBreak = false)
        {
            if (string.IsNullOrEmpty(htmlSource)) return "";

            //移除  javascript code.
            htmlSource = Regex.Replace(htmlSource, @"<script[\d\D]*?>[\d\D]*?</script>", string.Empty);

            //移除html tag.
            htmlSource = Regex.Replace(htmlSource, @"<[^>]*>", string.Empty);

            htmlSource = System.Web.HttpUtility.HtmlDecode(htmlSource);

            if (removeLineBreak)
            {
                htmlSource = htmlSource
                    .Replace("\r", string.Empty)
                    .Replace("\n", string.Empty);
            }

            return htmlSource.Trim();
        }

        /// <summary>
        /// 金額數字轉大寫
        /// </summary>
        /// <param name="type">金額/數量</param>
        /// <param name="Num">數字</param>
        /// <returns></returns>
        public static string GetChineseNum(string type, string Num)
        {
            #region
            try
            {
                string m_1, m_2, m_3, m_4, m_5, m_6, m_7, m_8, m_9;
                m_1 = Num;
                string numNum = "0123456789.";
                string numChina = "零壹貳叁肆伍陸柒捌玖點";
                string numChinaWeigh = "個拾佰仟萬拾佰仟億拾佰仟萬";
                if (Num.Substring(0, 1) == "0")//0123-->123
                    Num = Num.Substring(1, Num.Length - 1);
                if (!Num.Contains("."))
                    Num += ".00";
                else//123.234  123.23 123.2
                    Num = Num.Substring(0, Num.IndexOf('.') + 1 + (Num.Split('.')[1].Length > 2 ? 3 : Num.Split('.')[1].Length));
                m_1 = Num;
                m_2 = m_1;
                m_3 = m_4 = "";
                //m_2:1234-> 壹貳叁肆
                for (int i = 0; i < 11; i++)
                {
                    m_2 = m_2.Replace(numNum.Substring(i, 1), numChina.Substring(i, 1));
                }
                //m_3:佰拾萬仟佰拾個
                int iLen = m_1.Length;
                if (m_1.IndexOf('.') > 0)
                    iLen = m_1.IndexOf('.');//獲取整數位數
                for (int j = iLen; j >= 1; j--)
                    m_3 += numChinaWeigh.Substring(j - 1, 1);
                //m_4:2行+3行
                for (int i = 0; i < m_3.Length; i++)
                    m_4 += m_2.Substring(i, 1) + m_3.Substring(i, 1);
                //m_5:4行去"0"後拾佰仟
                m_5 = m_4;
                m_5 = m_5.Replace("零拾", "零");
                m_5 = m_5.Replace("零佰", "零");
                m_5 = m_5.Replace("零仟", "零");
                //m_6:00-> 0,000-> 0
                m_6 = m_5;
                for (int i = 0; i < iLen; i++)
                    m_6 = m_6.Replace("零零", "零");
                //m_7:6行去億,萬,個位"0"
                m_7 = m_6;
                m_7 = m_7.Replace("億零萬零", "億零");
                m_7 = m_7.Replace("億零萬", "億零");
                m_7 = m_7.Replace("零億", "億");
                m_7 = m_7.Replace("零萬", "萬");
                if (m_7.Length > 2)
                    m_7 = m_7.Replace("零個", "個");
                //m_8:7行+2行小數-> 數目
                m_8 = m_7;
                m_8 = m_8.Replace("個", "");
                if (m_2.Substring(m_2.Length - 3, 3) != "點零零")
                    m_8 += m_2.Substring(m_2.Length - 3, 3);
                //m_9:7行+2行小數-> 價格
                m_9 = m_7;
                m_9 = m_9.Replace("個", " 元");
                if (m_2.Substring(m_2.Length - 3, 3) != "點零零")
                {
                    m_9 += m_2.Substring(m_2.Length - 2, 2);
                    m_9 = m_9.Insert(m_9.Length - 1, "角");
                    m_9 += "分";
                }
                else m_9 += "整";
                if (m_9 != "零 元整")
                    m_9 = m_9.Replace("零 元", "");
                m_9 = m_9.Replace("零分", "整");
                if (type == "數量")
                    return m_8;
                else
                    return m_9;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            #endregion
        }
        /// <summary>
        /// 數字轉貨幣格式
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToCurrencyString(this object input)
        {
            var isNumber = input != null ? Regex.IsMatch(input.ToString(), @"\d") : false;
            return isNumber ? string.Format("{0:N0}", input) : "0";
        }

        /// <summary>
        /// 遮罩
        /// </summary>
        /// <param name="str">輸入字串</param>
        /// <param name="s">開始位置</param>
        /// <param name="len">長度</param>
        /// <param name="mark">遮罩字元</param>
        /// <returns></returns>
        public static string Masking(this string str, int s, int len, char mark = '*')
        {
            if (str.Length == 0 || str.Length < s + len)
            {
                return str;
            }
            var markStr = str.Substring(s, len);
            var tag = string.Empty;
            tag = tag.PadLeft(len, mark);
            return str.Replace(markStr, tag);
        }

        /// <summary>
        /// 轉大寫金額
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string ConvertAmountToChinese(this string amount)
        {
            string[] digits = { "零", "壹", "貳", "叁", "肆", "伍", "陸", "柒", "捌", "玖" };
            string[] units = { "", "拾", "佰", "仟", "萬", "拾", "佰", "仟", "億", "拾", "佰", "仟", "萬" };

            string result = "";

            // 移除金額中的非數字字符
            string number = Regex.Replace(amount, "[^0-9.]", "");

            // 將金額轉換為中文大寫
            if (decimal.TryParse(number, out decimal amountValue))
            {
                string[] parts = number.Split('.');
                string integerPart = parts[0]; // 整數部分
                string decimalPart = (parts.Length > 1) ? parts[1] : ""; // 小數部分

                // 處理整數部分
                int integerLength = integerPart.Length;
                for (int i = 0; i < integerLength; i++)
                {
                    int digit = int.Parse(integerPart[i].ToString());
                    string digitText = digits[digit];
                    int unitIndex = integerLength - i - 1;
                    string unit = units[unitIndex];
                    if (digit == 0)
                    {
                        // 處理連續的零，只保留最後一個零
                        if (result.EndsWith("零"))
                        {
                            if (unitIndex % 4 == 0) // 億或萬的位數
                                result = result.TrimEnd('零') + unit;
                            else
                                result = result.TrimEnd('零');
                        }
                        else if (unitIndex % 4 == 0) // 億或萬的位數
                        {
                            result += unit;
                        }
                    }
                    else
                    {
                        result += digitText + unit;
                    }
                }

                // 處理小數部分
                if (!string.IsNullOrEmpty(decimalPart))
                {
                    int decimalLength = decimalPart.Length;
                    for (int i = 0; i < decimalLength; i++)
                    {
                        int digit = int.Parse(decimalPart[i].ToString());
                        string digitText = digits[digit];
                        string unit = (i == 0) ? "角" : "分";
                        result += digitText + unit;
                    }
                }
                else
                {
                    result += "元整";
                }
            }

            return result;
        }

        /// <summary>
        /// 把使用分隔符號的字串轉換成 int 的集合
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitter"></param>
        /// <returns></returns>
        public static IEnumerable<int> SplitToIntSet(this string str, char splitter = '|', bool doDistinct = true)
        {
            var result = str.Split(splitter)
                .Select(classsetId => int.TryParse(classsetId, out var outId) ? (int?)outId : null)
                .Where(m => m.HasValue)
                .Select(m => m.Value);

            return doDistinct ? result.Distinct() : result;
        }
    }
}
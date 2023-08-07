using System;
namespace MyCodeBase.Library.Extensions
{
    /// <summary>
    /// 時間處理擴充
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 取得日期字串
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime date)
        {
            if (date == null)
            {
                return "";
            }

            var result = date.Year.ToString() + "/" + date.Month.ToString() + "/" + date.Day.ToString() ;
            return result.ToString();
        }
    }
}

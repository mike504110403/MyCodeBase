using NLog;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyCodeBase.Library.Extensions
{
    public static class NLogExtension
    {
        public static Logger Logger
        {
            get => LogManager.GetCurrentClassLogger();
            set { }
        }

        /// <summary>
        /// 紀錄 Level[Info] Msg，適用於要紀錄 string Msg
        /// </summary>
        /// <param name="data"></param>
        public static void Info(string data)
        {
            Logger.Info($"{data}");
        }

        /// <summary>
        /// 紀錄 Level[Info] Msg，適用於要紀錄 object data
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        public static void Info<T>(this T data)
        {
            if (data == null)
            {
                return;
            }

            var type = typeof(T);
            Logger.Info("==================== RequestLog ====================");
            Logger.Info("執行開始 時間:" + DateTime.Now);
            foreach (PropertyInfo item in data.GetType().GetProperties())
            {
                if (type.GetProperty(item.Name) != null && type.GetProperty(item.Name).GetValue(data, null) != null)
                {
                    Logger.Info($"{item.Name}：" + type.GetProperty(item.Name).GetValue(data, null));
                }
            }

            Logger.Info("執行完畢 時間:" + DateTime.Now);
            Logger.Info("==================== RequestLog ====================");
        }

        /// <summary>
        /// 紀錄 Level[Error] Msg
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(this string msg)
        {
            Logger.Error("==================== RequestLog ====================");
            Logger.Error("執行開始 時間:" + DateTime.Now);
            Logger.Error(msg);
            Logger.Error("執行完畢 時間:" + DateTime.Now);
            Logger.Error("==================== RequestLog ====================");
        }
        /// <summary>
        /// 紀錄有Generic類別的資訊
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        //public static void GenericInfo<T>(this T data)
        //{
        //    if (data == null)
        //    {
        //        return;
        //    }
        //    var rvDic = UrlActionHelper.ModelToRouteValueDictionary(data);
        //    if (rvDic == null || !rvDic.Any())
        //    {
        //        return;
        //    }
        //    foreach (var dic in rvDic)
        //    {
        //        Logger.Info($"{dic.Key} : {dic.Value}");
        //    }
        //}
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace MyCodeBase.Library.Extensions
{
    /// <summary>
    /// UrlAction擴充方法
    /// </summary>
    public static class UrlActionHelper
    {
        ///// <summary>
        ///// 物件轉換成RouteValueDictionary
        ///// </summary>
        ///// <typeparam name="T">泛型</typeparam>
        ///// <param name="obj">物件</param>
        ///// <returns></returns>
        //public static RouteValueDictionary ObjectToRouteValueDictionary<T>(T obj)
        //{
        //    var rvDic = new RouteValueDictionary();

        //    if (obj != null)
        //    {
        //        foreach (var property in obj.GetType().GetProperties())
        //        {
        //            //type符合集合類型(Array暫時沒有)
        //            if ((property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
        //                   typeof(ICollection).IsAssignableFrom(property.PropertyType))
        //            {
        //                IEnumerable t = (IEnumerable)property.GetValue(obj);
        //                if (t != null)
        //                {
        //                    var i = 0;
        //                    foreach (var item in t)
        //                    {
        //                        rvDic.Add(string.Concat(property.Name, "[", i, "]"), item);
        //                        i++;
        //                    }
        //                }

        //            }
        //            else
        //            {
        //                var value = property.GetGetMethod().Invoke(obj, null) == null ? "" : property.GetGetMethod().Invoke(obj, null);
        //                if (!string.IsNullOrEmpty(value.ToString()))
        //                {
        //                    rvDic.Add(property.Name, value);
        //                }

        //            }
        //        }
        //    }

        //    return rvDic;
        //}

        ///// <summary>
        ///// 物件轉換成RouteValueDictionary
        ///// </summary>
        ///// <typeparam name="T">泛型</typeparam>
        ///// <param name="obj">物件</param>
        ///// <returns></returns>
        //public static RouteValueDictionary ObjectToRouteValueDictionary<T>(T obj, int? page)
        //{
        //    var rvDic = new RouteValueDictionary();

        //    if (obj != null)
        //    {
        //        foreach (var property in obj.GetType().GetProperties())
        //        {
        //            //type符合集合類型(Array暫時沒有)
        //            if ((property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
        //                   typeof(ICollection).IsAssignableFrom(property.PropertyType))
        //            {
        //                IEnumerable t = (IEnumerable)property.GetValue(obj);
        //                if (t != null)
        //                {
        //                    var i = 0;
        //                    foreach (var item in t)
        //                    {
        //                        rvDic.Add(string.Concat(property.Name, "[", i, "]"), item);
        //                        i++;
        //                    }
        //                }

        //            }
        //            else
        //            {
        //                var value = property.GetGetMethod().Invoke(obj, null) == null ? "" : property.GetGetMethod().Invoke(obj, null);
        //                if (!string.IsNullOrEmpty(value.ToString()))
        //                {
        //                    rvDic.Add(property.Name, value);
        //                }

        //            }
        //        }
        //    }

        //    if (page != null)
        //    {
        //        rvDic.Add("page", page);
        //    }

        //    return rvDic;
        //}

        ///// <summary>
        ///// 轉換  Model To RouteValueDictionary
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="obj"></param>
        ///// <param name="paramName">參數名稱(若 類型為List 須給 binding的名稱)</param>
        ///// <returns></returns>
        //public static RouteValueDictionary ModelToRouteValueDictionary<T>(T obj, string paramName = default)
        //{
        //    var rvDic = new RouteValueDictionary();
        //    if (obj == null)
        //    {
        //        return rvDic;
        //    }
        //    var type = obj.GetType();
        //    if ((type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        //               || typeof(ICollection).IsAssignableFrom(type)
        //               || typeof(Object[]).IsAssignableFrom(type))
        //    {
        //        var list = obj as IEnumerable;
        //        var index = 0;
        //        foreach (var item in list)
        //        {
        //            var value = GetObjectRouteValueDictionary(item);
        //            foreach (var subItem in value)
        //            {
        //                rvDic.Add($"{paramName}[{index}].{subItem.Key}", subItem.Value);
        //            }
        //            index++;
        //        }
        //        return rvDic;
        //    }
        //    rvDic = GetObjectRouteValueDictionary(obj);
        //    return rvDic;
        //}

        ///// <summary>
        ///// 取得物件 RouteValueDictionary
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //private static RouteValueDictionary GetObjectRouteValueDictionary<T>(T obj)
        //{
        //    var rvDic = new RouteValueDictionary();
        //    var type = obj.GetType();
        //    var properties = type.GetProperties();
        //    for (var i = 0; i < properties.Length; i++)
        //    {
        //        var property = properties[i];
        //        var propertyType = property.PropertyType;
        //        if ((propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        //               || typeof(ICollection).IsAssignableFrom(propertyType)
        //               || typeof(Object[]).IsAssignableFrom(propertyType))
        //        {
        //            IEnumerable list = (IEnumerable)property.GetValue(obj);
        //            if (list == null)
        //            {
        //                continue;
        //            }
        //            var index = 0;
        //            foreach (var item in list)
        //            {
        //                var itemType = item.GetType();
        //                var subpropertyName = string.Concat(property.Name, "[", index, "]");
        //                var subproperties = itemType.GetProperties();
        //                if (itemType.IsFundamental())
        //                {
        //                    rvDic.Add($"{subpropertyName}", item);
        //                    continue;
        //                }
        //                if (subproperties != null && subproperties.Any())
        //                {
        //                    var subValue = GetObjectRouteValueDictionary(item);
        //                    foreach (var subItem in subValue)
        //                    {
        //                        rvDic.Add($"{subpropertyName}.{subItem.Key}", subItem.Value);
        //                    }
        //                    index++;
        //                    continue;
        //                }
        //                rvDic.Add(subpropertyName, item);
        //                index++;
        //            }
        //            continue;
        //        }
        //        var propertyValue = property.GetGetMethod().Invoke(obj, null);
        //        if (propertyValue == null)
        //        {
        //            continue;
        //        }
        //        rvDic.Add(property.Name, propertyValue);
        //    }
        //    return rvDic;
        //}
        ///// <summary>
        ///// 判斷是否為基礎類別陣列
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public static bool IsFundamental(this Type type)
        //{
        //    return type.IsPrimitive || type.IsEnum || type.Equals(typeof(string)) || type.Equals(typeof(int));
        //}

    }
}

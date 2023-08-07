using Aspose.Cells;
using Aspose.Words;
using Aspose.Words.Reporting;

using MyCodeBase.Library.ViewModels.Test;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MyCodeBase.Library.Extensions
{
    /// <summary>
    /// aspose擴充
    /// </summary>
    public static class AsposeExtension
    {
        #region 合併資料並生成報告
        /// <summary>
        /// bind doc中同名欄位
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc"></param>
        /// <param name="data"></param>
        /// <param name="fillNewRowWhenEmpty"></param>
        /// <returns></returns>
        public static Document BindData<T>(this Document doc, T data, bool fillNewRowWhenEmpty = false)
        {
            #region 郵件合併 doc內只能用 功能變數 -> MergyField
            // Data 類別第一層
            // 取得單一field bind後table
            var tableSingleField = GetBindDataSingleField(data);
            // doc跟bind後table合併
            doc.MailMerge.Execute(tableSingleField);

            // Data 類別第二層(List內的List)
            // 取得列表 field bind後table
            var tables = GetBindDataListField(data, fillNewRowWhenEmpty);
            // doc跟bind後table合併
            foreach (var item in tables)
            {
                doc.MailMerge.ExecuteWithRegions(item);
            }
            doc.Save("Test.docx", Aspose.Words.SaveFormat.Docx);
            #endregion
            #region ReportingEngine 可用 <<[]>> 較靈活 較簡單
            // 將模板文檔與生成report
            var engine = new ReportingEngine();
            engine.BuildReport(doc, data);
            #endregion
            return doc;
        }
        /// <summary>
        /// 把資料合併到 workBook 中同名的㯗位
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Workbook BindData<T>(this Workbook workBook, T data, bool fillNewRowWhenEmpty = true)
        {
            var designer = new WorkbookDesigner();
            designer.Workbook = workBook;

            var dataSet = new DataSet();
            // Data 類別第一層
            // 取得單一field bind後table
            var test = GetBindDataSingleField(data);
            // 多設table名與第一層資料對應
            test.TableName = "data";

            dataSet.Tables.Add(test);
            designer.SetDataSource(dataSet);

            // Data 類別第二層(List內的List)
            // 取得列表 field bind後table
            var listTables = GetBindDataListField(data, fillNewRowWhenEmpty);
            dataSet.Tables.AddRange(listTables.ToArray());
            // set回 workbook
            designer.SetDataSource(dataSet);
            // binding // 保留計算並生成excel
            designer.Process(true);

            return workBook;
        }

        /// <summary>
        /// 單一field bind 後回傳table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private static DataTable GetBindDataSingleField<T>(T data)
        {
            var table = new DataTable();
            // 取得資料的 名稱、屬性 列表 // 列表內資料非泛型、非列表
            var columnFieldList = data.GetType().GetProperties().Where(p => !(p.PropertyType.IsGenericType && p is IList)).Select(x => new { x.Name, x.PropertyType }).ToList();
            foreach (var column in columnFieldList)
            {
                // 塞到欄位中 類型為基礎類型或屬性的類型
                table.Columns.Add(column.Name, Nullable.GetUnderlyingType(column.PropertyType) ?? column.PropertyType);
            }

            var row = table.NewRow();
            // 取得資料的 名稱、值  列表
            var dataMapping = data.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(data, null)).ToList();
            // 取得資料的 值  列表
            var columnNameList = columnFieldList.Select(m => m.Name).ToList();
            foreach (var item in dataMapping)
            {
                // 名稱若有對到
                if (columnNameList.Any(x => x == item.Key))
                {
                    // 名稱對應的row 塞值 // 找不到報null錯
                    row[item.Key] = item.Value ?? DBNull.Value;
                }
            }
            table.Rows.Add(row);

            return table;
        }
        /// <summary>
        /// 列表field bind 後回傳table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="fillNewRowWhenEmpty"></param>
        /// <returns></returns>
        private static List<DataTable> GetBindDataListField<T>(T data, bool fillNewRowWhenEmpty)
        {
            var tables = new List<DataTable>();
            // 取得資料的 名稱、屬性 列表 // 列表內資料為泛型、列表
            var listTypeFields = data.GetType().GetProperties().Where(p => p.PropertyType.IsGenericType && p.GetValue(data, null) is IList).ToList();

            foreach (var item in listTypeFields)
            {
                var table = new DataTable
                {
                    TableName = item.Name
                };
                // 取得屬性的類型泛型 非單一時報錯
                var type = item.PropertyType.GetGenericArguments().Single();
                // 取得資料的 名稱、屬性 列表
                var columnFieldList = type.GetProperties().Select(x => new { x.Name, x.PropertyType }).ToList();
                // List<T> 中取得 T 的 屬性名稱、類型
                foreach (var column in columnFieldList)
                {
                    table.Columns.Add(column.Name, Nullable.GetUnderlyingType(column.PropertyType) ?? column.PropertyType);
                }

                // 取得列表值
                var rowDataSource = (IList)item.GetValue(data, null);

                foreach (var rowDataItemObj in rowDataSource)
                {
                    var rowDataItem = Convert.ChangeType(rowDataItemObj, type);
                    // 取得資料的 名稱、值  列表
                    var dataMapping = rowDataItem.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(rowDataItem, null)).ToList();
                    var row = table.NewRow();
                    foreach (var fieldData in dataMapping)
                    {
                        // 名稱若有對到
                        if (columnFieldList.Any(x => x.Name == fieldData.Key))
                        {
                            // 名稱對應的row 塞值 // 找不到報null錯
                            row[fieldData.Key] = fieldData.Value ?? DBNull.Value;
                        }
                    }
                    table.Rows.Add(row);
                }
                if (table.Rows.Count == 0 && fillNewRowWhenEmpty)
                {
                    table.Rows.Add(table.NewRow());
                }
                tables.Add(table);
            }

            return tables;
        }
        #endregion

        #region 取得文檔串流
        /// <summary>
        /// 取得文件匯出的 FileStream
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="saveFormat"></param>
        /// <returns></returns>
        public static System.IO.MemoryStream GetFileStream(this Document doc, Aspose.Words.SaveFormat saveFormat)
        {
            var fileStream = new System.IO.MemoryStream();
            doc.Save(fileStream, saveFormat);
            fileStream.Position = 0;
            return fileStream;
        }

        /// <summary>
        /// 取得文件匯出的 FileStream
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="saveFormat"></param>
        /// <returns></returns>
        public static System.IO.MemoryStream GetFileStream(this Workbook workbook, Aspose.Cells.SaveFormat saveFormat)
        {
            var fileStream = new System.IO.MemoryStream();

            switch (saveFormat)
            {
                case Aspose.Cells.SaveFormat.Csv:
                    var saveOptions = new TxtSaveOptions { Encoding = Encoding.UTF8 };
                    workbook.Save(fileStream, saveOptions);
                    break;
                default:
                    workbook.Save(fileStream, saveFormat);
                    break;
            }
            fileStream.Position = 0;
            return fileStream;
        }
        #endregion
    }
}

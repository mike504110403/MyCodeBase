using Aspose.Words;

using MyCodeBase.Library.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCodeBase.Web.Models.BaseService
{
    /// <summary>
    /// 文檔控制
    /// </summary>
    public class FileService
    {
        /// <summary>
        /// 合併多個doc 有分頁符號
        /// </summary>
        /// <param name="docs"></param>
        /// <param name="breakType"></param>
        /// <param name="IsBreak"></param>
        /// <returns></returns>
        public Document MergeDocs(IEnumerable<Document> docs, BreakType breakType = BreakType.SectionBreakNewPage, bool IsBreak = true)
        {
            var dstDoc = new Document();
            var builder = new DocumentBuilder(dstDoc);

            foreach (var doc in docs)
            {
                var attachDoc = doc;
                builder.InsertDocument(attachDoc, ImportFormatMode.KeepSourceFormatting);

                if (IsBreak)
                {
                    builder.InsertBreak(breakType);
                }
            }

            return dstDoc;
        }

        /// <summary>
        /// 串連並套印多個相同範例檔的 aspose doc 
        /// </summary>
        /// <typeparam name="T">aspose doc 資料模型</typeparam>
        /// <param name="list">aspose doc 資料列表</param>
        /// <param name="doc">aspose doc 範例檔</param>
        /// <param name="mode">串接模式</param>
        /// <returns></returns>
        public Aspose.Words.Document CombineModelsToAsposeDoc<T>(IEnumerable<T> list, Document doc, Aspose.Words.ImportFormatMode mode = Aspose.Words.ImportFormatMode.KeepSourceFormatting)
        {
            Aspose.Words.Document docs = null;
            foreach (var item in list)
            {
                doc.BindData(item);

                if (docs == default(Aspose.Words.Document))
                {
                    docs = doc;
                }
                else
                {
                    doc.AppendDocument(doc, mode);
                }
            }
            return docs ?? new Aspose.Words.Document();
        }

        /// <summary>
        /// 合併多個 aspose doc
        /// </summary>
        /// <param name="docList"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Aspose.Words.Document CombineAsposeDoc(IEnumerable<Aspose.Words.Document> docList, Aspose.Words.ImportFormatMode mode = Aspose.Words.ImportFormatMode.KeepSourceFormatting)
        {
            Aspose.Words.Document doc = null;

            foreach (var singleDoc in docList)
            {
                if (doc == default(Aspose.Words.Document))
                    doc = singleDoc;
                else
                    doc.AppendDocument(singleDoc, mode);
            }

            return doc ?? new Aspose.Words.Document();
        }


    }
}
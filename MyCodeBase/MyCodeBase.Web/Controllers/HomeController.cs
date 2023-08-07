using MyCodeBase.Library.ViewModels.Test;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Aspose.Words;
using MyCodeBase.Library.Extensions;
using Aspose.Words.Reporting;
using MyCodeBase.Web.Models.BaseService;
using MyCodeBase.Web.Models.FakeDataForDemoService;
using Aspose.Cells;

namespace MyCodeBase.Web.Controllers
{
    public class HomeController : BaseController
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            //logger.Trace("**** Trace *** ");
            //logger.Debug("**** Debug ***");
            //logger.Info("**** Info ***");
            //logger.Warn("**** Warn ***");
            //logger.Error("**** Error ***");
            //logger.Fatal("**** Fatal ***");
            return View();
        }
        /// <summary>
        /// 取得doc檔匯出串流
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportDocxFile()
        {
            var data = _FakeMultiLayerList.FakeListForBind();

            // 開啟範例文檔
            // var filePath = GetPrintTempFilePath("test.docx");
            // 路徑先寫死 等route整理好再用extension取
            var doc = new Document("D:\\MyPractice\\MyCodeBase\\MyCodeBase\\MyCodeBase.Web\\Template\\test.docx");
            doc.BindData(data);
            doc.Save("bindedDoc.docx", Aspose.Words.SaveFormat.Docx);
            //var docs = new List<Document>();
            //docs.Add(doc);
            //docs.Add(doc);
            ////var mergeDocs = _FileService.MergeDocs(docs);
            //var combineDocs = _FileService.CombineAsposeDoc(docs); 
            //var combinModels = _FileService.CombineModelsToAsposeDoc(docs, doc);
            
            return File(doc.GetFileStream(Aspose.Words.SaveFormat.Docx), "application/docx");
        }

        /// <summary>
        /// 取得xlsx檔匯出串流
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportXlsxFile()
        {
            var data = _FakeMultiLayerList.FakeListForBind();

            // 開啟範例文檔
             var filePath = GetPrintTempFilePath("test.docx");
            // 路徑先寫死 等route整理好再用extension取
            var workBook = new Workbook("D:\\MyPractice\\MyCodeBase\\MyCodeBase\\MyCodeBase.Web\\Template\\test.xlsx");
            workBook.BindData(data);
            workBook.Save("bindedDoc.xlsx", Aspose.Cells.SaveFormat.Xlsx);
            
            return File(workBook.GetFileStream(Aspose.Cells.SaveFormat.Xlsx), "application/xlsx");
        }
    }
}
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
using System.IO;
using MyCodeBase.Web.Filters.NLogFilters;

namespace MyCodeBase.Web.Controllers
{
    [ActionLogFilter]
    public class HomeController : BaseController
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            var userName = User.Identity.Name; // 獲取已驗證的用戶名
            var actionName = GetActionName(); // 自定義的action Name
            logger.Info($"{userName} Into {actionName} Page");
            return View();
        }


        #region 檔案相關
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
        /// <summary>
        /// 列印
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Print()
        {
            var data = _FakeMultiLayerList.FakeListForBind();

            // 開啟範例文檔
            var doc = new Document("D:\\MyPractice\\MyCodeBase\\MyCodeBase\\MyCodeBase.Web\\Template\\test.docx");
            doc.BindData(data);
            doc.Save("bindedDoc.docx", Aspose.Words.SaveFormat.Docx);

            // 如果有資料
            if (doc != null)
            {
                // 轉html字串
                var content = string.Empty;
                // 利用StreamReader將檔案讀成html格式 //沒有aspose憑證，檔案內會有浮水印，svae成html會報錯 可以將圖片傳到本地來解決
                using (StreamReader reader = new StreamReader(doc.GetFileStream(Aspose.Words.SaveFormat.Html)))
                {
                    content = reader.ReadToEnd(); // 將讀取內容存到content
                }
                return Content(content); // 回傳字串結果
            }
            else
            {
                return Json(new { nodata = true });
            }
        }
        #endregion
    }
}
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.Words;

using MyCodeBase.Library.Extensions;
using MyCodeBase.Web.Models.BaseService;
using MyCodeBase.Web.Models.FakeDataForDemoService;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCodeBase.Web.Controllers
{
    public class BaseController : Controller
    {
        #region 注入
        protected FileService _FileService;
        protected FakeMultiLayerList _FakeMultiLayerList;
        public BaseController()
        {
            _FileService = new FileService();
            _FakeMultiLayerList = new FakeMultiLayerList();
        }
        #endregion

        /// <summary>
        /// 取得動作名稱
        /// </summary>
        /// <returns></returns>
        protected string GetActionName()
        {
            // 取RouteConfig設定所對應目前的Route值 的action值
            return ControllerContext.RouteData.Values["action"].ToString();
        }

        #region Aspose.Words

        /// <summary>
        /// 取得套印檔案 Aspose Words Document
        /// </summary>
        /// <param name="fileName">套印檔案名稱</param>
        /// <param name="folderName">套印檔案在樣板根目錄中的資料夾名稱</param>
        /// <param name="rootFolderName">樣板根目錄資料夾名稱</param>
        /// <returns></returns>
        protected Aspose.Words.Document GetPrintTempFileAsposeDocument(string fileName, string folderName = "", string rootFolderName = "Template")
        {
            var fullTemplateFilePath = GetPrintTempFilePath(fileName, folderName, rootFolderName);
            return new Aspose.Words.Document(Server.MapPath(fullTemplateFilePath));
        }
        /// <summary>
        /// 傳入套印資料、範本位置後回傳套印完成檔案 doc、docx、pdf
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="fileName"></param>
        /// <param name="folderName"></param>
        /// <param name="isPdf"></param>
        /// <param name="rootFolderName"></param>
        /// <returns></returns>
        protected ActionResult GetAsposeDocumentResultFile<T>(T model, string fileName, Aspose.Words.SaveFormat saveFormat, string folderName = "", string rootFolderName = "Template")
        {
            var doc = GetPrintTempFileAsposeDocument(fileName, folderName, rootFolderName);
            doc.BindData(model);

            switch (saveFormat)
            {
                case Aspose.Words.SaveFormat.Pdf:
                    return File(doc.GetFileStream(Aspose.Words.SaveFormat.Pdf), MimeMapping.GetMimeMapping(".pdf"), fileName);
                case Aspose.Words.SaveFormat.Odt:
                    return File(doc.GetFileStream(Aspose.Words.SaveFormat.Odt), MimeMapping.GetMimeMapping(".odt"), fileName);
                case Aspose.Words.SaveFormat.Ott:
                    return File(doc.GetFileStream(Aspose.Words.SaveFormat.Ott), MimeMapping.GetMimeMapping(".ott"), fileName);
                case Aspose.Words.SaveFormat.Doc:
                    return File(doc.GetFileStream(Aspose.Words.SaveFormat.Doc), MimeMapping.GetMimeMapping(".doc"), fileName);
                case Aspose.Words.SaveFormat.Docx:
                    return File(doc.GetFileStream(Aspose.Words.SaveFormat.Docx), MimeMapping.GetMimeMapping(".docx"), fileName);
                default:
                    return null;
            }
        }
        
        #endregion

        #region Aspose.Cells

        /// <summary>
        /// 取得套印檔案 Aspose Cells
        /// </summary>
        /// <param name="fileName">套印檔案名稱</param>
        /// <param name="folderName">套印檔案在樣板根目錄中的資料夾名稱</param>
        /// <param name="rootFolderName">樣板根目錄資料夾名稱</param>
        /// <returns></returns>
        protected Workbook GetPrintTempFileAsposeCells(string fileName, string folderName = "", string rootFolderName = "Temp")
        {
            var fullTemplateFilePath = GetPrintTempFilePath(fileName, folderName, rootFolderName);
            return new Workbook(Server.MapPath(fullTemplateFilePath));
        }

        /// <summary>
        /// 傳入套印資料、範本位置後回傳套印完成檔案
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="fileName"></param>
        /// <param name="folderName"></param>
        /// <param name="isPdf"></param>
        /// <param name="rootFolderName"></param>
        /// <returns></returns>
        protected ActionResult GetAsposeCellResultFile<T>(T model, string fileName, Aspose.Cells.SaveFormat saveFormat, string folderName = "", string rootFolderName = "Temp")
        {
            var workBook = GetPrintTempFileAsposeCells(fileName, folderName, rootFolderName);
            workBook.BindData(model);

            switch (saveFormat)
            {
                case Aspose.Cells.SaveFormat.Pdf:
                    return File(workBook.GetFileStream(Aspose.Cells.SaveFormat.Pdf), MimeMapping.GetMimeMapping(".pdf"), fileName);
                case Aspose.Cells.SaveFormat.Csv:
                    return File(workBook.GetFileStream(Aspose.Cells.SaveFormat.Csv), MimeMapping.GetMimeMapping(".csv"), fileName);
                case Aspose.Cells.SaveFormat.Docx:
                    return File(workBook.GetFileStream(Aspose.Cells.SaveFormat.Xlsx), MimeMapping.GetMimeMapping(".xlsx"), fileName);
                case Aspose.Cells.SaveFormat.Ods:
                    return File(workBook.GetFileStream(Aspose.Cells.SaveFormat.Ods), MimeMapping.GetMimeMapping(".ods"), fileName);
                default:
                    return null;
            }
        }
        #endregion

        #region 共用
        /// <summary>
        /// 取得套印檔案路徑
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folderName"></param>
        /// <param name="rootFolderName"></param>
        /// <returns></returns>
        protected string GetPrintTempFilePath(string fileName, string folderName = "", string rootFolderName = "Temp")
        {
            folderName = folderName.TrimStart('/').TrimEnd('/');
            rootFolderName = rootFolderName.TrimStart('/').TrimEnd('/');

            folderName = string.IsNullOrEmpty(folderName) ? folderName : $"{folderName}/";

            return $"~/{rootFolderName}/{folderName}{fileName}";
        }
        #endregion
    }
}
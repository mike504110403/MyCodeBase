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
using Aspose.Words.Replacing;
using Aspose.Words.Tables;
using System.Text.RegularExpressions;
using System.Collections;

namespace MyCodeBase.Web.Controllers
{
    [ActionLogFilter]
    public class HomeController : BaseController
    {
        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            return View();
        }
        /// <summary>
        /// NLog測試
        /// </summary>
        /// <returns></returns>
        public ActionResult NLogTest()
        {
            //var userName = User.Identity.Name; // 獲取已驗證的用戶名
            //var actionName = GetActionName(); // 自定義的action Name
            //logger.Info($"{userName} Into {actionName} Page");

            logger.Trace("This is Trace");
            logger.Debug("This is Debug");
            logger.Info("This is Info");
            logger.Warn("This is Warn");
            logger.Error("This is Error");
            logger.Fatal("This is Fatal");

            try
            {
                var a = 0;
                int result = 6 / a;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }

            return Content("請前往'~/AppData/Logs'確認log檔");
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
        /// <summary>
        /// 操控doc文檔
        /// </summary>
        public void DocControll()
        {
            var doc = new Document();
            var builder = new DocumentBuilder(doc);

            #region 表格操作
            builder.StartTable(); // 建立第一個表格
            builder.InsertCell(); // 新增儲存格
            builder.Write("Row 1, Cell 1."); // 寫入文字
            builder.InsertCell(); // 同row再新增表格
            builder.Write("Row 1, Cell 2."); // 寫入文字
            builder.EndRow(); // 結束row
            builder.InsertCell(); // 在下一row新增儲存格
            builder.Write("Row 2, Cell 1.");
            builder.InsertCell();
            builder.Write("Row 2, Cell 2.");
            builder.EndTable(); // 結束表格

            // 全文件找第一個表格
            var table = (Table)doc.GetChild(NodeType.Table, 0, true);
            // 參數設置
            var options = new FindReplaceOptions
            {
                MatchCase = true, // 是否區分大小寫
                FindWholeWordsOnly = true // 是否全字匹配
            };
            // 將 "Mr" 替換為 "test"
            table.Rows[1].Cells[2].Range.Replace("Mr", "test", options);
            #endregion
            #region 段落(樣式)操作
            // 新建一個名為 MyStyle 的段落樣式
            doc.Styles.Add(StyleType.Paragraph, "MyStyle");
            var font = builder.Font;
            font.Bold = true;
            font.Color = System.Drawing.Color.Red;
            font.Italic = true;
            font.Name = "Arial";
            font.Size = 24;
            font.Spacing = 5;
            font.Underline = Underline.Double;
            // 設置段落樣式
            builder.ParagraphFormat.Style = doc.Styles["MyStyle"];
            builder.MoveToDocumentEnd(); // 移動到文件末尾
            builder.Writeln("I'm a very nice formatted string.");
            #endregion
            #region 書籤操作
            // 找到書籤 "MyBookmark" 的位置(Range)
            var bookmark = doc.Range.Bookmarks["MyBookmark"];
            // 取書籤的名稱和內容
            var name = bookmark.Name;
            var text = bookmark.Text;
            // 替換書籤的名稱和內容
            bookmark.Name = "RenamedBookmark";
            bookmark.Text = "This is a new bookmarked text.";
            #endregion
            #region 文字取代
            // 正則表達式 忽略大小寫
            var regex = new Regex("Hello World!", RegexOptions.IgnoreCase);
            // 取代文字
            doc.Range.Replace(regex, "Hi Everyone!");
            #endregion
            #region 分隔符控制
            // 歷遍所有段落
            foreach (Paragraph par in doc.GetChildNodes(NodeType.Paragraph, true))
            {
                par.ParagraphBreakFont.Hidden = false;
                // 歷遍文本片段
                foreach (Run run in par.GetChildNodes(NodeType.Run, true))
                {
                    if (run.Font.Hidden)
                        run.Font.Hidden = false;
                }
            }
            #endregion
            #region 分頁符控制
            NodeCollection paragraphs = doc.GetChildNodes(NodeType.Paragraph, true);
            // 歷遍段落
            foreach (Paragraph para in paragraphs)
            {
                // 段落情是否有分頁符
                if (para.ParagraphFormat.PageBreakBefore)
                    para.ParagraphFormat.PageBreakBefore = false;
                // 歷遍文本片段
                foreach (Run run in para.Runs)
                    // 移除分頁符
                    if (run.Text.Contains(ControlChar.PageBreak))
                        run.Text = run.Text.Replace(ControlChar.PageBreak, string.Empty);
            }
            #endregion
            #region 節操作
            for (int i = doc.Sections.Count - 2; i >= 0; i--)
            {
                // 將內容插到開頭
                doc.LastSection.PrependContent(doc.Sections[i]);
                // 移除節
                doc.Sections[i].Remove();
            }
            #endregion
            #region 評論操作
            var comment = new Aspose.Words.Comment(doc);
            // 新增段落
            Paragraph commentParagraph = new Paragraph(doc);
            commentParagraph.AppendChild(new Run(doc, "This is comment!!!"));
            // 將段落加到評論
            comment.AppendChild(commentParagraph);
            
            int commentId = 0;
            CommentRangeStart start = new CommentRangeStart(doc, commentId);
            CommentRangeEnd end = new CommentRangeEnd(doc, commentId);
            builder.Write("This text is before the comment. ");
            builder.InsertNode(comment); // 加入評論開始節點
            builder.InsertNode(start);
            builder.Write("This text is commented. ");
            builder.InsertNode(end); // 加入評論結束節點
            builder.Write("This text is after the comment.");

            var collectedComments = new ArrayList();
            NodeCollection comments = doc.GetChildNodes(NodeType.Comment, true);
            // 取出所有評論
            foreach (Aspose.Words.Comment com in comments)
                collectedComments.Add(com.Author + " " + com.DateTime + " " + com.ToString(Aspose.Words.SaveFormat.Text));
            // 移除評論
            foreach (Aspose.Words.Comment itemCom in collectedComments)
                itemCom.Remove(); // 移除單筆
            // 移除全部
            collectedComments.Clear();
            #endregion



        }

        #endregion
    }
}
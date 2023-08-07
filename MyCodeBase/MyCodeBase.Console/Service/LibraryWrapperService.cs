using MyCodeBase.Console.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static MyCodeBase.Console.Service.WriterService;

namespace MyCodeBase.Console.Service
{
    public class LibraryWrapperService
    {
        // 宣告時用介面宣告
        private IWriter _writer;
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name=""></param>
        public LibraryWrapperService(IWriter inWriter) // 參數可傳介面或實作介面的類別
        {
            _writer = inWriter;
        }

        public void Writer(string[] args)
        {
            LibraryWrapperService output;

            if (args.Count() > 0 && args[0] == "text")
            {
                output = new LibraryWrapperService(new TextOutput());
            }
            else
            {
                output = new LibraryWrapperService(new ConsoleOutput());
            }
        }
    }
}

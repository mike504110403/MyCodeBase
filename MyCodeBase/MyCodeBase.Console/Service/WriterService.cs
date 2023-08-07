using MyCodeBase.Console.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCodeBase.Console.Service
{
    public class WriterService
    {
        public class TextOutput : IWriter
        {
            public void Write(string text)
            {
                File.WriteAllText("Filex.txt", text);
            }
        }

        public class ConsoleOutput : IWriter
        {
            public void Write(string text)
            {
                System.Console.WriteLine(text);
            }
        }
    }
}

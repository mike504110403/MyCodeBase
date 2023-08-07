using Autofac;
using MyCodeBase.Console.Service;
using StackExchange.Redis;
using System;
using System.Collections.Specialized;
using System.Configuration;
using MyCodeBase.Library.Extensions;
using MyCodeBase.Library.ViewModels.Test;
using Aspose.Words;

namespace MyCodeBase.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Redis
            // 建立 reids 連線
            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            //// 也可以一次連多個
            ////ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("server1:6379,server2:6379");
            ////string config = redis.Configuration;

            //// 獲取 Redis 資料庫 
            //var db = redis.GetDatabase();

            //// 通過db使用Redis API （http://redis.io/commands）
            //db.StringSet("mykey", "myvalue", new TimeSpan(0, 10, 0), When.Always, CommandFlags.None);
            //// 創建連接到特定服務的 PUB/SUB 連接
            //ISubscriber sub = redis.GetSubscriber();
            //// 訂閱頻道，並處於監聽狀態，接受消息並處理
            //var result = string.Empty;
            //sub.Subscribe("messages", (channel, message) =>
            //{
            //    result = string.Format("Channel:{0} ; Message:{1} .", channel.ToString(), message);
            //});
            //// 在另一個進程或是機器上，發佈消息
            //sub.Publish("messages", "hello");

            //// 用 Key 找 Redis 內資料 
            //var findKey = "mykey";
            //var printKey = db.KeyExists(findKey)
            //    ? db.KeyExists(findKey).ToString() + ": " + db.StringGet(findKey)
            //    : findKey + " is not exist!";
            //Console.WriteLine(printKey);
            //// 寫入資料
            //var Key = "myKey";
            //var _lockKey = $"{"LockKey"}{Key}";
            //if (!db.KeyExists(_lockKey))
            //{
            //    // 透過多寫一個key來表示在寫入中
            //    db.StringSet(_lockKey, true.ToString());
            //    db.StringSet(Key, "改寫後的");
            //    // 移除標示用Key
            //    db.KeyDelete(_lockKey);
            //}
            //Console.WriteLine(Key + ": " + db.StringGet(Key));
            #endregion

            #region DI
            // 建container Builder
            //var builder = new ContainerBuilder();

            // 註冊
            // 註冊Type
            // builder.RegisterType<ConsoleLogger>.As<ILogger>();
            // 用實例註冊
            // var output = new StringWriter();
            // builder.RegisterInstance(output).As<TextWriter>();
            // 也可以用lambda
            // builder.Register(c => new ConfigReader("mysection")).As<IConfigReader>();
            // 用scan的批次註冊
            // builder.RegisterAssemblyTypes(myAssembly) .Where(t => t.Name.EndsWith("Repository")) .AsImplementedInterfaces();

            // 建container
            //var container = builder.Build();

            // 從container取得component
            //using (var scope = container.BeginLifetimeScope())
            //{
            //    var reader = container.Resolve<IConfigReader>();
            //}
            #endregion

            #region aspose
            //var t = 0;
            //var testLists = new List<TestList>();
            //while (t < 10)
            //{
            //    var testlist = new TestList()
            //    {
            //        Subject = "test",
            //        Score = 100
            //    };
            //    testLists.Add(testlist);

            //}
            //var test = new Test()
            //{
            //    UserName = "HHH",
            //    Age = 26,
            //    TestLists = testLists
            //};
            //// 開啟範例文檔
            //var doc = new Document("D:\\MyPractice\\DotNetPractice\\MyCodeBase\\MyCodeBase.Console\\Temp\\test.docx");
            //doc.BindData(test);
            //doc.Save("D:\\MyPractice\\DotNetPractice\\MyCodeBase\\MyCodeBase.Console\\OutputFile\\bindedDoc.docx", SaveFormat.Docx);
            #endregion

            #region MathExtension
            //var intString1 = "";
            //var intString2 = "123";
            //var intString3 = "我知道呀";
            //var intString4 = "12.0";
            //var intString5 = "12.12345";
            //Console.WriteLine(intString1);
            //Console.WriteLine("int: " + intString1.IntSafeParse());
            //Console.WriteLine("double: " + intString1.DoubleSafeParse());
            //Console.WriteLine(intString2);
            //Console.WriteLine("int: " + intString2.IntSafeParse());
            //Console.WriteLine("double: " + intString2.DoubleSafeParse());
            //Console.WriteLine(intString3);
            //Console.WriteLine("int: " + intString3.IntSafeParse());
            //Console.WriteLine("double: " + intString3.DoubleSafeParse());
            //Console.WriteLine(intString4);
            //Console.WriteLine("int: " + intString4.IntSafeParse());
            //Console.WriteLine("double: " + intString4.DoubleSafeParse());
            //Console.WriteLine(intString5);
            //Console.WriteLine("int: " + intString5.IntSafeParse());
            //Console.WriteLine("double: " + intString5.DoubleSafeParse());

            #endregion

            //// zh-tw
            //var test = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            //Console.WriteLine(test);
        }
    }
}
using StackExchange.Redis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCodeBase.Console.Service
{
    public class RedisConnectionFactory
    {
        private static readonly Lazy<ConnectionMultiplexer> Connection;

        /// <summary>
        /// 連線功能
        /// </summary>
        static RedisConnectionFactory()
        {
            var connectionString = "";
            var options = ConfigurationOptions.Parse(connectionString + ",password=mike1234");
            Connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options));
        }
        
    }
}

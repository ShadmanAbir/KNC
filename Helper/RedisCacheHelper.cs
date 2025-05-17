using StackExchange.Redis;
using System;

namespace KNC.Helper
{
    public static class RedisCacheHelper
    {
        private static readonly Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            try
            {
                var connStr = System.Configuration.ConfigurationManager.AppSettings["RedisConnection"];
                return ConnectionMultiplexer.Connect(connStr);
            }
            catch (Exception ex)
            {
                // Log or rethrow for debugging
                throw new InvalidOperationException("Could not connect to Redis. Check your connection string and server.", ex);
            }
        });

        public static ConnectionMultiplexer Connection => lazyConnection.Value;

        public static void Set(string key, string value, TimeSpan? expiry = null)
        {
            var db = Connection.GetDatabase();
            db.StringSet(key, value, expiry);
        }

        public static string Get(string key)
        {
            var db = Connection.GetDatabase();
            return db.StringGet(key);
        }

        public static void Remove(string key)
        {
            var db = Connection.GetDatabase();
            db.KeyDelete(key);
        }
    }
}
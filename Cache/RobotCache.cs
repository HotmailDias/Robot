using APIToyRobot.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace APIToyRobot.Cache
{
    public class RobotCache: IRobotCache
    {

        public void StoreCache(Robot robotData)
        {
            ObjectCache cache = MemoryCache.Default;
            var key = robotData.Identifier;
            var cacheItem = new CacheItem(key, robotData);
            var cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(7)
            };
            cache.Set(cacheItem, cacheItemPolicy);
        }


        public async Task<Robot> GetCache(string key)
        {
            var cache = MemoryCache.Default;
            return (Robot)cache.Get(key);
        }
    }
    public interface IRobotCache
    {
        void StoreCache(Robot robotData);
        Task<Robot> GetCache(string key);


    }

}




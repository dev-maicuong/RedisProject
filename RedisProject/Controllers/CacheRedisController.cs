using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheRedisController : ControllerBase
    {
        public readonly IDatabase _database;

        public CacheRedisController(IDatabase database)
        {
            _database = database;
        }
        // GET api/<CacheRedisController>/5
        [HttpGet("GetKey")]
        public async Task<string> GetKey(string keyRedis)
        {
            try
            {
                keyRedis = $"Key_Tang1:Tang2:{keyRedis}";
                var result = await _database.StringGetAsync(keyRedis);
                return result;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            
            //return await Task.Run(async () => await _database.StringGetAsync(key));
        }
        [HttpPost("CreateUseKey")]
        public async Task<string> CreateUseKey( [FromForm] string keyRedis, [FromForm] string  value)
        {
            try
            {
                if (string.IsNullOrEmpty(keyRedis) || string.IsNullOrEmpty(value)) return "Có một giá trị null";
                keyRedis = $"Key_Tang1:Tang2:{keyRedis}";
                var result = await _database.StringSetAsync(keyRedis, value, new TimeSpan(0,7,30));
                return result? "Lưu thành công": "Lưu thất bại";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet("GetHash")]
        public async Task<string> GetHash(string keyRedis, string valueKey)
        {
            try
            {
                keyRedis = $"Hash_Tang1:Tang2:{keyRedis}";
                var result = await _database.HashGetAsync(keyRedis, valueKey);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost("CreateUseHash")]
        public async Task<string> CreateUseHash([FromForm] string keyRedis, [FromForm] string key, [FromForm] string value)
        {
            try
            {
                if (string.IsNullOrEmpty(keyRedis) || string.IsNullOrEmpty(value)) return "Có một giá trị null";
                keyRedis = $"Hash_Tang1:Tang2:{keyRedis}";
                await _database.HashSetAsync(keyRedis, new[] { new HashEntry(key, value) }, CommandFlags.None);
                return 1 ==1 ? "Lưu thành công" : "Lưu thất bại";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet("GetListIndex")]
        public async Task<string> GetListIndex(string keyRedis, int index)
        {
            try
            {
                keyRedis = $"List_Tang1:Tang2:{keyRedis}";
                var result = await _database.ListGetByIndexAsync(keyRedis, index);
                return result.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet("GetListAll")]
        public async Task<string> GetListAll(string keyRedis)
        {
            try
            {
                keyRedis = $"List_Tang1:Tang2:{keyRedis}";
                var result = await _database.ListRangeAsync(keyRedis,0,-1);
                return string.Join("-", result);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost("CreateUseList")]
        public async Task<string> CreateUseList([FromForm] string keyRedis, [FromForm] string value)
        {
            try
            {
                if (string.IsNullOrEmpty(keyRedis) || string.IsNullOrEmpty(value)) return "Có một giá trị null";
                keyRedis = $"List_Tang1:Tang2:{keyRedis}";
                var listLength = await _database.ListRightPushAsync(keyRedis, value);
                return $"Độ dài của danh sách sau khi push: {listLength}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        // POST api/<CacheRedisController>
        public async Task<long> Add(string key, string value)
        {


            //var data = new
            //{
            //    statusCode = 0,
            //    message = "",
            //    data = new
            //    {
            //        key1 = "value1",
            //        key2 = "value3",
            //    }
            //};
            //var result = await _database.ListRangeAsync("myhash", );
            //return result;
            // Set theo string
            //var data = new
            //{
            //    statusCode = 0,
            //    message = "",
            //    data = new 
            //    { 
            //        key1 = "value1",
            //        key2 = "value2",
            //    }
            //};
            //var result = await _database.StringSetAsync(key, JsonConvert.SerializeObject(data), TimeSpan.FromSeconds(30));
            //return result;
            return 0;
        }
        public async Task<bool> AddList(string key, string value, long index)
        {
            return true;
        }

        // PUT api/<CacheRedisController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CacheRedisController>/5
        [HttpGet("Delete")]
        public async Task<bool> Delete(string key)
        {
            var result = await _database.KeyDeleteAsync(key);
            return result;
        }
    }
}

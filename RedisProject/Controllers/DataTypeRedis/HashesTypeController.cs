using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisProject.Controllers.DataTypeRedis
{
    [Route("api/redis/[controller]")]
    [ApiController]
    public class HashesTypeController : ControllerBase
    {
        public readonly IDatabase _dbRedis;
        public HashesTypeController(IDatabase dbRedis)
        {
            _dbRedis = dbRedis;
        }
        [HttpGet("GetHash")]
        public async Task<string> GetHash(string keyRedis, string valueKey)
        {
            try
            {
                keyRedis = $"Hash_Type:{keyRedis}";
                var result = await _dbRedis.HashGetAsync(keyRedis, valueKey);
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
                keyRedis = $"Hash_Type:{keyRedis}";
                await _dbRedis.HashSetAsync(keyRedis, new[] { new HashEntry(key, value) }, CommandFlags.None);
                return 1 == 1 ? "Lưu thành công" : "Lưu thất bại";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

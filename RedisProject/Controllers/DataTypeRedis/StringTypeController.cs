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
    public class StringTypeController : ControllerBase
    {
        public readonly IDatabase _dbRedis;
        public StringTypeController(IDatabase dbRedis)
        {
            _dbRedis = dbRedis;
        }
        [HttpGet("GetKey")]
        public async Task<string> GetKey(string keyRedis)
        {
            try
            {
                keyRedis = $"String_Type:{keyRedis}";
                var result = await _dbRedis.StringGetAsync(keyRedis);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost("CreateUseKey")]
        public async Task<string> CreateUseKey([FromForm] string keyRedis, [FromForm] string value)
        {
            try
            {
                if (string.IsNullOrEmpty(keyRedis) || string.IsNullOrEmpty(value)) return "Có một giá trị null";
                keyRedis = $"String_Type:{keyRedis}";
                var result = await _dbRedis.StringSetAsync(keyRedis, value, new TimeSpan(0, 7, 30));
                return result ? "Lưu thành công" : "Lưu thất bại";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

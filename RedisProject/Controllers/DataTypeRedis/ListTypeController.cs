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
    public class ListTypeController : ControllerBase
    {
        public readonly IDatabase _dbRedis;
        public ListTypeController(IDatabase dbRedis)
        {
            _dbRedis = dbRedis;
        }
        [HttpGet("GetListIndex")]
        public async Task<string> GetListIndex(string keyRedis, int index)
        {
            try
            {
                keyRedis = $"List_Type:{keyRedis}";
                var result = await _dbRedis.ListGetByIndexAsync(keyRedis, index);
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
                keyRedis = $"List_Type:{keyRedis}";
                var result = await _dbRedis.ListRangeAsync(keyRedis, 0, -1);
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
                keyRedis = $"List_Type:{keyRedis}";
                var listLength = await _dbRedis.ListRightPushAsync(keyRedis, value);
                return $"Độ dài của danh sách sau khi push: {listLength}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

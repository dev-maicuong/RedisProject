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
    public class SetTypeController : ControllerBase
    {
        readonly IDatabase _dbRedis;
        public SetTypeController(IDatabase dbRedis)
        {
            _dbRedis = dbRedis;
        }
        [HttpGet("Get")]
        public async Task<string> Get(string key)
        {
            try
            {
                key = $"Set_Type:{key}";
                var result = await _dbRedis.SetMembersAsync(key);
                return string.Join("--", result);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return $"";
        }

        [HttpPost("Create")]
        public async Task<string> Create([FromForm]string key = null, [FromForm] string val= null)
        {
            try
            {
                key = $"Set_Type:{key}";
                var result = await _dbRedis.SetAddAsync(key, val);
                return $"{result}";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

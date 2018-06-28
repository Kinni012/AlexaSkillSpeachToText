using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using ChatSample.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace DisplayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IHubContext<ChatHub> ctx;

        public ValuesController(IHubContext<ChatHub> ctx)
        {
            this.ctx = ctx;
        }

        string alexaSkillContent = "Test";
        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            await ctx.Clients?.All?.SendAsync("broadcastMessage", "name", "msg");
            return alexaSkillContent;
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody] dynamic value)
        {
            string content = value.Data;
            if (alexaSkillContent != content)
            {
                alexaSkillContent = content;
            }
        }
    }
}

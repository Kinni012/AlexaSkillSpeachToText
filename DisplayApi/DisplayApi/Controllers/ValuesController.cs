using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using ChatSample.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using System.Web;

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
            await ctx.Clients?.All?.SendAsync("broadcastMessage", "msg");
            return alexaSkillContent;
        }


        // POST api/values
        [HttpPost]
        public async void PostAsync([FromBody] string value)
        {
            await ctx.Clients?.All?.SendAsync("broadcastMessage", value);
            if (alexaSkillContent != value)
            {
                alexaSkillContent = value;
            }
        }
    }
}

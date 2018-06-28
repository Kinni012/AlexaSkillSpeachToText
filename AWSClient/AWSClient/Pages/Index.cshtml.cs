using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWSClient.Pages
{
    public class IndexModel : PageModel
    {
        public string fileContent { get; set; }
        public void OnGet()
        {
            var client = new HttpClient();

            //var res = client.PostAsync("http://10.0.1.195:7909/api/file/CreateFile", new StringContent("\"Test.txt\"", Encoding.UTF8, "application/json"));
            //Task.WaitAll();
            fileContent = "";
            //fileContent = res.Result?.ToString();
        }
    }
}

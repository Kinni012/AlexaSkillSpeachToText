using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AWSLambda1.HttpHandler
{
  public static class HttpHandler
  {
    public static string PerformPost(string url, string parameter)
    {
      var client = new HttpClient();
      var res = client.PostAsync(url, new StringContent(parameter, Encoding.UTF8, "application/json"));
      Task.WaitAll();
      var temp = res.Result;
      return temp.ToString();

      //var client = new HttpClient();
      //var res = client.PostAsync("http://10.0.0.195:7909/api/file/CreateFile", new StringContent("\"Test.txt\"", Encoding.UTF8, "application/json"));
      //Task.WaitAll();
      //var temp = res.Result;
    }
  }
}

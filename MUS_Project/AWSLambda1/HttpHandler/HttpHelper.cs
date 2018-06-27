using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AWSLambda1.HttpHandler
{
  public static class HttpHelper
  {
    public static string PerformPost(string url, string parameter)
    {
      Task<string> result = GetResponseString(url, parameter);
      Task.WaitAll();
      string s = result.Result;
      return s;

      //var client = new HttpClient();
      //var res = client.PostAsync("http://10.0.0.195:7909/api/file/CreateFile", new StringContent("\"Test.txt\"", Encoding.UTF8, "application/json"));
      //Task.WaitAll();
      //var temp = res.Result;
    }
    static async Task<string> GetResponseString(string uri, string parameter)
    {
      var httpClient = new HttpClient();
      var response = await httpClient.PostAsync(uri, new StringContent(parameter, Encoding.UTF8, "application/json"));
      var contents = await response.Content.ReadAsStringAsync();
      return contents;
    }
  }
}

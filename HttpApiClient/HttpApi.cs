using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;

namespace HttpApiClient
{
    public class HttpApi
    {
   

    private static HttpClient CreateHttpClient(HttpClientHandler handler) {
      var client = new HttpClient(handler);

      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      return client;
    }

    private static HttpClientHandler CreateHttpClientHandlerWithDecompression() {

      return new HttpClientHandler() {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
      };
    }

    private static HttpClient CreateHttpClient() {
      var client = new HttpClient();

      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      return client;
    }

    
    public static async Task<string> Get(string url) {

      HttpClientHandler handler = CreateHttpClientHandlerWithDecompression();

      using (HttpClient client = CreateHttpClient(handler)) {       

        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();            
      }
    }

    public static async Task<string> Delete(string url) {

      using (HttpClient client = CreateHttpClient()) {
       // client.DefaultRequestHeaders.Accept.Clear();
       /// client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = client.DeleteAsync(url).Result;
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();     
      }

    }

    public static async Task<string> Post(string url, object form) {

      using (HttpClient client = new HttpClient()) {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        string formAsString = JsonConvert.SerializeObject(form);
        Dictionary<string,string> formAsDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(formAsString);

        FormUrlEncodedContent body = new FormUrlEncodedContent(formAsDictionary);
        HttpResponseMessage result = await client.PostAsync(url, body);
        result.EnsureSuccessStatusCode();

        return await result.Content.ReadAsStringAsync();     
      }

    }

    public static async Task<string> Put(string url, object parameters) {

      using (HttpClient client = CreateHttpClient()) {
        /*client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));*/

        var jsonParameter = JsonConvert.SerializeObject(parameters);
        var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonParameter);

        FormUrlEncodedContent content = new FormUrlEncodedContent(values);
        HttpResponseMessage result = await client.PutAsync(url, content);

        result.EnsureSuccessStatusCode();
        return await result.Content.ReadAsStringAsync();
      }

    }





  }
}

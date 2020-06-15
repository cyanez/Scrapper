using System;
using System.Threading.Tasks;

using HttpApiClient;

namespace Empiria.DataAnalytics.WebScraping.ParseHubClient
{
  public class ParseHub
  {
    private readonly string baseUrl = "https://www.parsehub.com/api/v2/";
    private ParseHubProjectData project = new ParseHubProjectData();
    
    public  ParseHub(ParseHubProjectData phProjectData) {
      this.project = phProjectData;
      
    }

    public  async Task<string> RunProject() {

      string url = baseUrl + "projects/" + project.api_project + "/run";

      return await HttpApiClient.HttpApi.Post(url, project);
    }

    public  async Task<string> GetLastReadyData() {

      string url = baseUrl + project.api_project + "/last_ready_run/data?api_key=" + project.api_key;

      return await HttpApiClient.HttpApi.Get(url);
    }

    public async Task<string> GeDataRun(string RUN_TOKEN) {

      string url = baseUrl + "runs/" + RUN_TOKEN + "/data?api_key=" + project.api_key;
      return await HttpApiClient.HttpApi.Get(url);
    }



  }
}

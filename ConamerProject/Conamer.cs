using System;
using System.Threading.Tasks;
using System.Threading;
using Empiria.DataAnalytics.WebScraping.ParseHubClient;

using Newtonsoft.Json;

namespace ConamerProject
{
  public class ConamerData {
    public string name = string.Empty;
    public string homoclabe = string.Empty;

  }
  public class Conamer
  {
    private static string api_key = "tYcEW5qOq8i0";
    private static string api_project = "tpTQ28WzVsSA"; //"tiS4BPHGLvnE";

    private static ParseHubProjectData LoadProjectData(string start_url){
      ParseHubProjectData projectData = new ParseHubProjectData();

      projectData.api_key = api_key;
      projectData.api_project = api_project;
      projectData.start_url = start_url;

      return projectData;
    }

    public static async Task<string> Proccess(string start_url) {

      ParseHubProjectData projectData = LoadProjectData(start_url);

      ParseHub parseHub = new ParseHub(projectData);

      var run = await parseHub.RunProject();
      

      dynamic runObject = JsonConvert.DeserializeObject(run);
      var run_token = runObject.GetValue("run_token").ToString();

      Console.WriteLine(run_token);
      
      Thread.Sleep(45000);
     
      string result = await parseHub.GeDataRun(run_token);   

      //string result = await parseHub.GetLastReadyData();
      
      return result;
    }
    


  }
}

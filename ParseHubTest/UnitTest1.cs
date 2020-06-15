using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ParseHubTest
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public  void TestMethod1() {
      string start_url = "https://conamer.gob.mx/tramites/FichaPublicaV2/Index/?consulId=6000&Modalidad=&Copia=";

      var result = Scrapper.Scrapper.Scrap("Conamer", start_url).Result;

      Console.WriteLine(result);
      Console.ReadLine();
    }
  }
}

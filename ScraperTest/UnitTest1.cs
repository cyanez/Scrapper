using System;
using Xunit;

namespace ScraperTest
{
  public class UnitTest1
  {
    [Fact]
    public void Test1() {
      string start_url = "https://conamer.gob.mx/tramites/FichaPublicaV2/Index/?consulId=6000&Modalidad=&Copia=";

      var result = await Scrapper.Scrapper.Scrap("Conamer", start_url);

      Console.WriteLine(result);
      Console.ReadLine();
    }
  }
}

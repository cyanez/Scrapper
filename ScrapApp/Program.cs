using System;
using System.Collections.Generic;

using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Scrapper;

using Empiria.DataAnalytics.WebScraping.ParseHubClient;

using System.Data.SqlClient;
using System.Data;

namespace ScrapApp
{

 
  class Program
  {

    private Requerimientos requerimiento = new Requerimientos();
    private List<Articulos> arts = new List<Articulos>();
   
    static  void Main(string[] args) {
      //try {
      //     Caller().Wait();
      //  Console.WriteLine("Listo");
      //  Console.ReadLine();
      //  } catch (Exception e) {
      //    Console.WriteLine("excepcion" + e.ToString());
      //    Console.ReadLine();
      //  } 

  

     var x = ETL.Transform("https://conamer.gob.mx/tramites/FichaPublicaV2/Index/?consulId=8847&Modalidad=&Copia=");
      Console.WriteLine(x);
      Console.ReadLine();
    }

       

   

    private static async Task Caller() {       
      string start_url = "https://conamer.gob.mx/tramites/FichaPublicaV2/Index/?consulId=8847&Modalidad=&Copia=";
      await Procedure.WriteOrUpdateProcedure(start_url);

      /*string[] urls = { "https://conamer.gob.mx/tramites/FichaPublicaV2/Index/?consulId=8840&Modalidad=&Copia=",
 "https://conamer.gob.mx/tramites/FichaPublicaV2/Index/?consulId=4246&Modalidad=&Copia=",
"https://conamer.gob.mx/tramites/FichaPublicaV2/Index/?consulId=7245&Modalidad=&Copia="
      };
   
  foreach(string url in urls) {
    Console.WriteLine("iniciando");
        await Procedure.WriteOrUpdateProcedure(url);
        Console.WriteLine("terminamos con una ");
   }*/
      // string result = await Scrapper.Scrapper.Scrap("Conamer", start_url);

      //await Procedure.GetProcedure(start_url);


    }

    

  }
  }

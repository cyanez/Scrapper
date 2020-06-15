using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ConamerProject;

namespace Scrapper
{
    public class Scrapper
    {
    public static async Task<string> Scrap(string proyectName, string start_url) {
      switch (proyectName) {

        case "Conamer":
          return await ConamerProject.Conamer.Proccess(start_url);

        default:
          throw new Exception("No se econtro el Projecto" + proyectName);

      }

    }

  }
}

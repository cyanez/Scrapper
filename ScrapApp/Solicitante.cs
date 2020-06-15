using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapApp
{
  class Solicitante
  {
    public string Nombre { get; set; }
    
    public static List<Solicitante> GetSolicitantes(dynamic conamer){
      dynamic solicitantes = conamer["QuienPuedeRealizarTramite"].Children();
      List<Solicitante> solicitantesList = new List<Solicitante>();

      foreach (var item  in solicitantes) {

        Solicitante solicitante = new Solicitante();
        solicitante.Nombre = item.GetValue("Persona").ToString();
        solicitantesList.Add(solicitante);                      

      }

      return solicitantesList;

    }
    

      
  }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ScrapApp
{
  class ETL
  {
    private static dynamic output = new System.Dynamic.ExpandoObject();

    public static object Transform(string url) {
      string conamerRawJson = DataBaseOpeartions.GetProcedure(url)[3].ToString();
      dynamic conamer = JsonConvert.DeserializeObject(conamerRawJson);
      
      GetGeneralInfo(conamer);
      GetFundamentoJuridicoOrigenTramite(conamer);
      
      List<Requerimientos> requerimientosList = GetRequisitosList(conamer);

      output.Requerimientos = requerimientosList;

      output.FundamentosJuridicosOrigen = GetFundamentoJuridicoOrigenTramite(conamer);
            
      string outputJson = JsonConvert.SerializeObject(output, Formatting.Indented);
      return JsonConvert.DeserializeObject(outputJson);
      
    }
    private static void GetGeneralInfo(dynamic conamer) {    

      output.Dependencia = conamer.Dependencia;
      output.Unidad_Administrativa = conamer.UnidadAdministrativa;
      output.Homoclave = conamer.Homoclave;
      output.Nombre = conamer.Nombre;
      output.Nombre_Modalidad = conamer.NombreDeModalidad;
      output.CuandoRealizarTramite = conamer.CuandoRealizarTramite;
      output.Solicitantes = Solicitante.GetSolicitantes(conamer);
    }
    
    private static List<Requerimientos> GetRequisitosList(dynamic JSONProcedure) {
      dynamic requisitos = JSONProcedure["Requisitos"];

      Requerimientos requerimiento = new Requerimientos();
      List<Requerimientos> requerimientosList = new List<Requerimientos>();

      foreach (var item in requisitos.Children()) {
        string key = item.GetValue("Key").ToString();
        string value = item.GetValue("Value").ToString();

        switch (key) {

          case "Nombre": {
              requerimiento = new Requerimientos();
              requerimiento.Nombre = value;
            }
            break;
          case "Naturaleza": {
              requerimiento.Naturaleza = value;
            }
            break;
          case "Forma parte del formato": {
              requerimiento.Forma = value;
            }
            break;
          case "Descripción": {
              requerimiento.Descripcion = value;
              requerimientosList.Add(requerimiento);
            }
            break;
        }

      }
      return requerimientosList;

    }

    private static List<FundamentoJuridico> GetFundamentoJuridicoOrigenTramite(dynamic conamer) {
      
      var JsonFundamentos = conamer["Fundamentos"].Children();     
      List<FundamentoJuridico> fundamentos = new List<FundamentoJuridico>();
            
      foreach (var item in JsonFundamentos) {
        FundamentoJuridico fundamento = new FundamentoJuridico();

        fundamento.Fundamento = item.GetValue("Fundamento").ToString();
        fundamento.FechaPublicacion = item.GetValue("Publicacion").ToString();

        fundamentos.Add(fundamento);              

      }

      return fundamentos;
    }


  }
}

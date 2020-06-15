using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Data;

namespace ScrapApp
{
  class Procedure
  {

    private const int ValidityProcedureDays = 10;
    public static async Task WriteOrUpdateProcedure(string procedureURL) {
     DataBaseOpeartions.UpdateProcedure(procedureURL);
     
      string result = await Scrapper.Scrapper.Scrap("Conamer", procedureURL);
     
      DataBaseOpeartions.AddProcedure(procedureURL, result);
     
    }

    public static async Task UpdateAllProcedures() {
      List<string> proceduresUrl = DataBaseOpeartions.GetAllProceduresURLs();

      foreach (string procedureUrl in proceduresUrl) {
        await WriteOrUpdateProcedure(procedureUrl);
      }

    }

    public static bool IsCurrent(string procedureURL) {
      DataRow row = DataBaseOpeartions.GetProcedure(procedureURL);

      DateTime lastUpdateDate = Convert.ToDateTime(row[2].ToString());
      DateTime currentDate = DateTime.Now;

      TimeSpan differenceDate = currentDate - lastUpdateDate; 
      int differenceDays = differenceDate.Days;

      if(differenceDays >= ValidityProcedureDays) {
        return false;
      }

      return true;
    }

    public static async Task<string> GetProcedure(string procedureURL) {
      if (!IsCurrent(procedureURL)) {
        await WriteOrUpdateProcedure(procedureURL);
      }
      DataRow row = DataBaseOpeartions.GetProcedure(procedureURL);
      return row[3].ToString();

    }

  }
}

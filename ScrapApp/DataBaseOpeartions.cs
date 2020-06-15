using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

namespace ScrapApp
{
  class DataBaseOpeartions
  {
    private static int GetProcedureID() {

      string connectionString = "Server = DESKTOP-LGFTI19\\SQLEXPRESS; DataBase = Conamer; Integrated Security = true";
      SqlConnection connection = new SqlConnection(connectionString);
      string sql = "SELECT TOP 1 * FROM Procedures ORDER BY ProcedureID DESC ";

      connection.Open();
      SqlDataAdapter cmd = new SqlDataAdapter(sql, connection);
     
      DataTable table = new DataTable();
      cmd.Fill(table);
      connection.Close();

      int nextProcedureID = 0;

      if (table.Rows.Count < 1) {
        nextProcedureID = 1;
      } else {
        nextProcedureID = (int)table.Rows[0][0] + 1;
      }

      return nextProcedureID;
      
    }
    public static void AddProcedure(string procedureURL, string ProcedureJSONData) {

      string connectionString = "Server = DESKTOP-LGFTI19\\SQLEXPRESS; DataBase = Conamer; Integrated Security = true";
      SqlConnection connection = new SqlConnection(connectionString);
      string sql = "INSERT INTO Procedures(ProcedureID, ProcedureURL, LastUpdateDate, ProcedureJSONData, ProcedureStatus) " +
                  "VALUES(@ProcedureID, @ProcedureURL, @LastUpdateDate, @ProcedureJSONData, @ProcedureStatus)";

      connection.Open();
      SqlCommand cmd = new SqlCommand(sql, connection);

      cmd.Parameters.AddWithValue("@ProcedureID", GetProcedureID());
      cmd.Parameters.AddWithValue("@ProcedureURL", procedureURL);
      cmd.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);
      cmd.Parameters.AddWithValue("@ProcedureJSONData", ProcedureJSONData);
      cmd.Parameters.AddWithValue("@ProcedureStatus", 'A');

      cmd.ExecuteNonQuery();
      connection.Close();


    }

    public static void UpdateProcedure(string procedureURL) {

      string connectionString = "Server = DESKTOP-LGFTI19\\SQLEXPRESS; DataBase = Conamer; Integrated Security = true";
      SqlConnection connection = new SqlConnection(connectionString);
      string sql = "UPDATE Procedures SET ProcedureStatus = @ProcedureStatus WHERE ProcedureURL = @ProcedureURL";

      connection.Open();
      SqlCommand cmd = new SqlCommand(sql, connection);
      cmd.Parameters.AddWithValue("@ProcedureURL", procedureURL);
      cmd.Parameters.AddWithValue("@ProcedureStatus", 'X');

      cmd.ExecuteNonQuery();
      connection.Close();

    }

    public static DataRow GetProcedure(string procedureURL) {
      string connectionString = "Server = DESKTOP-LGFTI19\\SQLEXPRESS; DataBase = Conamer; Integrated Security = true";
      SqlConnection connection = new SqlConnection(connectionString);
      string sql = "SELECT * FROM PROCEDURES WHERE(ProcedureURL = @ProcedureURL) AND(ProcedureStatus = 'A' )";

      connection.Open();
      SqlDataAdapter cmd = new SqlDataAdapter(sql, connection);
      cmd.SelectCommand.Parameters.AddWithValue("@ProcedureURL", procedureURL);
      DataTable table = new DataTable();
      cmd.Fill(table);
      connection.Close();

      return table.Rows[0];
    }

    private static List<string> ConvertDataColumToList(DataTable dt) {

      List<string> list = new List<string>();
      foreach (DataRow row in dt.Rows) {
        list.Add(row[0].ToString());
      }     

      return list;
    }

    public static List<string> GetAllProceduresURLs() {
      string connectionString = "Server = DESKTOP-LGFTI19\\SQLEXPRESS; DataBase = Conamer; Integrated Security = true";
      SqlConnection connection = new SqlConnection(connectionString);
      string sql = "SELECT ProcedureURL FROM PROCEDURES WHERE(ProcedureStatus = 'A' )";

      connection.Open();
      SqlDataAdapter cmd = new SqlDataAdapter(sql, connection);
      DataTable table = new DataTable();
      cmd.Fill(table);
      connection.Close();

      List<string> list = ConvertDataColumToList(table);

      return list;
    }




  }
}

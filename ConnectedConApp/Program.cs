using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace ConnectedConApp
{
  class Program
  {
    static void Main(string[] args)
    {

    }

    static void UseReader()
    {
      SqlConnection northCon = null;
      SqlCommand categoryCMD = null;
      SqlCommand productCMD = null;
      try
      {
        northCon = new SqlConnection(@"Server=(LocalDB)\MSSQLLocalDB; Initial Catalog=Northwind; Integrated Security=true;");
        //northCon = new SqlConnection(@"Server=(LocalDB)\MSSQLLocalDB; Initial Catalog=Northwind; Integrated Security=true;MultipleActiveResultSets=true;");

        #region Category
        categoryCMD = new SqlCommand("Select * from Categories", northCon);
        northCon.Open();
        SqlDataReader reader = categoryCMD.ExecuteReader();
        while (reader.Read())
        {
          Console.WriteLine($"CategoryID: {reader["CategoryID"]}, CategoryName: {reader["CategoryName"]}, Description: {reader["Description"]}");
          //Console.WriteLine($"CategoryID: {reader.GetValue(0)}, CategoryName: {reader.GetString(1)}, Description: {reader.GetValue(2)}");
        }
        Console.WriteLine("Field Names");
        for (int i = 0; i < reader.FieldCount; i++)
        {
          Console.WriteLine(reader.GetName(i));
        }
        if (!reader.IsClosed)
        {
          reader.Close();
        }
        #endregion

        #region Product
        productCMD = new SqlCommand("Select * from Products", northCon);
        SqlDataReader prodReader = productCMD.ExecuteReader();
        while (prodReader.Read())
        {
          Console.WriteLine($"ProductID: {prodReader["ProductID"]}, ProductName: {prodReader["ProductName"]}, CategoryID: {prodReader["CategoryID"]}");
        }
        if (!prodReader.IsClosed)
        {
          prodReader.Close();
        }
        #endregion

      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: \n{ex.ToString()}");
      }
      finally
      {
        if (northCon.State == System.Data.ConnectionState.Open)
        {
          northCon.Close();
          Console.WriteLine("Connection is closed....");
        }
      }

    }
  }
}

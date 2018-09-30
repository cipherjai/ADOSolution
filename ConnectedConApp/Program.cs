using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
// library is needed to read from config file 
using System.Configuration;
using System.Data;

namespace ConnectedConApp
{
  class Program
  {
    static void Main(string[] args)
    {
      UseUpdateCustomer();
      //UseNonQuery();
      //UseScaler();      
    }

    private static void UseUpdateCustomer()
    {
      SqlConnection northCon = null;
      SqlCommand customerCMD = null;
      DataTable customersTable = null;

      try
      {
        //northCon = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthConnection"].ConnectionString);

        //SqlParameter[] sqlParameters = new SqlParameter[2];
        //sqlParameters[0] = new SqlParameter("@City", "Panvel");
        //sqlParameters[1] = new SqlParameter("@CustomerId", "JJJ");


        //customerCMD = new SqlCommand("Update Customers set City=@City where CustomerId=@CustomerId", northCon);
        //customerCMD.Parameters.AddRange(sqlParameters);
        //Console.WriteLine($"customerCMD.CommandText: {customerCMD.CommandText}");
        //Console.ReadKey(true);
        //northCon.Open();
        //int recEffected = customerCMD.ExecuteNonQuery();
        //if(recEffected == 1)
        //{
        //  Console.WriteLine("Customers record updated ...");


        northCon = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthConnection"].ConnectionString);
        customerCMD = new SqlCommand("Select * from Customers", northCon);

        customersTable.Load(customerCMD.ExecuteReader());

        foreach (DataRow item in customersTable.Rows)
        {
          Console.WriteLine($"CustomerId:  {item["CustomerID"]} -- {item["ContactName"]} -- {item["City"]}");

        }
      }
      catch (Exception ex)
      {

      }
      finally
      {
        if (northCon.State == System.Data.ConnectionState.Open)
        {
          northCon.Close();
        }
      }
      throw new NotImplementedException();
    }

    // using sql parameters


    private static void UseNonQuery()
    {
      SqlConnection northCon = null;
      SqlCommand customerCMD = null;
      string name = "Jai", city = "country", country = "India";
      try
      {
        northCon = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthConnection"].ConnectionString);
        string cmdStr = $"Insert into Customers(CustomerId, CompanyName, ContactName, City, Country) values('JJJ', 'FireFly.Inc', 'Jai', 'Mumbai')";
        customerCMD = new SqlCommand(cmdStr, northCon);
        Console.WriteLine($"customerCMD CommandText: {customerCMD.CommandText}");
        northCon.Open();
        int recEffected = customerCMD.ExecuteNonQuery();
        if(recEffected == 1)
        {
          Console.WriteLine("New Customer Created successfully");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
      finally
      {
        if (northCon.State == System.Data.ConnectionState.Open)
        {
          northCon.Close();
        }
      }
      
    }

    private static void UseScaler()
    {
      SqlConnection northCon = null;
      SqlCommand productCMD = null;
      try
      {
        northCon = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthConnection"].ConnectionString);
        productCMD = new SqlCommand("Select Max(UnitPrice) from Products", northCon);
        northCon.Open();
        decimal maxPrice = Convert.ToDecimal(productCMD.ExecuteScalar());
        Console.WriteLine($"Max UnitPrice from Product Table is: {maxPrice}");

      }catch(Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
      finally
      {
        if (northCon.State == System.Data.ConnectionState.Open)
        {
          northCon.Close();
        }
      }

      
    }

    static void UseReader()
    {
      SqlConnection northCon = null;
      SqlCommand categoryCMD = null;
      SqlCommand productCMD = null;
      try
      {
        //northCon = new SqlConnection(@"Server=(LocalDB)\MSSQLLocalDB; Initial Catalog=Northwind; Integrated Security=true;");
        //northCon = new SqlConnection(@"Server=(LocalDB)\MSSQLLocalDB; Initial Catalog=Northwind; Integrated Security=true;MultipleActiveResultSets=true;");
        northCon = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthConnection"].ConnectionString);
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

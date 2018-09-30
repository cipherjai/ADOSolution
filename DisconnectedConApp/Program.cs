//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DisconnectedConApp
//{
//  class Program
//  {

//    DataSet _northDS;
//    DataTable _productTable, _accountsTable, _customerTable, _categoriesTable;

//    DataRelation _catAndProdRelation;

//    SqlConnection _northCon;
//    SqlDataAdapter _customerAdapter, _productAdapter, _categoriesAdapter;
//    SqlCommandBuilder _customerDB;



//    public Program()
//    {
//      _northCon = new SqlConnection(GetConnectionString);
//      _northDS = new DataSet();
//    }

//    private void AddAccount()
//    {
//      DataTable accountTable = new DataTable("Accounts");
//      DataColumn[] dcs = new DataColumn[3];
//      dcs[0] = new DataColumn("AccountNumber", typeof(int));
//      dcs[1] = new DataColumn("HoldersName", typeof(string));
//      dcs[2] = new DataColumn("Balance", typeof(decimal));

//      accountTable.Columns.AddRange(dcs);

//      // creates an empty row first thing, then indexes

//      DataRow dr = accountTable.NewRow();
//      dr["AccountNumber"] = 100;
//      dr["HoldersName"] = "Jai";
//      dr["Balance"] = 123909000;

//      DataRow dr1 = accountTable.NewRow();
//      dr1["AccountNumber"] = 101;
//      dr1["HoldersName"] = "Jai Gupta";
//      dr1["Balance"] = 123909001;

//      DataRow dr2 = accountTable.NewRow();
//      dr2["AccountNumber"] = 102;
//      dr2["HoldersName"] = "Surya";
//      dr2["Balance"] = 123909002;

//      accountTable.Rows.Add(dr);
//      accountTable.Rows.Add(dr1);
//      accountTable.Rows.Add(dr2);

//      _northDS.Tables.Add(accountTable);
//      _accountsTable = _northDS.Tables["Accounts"];
//    }

//    // Adding as a value to the properties

//    public string GetConnectionString
//    {
//      get{ return ConfigurationManager.ConnectionStrings["NorthConnection"].ConnectionString; }
//    }

//    public void LoadNorth()
//    {
//      try
//      {



//        _customerAdapter = new SqlDataAdapter("Select * from Customers", _northCon);
//        _customerAdapter.Fill(_northDS);
//        _customerTable = _northDS.Tables["Customers"];



//        _productAdapter = new SqlDataAdapter("Select * from Products", _northCon);
//        _productAdapter.Fill(_northDS);
//        _productTable = _northDS.Tables["Products"];


//        _categoriesAdapter = new SqlDataAdapter("Select * from Categories", _northCon);
//        _categoriesAdapter.Fill(_northDS);
//        _categoriesTable = _northDS.Tables["Categories"];

//        _catAndProdRelation = new DataRelation("CatAndProdoRel", _categoriesTable.Columns["CategoryId"],
//        _productTable.Columns["CategoryId"]);
//        _northDS.Relations.Add(_catAndProdRelation);
//        CustNotNull();
//        SetPkForCustomers();
//      }
//      catch(Exception ex)
//      {
//        Console.WriteLine(ex.ToString());
//      }
//      finally
//      {

//      }
//    }

//// Customer id is an alphanumeric key

//    public void DisplayCustomers()
//    {
//      int ctr = 1;
//      //for (int i = 0; i < _northDS.Tables[0].Rows.Count; i++)
//      //{
//      //  Console.WriteLine($"{ctr++} -> {_northDS.Tables[0].Rows[i][0]} -- {_northDS.Tables[0].Rows[i][1]} ");
//      //}

//      foreach (DataRow item in _customerTable.Rows)
//      {
//        Console.WriteLine($"{ctr++} -> {item["CustomerID"]} -- {item["CompanyName"]}" );
//      }
//    }

//    public void DisplayProducts()
//    {
//      int ctr = 1;
//      foreach(DataRow item in _productTable.Rows)
//      {
//        Console.WriteLine($"{ctr++} -> {item["ProductName"]} -- {item["HoldersName"]}");
//      }
//    }

//    public void DisplayAccounts()
//    {
//      int ctr = 1;
//      foreach (DataRow item in _accountsTable.Rows)
//      {
//        Console.WriteLine($"{ctr++} -> {item["AccountNumber"]} -- {item["HoldersName"]} -- {item["Balance"]}");
//      }
//    }

//    public void DisplayCategories()
//    {
//      int ctr = 1;
//      foreach (DataRow item in _categoriesTable.Rows)
//      {
//        Console.WriteLine($"{ctr++} -> {item["CategoryId"]} -- {item["CategoryName"]} -- {item["CategoryDescription"]} ");
//      }
//    }


//    // Constraint for CompanyName
//    private void CustNotNull()
//    {
//      _customerTable.Columns["CompanyName"].AllowDBNull = false;
//    }

//    private void SetPkForCustomers()
//    {
//      DataColumn[] pkdc = new DataColumn[1];
//      pkdc[0] = _customerTable.Columns["CustomerID"];

//      _customerTable.PrimaryKey = pkdc;
//    }

//    public void AddCustomer()
//    {
//      CustNotNull();
//      DataRow dr = _customerTable.NewRow();

//      dr["CustomerID"] = "AABB";
//      //dr["CompanyName"] = "Firefly";
//      dr["CompanyName"] = null;
//      // Not Null Constraint
//      dr["ContactName"] = "Shyam";
//      dr["City"] = "Mumbai";
//      dr["Country"] = "India";

//      try
//      {
//        _customerTable.Rows.Add(dr);

//      }
//      catch (Exception ex)
//      {
//        Console.WriteLine(ex.ToString());
//      }
//    }

//    public int DSTableCount
//    {
//      get
//      {
//        return _northDS.Tables.Count;
//      }
//    }


//    // optimistic concurrency - disconnected architecture - they do it for us by default by performing the DML operations 
//    // 
//    static void Main(string[] args)
//    {
//      Program pp = new Program();
//      pp.LoadNorth();
//      Console.WriteLine("_____________Customers_____________");
//      pp.DisplayCustomers();
//      Console.WriteLine("_____________Products______________");
//      pp.DisplayProducts();
//      Console.WriteLine("_____________Accounts______________");
//      pp.AddAccount();
//      pp.DisplayAccounts();
//      Console.WriteLine("_____________Categories____________");
//      pp.DisplayCategories();
//      pp.AddCustomer();
//      // Todo: Add the Command 
//      pp.DisplayCustomers();
//      Console.WriteLine($"No of tables in the Dataset is : {pp.DSTableCount}");
//    }
//  }
//}

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;
namespace DisconnectedConApp
{
  class Program
  {
    DataSet _northDS;
    DataTable _customersTable;
    DataTable _productsTable, _accountsTable, _categoriesTable;
    DataRelation _catAndProdRelation;

    SqlConnection _northCon;
    SqlDataAdapter _customerAdapter, _productsAdapter, _categoriesAdapter;
    SqlCommandBuilder _customerDB;

    public Program()
    {
      _northCon = new SqlConnection(GetConnectionString);
      _northDS = new DataSet();
    }

    private string GetConnectionString
    {
      get
      {
        return ConfigurationManager.ConnectionStrings["northConnection"].ConnectionString;
      }
    }

    private void AddAccount()
    {
      DataTable accountTable = new DataTable("Accounts");
      DataColumn[] dcs = new DataColumn[3];
      dcs[0] = new DataColumn("AccountNumber", typeof(int));
      dcs[1] = new DataColumn("HoldersName", typeof(string));
      dcs[2] = new DataColumn("Balance", typeof(decimal));

      accountTable.Columns.AddRange(dcs);

      DataRow dr1 = accountTable.NewRow();
      dr1["AccountNumber"] = 101;
      dr1["HoldersName"] = "Donald";
      dr1["Balance"] = 900.90m;

      DataRow dr2 = accountTable.NewRow();
      dr2["AccountNumber"] = 102;
      dr2["HoldersName"] = "Shyam";
      dr2["Balance"] = 2000.90m;

      DataRow dr3 = accountTable.NewRow();
      dr3["AccountNumber"] = 103;
      dr3["HoldersName"] = "Deepak";
      dr3["Balance"] = 1050.90m;

      accountTable.Rows.Add(dr1);
      accountTable.Rows.Add(dr2);
      accountTable.Rows.Add(dr3);

      _northDS.Tables.Add(accountTable);
      _accountsTable = _northDS.Tables["Accounts"];
    }
    public void DisplayAccounts()
    {
      int ctr = 1;
      foreach (DataRow item in _accountsTable.Rows)
      {
        System.Console.WriteLine($"{ctr++}-> {item["AccountNumber"]}--{item["HoldersName"]}--{item["Balance"]}");
      }
      //for (int i = 0; i < _northDS.Tables[0].Rows.Count; i++)
      //{
      //    System.Console.WriteLine($"{ctr++}-> {_northDS.Tables[0].Rows[i][0]}--{_northDS.Tables[0].Rows[i][1]}");
      //}
    }

    public void LoadNorth()
    {
      try
      {
        _customerAdapter = new SqlDataAdapter("select * from Customers", _northCon);
        _customerAdapter.FillSchema(_northDS, SchemaType.Source, "Customers");
        _customerAdapter.Fill(_northDS, "Customers");
        _customersTable = _northDS.Tables["Customers"];
        //_customersTable = _northDS.Tables[0];
        _customerDB = new SqlCommandBuilder(_customerAdapter);

        _categoriesAdapter = new SqlDataAdapter("select * from Categories", _northCon);
        _categoriesAdapter.Fill(_northDS, "Categories");
        _categoriesTable = _northDS.Tables["Categories"];

        _productsAdapter = new SqlDataAdapter("select * from Products", _northCon);
        _productsAdapter.Fill(_northDS, "Products");
        _productsTable = _northDS.Tables["Products"];

        _catAndProdRelation = new DataRelation("CatAndProdRel", _categoriesTable.Columns["CategoryID"],
            _productsTable.Columns["CategoryID"]);
        _northDS.Relations.Add(_catAndProdRelation);

        CustNotNull();
        SetPKForCustomers();
      }
      catch (System.Exception ex)
      {

        System.Console.WriteLine(ex.ToString());
      }
    }
    public void DisplayCustomers()
    {
      int ctr = 1;
      foreach (DataRow item in _customersTable.Rows)
      {
        System.Console.WriteLine($"{ctr++}-> {item["CustomerID"]}--{item["CompanyName"]} --{item["ContactName"]}--{item["City"]}--{item["Country"]}");
      }
      //for (int i = 0; i < _northDS.Tables[0].Rows.Count; i++)
      //{
      //    System.Console.WriteLine($"{ctr++}-> {_northDS.Tables[0].Rows[i][0]}--{_northDS.Tables[0].Rows[i][1]}");
      //}
    }


    // hunt for a record
    public void EditCustomer(string customerId)
    {
      try
      {
        DataRow[] dr = _customersTable.Select($"CustomerID='{customerId}'");
        Console.WriteLine($"CustomerID: {dr[0]["CustomerID"]} and City: {dr[0]["City"]}");

        dr[0]["City"] = "Bombay-ABC";
      }
      catch
      {

      }
    }






    public void DisplayProducts()
    {
      int ctr = 1;
      foreach (DataRow item in _productsTable.Rows)
      {
        System.Console.WriteLine($"{ctr++}-> {item["ProductID"]}--{item["ProductName"]}");
      }
      //for (int i = 0; i < _northDS.Tables[0].Rows.Count; i++)
      //{
      //    System.Console.WriteLine($"{ctr++}-> {_northDS.Tables[0].Rows[i][0]}--{_northDS.Tables[0].Rows[i][1]}");
      //}
    }
    public void DisplayCategories()
    {
      int ctr = 1;
      foreach (DataRow item in _categoriesTable.Rows)
      {
        System.Console.WriteLine($"{ctr++}-> {item["CategoryID"]}--{item["CategoryName"]} --{item["Description"]}");
      }
      //for (int i = 0; i < _northDS.Tables[0].Rows.Count; i++)
      //{
      //    System.Console.WriteLine($"{ctr++}-> {_northDS.Tables[0].Rows[i][0]}--{_northDS.Tables[0].Rows[i][1]}");
      //}
    }
    public int DSTableCount
    {
      get
      {
        return _northDS.Tables.Count;
      }
    }

    private void CustNotNull()
    {
      _customersTable.Columns["CompanyName"].AllowDBNull = false;
    }
    private void SetPKForCustomers()
    {
      DataColumn[] pkdc = new DataColumn[1];
      pkdc[0] = _customersTable.Columns["CustomerID"];

      _customersTable.PrimaryKey = pkdc;
    }
    public void AddCustomer()
    {
      CustNotNull();
      SetPKForCustomers();
      DataRow dr = _customersTable.NewRow();
      dr["CustomerID"] = "AABB";
      dr["CompanyName"] = "Consultancy";
      dr["ContactName"] = "SM";
      dr["City"] = "Kalyan";
      dr["Country"] = "India";
      try
      {
        _customersTable.Rows.Add(dr);
      }
      catch (Exception ex)
      {

        Console.WriteLine(ex.ToString());
      }

    }
    public void SaveToDB()
    {
      try
      {
        Console.WriteLine($"Insert:\n {_customerDB.GetInsertCommand().CommandText}");
        Console.ReadKey(true);
        Console.WriteLine($"Update:\n {_customerDB.GetUpdateCommand().CommandText}");
        Console.ReadKey(true);
        Console.WriteLine($"Delete:\n {_customerDB.GetDeleteCommand().CommandText}");
        Console.ReadKey(true);
        //_customerAdapter.Update(_northDS, "Customers");

      }
      catch (DBConcurrencyException ex)
      {

        Console.WriteLine($"ex.Message()\n\n");
        DataSet tempDS = new DataSet();
        SqlDataAdapter tempAdapter = new SqlDataAdapter("Select * from Customers", _northCon);
        tempAdapter.FillSchema(tempDS, SchemaType.Source, "Customers");
        tempAdapter.Fill(tempDS, "Customers");

        _northDS.Merge(tempDS, true);
        _customerAdapter.Update(_northDS, "Customers");
        Console.WriteLine("Last in win's");
      }
      catch (Exception ex)
      {

        Console.WriteLine(ex.ToString());
      }
    }

    static void Main(string[] args)
    {
      Program pp = new Program();
      pp.LoadNorth();
      System.Console.WriteLine("-----------------Customers---------------");
      pp.DisplayCustomers();



      //Console.WriteLine("-------------------New Customer Added");
      //pp.AddCustomer();
      //pp.DisplayCustomers();

      Console.Clear();
      pp.SaveToDB();
      Console.WriteLine("Saving to DB");
      Console.ReadKey(true);


      pp.EditCustomer("AAA");
      Console.ReadKey(true);
      pp.SaveToDB();
      Console.WriteLine("Saving to DB");


      //pp.LoadNorth();
      //pp.DisplayCustomers();

      //System.Console.WriteLine("-----------------Categories---------------");
      //pp.DisplayCategories();
      //System.Console.WriteLine("-----------------Products---------------");
      //pp.DisplayProducts();

      //pp.AddAccount();
      //System.Console.WriteLine("-----------------Accounts---------------");
      //pp.DisplayAccounts();

      System.Console.WriteLine($"No of Tables in the dataset is {pp.DSTableCount}");


    }
  }
}

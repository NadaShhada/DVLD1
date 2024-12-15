using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GeneratSettings;


namespace DataAccessLayer
{
    public class  clsCountriesData
    {
static public DataTable GetAllCountries()
                   
     {  SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                                      string quere =  @" SELECT * FROM [dbo].[Countries]   ";
SqlCommand command = new SqlCommand(quere, connection);

          DataTable dt=new DataTable();

          try
          {
              connection.Open();
                   SqlDataReader reader=  command.ExecuteReader();
               if(reader.HasRows)
           {
                      dt.Load(reader);
            }
                             reader.Close();

          }
          catch (Exception ex) { }
          finally
          {
              connection.Close();
          }

          return dt;


      }    

static public bool FindCountries(ref int CountryID,string CountryName)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" SELECT * FROM [dbo].[Countries]  WHERE CountryID = @CountryID ;";

	SqlCommand command = new SqlCommand(quere, connection);


 command.Parameters.AddWithValue("@CountryID", CountryID);

	bool IsRead = false;

	try
	{
		connection.Open();
		SqlDataReader reader = command.ExecuteReader();
		if (reader.Read())
		{
			IsRead = true;
			CountryID =  Convert.ToInt32(reader["CountryID"]);
		CountryName =  Convert.ToString(reader["CountryName"]);
	
		}
		reader.Close();
	}
	catch (Exception ex) { }
	finally
	{
		connection.Close();
	}

	return IsRead;
}
static public int AddCountries(string CountryName)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" INSERT INTO [dbo].[Countries]    (
			CountryName)  
	VALUES (
			@CountryName) ;";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@CountryName", CountryName);


	int ID = 0;

	try
	{
		connection.Open();
		object Result = command.ExecuteScalar();
		if (Result != null && int.TryParse(Result.ToString(), out int IDNumber))
		{
			ID = IDNumber;
		}
	}
	catch (Exception ex) { }
	finally
	{
		connection.Close();
	}

	return ID;
}
static public bool UpdateCountries(int CountryID,string CountryName)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" UPDATE [dbo].[Countries]      
	 SET (
			@CountryName) ; 
 WHERE  CountryID=@CountryID";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@CountryName", CountryName);


	bool IsUpdate = false;

	try
	{
		connection.Open();
		int EffectedRow = command.ExecuteNonQuery();
		if (EffectedRow > 0)
		{
			IsUpdate = true;
		}
	}
	catch (Exception ex) { }
	finally
	{
		connection.Close();
	}

	return IsUpdate;
}
static public bool DeleteCountries(int CountryID,string CountryName)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" DELETE FROM [dbo].[Countries]      	 WHERE CountryID=@CountryID";

	SqlCommand command = new SqlCommand(quere, connection);


 command.Parameters.AddWithValue("@CountryID", CountryID);

	bool IsDelete = false;

	try
	{
		connection.Open();
		int EffectedRow = command.ExecuteNonQuery();
		if (EffectedRow > 0)
		{
			IsDelete = true;
		}
	}
	catch (Exception ex) { }
	finally
	{
		connection.Close();
	}

	return IsDelete;
}

    }
}


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
    public class  clsApplicationTypesData
    {
static public DataTable GetAllApplicationTypes()
                   
     {  SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                                      string quere =  @" SELECT * FROM [dbo].[ApplicationTypes]   ";
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

static public bool FindApplicationTypes(ref int ApplicationTypeID,string ApplicationTypeTitle,float ApplicationFees)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" SELECT * FROM [dbo].[ApplicationTypes]  WHERE ApplicationTypeID = @ApplicationTypeID ;";

	SqlCommand command = new SqlCommand(quere, connection);


 command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

	bool IsRead = false;

	try
	{
		connection.Open();
		SqlDataReader reader = command.ExecuteReader();
		if (reader.Read())
		{
			IsRead = true;
			ApplicationTypeID =  Convert.ToInt32(reader["ApplicationTypeID"]);
		ApplicationTypeTitle =  Convert.ToString(reader["ApplicationTypeTitle"]);
		ApplicationFees =  Convert.ToSingle(reader["ApplicationFees"]);
	
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
static public int AddApplicationTypes(string ApplicationTypeTitle,float ApplicationFees)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" INSERT INTO [dbo].[ApplicationTypes]    (
			ApplicationTypeTitle,
			ApplicationFees)  
	VALUES (
			@ApplicationTypeTitle,
			@ApplicationFees) ;";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
 command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);


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
static public bool UpdateApplicationTypes(int ApplicationTypeID,string ApplicationTypeTitle,float ApplicationFees)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" UPDATE [dbo].[ApplicationTypes]      
	 SET (
			@ApplicationTypeTitle,
			@ApplicationFees) ; 
 WHERE  ApplicationTypeID=@ApplicationTypeID";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
 command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);


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
static public bool DeleteApplicationTypes(int ApplicationTypeID,string ApplicationTypeTitle,float ApplicationFees)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" DELETE FROM [dbo].[ApplicationTypes]      	 WHERE ApplicationTypeID=@ApplicationTypeID";

	SqlCommand command = new SqlCommand(quere, connection);


 command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

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


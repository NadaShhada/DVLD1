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
    public class  clssysdiagramsData
    {
static public DataTable GetAllsysdiagrams()
                   
     {  SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                                      string quere =  @" SELECT * FROM [dbo].[sysdiagrams]   ";
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

static public bool Findsysdiagrams(ref string name,int principal_id,int diagram_id,int version,unknown definition)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" SELECT * FROM [dbo].[sysdiagrams]  WHERE name = @name ;";

	SqlCommand command = new SqlCommand(quere, connection);


 command.Parameters.AddWithValue("@name", name);

	bool IsRead = false;

	try
	{
		connection.Open();
		SqlDataReader reader = command.ExecuteReader();
		if (reader.Read())
		{
			IsRead = true;
			name =  Convert.ToString(reader["name"]);
		principal_id =  Convert.ToInt32(reader["principal_id"]);
		diagram_id =  Convert.ToInt32(reader["diagram_id"]);
		      if (reader["version"] != DBNull.Value)
                         {
                             version = (Convert.ToInt32(reader["version"]));
                         } version =  Convert.ToInt32(reader["version"]);
		      if (reader["definition"] != DBNull.Value)
                         {
                             definition = (koko2reader["definition"]));
                         } definition =  koko2reader["definition"]);
	
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
static public int Addsysdiagrams(int principal_id,int diagram_id,int version,unknown definition)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" INSERT INTO [dbo].[sysdiagrams]    (
			principal_id,
			diagram_id,
			version,
			definition)  
	VALUES (
			@principal_id,
			@diagram_id,
			@version,
			@definition) ;";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@principal_id", principal_id);
 command.Parameters.AddWithValue("@diagram_id", diagram_id);
 if ( (version !=0))
                                                                                              command.Parameters.AddWithValue("@version", version);
                                                                                          else
                                                                                              command.Parameters.AddWithValue("@version", System.DBNull.Value);
 command.Parameters.AddWithValue("@version", version);
 if ( (definition !=0))
                                                                                              command.Parameters.AddWithValue("@definition", definition);
                                                                                          else
                                                                                              command.Parameters.AddWithValue("@definition", System.DBNull.Value);
 command.Parameters.AddWithValue("@definition", definition);


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
static public bool Updatesysdiagrams(string name,int principal_id,int diagram_id,int version,unknown definition)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" UPDATE [dbo].[sysdiagrams]      
	 SET (
			@principal_id,
			@diagram_id,
			@version,
			@definition) ; 
 WHERE  name=@name";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@principal_id", principal_id);
 command.Parameters.AddWithValue("@diagram_id", diagram_id);
 if ( (version !=0))
                                                                                              command.Parameters.AddWithValue("@version", version);
                                                                                          else
                                                                                              command.Parameters.AddWithValue("@version", System.DBNull.Value);
 command.Parameters.AddWithValue("@version", version);
 if ( (definition !=0))
                                                                                              command.Parameters.AddWithValue("@definition", definition);
                                                                                          else
                                                                                              command.Parameters.AddWithValue("@definition", System.DBNull.Value);
 command.Parameters.AddWithValue("@definition", definition);


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
static public bool Deletesysdiagrams(string name,int principal_id,int diagram_id,int version,unknown definition)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" DELETE FROM [dbo].[sysdiagrams]      	 WHERE name=@name";

	SqlCommand command = new SqlCommand(quere, connection);


 command.Parameters.AddWithValue("@name", name);

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


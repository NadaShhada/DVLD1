using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;


namespace DataAccessLayer
{
    public class  clsTestTypesData
    {
        static public bool GetTestTypeInfoByTestID(int TestTypeID,ref string TestTypeTitle,ref string TestTypeDescription,ref float TestTypeFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string quere = @" SELECT * FROM [dbo].[TestTypes]  WHERE TestTypeID = @TestTypeID ;";

            SqlCommand command = new SqlCommand(quere, connection);


            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsRead = true;
                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    TestTypeTitle = Convert.ToString(reader["TestTypeTitle"]);
                    TestTypeDescription = Convert.ToString(reader["TestTypeDescription"]);
                    TestTypeFees = Convert.ToSingle(reader["TestTypeFees"]);

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
        static public DataTable GetAllTestTypes()
                   
     {  SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                                      string quere =  @" SELECT * FROM [dbo].[TestTypes]   ";
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

static public bool FindTestTypes( int TestTypeID,string TestTypeTitle,string TestTypeDescription,float TestTypeFees)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" SELECT * FROM [dbo].[TestTypes]  WHERE TestTypeID = @TestTypeID ;";

	SqlCommand command = new SqlCommand(quere, connection);


 command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

	bool IsRead = false;

	try
	{
		connection.Open();
		SqlDataReader reader = command.ExecuteReader();
		if (reader.Read())
		{
			IsRead = true;
			TestTypeID =  Convert.ToInt32(reader["TestTypeID"]);
		TestTypeTitle =  Convert.ToString(reader["TestTypeTitle"]);
		TestTypeDescription =  Convert.ToString(reader["TestTypeDescription"]);
		TestTypeFees =  Convert.ToSingle(reader["TestTypeFees"]);
	
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
static public int AddTestTypes(string TestTypeTitle,string TestTypeDescription,float TestTypeFees)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" INSERT INTO [dbo].[TestTypes]    (
			TestTypeTitle,
			TestTypeDescription,
			TestTypeFees)  
	VALUES (
			@TestTypeTitle,
			@TestTypeDescription,
			@TestTypeFees) ;";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
 command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
 command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);


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
static public bool UpdateTestTypes(int TestTypeID, string TestTypeTitle,  string TestTypeDescription,  float TestTypeFees)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" UPDATE [dbo].[TestTypes]      
	 SET (
			@TestTypeTitle,
			@TestTypeDescription,
			@TestTypeFees) ; 
 WHERE  TestTypeID=@TestTypeID";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
 command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
 command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);


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
static public bool DeleteTestTypes(int TestTypeID)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" DELETE FROM [dbo].[TestTypes]      	 WHERE TestTypeID=@TestTypeID";

	SqlCommand command = new SqlCommand(quere, connection);


 command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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


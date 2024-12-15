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
    public class  clsDriversData
    {
static public DataTable GetAllDrivers()
                   
     {  SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                                      string quere = @" SELECT * FROM  Drivers_View order by FullName  ";
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

static public bool FindDriverByDriverID( int DriverID,ref int PersonID,ref int CreatedByUserID,ref DateTime CreatedDate)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" SELECT * FROM [dbo].[Drivers]  WHERE DriverID = @DriverID ;";

	SqlCommand command = new SqlCommand(quere, connection);


 command.Parameters.AddWithValue("@DriverID", DriverID);

	bool IsRead = false;

	try
	{
		connection.Open();
		SqlDataReader reader = command.ExecuteReader();
		if (reader.Read())
		{
			IsRead = true;
			DriverID =  Convert.ToInt32(reader["DriverID"]);
		PersonID =  Convert.ToInt32(reader["PersonID"]);
		CreatedByUserID =  Convert.ToInt32(reader["CreatedByUserID"]);
		CreatedDate =  Convert.ToDateTime(reader["CreatedDate"]);
	
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

        static public bool FindDriverByPersonID(ref int DriverID,  int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string quere = @" SELECT * FROM [dbo].[Drivers]  WHERE PersonID = @PersonID ;";

            SqlCommand command = new SqlCommand(quere, connection);


            command.Parameters.AddWithValue("@PersonID", PersonID);

            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsRead = true;
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);

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

        static public int AddDrivers(int PersonID,int CreatedByUserID)
{ int DriverID  =-1;
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" INSERT INTO [dbo].[Drivers]    (
			PersonID,
			CreatedByUserID,
			CreatedDate)  
	VALUES (
			@PersonID,
			@CreatedByUserID,
			@CreatedDate) ;
            SELECT SCOPE_IDENTITY(); ";

    SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@PersonID", PersonID);
 command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
 command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);



	try
	{
		connection.Open();
		object Result = command.ExecuteScalar();
		if (Result != null && int.TryParse(Result.ToString(), out int IDNumber))
		{
			DriverID = IDNumber;
		}
	}
	catch (Exception ex) { }
	finally
	{
		connection.Close();
	}

	return DriverID;
}
static public bool UpdateDrivers(int DriverID,int PersonID,int CreatedByUserID,DateTime CreatedDate)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" UPDATE [dbo].[Drivers]      
	 SET (
			@PersonID,
			@CreatedByUserID,
			@CreatedDate) ; 
 WHERE  DriverID=@DriverID";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@PersonID", PersonID);
 command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
 command.Parameters.AddWithValue("@CreatedDate", CreatedDate);


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

    }
}


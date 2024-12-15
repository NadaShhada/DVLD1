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
    public class  clsLicensesData
    {
static public DataTable GetAllLicenses()
                   
     {  SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                                      string quere =  @" SELECT * FROM [dbo].[Licenses]   ";
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

		public static DataTable GetDriverLicenses(int DriverID)
		{
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT     
                           Licenses.LicenseID,
                           ApplicationID,
		                   LicenseClasses.ClassName, Licenses.IssueDate, 
		                   Licenses.ExpirationDate, Licenses.IsActive
                           FROM Licenses INNER JOIN
                                LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                            where DriverID=@DriverID
                            Order By IsActive Desc, ExpirationDate Desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }


        static public bool FindLicenses( int LicenseID,ref int ApplicationID, ref int DriverID,ref int LicenseClass, ref DateTime IssueDate,ref DateTime ExpirationDate,ref string Notes,ref float PaidFees	,ref bool IsActive,ref short IssueReason,ref int CreatedByUserID)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" SELECT * FROM [dbo].[Licenses]  WHERE LicenseID = @LicenseID ;";

	SqlCommand command = new SqlCommand(quere, connection);


 command.Parameters.AddWithValue("@LicenseID", LicenseID);

	bool IsRead = false;

	try
	{
		connection.Open();
		SqlDataReader reader = command.ExecuteReader();
		if (reader.Read())
		{
			IsRead = true;
			LicenseID =  Convert.ToInt32(reader["LicenseID"]);
		ApplicationID =  Convert.ToInt32(reader["ApplicationID"]);
		DriverID =  Convert.ToInt32(reader["DriverID"]);
		LicenseClass =  Convert.ToInt32(reader["LicenseClass"]);
		IssueDate =  Convert.ToDateTime(reader["IssueDate"]);
		ExpirationDate =  Convert.ToDateTime(reader["ExpirationDate"]);
		      if (reader["Notes"] != DBNull.Value)
                         {
                             Notes = (Convert.ToString(reader["Notes"]));
                         } Notes =  Convert.ToString(reader["Notes"]);
		PaidFees =  Convert.ToSingle(reader["PaidFees"]);
		IsActive =  Convert.ToBoolean(reader["IsActive"]);
		IssueReason =  Convert.ToInt16(reader["IssueReason"]);
		CreatedByUserID =  Convert.ToInt32(reader["CreatedByUserID"]);
	
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
static public int AddLicenses(int ApplicationID,int DriverID,int LicenseClass,DateTime IssueDate,DateTime ExpirationDate,string Notes,float PaidFees,bool IsActive,short IssueReason,int CreatedByUserID)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" INSERT INTO [dbo].[Licenses]    (
			ApplicationID,
			DriverID,
			LicenseClass,
			IssueDate,
			ExpirationDate,
			Notes,
			PaidFees,
			IsActive,
			IssueReason,
			CreatedByUserID)  
	VALUES (
			@ApplicationID,
			@DriverID,
			@LicenseClass,
			@IssueDate,
			@ExpirationDate,
			@Notes,
			@PaidFees,
			@IsActive,
			@IssueReason,
			@CreatedByUserID) ;";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
 command.Parameters.AddWithValue("@DriverID", DriverID);
 command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
 command.Parameters.AddWithValue("@IssueDate", IssueDate);
 command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
 if (!string.IsNullOrEmpty(Notes ))
                                                                                              command.Parameters.AddWithValue("@Notes", Notes);
                                                                                          else
                                                                                              command.Parameters.AddWithValue("@Notes", System.DBNull.Value);
 command.Parameters.AddWithValue("@Notes", Notes);
 command.Parameters.AddWithValue("@PaidFees", PaidFees);
 command.Parameters.AddWithValue("@IsActive", IsActive);
 command.Parameters.AddWithValue("@IssueReason", IssueReason);
 command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


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
static public bool UpdateLicenses(int LicenseID, int ApplicationID, int DriverID,  int LicenseClass,  DateTime IssueDate, DateTime ExpirationDate,  string Notes, float PaidFees, bool IsActive, short IssueReason,  int CreatedByUserID)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" UPDATE [dbo].[Licenses]      
	 SET (
			@ApplicationID,
			@DriverID,
			@LicenseClass,
			@IssueDate,
			@ExpirationDate,
			@Notes,
			@PaidFees,
			@IsActive,
			@IssueReason,
			@CreatedByUserID) ; 
 WHERE  LicenseID=@LicenseID";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
 command.Parameters.AddWithValue("@DriverID", DriverID);
 command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
 command.Parameters.AddWithValue("@IssueDate", IssueDate);
 command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
 if (!string.IsNullOrEmpty(Notes ))
                                                                                              command.Parameters.AddWithValue("@Notes", Notes);
                                                                                          else
                                                                                              command.Parameters.AddWithValue("@Notes", System.DBNull.Value);
 command.Parameters.AddWithValue("@Notes", Notes);
 command.Parameters.AddWithValue("@PaidFees", PaidFees);
 command.Parameters.AddWithValue("@IsActive", IsActive);
 command.Parameters.AddWithValue("@IssueReason", IssueReason);
 command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


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

		public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
		{

			int LicenseID = -1;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

			string query = @"SELECT        Licenses.LicenseID
                            FROM Licenses INNER JOIN
                                                     Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             
                             Licenses.LicenseClass = @LicenseClass 
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@PersonID", PersonID);
			command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

			try
			{
				connection.Open();

				object result = command.ExecuteScalar();

				if (result != null && int.TryParse(result.ToString(), out int insertedID))
				{
					LicenseID = insertedID;
				}
			}

			catch (Exception ex)
			{
				//Console.WriteLine("Error: " + ex.Message);

			}

			finally
			{
				connection.Close();
			}


			return LicenseID;
		}

        public static bool DeactivateLicense(int LicenseID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Licenses
                           SET 
                              IsActive = 0
                             
                         WHERE LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

    }
}


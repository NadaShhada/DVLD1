using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;


namespace DataAccessLayer
{
    public class clsApplicationsData
    {
        static public DataTable GetAllApplications()

        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string quere = @" SELECT * FROM [dbo].[Applications]   ";
            SqlCommand command = new SqlCommand(quere, connection);

            DataTable dt = new DataTable();

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
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return dt;


        }
        static public bool GetApplcationInfoByID(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID, ref short ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string quere = @" SELECT * FROM [dbo].[Applications]  WHERE ApplicationID = @ApplicationID ;";

            SqlCommand command = new SqlCommand(quere, connection);


            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsRead = true;
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    ApplicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    ApplicationStatus = Convert.ToInt16(reader["ApplicationStatus"]);
                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

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

        static public bool FindApplications(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string quere = @" SELECT * FROM [dbo].[Applications]  WHERE ApplicationID = @ApplicationID ;";

            SqlCommand command = new SqlCommand(quere, connection);


            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            bool IsRead = false;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsRead = true;
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    ApplicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    ApplicationStatus = (byte)(reader["ApplicationStatus"]);
                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

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
        static public int AddApplications(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, short ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string quere = @" INSERT INTO [dbo].[Applications]    (
			ApplicantPersonID,
			ApplicationDate,
			ApplicationTypeID,
			ApplicationStatus,
			LastStatusDate,
			PaidFees,
			CreatedByUserID)  
	VALUES (
			@ApplicantPersonID,
			@ApplicationDate,
			@ApplicationTypeID,
			@ApplicationStatus,
			@LastStatusDate,
			@PaidFees,
			@CreatedByUserID) ;";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
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
        static public bool UpdateApplications(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, short ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string quere = @" UPDATE [dbo].[Applications]      
	 SET (
			@ApplicantPersonID,
			@ApplicationDate,
			@ApplicationTypeID,
			@ApplicationStatus,
			@LastStatusDate,
			@PaidFees,
			@CreatedByUserID) ; 
 WHERE  ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(quere, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
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
        static public bool DeleteApplications(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string quere = @" DELETE FROM [dbo].[Applications]      	 WHERE ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(quere, connection);


            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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
        static public bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found=1 FROM [dbo].[Applications]  WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;








        }
        static public int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT ActiveApplicationID=ApplicationID FROM Applications WHERE ApplicantPersonID = @ApplicantPersonID and ApplicationTypeID=@ApplicationTypeID and ApplicationStatus=1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public static int GetActiveApplicationIDForLiceenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }
        public static bool UpdateStatus(int ApplicationID, short NewStatus)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string quere = @" UPDATE [dbo].[Applications]      
	 SET (
			@ApplicantPersonID,
		
			@ApplicationStatus=@NewStatus,
			@LastStatusDate=@LastStatusDate,
			) ; 
 WHERE  ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(quere, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);


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




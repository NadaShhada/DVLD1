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
    public class  clsPeopleData
    {
static public DataTable GetAllPeople()
                   
     {  SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                                      string quere =  @" SELECT * FROM [dbo].[People]   ";
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

static public bool FindPeople(ref int PersonID,string NationalNo,string FirstName,string SecondName,string ThirdName,string LastName,DateTime DateOfBirth,short Gendor,string Address,string Phone,string Email,int NationalityCountryID,string ImagePath)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" SELECT * FROM [dbo].[People]  WHERE PersonID = @PersonID ;";

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
			PersonID =  Convert.ToInt32(reader["PersonID"]);
		NationalNo =  Convert.ToString(reader["NationalNo"]);
		FirstName =  Convert.ToString(reader["FirstName"]);
		SecondName =  Convert.ToString(reader["SecondName"]);
		      if (reader["ThirdName"] != DBNull.Value)
                         {
                             ThirdName = (Convert.ToString(reader["ThirdName"]));
                         } ThirdName =  Convert.ToString(reader["ThirdName"]);
		LastName =  Convert.ToString(reader["LastName"]);
		DateOfBirth =  Convert.ToDateTime(reader["DateOfBirth"]);
		Gendor =  Convert.ToInt16(reader["Gendor"]);
		Address =  Convert.ToString(reader["Address"]);
		Phone =  Convert.ToString(reader["Phone"]);
		      if (reader["Email"] != DBNull.Value)
                         {
                             Email = (Convert.ToString(reader["Email"]));
                         } Email =  Convert.ToString(reader["Email"]);
		NationalityCountryID =  Convert.ToInt32(reader["NationalityCountryID"]);
		      if (reader["ImagePath"] != DBNull.Value)
                         {
                             ImagePath = (Convert.ToString(reader["ImagePath"]));
                         } ImagePath =  Convert.ToString(reader["ImagePath"]);
	
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
static public int AddPeople(string NationalNo,string FirstName,string SecondName,string ThirdName,string LastName,DateTime DateOfBirth,short Gendor,string Address,string Phone,string Email,int NationalityCountryID,string ImagePath)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" INSERT INTO [dbo].[People]    (
			NationalNo,
			FirstName,
			SecondName,
			ThirdName,
			LastName,
			DateOfBirth,
			Gendor,
			Address,
			Phone,
			Email,
			NationalityCountryID,
			ImagePath)  
	VALUES (
			@NationalNo,
			@FirstName,
			@SecondName,
			@ThirdName,
			@LastName,
			@DateOfBirth,
			@Gendor,
			@Address,
			@Phone,
			@Email,
			@NationalityCountryID,
			@ImagePath) ;";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@NationalNo", NationalNo);
 command.Parameters.AddWithValue("@FirstName", FirstName);
 command.Parameters.AddWithValue("@SecondName", SecondName);
 if (!string.IsNullOrEmpty(ThirdName ))
                                                                                              command.Parameters.AddWithValue("@ThirdName", ThirdName);
                                                                                          else
                                                                                              command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
 command.Parameters.AddWithValue("@ThirdName", ThirdName);
 command.Parameters.AddWithValue("@LastName", LastName);
 command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
 command.Parameters.AddWithValue("@Gendor", Gendor);
 command.Parameters.AddWithValue("@Address", Address);
 command.Parameters.AddWithValue("@Phone", Phone);
 if (!string.IsNullOrEmpty(Email ))
                                                                                              command.Parameters.AddWithValue("@Email", Email);
                                                                                          else
                                                                                              command.Parameters.AddWithValue("@Email", System.DBNull.Value);
 command.Parameters.AddWithValue("@Email", Email);
 command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
 if (!string.IsNullOrEmpty(ImagePath ))
                                                                                              command.Parameters.AddWithValue("@ImagePath", ImagePath);
                                                                                          else
                                                                                              command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
 command.Parameters.AddWithValue("@ImagePath", ImagePath);


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
static public bool UpdatePeople(int PersonID,string NationalNo,string FirstName,string SecondName,string ThirdName,string LastName,DateTime DateOfBirth,short Gendor,string Address,string Phone,string Email,int NationalityCountryID,string ImagePath)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" UPDATE [dbo].[People]      
	 SET (
			@NationalNo,
			@FirstName,
			@SecondName,
			@ThirdName,
			@LastName,
			@DateOfBirth,
			@Gendor,
			@Address,
			@Phone,
			@Email,
			@NationalityCountryID,
			@ImagePath) ; 
 WHERE  PersonID=@PersonID";

	SqlCommand command = new SqlCommand(quere, connection);

 command.Parameters.AddWithValue("@NationalNo", NationalNo);
 command.Parameters.AddWithValue("@FirstName", FirstName);
 command.Parameters.AddWithValue("@SecondName", SecondName);
 if (!string.IsNullOrEmpty(ThirdName ))
                                                                                              command.Parameters.AddWithValue("@ThirdName", ThirdName);
                                                                                          else
                                                                                              command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
 command.Parameters.AddWithValue("@ThirdName", ThirdName);
 command.Parameters.AddWithValue("@LastName", LastName);
 command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
 command.Parameters.AddWithValue("@Gendor", Gendor);
 command.Parameters.AddWithValue("@Address", Address);
 command.Parameters.AddWithValue("@Phone", Phone);
 if (!string.IsNullOrEmpty(Email ))
                                                                                              command.Parameters.AddWithValue("@Email", Email);
                                                                                          else
                                                                                              command.Parameters.AddWithValue("@Email", System.DBNull.Value);
 command.Parameters.AddWithValue("@Email", Email);
 command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
 if (!string.IsNullOrEmpty(ImagePath ))
                                                                                              command.Parameters.AddWithValue("@ImagePath", ImagePath);
                                                                                          else
                                                                                              command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
 command.Parameters.AddWithValue("@ImagePath", ImagePath);


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
static public bool DeletePeople(int PersonID,string NationalNo,string FirstName,string SecondName,string ThirdName,string LastName,DateTime DateOfBirth,short Gendor,string Address,string Phone,string Email,int NationalityCountryID,string ImagePath)
{
	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

	string quere = @" DELETE FROM [dbo].[People]      	 WHERE PersonID=@PersonID";

	SqlCommand command = new SqlCommand(quere, connection);


 command.Parameters.AddWithValue("@PersonID", PersonID);

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


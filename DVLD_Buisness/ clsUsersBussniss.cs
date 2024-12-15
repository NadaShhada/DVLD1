using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsUsers
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public   int  UserID { set; get; } 
        public   int  PersonID { set; get; } 
        public   string  UserName { set; get; } 
        public   string  Password { set; get; } 
        public   bool  IsActive { set; get; } 


           clsUsers(){        this.  UserID =-1 ;
        this.  PersonID =-1 ;
        this.  UserName ="" ;
        this.  Password ="" ;
        this.  IsActive =false ;
         Mode = enMode.AddNew;
}

           clsUsers(int UserID,int PersonID,string UserName,string Password,bool IsActive){        this. UserID=UserID;
        this. PersonID=PersonID;
        this. UserName=UserName;
        this. Password=Password;
        this. IsActive=IsActive;
         Mode = enMode.Update;
}

        private bool _AddUsers()
        {
 
            this.UserID = clsUsersData.AddUsers( PersonID, UserName, Password, IsActive);
              

            return (this.UserID != -1);
        }

        static public DataTable GetAllUsers()
        {
                return clsUsersData.GetAllUsers();
         }

        private bool _UpdateUsers()
        {
 
            bool IsSuccess= clsUsersData.UpdateUsers( PersonID, UserName, Password, IsActive);
              

            return IsSuccess;
        }

         public clsUsers FindUsers(int UserID)
           {
                 int PersonID= -1 ; string UserName="" ; string Password="" ; bool IsActive=false; 

               if(clsUsersData.FindUsers( ref UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
               {
                   return new clsUsers( UserID, PersonID, UserName, Password, IsActive);
               }
             return null;
    }

           static bool DeleteUsers(int UserID)
        {
              return clsUsersData.DeleteUsers(UserID);
        }

        public bool Save()
        {
          
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddUsers())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:                    return _UpdateUsers();

            }

            return false;
         

        }
    } 
    }


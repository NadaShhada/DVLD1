using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DVLD_Buisness;
namespace Bussiness_Layer
{
    public class clsDrivers
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public   int  DriverID { set; get; } 
        public   int  PersonID { set; get; }
        public clsPerson PersonInfo;
        public   int  CreatedByUserID { set; get; } 
        public   DateTime  CreatedDate { set; get; } 


      public     clsDrivers(){        this.  DriverID =-1 ;
        this.  PersonID =-1 ;
        this.  CreatedByUserID =-1 ;
        this.  CreatedDate =DateTime.Now ;
         Mode = enMode.AddNew;
}

           clsDrivers(int DriverID,int PersonID,int CreatedByUserID,DateTime CreatedDate){        this. DriverID=DriverID;
        this. PersonID=PersonID;
        this. CreatedByUserID=CreatedByUserID;
        this. CreatedDate=CreatedDate;
         Mode = enMode.Update;
}

        private bool _AddDrivers()
        {
 
            this.DriverID = clsDriversData.AddDrivers( PersonID, CreatedByUserID);
              

            return (this.DriverID != -1);
        }

        static public DataTable GetAllDrivers()
        {
                return clsDriversData.GetAllDrivers();
         }

        private bool _UpdateDrivers()
        {
 
            bool IsSuccess= clsDriversData.UpdateDrivers(this.DriverID,this .PersonID, this.CreatedByUserID, this.CreatedDate);
              

            return IsSuccess;
        }

         public static clsDrivers FindDriverByDriverID(int DriverID)
           {
                 int PersonID= -1 ; int CreatedByUserID= -1 ; DateTime CreatedDate=DateTime.Now; 

               if(clsDriversData.FindDriverByDriverID(  DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
               {
                   return new clsDrivers( DriverID, PersonID, CreatedByUserID, CreatedDate);
               }
             return null;
    }
        public static clsDrivers FindDriverByPersonID(int PersonID)
        {
            int DriverID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (clsDriversData.FindDriverByPersonID(ref DriverID,  PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            return null;
        }

        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicenses.GetDriverLicenses(DriverID);
        }
        public bool Save()
        {
          
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddDrivers())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDrivers();

            }

            return false;
         

        }
    } 
    }


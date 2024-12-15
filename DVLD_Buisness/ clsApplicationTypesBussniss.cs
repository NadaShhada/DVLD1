using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsApplicationTypes
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public   int  ApplicationTypeID { set; get; } 
        public   string  ApplicationTypeTitle { set; get; } 
        public   float  ApplicationFees { set; get; } 


           clsApplicationTypes(){        this.  ApplicationTypeID =-1 ;
        this.  ApplicationTypeTitle ="" ;
        this.  ApplicationFees =-1 ;
         Mode = enMode.AddNew;
}

           clsApplicationTypes(int ApplicationTypeID,string ApplicationTypeTitle,float ApplicationFees){        this. ApplicationTypeID=ApplicationTypeID;
        this. ApplicationTypeTitle=ApplicationTypeTitle;
        this. ApplicationFees=ApplicationFees;
         Mode = enMode.Update;
}

        private bool _AddApplicationTypes()
        {
 
            this.ApplicationTypeID = clsApplicationTypesData.AddApplicationTypes(  ApplicationTypeTitle, ApplicationFees);
              

            return (this.ApplicationTypeID != -1);
        }

        static public DataTable GetAllApplicationTypes()
        {
                return clsApplicationTypesData.GetAllApplicationTypes();
         }

        private bool _UpdateApplicationTypes()
        {
 
            bool IsSuccess= clsApplicationTypesData.UpdateApplicationTypes( ApplicationTypeTitle, ApplicationFees);
              

            return IsSuccess;
        }

         public clsApplicationTypes FindApplicationTypes(int ApplicationTypeID)
           {
                 string ApplicationTypeTitle="" ; float ApplicationFees= -1 ; 

               if(clsApplicationTypesData.FindApplicationTypes( ref ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
               {
                   return new clsApplicationTypes( ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
               }
             return null;
    }

           static bool DeleteApplicationTypes(int ApplicationTypeID)
        {
              return clsApplicationTypesData.DeleteApplicationTypes(ApplicationTypeID);
        }
        static public bool GetApplicationTypeInfoByID( int ApplicationTypeID)
        {
            string ApplicationTypeTitle;
            float ApplicationFees;
            return clsApplicationTypes.GetApplicationTypeInfoByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees);

        }

        public bool Save()
        {
          
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddApplicationTypes())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateApplicationTypes();

            }

            return false;
         

        }
    } 
    }


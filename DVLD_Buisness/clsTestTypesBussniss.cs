using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsTestTypes
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public int  TestTypeID { set; get; } 
        public   string  TestTypeTitle { set; get; } 
        public   string  TestTypeDescription { set; get; } 
        public   float  TestTypeFees { set; get; } 


           clsTestTypes(){        this.  TestTypeID =-1 ;
        this.  TestTypeTitle ="" ;
        this.  TestTypeDescription ="" ;
        this.  TestTypeFees =-1 ;
         Mode = enMode.AddNew;
}

           clsTestTypes(int TestTypeID,string TestTypeTitle,string TestTypeDescription,float TestTypeFees){        this. TestTypeID=TestTypeID;
        this. TestTypeTitle=TestTypeTitle;
        this. TestTypeDescription=TestTypeDescription;
        this. TestTypeFees=TestTypeFees;
         Mode = enMode.Update;
}

        private bool _AddTestTypes()
        {
 
            this.TestTypeID = clsTestTypesData.AddTestTypes( TestTypeTitle, TestTypeDescription, TestTypeFees);
              

            return (this.TestTypeID != -1);
        }

        static public DataTable GetAllTestTypes()
        {
                return clsTestTypesData.GetAllTestTypes();
         }

        private bool _UpdateTestTypes()
        {
               bool IsSuccess=          clsTestTypesData.UpdateTestTypes(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);

            return IsSuccess;
        }

         public static clsTestTypes FindTestTypes(clsTestTypes.enTestType TestTypeID)
           {
                 string TestTypeTitle="" ; string TestTypeDescription="" ; float TestTypeFees= -1 ; 

               if(clsTestTypesData.FindTestTypes(  (int)TestTypeID,  TestTypeTitle,  TestTypeDescription,  TestTypeFees))
               {
                   return new clsTestTypes( (int)TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
               }
             return null;
    }

           static bool DeleteTestTypes(int TestTypeID)
        {
              return clsTestTypesData.DeleteTestTypes(TestTypeID);
        }

        public bool Save()
        {
          
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddTestTypes())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTestTypes();

            }

            return false;
         

        }
    } 
    }


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsCountries
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public   int  CountryID { set; get; } 
        public   string  CountryName { set; get; } 


           clsCountries(){        this.  CountryID =-1 ;
        this.  CountryName ="" ;
         Mode = enMode.AddNew;
}

           clsCountries(int CountryID,string CountryName){        this. CountryID=CountryID;
        this. CountryName=CountryName;
         Mode = enMode.Update;
}

        private bool _AddCountries()
        {
 
            this.CountryID = clsCountriesData.AddCountries( CountryName);
              

            return (this.CountryID != -1);
        }

        static public DataTable GetAllCountries()
        {
                return clsCountriesData.GetAllCountries();
         }

        private bool _UpdateCountries()
        {
 
            bool IsSuccess= clsCountriesData.UpdateCountries( CountryName);
              

            return IsSuccess;
        }

         public clsCountries FindCountries(int CountryID)
           {
                 string CountryName="" ; 

               if(clsCountriesData.FindCountries( ref CountryID, ref CountryName))
               {
                   return new clsCountries( CountryID, CountryName);
               }
             return null;
    }

           static bool DeleteCountries(int CountryID)
        {
              return clsCountriesData.DeleteCountries(CountryID);
        }

        public bool Save()
        {
          
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddCountries())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:                    return _UpdateCountries();

            }

            return false;
         

        }
    } 
    }


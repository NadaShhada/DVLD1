using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clsPeople
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public   int  PersonID { set; get; } 
        public   string  NationalNo { set; get; } 
        public   string  FirstName { set; get; } 
        public   string  SecondName { set; get; } 
        public   string  ThirdName { set; get; } 
        public   string  LastName { set; get; } 
        public   DateTime  DateOfBirth { set; get; } 
        public   short  Gendor { set; get; } 
        public   string  Address { set; get; } 
        public   string  Phone { set; get; } 
        public   string  Email { set; get; } 
        public   int  NationalityCountryID { set; get; } 
        public   string  ImagePath { set; get; } 


           clsPeople(){        this.  PersonID =-1 ;
        this.  NationalNo ="" ;
        this.  FirstName ="" ;
        this.  SecondName ="" ;
        this.  ThirdName ="" ;
        this.  LastName ="" ;
        this.  DateOfBirth =DateTime.Now ;
        this.  Gendor =-1 ;
        this.  Address ="" ;
        this.  Phone ="" ;
        this.  Email ="" ;
        this.  NationalityCountryID =-1 ;
        this.  ImagePath ="" ;
         Mode = enMode.AddNew;
}

           clsPeople(int PersonID,string NationalNo,string FirstName,string SecondName,string ThirdName,string LastName,DateTime DateOfBirth,short Gendor,string Address,string Phone,string Email,int NationalityCountryID,string ImagePath){        this. PersonID=PersonID;
        this. NationalNo=NationalNo;
        this. FirstName=FirstName;
        this. SecondName=SecondName;
        this. ThirdName=ThirdName;
        this. LastName=LastName;
        this. DateOfBirth=DateOfBirth;
        this. Gendor=Gendor;
        this. Address=Address;
        this. Phone=Phone;
        this. Email=Email;
        this. NationalityCountryID=NationalityCountryID;
        this. ImagePath=ImagePath;
         Mode = enMode.Update;
}

        private bool _AddPeople()
        {
 
            this.PersonID = clsPeopleData.AddPeople( NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
              

            return (this.PersonID != -1);
        }

        static public DataTable GetAllPeople()
        {
                return clsPeopleData.GetAllPeople();
         }

        private bool _UpdatePeople()
        {
 
            bool IsSuccess= clsPeopleData.UpdatePeople( NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
              

            return IsSuccess;
        }

         public clsPeople FindPeople(int PersonID)
           {
                 string NationalNo="" ; string FirstName="" ; string SecondName="" ; string ThirdName="" ; string LastName="" ; DateTime DateOfBirth=DateTime.Now; short Gendor= -1 ; string Address="" ; string Phone="" ; string Email="" ; int NationalityCountryID= -1 ; string ImagePath="" ; 

               if(clsPeopleData.FindPeople( ref PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
               {
                   return new clsPeople( PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
               }
             return null;
    }

           static bool DeletePeople(int PersonID)
        {
              return clsPeopleData.DeletePeople(PersonID);
        }

        public bool Save()
        {
          
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddPeople())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:                    return _UpdatePeople();

            }

            return false;
         

        }
    } 
    }


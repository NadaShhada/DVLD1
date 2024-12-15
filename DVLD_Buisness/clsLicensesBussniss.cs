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
    public class clsLicenses
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };
        public clsDrivers DriverInfo;
        public int  LicenseID { set; get; } 
        public   int  ApplicationID { set; get; } 
        public   int  DriverID { set; get; } 
        public   int  LicenseClass { set; get; }
        public clsLicenseClasses LicenseClassesInfo;
        public   DateTime  IssueDate { set; get; } 
        public   DateTime  ExpirationDate { set; get; } 
        public   string  Notes { set; get; } 
        public   float  PaidFees { set; get; } 
        public   bool  IsActive { set; get; }
        public bool IsDetained
        {
            get { return clsDetainedLicense.IsLicenseDetained(this.LicenseID); }
        }
        public clsDetainedLicense DetainedInfo { set; get; }

        public enIssueReason IssueReason { set; get; }

        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }
        public int  CreatedByUserID { set; get; } 


        public   clsLicenses(){        this.  LicenseID =-1 ;
        this.  ApplicationID =-1 ;
        this.  DriverID =-1 ;
        this.  LicenseClass =-1 ;
        this.  IssueDate =DateTime.Now ;
        this.  ExpirationDate =DateTime.Now ;
        this.  Notes ="" ;
        this.  PaidFees =-1 ;
        this.  IsActive =false ;
        this.  IssueReason =enIssueReason.FirstTime ;
        this.  CreatedByUserID =-1 ;
         Mode = enMode.AddNew;
}

           clsLicenses(int LicenseID,int ApplicationID,int DriverID,int LicenseClass,DateTime IssueDate,DateTime ExpirationDate,string Notes,float PaidFees,bool IsActive,short IssueReason,int CreatedByUserID){        this. LicenseID=LicenseID;
        this. ApplicationID=ApplicationID;
        this. DriverID=DriverID;
        this. LicenseClass=LicenseClass;
        this. IssueDate=IssueDate;
        this. ExpirationDate=ExpirationDate;
        this. Notes=Notes;
        this. PaidFees=PaidFees;
        this. IsActive=IsActive;
        this. IssueReason=enIssueReason.FirstTime;
        this. CreatedByUserID=CreatedByUserID;
         Mode = enMode.Update;
}

        private bool _AddLicenses()
        {
 
            this.LicenseID = clsLicensesData.AddLicenses( ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (byte)IssueReason, CreatedByUserID);
              

            return (this.LicenseID != -1);
        }

        static public DataTable GetAllLicenses()
        {
                return clsLicensesData.GetAllLicenses();
         }

        private bool _UpdateLicenses()
        {
 
            bool IsSuccess= clsLicensesData.UpdateLicenses( LicenseID,   this.ApplicationID,  this.DriverID,  this.LicenseClass,  this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,(byte) this.IssueReason, this.CreatedByUserID);
              

            return IsSuccess;
        }

         public static clsLicenses FindLicenses(int LicenseID)
           {
                 int ApplicationID= -1 ; int DriverID= -1 ; int LicenseClass= -1 ; DateTime IssueDate=DateTime.Now; DateTime ExpirationDate=DateTime.Now; string Notes="" ; float PaidFees= -1 ; bool IsActive=false; short IssueReason= -1 ; int CreatedByUserID= -1 ; 

               if(clsLicensesData.FindLicenses(  LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
               {
                   return new clsLicenses( LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
               }
             return null;
    }

       

        public bool Save()
        {
          
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddLicenses())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLicenses();

            }

            return false;
         

        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicensesData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicensesData.GetDriverLicenses(DriverID);
        }

        public Boolean IsLicenseExpired()
        {

            return (this.ExpirationDate < DateTime.Now);

        }

        public bool DeactivateCurrentLicense()
        {
            return (clsLicensesData.DeactivateLicense(this.LicenseID));
        }

        public clsLicenses RenewLicense(string Notes, int CreatedByUserID)
        {

            //First Create Applicaiton 
            clsApplications Application = new clsApplications();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplications.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.FindApplicationTypes((int)clsApplications.enApplicationType.RenewDrivingLicense).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicenses NewLicense = new clsLicenses();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.LicenseClassesInfo.DefaultValidityLength;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.LicenseClassesInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicenses.enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;


            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }
        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {

            //First Create Applicaiton 
            clsApplications Application = new clsApplications();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicsense;
            Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.FindApplicationTypes((int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }

            ApplicationID = Application.ApplicationID;


            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, Application.ApplicationID);

        }

        public int Detain(float FineFees, int CreatedByUserID)
        {
            clsDetainedLicense detainedLicense = new clsDetainedLicense();
            detainedLicense.LicenseID = this.LicenseID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = Convert.ToSingle(FineFees);
            detainedLicense.CreatedByUserID = CreatedByUserID;

            if (!detainedLicense.Save())
            {

                return -1;
            }

            return detainedLicense.DetainID;

        }

        public clsLicenses Replace(enIssueReason IssueReason, int CreatedByUserID)
        {


            //First Create Applicaiton 
            clsApplications Application = new clsApplications();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplications.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplications.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.FindApplicationTypes(Application.ApplicationTypeID).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }


            clsLicenses NewLicense = new clsLicenses();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;



            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }

    }
}


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
    public class clsApplications
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enApplicationType {NewDrivingLicense=1,RenewDrivingLicense=2,ReplaceLostDrivingLicense=3 ,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        }

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public int  ApplicationID { set; get; } 
        public   int  ApplicantPersonID { set; get; } 
        public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }
        public   DateTime  ApplicationDate { set; get; } 
        public   int  ApplicationTypeID { set; get; }

        public clsApplicationTypes ApplicationTypeInfo;

        public enApplicationStatus ApplicationStatus { set; get; }  

        public string StatusText
        {

            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";

                }
            }
        }
        public   DateTime  LastStatusDate { set; get; } 
        public   float  PaidFees { set; get; } 
        public   int  CreatedByUserID { set; get; } 

        public clsUser CreatedByUserInfo { set; get; }

       public    clsApplications(){        this.  ApplicationID =-1 ;
        this.  ApplicantPersonID =-1 ;
        this.  ApplicationDate =DateTime.Now ;
        this.  ApplicationTypeID =-1 ;
        this.  ApplicationStatus =enApplicationStatus.New;
        this.  LastStatusDate =DateTime.Now ;
        this.  PaidFees =-1 ;
        this.  CreatedByUserID =-1 ;
         Mode = enMode.AddNew;
}

        public clsApplications(int ApplicationID,int ApplicantPersonID,DateTime ApplicationDate,int ApplicationTypeID,enApplicationStatus ApplicationStatus,DateTime LastStatusDate,float PaidFees,int CreatedByUserID){ 
            this. ApplicationID=ApplicationID;
        this. ApplicantPersonID=ApplicantPersonID;
        this. ApplicationDate=ApplicationDate;
        this. ApplicationTypeID=ApplicationTypeID;
            this.ApplicationTypeInfo=clsApplicationTypes.FindApplicationTypes(ApplicationTypeID);
        this. ApplicationStatus= ApplicationStatus;
        this. LastStatusDate=LastStatusDate;
        this. PaidFees=PaidFees;
        this. CreatedByUserID=CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
         Mode = enMode.Update;
}

        private bool _AddApplications()
        {
 
            this.ApplicationID = clsApplicationsData.AddApplications( ApplicantPersonID, ApplicationDate, ApplicationTypeID,(byte) ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
              

            return (this.ApplicationID != -1);
        }

        static public DataTable GetAllApplications()
        {
                return clsApplicationsData.GetAllApplications();
         }

        private bool _UpdateApplications()
        {
 
            bool IsSuccess= clsApplicationsData.UpdateApplications(ApplicationID,  ApplicantPersonID,  ApplicationDate,  ApplicationTypeID,(byte)  ApplicationStatus,  LastStatusDate,  PaidFees,  CreatedByUserID);
              

            return IsSuccess;
        }

         public static  clsApplications FindBaseApplicationsType(int ApplicationID)
           {
                 int ApplicantPersonID= -1 ; DateTime ApplicationDate=DateTime.Now; int ApplicationTypeID= -1 ; byte ApplicationStatus= 1 ; DateTime LastStatusDate=DateTime.Now; float PaidFees= -1 ; int CreatedByUserID= -1 ; 

               if(clsApplicationsData.FindApplications(  ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
               {
                   return new clsApplications( ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
               }
             return null;
    }
         public bool Cancel()
        {
            return clsApplicationsData.UpdateStatus(ApplicationID,2);
        }

        public bool SetComplete()
        {
            return clsApplicationsData.UpdateStatus(ApplicationID, 3);
        }


        static bool DeleteApplications(int ApplicationID)
        {
              return clsApplicationsData.DeleteApplications(ApplicationID);
        }

        public bool Delete()
        {
            return clsApplicationsData.DeleteApplications(this.ApplicationID);
        }

        public bool Save()
        {
          
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddApplications())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateApplications();

            }

            return false;
         

        }

        public static bool IsApplicationExist(int ApplcationID)
        {
            return clsApplicationsData.IsApplicationExist(ApplcationID);

        }
        public static bool DoesPersonHaveActiveApplication(int PersonID,int ApplicationTypeID)
        {
            return clsApplicationsData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }
        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return clsApplications.DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }
        public static int GetActiveApplicationID(int PersonID,int ApplicationTypeID)
        {
            return clsApplicationsData.GetActiveApplicationID(PersonID, ApplicationTypeID);
        }

        public  int GetActiveApplicationID(int ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID,clsApplications.enApplicationType ApplicationTypeID,int LicenseClassID)
        {
            return clsApplicationsData.GetActiveApplicationIDForLiceenseClass(PersonID,(int)ApplicationTypeID,LicenseClassID);
        }
        public  int GetActiveApplicationIDForLicenseClass( clsApplications.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationsData.GetActiveApplicationIDForLiceenseClass(this.ApplicantPersonID, (int)ApplicationTypeID, LicenseClassID);
        }
    } 
    }


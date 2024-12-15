using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace Bussiness_Layer
{
    public class clssysdiagrams
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public   string  name { set; get; } 
        public   int  principal_id { set; get; } 
        public   int  diagram_id { set; get; } 
        public   int  version { set; get; } 
        public   unknown  definition { set; get; } 


           clssysdiagrams(){        this.  name ="" ;
        this.  principal_id =-1 ;
        this.  diagram_id =-1 ;
        this.  version =-1 ;
        this.  definition =-1 ;
         Mode = enMode.AddNew;
}

           clssysdiagrams(string name,int principal_id,int diagram_id,int version,unknown definition){        this. name=name;
        this. principal_id=principal_id;
        this. diagram_id=diagram_id;
        this. version=version;
        this. definition=definition;
         Mode = enMode.Update;
}

        private bool _Addsysdiagrams()
        {
 
            this.name = clssysdiagramsData.Addsysdiagrams( principal_id, diagram_id, version, definition);
              

            return (this.name != -1);
        }

        static public DataTable GetAllsysdiagrams()
        {
                return clssysdiagramsData.GetAllsysdiagrams();
         }

        private bool _Updatesysdiagrams()
        {
 
            bool IsSuccess= clssysdiagramsData.Updatesysdiagrams( principal_id, diagram_id, version, definition);
              

            return IsSuccess;
        }

         public clssysdiagrams Findsysdiagrams(int name)
           {
                 int principal_id= -1 ; int diagram_id= -1 ; int version= -1 ; unknown definition= -1 ; 

               if(clssysdiagramsData.Findsysdiagrams( ref name, ref principal_id, ref diagram_id, ref version, ref definition))
               {
                   return new clssysdiagrams( name, principal_id, diagram_id, version, definition);
               }
             return null;
    }

           static bool Deletesysdiagrams(int name)
        {
              return clssysdiagramsData.Deletesysdiagrams(name);
        }

        public bool Save()
        {
          
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_Addsysdiagrams())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _Updatesysdiagrams();

            }

            return false;
         

        }
    } 
    }


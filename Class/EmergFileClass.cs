using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SMSmanage.Class;
using SMSmanage.Entity;

namespace SMSmanage.Class
{
    public class EmergFileClass
    {
        public bool Insert(EmergFileEntity ety, SqlTransaction tran)
        {
            DataBase db = new DataBase();
            return db.Create(ety, tran) > 0;
        }

        public DataSet Search(string strsql)
        {
            if (strsql == "") strsql = "select * from EmergFile";
            DataBase db = new DataBase();
            return db.Execute(null, strsql);
        }

        public DataSet Search(EmergFileEntity ety, SqlTransaction tran)
        {
            string strsql = "select * from EmergFile where 1=1 ";
            Tools _tool = new Tools();
            strsql += _tool.GetEntityToWheresql("", "like", ety);
            return Search(strsql);
        }

        public bool Update(EmergFileEntity ety, SqlTransaction tran)
        {
            DataBase db = new DataBase();
            return db.Update(ety, null, "");
        }

        public DataSet SearchForReadySms(string FileBoxId)
        {
            string strsql = " select * from filebox where FileBoxId ='" + FileBoxId + "'";
            DataBase db = new DataBase();
            return db.Execute(null, strsql);
        }

        public DataSet SearchAllGetDept()
        {
            string strsql = " select distinct getdept from filebox ";
            DataBase db = new DataBase();
            return db.Execute(null, strsql);
        }
    }
}

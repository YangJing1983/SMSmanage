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
    public class MessageUserClass
    {
        public bool Insert(MessageUserEntity ety, SqlTransaction tran)
        {
            DataBase db = new DataBase();
            return db.Create(ety, tran) > 0;
        }

        public DataSet Search(string strsql)
        {
            if (strsql == "") strsql = "select * from MessageUser";
            DataBase db = new DataBase();
            return db.Execute(null, strsql);
        }

        public DataSet Search(MessageUserEntity ety, SqlTransaction tran)
        {
            string strsql = "select * from MessageUser where 1=1 ";
            Tools _tool = new Tools();
            strsql += _tool.GetEntityToWheresql("", "like", ety);
            return Search(strsql);
        }

        public bool Update(MessageUserEntity ety, SqlTransaction tran)
        {
            DataBase db = new DataBase();
            return db.Update(ety, null, "");
        }

        public void Delete(string MessageUserId)
        {
            string strsql = "delete from MessageUser where MessageUserId='" + MessageUserId + "'";
            DataBase db = new DataBase();
            db.ExecuteNoQ(null, strsql);
        }

        public DataSet SearchForDeptName()
        {
            string strsql = " select deptname from deptin";
            DataBase db = new DataBase();
            return db.Execute(null, strsql);
        }
    }
}

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
    public class SendMessageClass
    {
        public bool Insert(SendMessageEntity ety, SqlTransaction tran)
        {
            DataBase db = new DataBase();
            return db.Create(ety, tran) > 0;
        }

        public DataSet Search(string strsql)
        {
            if (strsql == "") strsql = "select * from SendMessage";
            DataBase db = new DataBase();
            return db.Execute(null, strsql);
        }

        public DataSet Search(SendMessageEntity ety, SqlTransaction tran)
        {
            string strsql = "select * from SendMessage where 1=1 ";
            Tools _tool = new Tools();
            strsql += _tool.GetEntityToWheresql("", "like", ety);
            return Search(strsql);
        }

        public DataSet Search(SendMessageEntity ety,string begintime,string endtime)
        {
            string strsql = "select * from SendMessage where 1=1 ";
            Tools _tool = new Tools();
            strsql += _tool.GetEntityToWheresql("", "like", ety);
            if (begintime != "") strsql += " and datediff(day, SendTime, '" + begintime + "') <= 0";
            if (endtime != "") strsql += " and datediff(day, SendTime, '" + endtime + "') >= 0";
            return Search(strsql);
        }
    }
}

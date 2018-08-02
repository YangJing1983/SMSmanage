using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;


namespace SMSmanage.Class
{
    public class DataBase
    {
        SqlConnection conn;
        public DataBase()
        {
            try
            {
                conn = new SqlConnection(Str_odbc);
                conn.Open();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool Close()
        {
            bool flag;
            try
            {
                conn.Close();
                flag = true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return flag;
        }

        //private string _str_odbc = "DATABASE=filesystem;DRIVER={MySQL ODBC 5.1 Driver};OPTION=0;PWD=spring;PORT=3306;SERVER=127.0.0.1;UID=root";
        private string _str_odbc = "data source=127.0.0.1;user id=sa;password=spring;initial catalog=ynagt";

        public string Str_odbc
        {
            get { return _str_odbc; }
            set { _str_odbc = value; }
        }

        private int GetSerial(string TableName, string SerialKey, string SerialValueKey, string SerialName, SqlTransaction tran)
        {
            string str = "select * from " + TableName + " where " + SerialKey + " = '" + SerialName + "'";
            int num = 0;
            SqlCommand selectCommand = new SqlCommand(str, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            if (tran == null) selectCommand.Transaction = conn.BeginTransaction();
            else selectCommand.Transaction = tran;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                str = "select " + SerialValueKey + " from " + TableName + " where " + SerialKey + "= '" + SerialName + "'";
                selectCommand.CommandText = str;
                new SqlDataAdapter(selectCommand).Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    num = Convert.ToInt32(dataSet.Tables[0].Rows[0][SerialValueKey]) + 1;
                }

                str = "update " + TableName + " set " + SerialValueKey + "=" + num + " where " + SerialKey + " = '" + SerialName + "'";
                selectCommand.CommandText = str;
                selectCommand.ExecuteNonQuery();
                //selectCommand.Transaction.Commit();


            }
            else
            {
                str = "insert into " + TableName + "(" + SerialKey + "," + SerialValueKey + ") values('" + SerialName + "',1)";
                selectCommand.CommandText = str;
                selectCommand.ExecuteNonQuery();
                //selectCommand.Transaction.Commit();
                num = 1;
            }
            return num;
        }

        public int Create(object EtyInput, SqlTransaction _tran)
        {
            int num_id_new = 0;
            bool flag = false;
            string tablename = "";
            SqlCommand command = new SqlCommand();
            SqlTransaction tran = null;
            if (tran == null)
            {
                tran = conn.BeginTransaction();
                flag = true;
            }
            else
            {
                tran = _tran;
            }
            Type type = EtyInput.GetType();
            PropertyInfo[] properties = type.GetProperties();
            string str = "";
            string str2 = "";
            try
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    string name = properties[i].Name;
                    if (name == "entity_Table")
                    {
                        tablename = type.GetProperty("entity_Table").GetValue(EtyInput, null).ToString();
                    }
                    if (name.Length > 6 && name.Substring(name.Length - 6, 6) == "_index")
                    {
                        num_id_new = GetSerial("TableIndex", "TableIdName", "CurrentIndex", name.Substring(0, name.Length - 6), tran);
                        str2 = "'" + num_id_new + "'";
                    }
                }
                for (int i = 0; i < properties.Length; i++)
                {
                    string name = properties[i].Name;
                    if (name.Contains("_flag"))
                    {
                        string indexColumn = name.Substring(0, name.Length - 5);
                        SqlParameter parameter = new SqlParameter();
                        if (str != "")
                        {
                            str = str + ", ";
                        }
                        str += indexColumn;
                        if (i != 0)
                        {
                            if (str2 != "")
                            {
                                str2 = str2 + ", ";
                            }
                            str2 += "'" + type.GetProperty(indexColumn).GetValue(EtyInput, null) + "'";
                        }
                    }
                }
                
                string str6 = "insert into " + tablename + "(" + str + ") values(" + str2 + ")";

                //str6 = str6 + " select scope_identity()";
                command.Connection = conn;
                command.Transaction = tran;
                command.CommandText = str6;
                command.ExecuteNonQuery();
                command.Transaction.Commit();
                Close();

            }
            catch (Exception exception)
            {
                if ((tran != null) && flag)
                {
                    tran.Rollback();
                    Close();
                }
                throw new Exception(exception.Message);
            }
            return num_id_new;
        }

        public void ExecuteNoQ(SqlTransaction tran, string strsql)
        {
            bool flag = false;
            SqlCommand command = new SqlCommand();
            command.CommandText = strsql;
            command.Connection = conn;
            try
            {
                if (tran != null)
                {
                    command.Transaction = tran;
                }
                else
                {
                    tran = conn.BeginTransaction();
                    command.Transaction = tran;
                    flag = true;
                }
                command.ExecuteNonQuery();
                if (flag)
                {
                    tran.Commit();
                    Close();
                }
            }
            catch (Exception exception)
            {
                if (flag)
                {
                    tran.Rollback();
                    Close();
                }
                throw new Exception(exception.Message);
            }
        }

        public DataSet Execute(SqlTransaction tran, string strsql)
        {
            bool flag = false;
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            command.CommandText = strsql;
            command.Connection = conn;
            try
            {
                if (tran != null)
                {
                    command.Transaction = tran;
                }
                else
                {
                    tran = conn.BeginTransaction();
                    command.Transaction = tran;
                    flag = true;
                }
                SqlDataAdapter oa=new SqlDataAdapter(command);
                
                oa.Fill(ds);
                if (flag)
                {
                    tran.Commit();
                    Close();
                }
                return ds;
            }
            catch (Exception exception)
            {
                if (flag)
                {
                    tran.Rollback();
                    Close();
                    return ds;
                }
                throw new Exception(exception.Message);
            }
        }

        public bool Update(object objEntity, SqlTransaction tran, string wheresql)
        {
            bool flag = false;
            SqlTransaction transaction = null;
            if (tran == null)
            {
                transaction = conn.BeginTransaction();
                flag = true;
            }
            else
            {
                transaction = tran;
            }
            Type type = objEntity.GetType();
            PropertyInfo[] properties = type.GetProperties();
            string tablename = "";
            string str2 = "";
            try
            {
                SqlCommand command = new SqlCommand();
                for (int i = 0; i < properties.Length; i++)
                {
                    string name = properties[i].Name;
                    if (name.Contains("entity_Table"))
                    {
                        tablename = (string)type.GetProperty(name).GetValue(objEntity, null);
                    }
                    if (name.Contains("_flag"))
                    {
                        string str4 = name.Substring(0, name.Length - 5);
                        if (Convert.ToBoolean(type.GetProperty(str4 + "_flag").GetValue(objEntity, null)))
                        {
                            if (str2 != "")
                            {
                                str2 = str2 + ", ";
                            }
                            object obj2 = str2;
                            str2 = string.Concat(new object[] { obj2, str4, "='", type.GetProperty(str4).GetValue(objEntity, null), "'" }); 
                        }

                    }
                   
                }
                
                string _name = "";
                string str_id = "";
                for (int i = 0; i < properties.Length; i++)
                {
                    string str1 = properties[i].Name;
                    if ((str1.Length > 6) && (str1.Substring(str1.Length - 6, 6) == "_index"))
                    {
                        str_id = str1.Substring(0, str1.Length - 6);
                        _name = (string)type.GetProperty(str_id).GetValue(objEntity, null);
                    }
                }
                string str8 = "";
                if ((wheresql != null) && (wheresql != ""))
                {
                    str8 = "update " + tablename + " set " + str2 + wheresql;
                }
                else
                {
                    str8 = string.Concat(new object[] { "update ", tablename, " set ", str2, " where ", str_id, "='", _name, "'" });
                }
                command.Connection = conn;
                command.Transaction = transaction;
                command.CommandText = str8;
                command.ExecuteNonQuery();
                if (flag)
                {
                    transaction.Commit();
                    Close();
                }
                Close();
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
                if ((transaction != null) && flag)
                {
                    transaction.Rollback();
                    Close();
                    return false;
                }
                
            }
        }
    }
}

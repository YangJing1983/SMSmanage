using System;
using System.Data;
using System.Reflection;

namespace SMSmanage
{
    public class Tools
    {
        private string _err = "";

        private bool AddPropertiy(object ObjBus, string ProName, object ProValue)
        {
            Type type = ObjBus.GetType();
            return false;
        }

        public object GetValue(object entity, string ProName)
        {
            object obj2;
            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();
            try
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    if (properties[i].Name.ToLower() == ProName.ToLower())
                    {
                        return type.GetProperty(properties[i].Name).GetValue(entity, null);
                    }
                }
                obj2 = null;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return obj2;
        }

        public bool LoadMethod(object ObjBus, string MethodName, object[] Para)
        {
            bool flag;
            try
            {
                ObjBus.GetType().InvokeMember(MethodName, BindingFlags.InvokeMethod, Type.DefaultBinder, ObjBus, Para);
                flag = true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return flag;
        }

        public bool SetValue(object entity, string ProName, object ProValue)
        {
            bool flag;
            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();
            try
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo property = null;
                    if (properties[i].Name.ToLower() == ProName.ToLower())
                    {
                        property = type.GetProperty(properties[i].Name);
                        if (property.PropertyType.ToString() == "System.Byte[]")
                        {
                            if (!Convert.IsDBNull(ProValue))
                            {
                                property.SetValue(entity, ProValue, null);
                            }
                            else
                            {
                                property.SetValue(entity, null, null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.Int32")
                        {
                            if (!(Convert.IsDBNull(ProValue) || (ProValue.ToString() == "")))
                            {
                                property.SetValue(entity, Convert.ToInt32(ProValue), null);
                            }
                            else
                            {
                                property.SetValue(entity, 0, null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.Int16")
                        {
                            if (!(Convert.IsDBNull(ProValue) || (ProValue.ToString() == "")))
                            {
                                property.SetValue(entity, Convert.ToInt16(ProValue), null);
                            }
                            else
                            {
                                property.SetValue(entity, 0, null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.Int64")
                        {
                            if (!(Convert.IsDBNull(ProValue) || (ProValue.ToString() == "")))
                            {
                                property.SetValue(entity, Convert.ToInt64(ProValue), null);
                            }
                            else
                            {
                                property.SetValue(entity, 0, null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.DateTime")
                        {
                            if (!Convert.IsDBNull(ProValue))
                            {
                                property.SetValue(entity, Convert.ToDateTime(ProValue), null);
                            }
                            else
                            {
                                property.SetValue(entity, Convert.ToDateTime("1900-01-01 00:00:00"), null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.String")
                        {
                            if (!Convert.IsDBNull(ProValue))
                            {
                                property.SetValue(entity, ProValue.ToString(), null);
                            }
                            else
                            {
                                property.SetValue(entity, "", null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.Single")
                        {
                            if (!(Convert.IsDBNull(ProValue) || (ProValue.ToString() == "")))
                            {
                                property.SetValue(entity, Convert.ToSingle(ProValue), null);
                            }
                            else
                            {
                                property.SetValue(entity, 0, null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.Single")
                        {
                            if (!Convert.IsDBNull(ProValue))
                            {
                                property.SetValue(entity, Convert.ToBoolean(ProValue), null);
                            }
                            else
                            {
                                property.SetValue(entity, false, null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.String[]")
                        {
                            if (!Convert.IsDBNull(ProValue))
                            {
                                property.SetValue(entity, ProValue, null);
                            }
                            else
                            {
                                property.SetValue(entity, false, null);
                            }
                        }
                        else if (!Convert.IsDBNull(ProValue))
                        {
                            property.SetValue(entity, ProValue.ToString(), null);
                        }
                        else
                        {
                            property.SetValue(entity, null, null);
                        }
                        return true;
                    }
                }
                flag = false;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return flag;
        }

        public object GetValueFull(object entity, string ProName, char split)
        {
            object obj2;
            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();
            try
            {
                int index = ProName.IndexOf(split);
                if (index > -1)
                {
                    string str = ProName.Substring(0, index);
                    ProName = ProName.Substring(index + 1);
                    if (type.GetProperty("Entity_Table").GetValue(entity, null).ToString().ToLower() == str.ToLower())
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            if (properties[i].Name.ToLower() == ProName.ToLower())
                            {
                                return type.GetProperty(properties[i].Name).GetValue(entity, null);
                            }
                        }
                    }
                }
                obj2 = null;
            }
            catch (Exception exception)
            {
                this._err = exception.Message;
                throw new Exception(exception.Message);
            }
            return obj2;
        }

        public bool SetValueFullName(object entity, string ProName, object ProValue, char split)
        {
            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();
            int index = ProName.IndexOf(split);
            if (index > -1)
            {
                string str = ProName.Substring(0, index);
                ProName = ProName.Substring(index + 1);
                if (type.GetProperty("Entity_Table").GetValue(entity, null).ToString().ToLower() == str.ToLower())
                {
                    return this.SetValue(entity, ProName, ProValue);
                }
            }
            return false;
        }

        public string Err
        {
            get
            {
                return this._err;
            }
        }

        public  string GetEntityToWheresql(string TableName, string SearchEqualType, object varEntity)
        {
            try
            {
                string str = "";
                if (TableName != "")
                {
                    str = TableName + ".";
                }
                Type type = varEntity.GetType();
                string str2 = " ";
                string str3 = "";
                object obj2 = type.Assembly.CreateInstance(type.FullName);
                PropertyInfo[] properties = type.GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    string name = properties[i].Name;
                    if (((name.Length > 5) && (name.Substring(name.Length - 5, 5) == "_flag")) && type.GetProperty(name).GetValue(varEntity, null).Equals(true))
                    {
                        string str5 = name.Substring(0, name.Length - 5);
                        str3 = " and " + str + str5 + " ";
                        if ((((type.GetProperty(str5).PropertyType.ToString() == "System.Int32") || (type.GetProperty(str5).PropertyType.ToString() == "System.Int16")) || (type.GetProperty(str5).PropertyType.ToString() == "System.Int64")) || (type.GetProperty(str5).PropertyType.ToString() == "System.Single"))
                        {
                            str3 = str3 + " = '" + type.GetProperty(str5).GetValue(varEntity, null).ToString() + "' ";
                        }
                        else if (type.GetProperty(str5).PropertyType.ToString() == "System.String")
                        {
                            if (SearchEqualType == "like")
                            {
                                string str8 = type.GetProperty(str5).GetValue(varEntity, null).ToString();
                                bool flag = false;
                                if (((str8.IndexOf('%') > -1) && (str8.IndexOf('\'') > -1)) && (str8.IndexOf('!') > -1))
                                {
                                    flag = true;
                                    str8 = str8.Replace("%", "!").Replace("'", "!'").Replace("!", "!!").Replace("[", "![");
                                }
                                if (!flag)
                                {
                                    str3 = str3 + " like '%" + str8 + "%' ";
                                }
                                else
                                {
                                    str3 = str3 + " like '%" + str8 + "%' escape '!'";
                                }
                            }
                            else if (SearchEqualType == "equal")
                            {
                                str3 = str3 + " = '" + type.GetProperty(str5).GetValue(varEntity, null).ToString() + "' ";
                            }
                        }
                        else if (type.GetProperty(str5).PropertyType.ToString() == "System.DateTime")
                        {
                            str3 = " and datediff(day," + str5 + ",'" + type.GetProperty(str5).GetValue(varEntity, null).ToString() + "')=0 ";
                        }
                        str2 = str2 + str3;
                    }
                }
                return str2;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public  object FillEntity(DataRow dRow, Type entityType)
        {
            object obj3;
            try
            {
                object obj2 = entityType.Assembly.CreateInstance(entityType.ToString());
                PropertyInfo[] properties = entityType.GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    string name = properties[i].Name;
                    if (name.Contains("_flag"))
                    {
                        name = name.Substring(0, name.Length - 5);
                        PropertyInfo property = entityType.GetProperty(name);
                        if (property.PropertyType.ToString() == "System.Byte[]")
                        {
                            if (!Convert.IsDBNull(dRow[name]))
                            {
                                property.SetValue(obj2, dRow[name], null);
                            }
                            else
                            {
                                property.SetValue(obj2, null, null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.Int32")
                        {
                            if (!Convert.IsDBNull(dRow[name]))
                            {
                                property.SetValue(obj2, Convert.ToInt32(dRow[name]), null);
                            }
                            else
                            {
                                property.SetValue(obj2, 0, null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.Int16")
                        {
                            if (!Convert.IsDBNull(dRow[name]))
                            {
                                property.SetValue(obj2, Convert.ToInt16(dRow[name]), null);
                            }
                            else
                            {
                                property.SetValue(obj2, 0, null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.Int64")
                        {
                            if (!Convert.IsDBNull(dRow[name]))
                            {
                                property.SetValue(obj2, Convert.ToInt64(dRow[name]), null);
                            }
                            else
                            {
                                property.SetValue(obj2, 0, null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.DateTime")
                        {
                            if (!Convert.IsDBNull(dRow[name]))
                            {
                                property.SetValue(obj2, Convert.ToDateTime(dRow[name]), null);
                            }
                            else
                            {
                                property.SetValue(obj2, Convert.ToDateTime("1900-01-01 00:00:00"), null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.String")
                        {
                            if (!Convert.IsDBNull(dRow[name]))
                            {
                                property.SetValue(obj2, dRow[name].ToString(), null);
                            }
                            else
                            {
                                property.SetValue(obj2, "", null);
                            }
                        }
                        else if (property.PropertyType.ToString() == "System.Single")
                        {
                            if (!Convert.IsDBNull(dRow[name]))
                            {
                                property.SetValue(obj2, Convert.ToSingle(dRow[name]), null);
                            }
                            else
                            {
                                property.SetValue(obj2, 0, null);
                            }
                        }
                        else if (!Convert.IsDBNull(dRow[name]))
                        {
                            property.SetValue(obj2, dRow[name].ToString(), null);
                        }
                        else
                        {
                            property.SetValue(obj2, null, null);
                        }
                    }
                }
                obj3 = obj2;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return obj3;
        }
    }
}
/*
 * 公共函数库 此类因为是静态的 最好在这里都放已经成熟的方法
 * 项目特有的方法请单独建立方法类 
 * 另外作为系统属性 参数请放到GlobalInfo.cs里去 不要将静态属性 静态方法夹杂一起
 * 
 * 2008.8.12 etain
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Security;
using System.Security.Cryptography;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using System.Xml;

namespace SMSmanage
{
    public static class GlobalFunction
    {

        public static char ProChar = '.';

        /// <summary>
        /// 输入整型正则判断 是整型返回true
        /// </summary>
        /// <param name="InputStr"></param>
        /// <returns></returns>
        public static bool CheckIntInput(string InputStr)
        {
            Regex splitDot = new Regex(@"^\d+$");
            return splitDot.IsMatch(InputStr);
        }

        /// <summary>
        /// 从数据库获得服务器时间 
        /// 注意获得的时间是从默认数据库中取的
        /// </summary>
        /// <param name="SqlEx"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        //public static DateTime GetServerTime(SqlExecute SqlEx, IDbTransaction tran)
        //{
        //    try
        //    {
        //        bool bolSelfSQLEx = false;
        //        if (SqlEx == null)
        //        {
        //            bolSelfSQLEx = true;
        //            SqlEx = GlobalFunction.GetSysConnection(GlobalInfo.DefaultConnect);
        //        }

        //        DataSet ds = SqlEx.Execute((System.Data.SqlClient.SqlTransaction)tran, "Select GetDate()");
        //        if (bolSelfSQLEx)
        //        {
        //            SqlEx.Close();
        //            SqlEx = null;
        //        }
        //        return Convert.ToDateTime(ds.Tables[0].Rows[0][0].ToString());
        //    }
        //    catch (Exception ex) { throw new Exception("获取服务器时间失败：" + ex.Message); }
        //}

        /// <summary>
        /// 创建sql数据库执行器
        /// </summary>
        /// <param name="DbName"></param>
        /// <returns></returns>
        //public static SqlExecute GetSysConnection(string DbName)
        //{
        //    if (DbName == "")
        //    { DbName = GlobalInfo.DefaultConnect; }
        //    return new SqlExecute(xmlReadWrite.ConfigueGet("ProgrameSet.xml", "appSettings", DbName));
        //}

        /// <summary>
        /// 创建Access数据库执行器
        /// </summary>
        /// <param name="AccessConnect"></param>
        /// <returns></returns>
        //public static AccessExecute GetAccessSysConnection(string AccessConnect)
        //{
        //    return new AccessExecute(AccessConnect);
        //}


        /// <summary>
        /// 将dataset加载到listview中，中间调用了InitDataRowToLvwByIndex方法
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="isClear"></param>
        /// <param name="lvw"></param>
        public static void InitDatasetToLvw(DataSet ds, bool isClear, ListView lvw)
        {
            if (isClear) lvw.Items.Clear();
            if (ds == null) return;
            if (ds.Tables[0].Rows.Count > 0)
            {
                ListViewItem item = new ListViewItem();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    item = lvw.Items.Add(ds.Tables[0].Rows[i][lvw.Columns[0].Tag.ToString()].ToString());
                    InitDataRowToLvwByIndex(ds.Tables[0].Rows[i], lvw, null, item.Index);
                }
            }
        }

        /// <summary>
        /// 将一个DataRow加载到listview的一行中，这里字段加载利用Tag
        /// 其中  如果列头名称中有“日期”的将过滤掉小时、分钟、秒钟
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="lvw"></param>
        /// <param name="LvG"></param>
        /// <param name="intIndex"></param>
        public static void InitDataRowToLvwByIndex(DataRow dr, ListView lvw, ListViewGroup LvG, int intIndex)
        {
            ListViewItem item = null;
            item = lvw.Items[intIndex];
            if (lvw.Columns[0].Tag.ToString() == "")
                item.Text = "";
            else
                item.Text = dr[lvw.Columns[0].Tag.ToString()].ToString();

            for (int j = 1; j < lvw.Columns.Count; j++)
            {
                if (lvw.Columns[j].Tag != null && lvw.Columns[j].Tag.ToString().Trim() != "")
                {
                    string itemStr = dr[lvw.Columns[j].Tag.ToString()].ToString();
                    if (itemStr == "1900-01-01 00:00:00") itemStr = "";
                    if (ReCheckFloatInput(itemStr) == 0) itemStr = (Convert.ToSingle(itemStr).ToString("0.0"));
                    if (lvw.Columns[j].Text.IndexOf("日期") >= 0) itemStr = (Convert.ToDateTime(itemStr).ToString("yyyy-MM-dd"));
                    item.SubItems.Add(itemStr);
                    if (LvG != null) item.Group = LvG;
                }
            }
        }


        /// <summary>
        /// 带单位组的数据填充
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="isClear"></param>
        /// <param name="lvw"></param>
        /// <param name="groupField">标志分组的字段值</param>
        public static void InitDsToLvwByGroup(DataSet ds, bool isClear, ListView lvw, string groupField,int  imageindex)
        {
            if (isClear) lvw.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ListViewItem item = new ListViewItem();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListViewGroup lGroup = null;
                    string filetype = ds.Tables[0].Rows[i][groupField].ToString();
                    for (int k = 0; k < lvw.Groups.Count; k++)
                    {
                        if (lvw.Groups[k].Name == filetype)
                        {
                            lGroup = lvw.Groups[k];
                        }
                    }
                    if (lGroup != null)
                    {
                        item = lvw.Items.Add(ds.Tables[0].Rows[i][lvw.Columns[0].Tag.ToString()].ToString());
                        if (imageindex > -1) item.ImageIndex = 0;
                        item.Group = lGroup;
                        for (int j = 1; j < lvw.Columns.Count; j++)
                        {
                            if (lvw.Columns[j].Tag != null && lvw.Columns[j].Tag.ToString().Trim() != "")
                            {
                                item.SubItems.Add(ds.Tables[0].Rows[i][lvw.Columns[j].Tag.ToString()].ToString());

                            }
                            else item.SubItems.Add("");
                        }
                    }


                }
            }
        }

        /// <summary>
        /// 将数据集添加到cob中
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="isClear"></param>
        /// <param name="cob"></param>
        /// <param name="IndexName"></param>
        public static void InitDatatoCob(DataSet ds, bool isClear, ComboBox cob, string IndexName)
        {
            if (isClear) cob.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    cob.Items.Add(ds.Tables[0].Rows[i][IndexName].ToString());
            }
        }

        /// <summary>
        /// 查找父窗体中是否有该子窗体
        /// </summary>
        /// <param name="frm">父窗体</param>
        /// <param name="frmType">子窗体</param>
        /// <returns></returns>
        public static int FindForm(Form frm, object frmType)
        {
            Type typeForm = frmType.GetType();
            for (int i = 0; i < frm.MdiChildren.Length; i++)
            {
                if (typeForm.Name == frm.MdiChildren[i].Name)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 选中Check框
        /// </summary>
        /// <param name="lvw">ListView对象</param>
        public static void LvwColChecked(ListView lvw)
        {
            bool bFlag = false;
            if (lvw.Items.Count > 0)
            {
                //设置选中列的背景图片
                if (lvw.Columns[0].ImageIndex == 0) lvw.Columns[0].ImageIndex = 1;
                else lvw.Columns[0].ImageIndex = 0;
                //查看第一行是否选中
                if (lvw.Items[0].Checked == false) bFlag = true;
                for (int i = 0; i < lvw.Items.Count; i++)
                {
                    lvw.Items[i].Checked = bFlag;
                }
            }
        }

        /// <summary>
        /// 按照路径打开本地文件
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="FilePath"></param>
        //public static void OpenFile(IntPtr hwnd, string FilePath)
        //{
        //    ShellRun.CurrentRun.RunExecute_MaxWindows(hwnd, FilePath, 1);
        //}


        /// <summary>
        /// 判断输入的字符串是否是符点型
        /// </summary>
        /// <param name="InputStr"></param>
        /// <returns></returns>
        public static bool CheckFloatInput(string InputStr)
        {
            if (CheckIntInput(InputStr)) return true;
            else
            {
                Regex splitDot = new Regex(@"^\d+.\d+$");
                return splitDot.IsMatch(InputStr);
            }
        }

        /// <summary>
        /// 判断输入是否是有理数，包括整数或小数
        /// </summary>
        /// <param name="InputStr"></param>
        /// <returns></returns>
        public static int ReCheckFloatInput(string InputStr)
        {
            if (CheckIntInput(InputStr)) return 1;
            else
            {
                Regex splitDot = new Regex(@"^\d+\.\d+$");
                if (splitDot.IsMatch(InputStr)) return 0;
                else return 2;
            }
        }

        /// <summary>
        /// 将全角数字变成半角数字
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string ChangeBanJiao(string inputStr)
        {
            return inputStr.Replace("０", "0").Replace("１", "1").Replace("２", "2").Replace("３", "3").Replace("４", "4").Replace("５", "5").Replace("６", "6").Replace("７", "7").Replace("８", "8").Replace("９", "9");
        }


        /// <summary>
        /// 将sender里的输入变成数字 如果输入的是全角数字则变换成半角数字
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static bool ChangeIntInput(object sender)
        {
            try
            {
                if (CheckIntInput(((TextBox)sender).Text.Trim()))
                {
                    ((TextBox)sender).Text = ChangeBanJiao(((TextBox)sender).Text);
                }
                else ((TextBox)sender).Text = "";
                return true;
            }
            catch { return false; }
        }

        #region 控件大小缩放函数
        //以下两方法直接绑定在事件上使用
        //
        public static void Control_Leave(object sender, EventArgs e, int moveTips)
        {
            ((Control)sender).Width -= moveTips * 2;
            ((Control)sender).Height -= moveTips * 2;
            ((Control)sender).Font = new Font(((Control)sender).Font.FontFamily, (float)(((Control)sender).Font.Size - 1), ((Control)sender).Font.Style);
            ((Control)sender).Left += moveTips;
            ((Control)sender).Top += moveTips;
        }

        public static void Control_Enter(object sender, EventArgs e, int moveTips)
        {
            ((Control)sender).Width += moveTips * 2;
            ((Control)sender).Height += moveTips * 2;
            ((Control)sender).Font = new Font(((Control)sender).Font.FontFamily, (float)(((Control)sender).Font.Size + 1), ((Control)sender).Font.Style);
            ((Control)sender).Left -= moveTips;
            ((Control)sender).Top -= moveTips;
        }
        #endregion

        /// <summary>
        /// 把dataset 加载到 listview 中 支持加载组方式 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="isClear"></param>
        /// <param name="lvw"></param>
        /// <param name="LvG"></param>
        public static void initDataSetToGroupLvw(DataSet ds, bool isClear, ListView lvw, ListViewGroup LvG)
        {
            if (isClear) lvw.Items.Clear();
            try
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ListViewItem item = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (lvw.Columns[0].Tag.ToString() == "")
                            item = lvw.Items.Add("");
                        else
                            item = lvw.Items.Add(ds.Tables[0].Rows[i][lvw.Columns[0].Tag.ToString()].ToString());
                        InitDataRowToLvwByIndex(ds.Tables[0].Rows[i], lvw, LvG, item.Index);
                    }
                }
                else
                {
                    if (isClear) lvw.Items.Clear();
                }
            }
            catch (Exception ex)
            { return; }
        }



        /// <summary>
        /// 清空一个控件集合的控件值
        /// </summary>
        /// <param name="objContain"></param>
        public static void ClearText(Control.ControlCollection objContain)
        {
            for (int i = 0; i < objContain.Count; i++)
            {
                Control X = objContain[i];
                if (X.GetType().Name == "TextBox")
                {
                    ((TextBox)X).Text = "";
                }
                else if (X.GetType().Name == "ComboBox")
                {
                    ((ComboBox)X).Text = "";
                }
                else if (X.GetType().Name == "DateTimePicker")
                {
                    ((DateTimePicker)X).Value = DateTime.Now;
                }
                else if (X.GetType().Name == "NumericUpDown")
                {
                    ((NumericUpDown)X).Value = 0;
                }

            }
        }
        /// <summary>
        /// 根据锁定情况清空一个控件集合的控件值
        /// </summary>
        /// <param name="objContain"></param>
        public static void ClearTextByChkLock(Control.ControlCollection objContain)
        {
            for (int i = 0; i < objContain.Count; i++)
            {
                Control X = objContain[i];
                if (X.GetType().Name == "TextBox")
                {
                    if (CheckLock(objContain, X.Name))
                        ((TextBox)X).Text = "";
                }
                else if (X.GetType().Name == "ComboBox")
                {
                    if (CheckLock(objContain, X.Name))
                        ((ComboBox)X).Text = "";
                }
                else if (X.GetType().Name == "DateTimePicker")
                {
                    if (CheckLock(objContain, X.Name))
                        ((DateTimePicker)X).Value = DateTime.Now;
                }
                else if (X.GetType().Name == "NumericUpDown")
                {
                    if (CheckLock(objContain, X.Name))
                        ((NumericUpDown)X).Value = 0;
                }
            }
        }
        private static bool CheckLock(Control.ControlCollection objContain, string strControlName)
        {
            try
            {
                for (int i = 0; i < objContain.Count; i++)
                {
                    Control X = objContain[i];
                    if (X.GetType().Name == "CheckBox"
                        && X.Name.ToLower() == "chk" + strControlName.ToLower()
                        && X.Tag.ToString().ToLower() == "chklock")
                    {
                        return !((CheckBox)X).Checked;
                    }
                }
                return true;
            }
            catch (Exception ex) { return true; }
        }

        #region 控件对实体
        public static void EntityToControls(Control.ControlCollection objContain, object Entity)
        {
            Tools tools = new Tools();
            for (int i = 0; i < objContain.Count; i++)
            {
                Control X = objContain[i];
                if (X.Tag != null && X.Tag.ToString() != "")
                {
                    if (X.GetType().Name == "TextBox")
                    {
                        ((TextBox)X).Text = tools.GetValue(Entity, X.Tag.ToString().ToLower()).ToString();
                    }
                    else if (X.GetType().Name == "ComboBox")
                    {
                        ((ComboBox)X).Text = tools.GetValue(Entity, X.Tag.ToString().ToLower()).ToString();
                    }
                    else if (X.GetType().Name == "DateTimePicker")
                    {
                        ((DateTimePicker)X).Value = Convert.ToDateTime(tools.GetValue(Entity, X.Tag.ToString().ToLower()));
                    }
                    else if (X.GetType().Name == "NumericUpDown")
                    {
                        ((NumericUpDown)X).Value = Convert.ToInt32(tools.GetValue(Entity, X.Tag.ToString().ToLower()));
                    }
                    else if (X.GetType().Name == "Label")
                    {
                        ((Label)X).Text = tools.GetValue(Entity, X.Tag.ToString().ToLower()).ToString();
                    }
                }

            }
        }
        public static object ControlsToEntity(Control.ControlCollection objContain, Type ojbType)
        {
            Tools tools = new Tools();
            object Entity = ojbType.Assembly.CreateInstance(ojbType.FullName);
            for (int i = 0; i < objContain.Count; i++)
            {
                Control X = objContain[i];
                if (X.Tag != null && X.Tag.ToString() != "")
                {
                    if (X.GetType().Name == "TextBox")
                    {
                        if (((TextBox)X).Text != "")
                            tools.SetValue(Entity, X.Tag.ToString().ToLower(), ((TextBox)X).Text);
                    }
                    else if (X.GetType().Name == "ComboBox")
                    {
                        if (((ComboBox)X).Text != "")
                            tools.SetValue(Entity, X.Tag.ToString().ToLower(), ((ComboBox)X).Text);
                    }
                    else if (X.GetType().Name == "DateTimePicker")
                    {
                        tools.SetValue(Entity, X.Tag.ToString().ToLower(), ((DateTimePicker)X).Value);
                    }
                    else if (X.GetType().Name == "NumericUpDown")
                    {
                        tools.SetValue(Entity, X.Tag.ToString().ToLower(), ((NumericUpDown)X).Value);
                    }
                }

            }
            return Entity;
        }
        public static void EntityToControlsFull(Control.ControlCollection objContain, object Entity)
        {
            Tools tools = new Tools();
            for (int i = 0; i < objContain.Count; i++)
            {
                Control X = objContain[i];
                if (X.Tag != null && X.Tag.ToString() != "")
                {
                    if (X.GetType().Name == "TextBox")
                    {
                        ((TextBox)X).Text = tools.GetValueFull(Entity, X.Tag.ToString().ToLower(), ProChar).ToString();
                    }
                    else if (X.GetType().Name == "ComboBox")
                    {
                        ((ComboBox)X).Text = tools.GetValueFull(Entity, X.Tag.ToString().ToLower(), ProChar).ToString();
                    }
                    else if (X.GetType().Name == "DateTimePicker")
                    {
                        ((DateTimePicker)X).Value = Convert.ToDateTime(tools.GetValueFull(Entity, X.Tag.ToString().ToLower(), ProChar));
                    }
                    else if (X.GetType().Name == "NumericUpDown")
                    {
                        ((NumericUpDown)X).Value = Convert.ToInt32(tools.GetValueFull(Entity, X.Tag.ToString().ToLower(), ProChar));
                    }
                    else if (X.GetType().Name == "Label")
                    {
                        ((Label)X).Text = tools.GetValueFull(Entity, X.Tag.ToString().ToLower(), ProChar).ToString();
                    }
                }

            }
        }
        public static object ControlsToEntityFull(Control.ControlCollection objContain, Type ojbType)
        {
            Tools tools = new Tools();
            object Entity = ojbType.Assembly.CreateInstance(ojbType.FullName);
            for (int i = 0; i < objContain.Count; i++)
            {
                Control X = objContain[i];
                if (X.Tag != null && X.Tag.ToString() != "")
                {
                    if (X.GetType().Name == "TextBox")
                    {
                        if (((TextBox)X).Text != "")
                            tools.SetValueFullName(Entity, X.Tag.ToString().ToLower(), ((TextBox)X).Text, ProChar);
                    }
                    else if (X.GetType().Name == "ComboBox")
                    {
                        if (((ComboBox)X).Text != "")
                            tools.SetValueFullName(Entity, X.Tag.ToString().ToLower(), ((ComboBox)X).Text, ProChar);
                    }
                    else if (X.GetType().Name == "DateTimePicker")
                    {
                        tools.SetValueFullName(Entity, X.Tag.ToString().ToLower(), ((DateTimePicker)X).Value, ProChar);
                    }
                    else if (X.GetType().Name == "NumericUpDown")
                    {
                        tools.SetValueFullName(Entity, X.Tag.ToString().ToLower(), ((NumericUpDown)X).Value, ProChar);
                    }
                }

            }
            return Entity;
        }

        public static void ClearControls(Control.ControlCollection objContain)
        {
            Tools tools = new Tools();
            for (int i = 0; i < objContain.Count; i++)
            {
                Control X = objContain[i];
                if (X.GetType().Name == "TextBox")
                {
                    ((TextBox)X).Text = "";
                       
                }
                else if (X.GetType().Name == "ComboBox")
                {
                    ((ComboBox)X).Text = "";
                        
                }
                else if (X.GetType().Name == "DateTimePicker")
                {
                    ((DateTimePicker)X).Value = DateTime.Now;
                }
                else if (X.GetType().Name == "NumericUpDown")
                {
                    ((NumericUpDown)X).Value = 0;
                }
            }         
        }
        #endregion

        public static string MD5ComputeHash(string s)
        {
            //将 s 转换为字节数组
            Byte[] dataToHash = (new UnicodeEncoding()).GetBytes(s);

            //使用由加密配置系统返回的 MD5 实例从 String 1 创建哈希值
            byte[] hashvalue = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(dataToHash);
            return BitConverter.ToString(hashvalue).Replace("-", "");
        }

        /// <summary>
        /// 对输入不正确的错误提示
        /// </summary>
        /// <param name="MessageStr"></param>
        /// <returns></returns>
        public static bool ShowFalse(string MessageStr)
        {
            MessageBox.Show(MessageStr, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public static void InitDataToListBox(DataSet ds, bool isClear, ListBox box, string IndexName)
        {
            if (isClear) box.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    box.Items.Add(ds.Tables[0].Rows[i][IndexName].ToString());
            }
        }


        public static void LvwChecked(ListView lvw, bool bFlag)
        {
            for (int i = 0; i < lvw.Items.Count; i++)
            {
                lvw.Items[i].Checked = bFlag;
            }
        }



        //public static DataSet SearchTable(SqlExecute exsql, string TableName,string wheresqladd, IDbTransaction tran)
        //{
        //    try
        //    {
        //        string strsql = "select * from " + TableName + " where 1=1";
        //        if (wheresqladd != "")
        //        {
        //            strsql += " and " + wheresqladd;
        //        }
        //        return exsql.Execute((System.Data.SqlClient.SqlTransaction)tran, strsql);
        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
        //}

        /// <summary>
        /// 将一个数字补齐0，并去一定长度
        /// </summary>
        /// <param name="num"></param>
        /// <param name="Legth"></param>
        /// <returns></returns>
        public static string FormatStringAsLength(int num, int Legth)
        {
            string temp = num.ToString();
            if (temp.Length > Legth)
            { temp = temp.Substring(temp.Length - Legth, Legth); }
            else
            {
                for (int i = 0; i < Legth; i++)
                {
                    temp = "0" + temp;
                }
                temp = temp.Substring(temp.Length - Legth, Legth);
            }
            return temp;
        }

        //public static void NoticeMsgPort(FileSystem.FrmMain varMain)
        //{
        //    try
        //    {
        //        varMain.ReFreshData();
        //    }
        //    catch { }
        //}

        public static string ConfigueGet(string FileName, string section, string keyName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + FileName;
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNode node = document.DocumentElement.SelectSingleNode(section);
                if (node != null)
                {
                    XmlNode node2 = node.SelectSingleNode(keyName);
                    if (node2 != null)
                    {
                        return node2.InnerXml;
                    }
                    return "";
                }
                return "";
            }
            return "";
        }

        public static string ConfigueGet(string FileName, string section, string keyName, string attributeName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + FileName;
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNode node = document.DocumentElement.SelectSingleNode(section);
                if (node != null)
                {
                    XmlNode node2 = node.SelectSingleNode(keyName);
                    if ((node2 != null) && (node2.Attributes[attributeName] != null))
                    {
                        return node2.Attributes[attributeName].Value;
                    }
                    return "";
                }
                return "";
            }
            return "";
        }

        public static void ConfigueSet(string FileName, string section, string keyName, string keyValue)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + FileName;
            XmlDocument document = new XmlDocument();
            if (File.Exists(path))
            {
                document.Load(path);
            }
            else
            {
                document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><root/>");
            }
            XmlNode newChild = document.DocumentElement.SelectSingleNode(section);
            if (newChild == null)
            {
                newChild = document.CreateNode(XmlNodeType.Element, section, "");
                document.DocumentElement.AppendChild(newChild);
            }
            XmlNode node2 = newChild.SelectSingleNode(keyName);
            if (node2 == null)
            {
                node2 = document.CreateNode(XmlNodeType.Element, keyName, "");
                newChild.AppendChild(node2);
            }
            node2.InnerXml = keyValue;
            document.Save(path);
        }

        public static void ConfigueSet(string FileName, string section, string keyName, string attributeName, string attributeValue)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + FileName;
            XmlDocument document = new XmlDocument();
            if (File.Exists(path))
            {
                document.Load(path);
            }
            else
            {
                document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><root/>");
            }
            XmlNode newChild = document.DocumentElement.SelectSingleNode(section);
            if (newChild == null)
            {
                newChild = document.CreateNode(XmlNodeType.Element, section, "");
                document.DocumentElement.AppendChild(newChild);
            }
            XmlNode node2 = newChild.SelectSingleNode(keyName);
            if (node2 == null)
            {
                node2 = document.CreateNode(XmlNodeType.Element, keyName, "");
                newChild.AppendChild(node2);
            }
            if (node2.Attributes[attributeName] == null)
            {
                XmlAttribute node = document.CreateAttribute(attributeName);
                node2.Attributes.Append(node);
            }
            node2.Attributes[attributeName].Value = attributeValue;
            document.Save(path);
        }

        public static bool ReadXmltoDataSet(string FileName, ref DataSet ds)
        {
            ds = new DataSet();
            ds.ReadXml(FileName);
            return (ds != null);
        }

        public static bool WriteDataSetToXml(string FileName, DataSet ds)
        {
            if (ds != null)
            {
                ds.WriteXml(FileName);
                return true;
            }
            return false;
        }

    }
}

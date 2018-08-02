using System;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SMSmanage.Class;
using SMSmanage.Entity;

namespace SMSmanage
{
    public partial class FrmMain : Form
    {

        TcpListener listenercontrol;
        Thread threadcontrol;
        System.Windows.Forms.Timer time_Day;
        System.Windows.Forms.Timer time_LoadData;
        System.Windows.Forms.Timer time_SendMessage;
        string tag_Day ="";
        delegate void SetDgvCallback(string str, int recount, string fileid, string fileboxid);

        #region  提取SMS.dll  API
        [DllImport("sms.dll", EntryPoint = "Sms_Connection")]
        public static extern uint Sms_Connection(string CopyRight, uint Com_Port, uint Com_BaudRate, out string Mobile_Type, out string CopyRightToCOM);

        [DllImport("sms.dll", EntryPoint = "Sms_Disconnection")]
        public static extern uint Sms_Disconnection();

        [DllImport("sms.dll", EntryPoint = "Sms_Send")]
        public static extern uint Sms_Send(string Sms_TelNum, string Sms_Text);

        [DllImport("sms.dll", EntryPoint = "Sms_Receive")]
        public static extern uint Sms_Receive(string Sms_Type, out string Sms_Text);

        [DllImport("sms.dll", EntryPoint = "Sms_Delete")]
        public static extern uint Sms_Delete(string Sms_Index);

        [DllImport("sms.dll", EntryPoint = "Sms_AutoFlag")]
        public static extern uint Sms_AutoFlag();

        [DllImport("sms.dll", EntryPoint = "Sms_NewFlag")]
        public static extern uint Sms_NewFlag();
        #endregion

        public FrmMain()
        {
            InitializeComponent();

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //每日提醒
            time_Day = new System.Windows.Forms.Timer();
            time_Day.Interval = 10000;
            time_Day.Tick += Time_Day_Tick;

            time_LoadData = new System.Windows.Forms.Timer();
            time_LoadData.Interval = 1000;
            time_LoadData.Tick += Time_LoadData_Tick;

            time_SendMessage = new System.Windows.Forms.Timer();
            time_SendMessage.Interval = 6000;
            time_SendMessage.Tick += Time_SendMessage_Tick;

            progressBar.Value = 0;
            progressBar.Maximum = 30;
            tag_Day = DateTime.Now.ToString("yyyyMMdd");

            IPAddress ipcontorl = GetFirstIP();
            int portcontrol = 9304;
            listenercontrol = new TcpListener(ipcontorl, portcontrol);
            listenercontrol.Start();
            threadcontrol = new Thread(new ThreadStart(ListeningControl));
            threadcontrol.Start();

            string TypeStr = "";
            string CopyRightToCOM = "";
            string CopyRightStr = "//上海迅赛信息技术有限公司,网址www.xunsai.com//";

            string smsport ="5";

            
            try
            {
                uint n = Sms_Connection(CopyRightStr, uint.Parse(smsport), 115200, out TypeStr, out CopyRightToCOM);
                if (n == 1)
                {
                    time_Day.Start();
                    time_LoadData.Start();
                    time_SendMessage.Start();
                }
                else MessageBox.Show("短信发送设备打开失败！");
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            FrmSet frm = new FrmSet();
            frm.ShowDialog();
        }

        private string SendSMS(string phone, string message)
        {
            string sendState = "发送失败";
            if (phone.Substring(0, 2) == "86" && phone.Length == 13) phone = phone.Substring(2, 11);
            uint n = Sms_Send(phone, message);
            Thread.Sleep(1500);
            if (n == 1) sendState = "发送成功";
            return sendState;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtPhone.Text != "")
            {
                MessageBox.Show(SendSMS(txtPhone.Text, txtMessage.Text));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FrmSearch frm = new FrmSearch();
            frm.ShowDialog();
        }

        private void btnHidden_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                listenercontrol.Stop();
                threadcontrol.Abort();
            }
            catch
            {

            }
        }

        //接收交换箱程序发过来的指令
        private void ListeningControl()
        {
            while (true)
            {
                TcpClient client = listenercontrol.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                byte[] bytes = new byte[client.ReceiveBufferSize];
                stream.Read(bytes, 0, (int)client.ReceiveBufferSize);
                string getdata = Encoding.ASCII.GetString(bytes);
                getdata = Encoding.UTF8.GetString(bytes);
                ExecCommand(getdata.TrimEnd('\0'));
            }
        }

        //接收交换箱程序发过来的指令
        public void ExecCommand(string cmdData)
        {
            //MessageBox.Show(cmdData);
            if (cmdData == "") return;
            string[] s = cmdData.Split('|');//SMS|箱名|FILEID|FILEBOXID
            if (s[0] == "SMS") CreateNewRow(s[1], 0, s[2], s[3]);
        }

        public void CreateNewRow(string boxname, int recount, string fileid, string fileboxid)
        {
            if (dgvSendSms.InvokeRequired)
            {
                SetDgvCallback setdgvCallbak = new SetDgvCallback(CreateNewRow);
                dgvSendSms.Invoke(setdgvCallbak, boxname, recount, fileid, fileboxid);
            }
            else
            {
                txtMessage.Text = boxname;
                MessageUserClass dao = new MessageUserClass();
                MessageUserEntity ety = new MessageUserEntity();
                ety.Deptname = boxname;
                DataTable dt = dao.Search(ety, null).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string[] username = dt.Rows[0]["UserName"].ToString().Split(',');
                    string[] phonenum = dt.Rows[0]["PhoneNum"].ToString().Split(',');
                    if (recount > 0)
                    {   //重复发送
                        for (int i = 0; i < username.Length; i++)
                        {
                            int index = dgvSendSms.Rows.Add();
                            dgvSendSms.Rows[index].Cells[0].Value = dt.Rows[0]["DeptName"].ToString();
                            dgvSendSms.Rows[index].Cells[1].Value = username[i];
                            dgvSendSms.Rows[index].Cells[2].Value = phonenum[i];
                            dgvSendSms.Rows[index].Cells[3].Value = GlobalFunction.ConfigueGet("ProgrameSet.xml", "appSettings", "defMessage");
                            dgvSendSms.Rows[index].Cells[5].Value = "未发送";
                            dgvSendSms.Rows[index].Cells[6].Value = "已重复发送" + recount + "次";

                        }
                    }
                    else
                    {   //首次发送
                        if (username.Length > 1)
                        {
                            int index = dgvSendSms.Rows.Add();
                            dgvSendSms.Rows[index].Cells[0].Value = dt.Rows[0]["DeptName"].ToString();
                            dgvSendSms.Rows[index].Cells[1].Value = username[1];
                            dgvSendSms.Rows[index].Cells[2].Value = phonenum[1];
                            dgvSendSms.Rows[index].Cells[3].Value = GlobalFunction.ConfigueGet("ProgrameSet.xml", "appSettings", "defMessage");
                            dgvSendSms.Rows[index].Cells[5].Value = "未发送";
                        }
                        else
                        {
                            int index = dgvSendSms.Rows.Add();
                            dgvSendSms.Rows[index].Cells[0].Value = dt.Rows[0]["DeptName"].ToString();
                            dgvSendSms.Rows[index].Cells[1].Value = username[0];
                            dgvSendSms.Rows[index].Cells[2].Value = phonenum[0];
                            dgvSendSms.Rows[index].Cells[3].Value = GlobalFunction.ConfigueGet("ProgrameSet.xml", "appSettings", "defMessage");
                            dgvSendSms.Rows[index].Cells[5].Value = "未发送";
                        }

                        EmergFileEntity ety_emerg = new EmergFileEntity();
                        EmergFileClass dao_emerg = new EmergFileClass();
                        ety_emerg.FileBoxId = fileboxid;
                        ety_emerg.Fileid = fileid;
                        ety_emerg.State = "1";
                        DataTable dt_emerg = dao_emerg.Search(ety_emerg, null).Tables[0];
                        if (dt_emerg.Rows.Count < 1)
                        {
                            ety_emerg.Recount = "1";
                            dao_emerg.Insert(ety_emerg, null);
                        }
                    }
                }
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            frm.ShowDialog();
            this.Visible = true;
        }


        private void Time_SendMessage_Tick(object sender, EventArgs e)
        {
            if (dgvSendSms.Rows.Count > 0 && WeekTest())
            {
                string temp = "";
                temp = SendSMS(dgvSendSms.Rows[0].Cells[2].Value.ToString(), dgvSendSms.Rows[0].Cells[3].Value.ToString());
                SendMessageClass dao = new SendMessageClass();
                SendMessageEntity ety = new SendMessageEntity();
                ety.Senddept = dgvSendSms.Rows[0].Cells[0].Value.ToString();
                ety.Senduser = dgvSendSms.Rows[0].Cells[1].Value.ToString();
                ety.Phonenum = dgvSendSms.Rows[0].Cells[2].Value.ToString();
                ety.Message = dgvSendSms.Rows[0].Cells[3].Value.ToString();
                if (dgvSendSms.Rows[0].Cells[6].Value != null) ety.Remark = dgvSendSms.Rows[0].Cells[6].Value.ToString();
                ety.State = temp;
                ety.Sendtime = DateTime.Now;
                dao.Insert(ety, null);
                dgvSendSms.Rows.RemoveAt(0);
                txtSend.Text = Convert.ToString(int.Parse(txtSend.Text == "" ? "0" : txtSend.Text) + 1);
            }
        }

        private void Time_LoadData_Tick(object sender, EventArgs e)
        {
            progressBar.Value += 1;
            txtWaitSend.Text = dgvSendSms.Rows.Count.ToString();
            if (progressBar.Value != 30) return;
            ReduceMemory();
            progressBar.Value = 0;
            if (WeekTest()) return;
            EmergFileClass dao = new EmergFileClass();
            EmergFileEntity ety = new EmergFileEntity();
            ety.State = "1";
            DataTable dt_emerg = dao.Search(ety, null).Tables[0];
            if (dt_emerg.Rows.Count > 0)
            {
                for (int i = 0; i < dt_emerg.Rows.Count; i++)
                {
                    //查询该件是否还在交换箱中
                    DataTable dt_filebox = dao.SearchForReadySms(dt_emerg.Rows[i]["FileBoxId"].ToString()).Tables[0];
                    if (dt_filebox.Rows.Count > 0)
                    {
                        bool tag = false;

                        if (int.Parse(dt_emerg.Rows[i]["Recount"].ToString()) < 3)
                        {
                            CreateNewRow(dt_filebox.Rows[0]["GetDept"].ToString(), int.Parse(dt_emerg.Rows[i]["Recount"].ToString()), dt_filebox.Rows[0]["FileId"].ToString(), dt_filebox.Rows[0]["FileBoxId"].ToString());
                            tag = true;
                        }

                        //修改重发次数
                        if (tag)
                        {
                            ety = new EmergFileEntity();
                            ety.Emergfileid = dt_emerg.Rows[0][0].ToString();
                            ety.Recount = Convert.ToString(int.Parse(dt_emerg.Rows[i]["Recount"].ToString()) + 1);
                            dao.Update(ety, null);
                        }
                    }
                    else
                    {
                        //修改发送状态
                        ety = new EmergFileEntity();
                        ety.Emergfileid = dt_emerg.Rows[0][0].ToString();
                        ety.State = "0";
                        dao.Update(ety, null);
                    }
                }
            }
        }

        //检测周末或者上班时间
        private bool WeekTest()
        {
            if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                if (DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 08:30:00") && DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 17:30:00"))
                    return true;
                else return false;
            }
            else return false;
        }

        private void Time_Day_Tick(object sender, EventArgs e)
        {
            if (tag_Day != DateTime.Now.ToString("yyyyMMdd")) tag_Day = DateTime.Now.ToString("yyyyMMdd");
            else return;
            if (!WeekTest()) return;
            EmergFileClass dao_emerg_day = new EmergFileClass();
            MessageUserClass dao_user = new MessageUserClass();
            MessageUserEntity ety_user = new MessageUserEntity();
            DataTable dt = dao_emerg_day.SearchAllGetDept().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ety_user.Deptname = dt.Rows[0][0].ToString();
                DataTable dt_user = dao_user.Search(ety_user, null).Tables[0];
                if (dt_user.Rows.Count > 0)
                {
                    string[] username = dt_user.Rows[0]["UserName"].ToString().Split(',');
                    string[] phonenum = dt_user.Rows[0]["PhoneNum"].ToString().Split(',');
                    if (username.Length > 1)
                    {
                        int index = dgvSendSms.Rows.Add();
                        dgvSendSms.Rows[index].Cells[0].Value = dt.Rows[0]["DeptName"].ToString();
                        dgvSendSms.Rows[index].Cells[1].Value = username[1];
                        dgvSendSms.Rows[index].Cells[2].Value = phonenum[1];
                        dgvSendSms.Rows[index].Cells[3].Value = "你有文件待取！";
                        dgvSendSms.Rows[index].Cells[5].Value = "未发送";
                        dgvSendSms.Rows[index].Cells[6].Value = "每日文件提醒";
                    }
                    else
                    {
                        int index = dgvSendSms.Rows.Add();
                        dgvSendSms.Rows[index].Cells[0].Value = dt.Rows[0]["DeptName"].ToString();
                        dgvSendSms.Rows[index].Cells[1].Value = username[0];
                        dgvSendSms.Rows[index].Cells[2].Value = phonenum[0];
                        dgvSendSms.Rows[index].Cells[3].Value = "你有文件待取！";
                        dgvSendSms.Rows[index].Cells[5].Value = "未发送";
                        dgvSendSms.Rows[index].Cells[6].Value = "每日文件提醒";
                    }
                }
            }
        }

        #region 获取本机IP地址
        public static IPAddress[] GetLocalIP()
        {
            string name = Dns.GetHostName();
            IPHostEntry me = Dns.GetHostEntry(name);
            return me.AddressList;
        }

        public static IPAddress GetFirstIP()
        {
            IPAddress[] ips = GetLocalIP();
            foreach (IPAddress ip in ips)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    continue;
                return ip;
            }
            return ips != null && ips.Length > 0 ? ips[0] : new IPAddress(0x0);
        }
        #endregion


        private void btnDept_Click(object sender, EventArgs e)
        {
            FrmDept frm = new FrmDept();
            frm.ShowDialog();
        }

        //释放内存
        private void ReduceMemory()
        {
            Process A = Process.GetCurrentProcess();
            A.MaxWorkingSet = Process.GetCurrentProcess().MaxWorkingSet;
            A.Dispose();
        }
    }
}

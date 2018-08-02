using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SMSmanage
{
    public partial class FrmSet : Form
    {
        public FrmSet()
        {
            InitializeComponent();
        }

        private void FrmSet_Load(object sender, EventArgs e)
        {
            txtPort.Text = GlobalFunction.ConfigueGet("ProgrameSet.xml", "hardSetting", "port");
            txtCentreNum.Text = GlobalFunction.ConfigueGet("ProgrameSet.xml", "hardSetting", "centerNumber");
            txtProtrocalType.Text = GlobalFunction.ConfigueGet("ProgrameSet.xml", "hardSetting", "ProtrocalType");
            txtHoldTime.Text = GlobalFunction.ConfigueGet("ProgrameSet.xml", "hardSetting", "HoldTIme");

            txtIpPort.Text = GlobalFunction.ConfigueGet("ProgrameSet.xml", "hardSetting", "ListenIp");
            txtListenPort.Text = GlobalFunction.ConfigueGet("ProgrameSet.xml", "hardSetting", "ListenPort");
            txtServeIp.Text = GlobalFunction.ConfigueGet("ProgrameSet.xml", "hardSetting", "ServeIp");
            txtMessage.Text = GlobalFunction.ConfigueGet("ProgrameSet.xml", "appSettings", "defMessage");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPort.Text != "") GlobalFunction.ConfigueSet("ProgrameSet.xml", "hardSetting", "port", txtPort.Text);
            if (txtCentreNum.Text != "") GlobalFunction.ConfigueSet("ProgrameSet.xml", "hardSetting", "centerNumber", txtCentreNum.Text);
            if (txtProtrocalType.Text != "") GlobalFunction.ConfigueSet("ProgrameSet.xml", "hardSetting", "ProtrocalType", txtProtrocalType.Text);
            if (txtHoldTime.Text != "") GlobalFunction.ConfigueSet("ProgrameSet.xml", "hardSetting", "HoldTIme", txtHoldTime.Text);

            if (txtIpPort.Text != "") GlobalFunction.ConfigueSet("ProgrameSet.xml", "hardSetting", "ListenIp", txtIpPort.Text);
            if (txtListenPort.Text != "") GlobalFunction.ConfigueSet("ProgrameSet.xml", "hardSetting", "ListenPort", txtListenPort.Text);
            if (txtServeIp.Text != "") GlobalFunction.ConfigueSet("ProgrameSet.xml", "hardSetting", "ServeIp", txtServeIp.Text);
            if (txtMessage.Text != "") GlobalFunction.ConfigueSet("ProgrameSet.xml", "appSettings", "defMessage", txtMessage.Text);
        }
    }
}

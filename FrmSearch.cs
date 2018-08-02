using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SMSmanage.Entity;
using SMSmanage.Class;

namespace SMSmanage
{
    public partial class FrmSearch : Form
    {
        public FrmSearch()
        {
            InitializeComponent();
        }

        private void FrmSearch_Load(object sender, EventArgs e)
        {
            dtpBegin.Value = DateTime.Now.AddMonths(-1);
            LoadData();
        }

        private void LoadData()
        {
            SendMessageEntity ety = new SendMessageEntity();
            SendMessageClass dao = new SendMessageClass();
            ety = (SendMessageEntity)GlobalFunction.ControlsToEntity(panel1.Controls, typeof(SendMessageEntity));
            DataTable dt = dao.Search(ety, dtpBegin.Value.ToString(), dtpEnd.Value.ToString()).Tables[0];
            dgvSendSms.AutoGenerateColumns = false;
            dgvSendSms.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SendMessageEntity ety = new SendMessageEntity();
            SendMessageClass dao = new SendMessageClass();
            ety = (SendMessageEntity)GlobalFunction.ControlsToEntity(panel1.Controls, typeof(SendMessageEntity));
            DataTable dt = dao.Search(ety, dtpBegin.Value.ToString(), dtpEnd.Value.ToString()).Tables[0];
            dgvSendSms.AutoGenerateColumns = false;
            dgvSendSms.DataSource = dt;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SMSmanage.Class;
using SMSmanage.Entity;

namespace SMSmanage
{
    public partial class FrmDept : Form
    {
        string messageuserid = "";
        public FrmDept()
        {
            InitializeComponent();
        }

        private void FrmDept_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            MessageUserClass dao = new MessageUserClass();
            DataTable dt = dao.Search("").Tables[0];
            dgvMessageUser.AutoGenerateColumns = false;
            dgvMessageUser.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDeptName.Text != "" && txtPhoneNum.Text != "" && txtUserName.Text != "")
            {
                MessageUserClass dao = new MessageUserClass();
                MessageUserEntity ety = (MessageUserEntity)GlobalFunction.ControlsToEntity(panel1.Controls, typeof(MessageUserEntity));
                if (ety.Phonenum.Substring(0, 2) != "86") ety.Phonenum = "86" + ety.Phonenum;
                if (messageuserid == "")
                {
                    dao.Insert(ety, null);
                }
                else
                {
                    ety.Messageuserid = messageuserid;
                    dao.Update(ety, null);
                }
                LoadData();
                GlobalFunction.ClearControls(panel1.Controls);
                messageuserid = "";
            }
            else
                MessageBox.Show("不能空保存！");
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (messageuserid!="")
            {
                MessageUserClass dao = new MessageUserClass();
                dao.Delete(messageuserid);
                LoadData();
                GlobalFunction.ClearControls(panel1.Controls);
                messageuserid = "";
            }
        }

        private void dgvMessageUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>-1)
            {
                messageuserid = dgvMessageUser.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtDeptName.Text = dgvMessageUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtUserName.Text = dgvMessageUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtPhoneNum.Text = dgvMessageUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtRemark.Text = dgvMessageUser.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        private void btnDao_Click(object sender, EventArgs e)
        {
            MessageUserClass dao = new MessageUserClass();
            DataTable dt = dao.SearchForDeptName().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MessageUserEntity ety = new MessageUserEntity();
                ety.Deptname = dt.Rows[i][0].ToString();
                ety.Username = dt.Rows[i][0].ToString();
                ety.Phonenum = "86123";
                dao.Insert(ety, null);
            }
            
        }
    }
}

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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            this.ControlBox = false;  
        }

        private void txtPassWord_TextChanged(object sender, EventArgs e)
        {
            if (txtPassWord.Text=="spring")
            {
                this.Close();
            }
        }
    }
}

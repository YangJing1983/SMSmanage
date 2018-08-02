using System;

namespace SMSmanage.Entity
{
	public class MessageUserEntity
	{
		#region All Propertys Is Auto Created.
		private bool _Messageuserid_flag= false;
		public bool Messageuserid_flag{
			get{return _Messageuserid_flag;}
		}
		private string _Messageuserid =  "";
		public string Messageuserid{
			get{return _Messageuserid;}
			set{_Messageuserid = value; _Messageuserid_flag = true;}
		}
		private string _Messageuserid_index = "";
		public string Messageuserid_index {
			get{return _Messageuserid_index;}
			set{_Messageuserid_index = value;}
		}

		private bool _Deptname_flag= false;
		public bool Deptname_flag{
			get{return _Deptname_flag;}
		}
		private string _Deptname =  "";
		public string Deptname{
			get{return _Deptname;}
			set{_Deptname = value; _Deptname_flag = true;}
		}

		private bool _Username_flag= false;
		public bool Username_flag{
			get{return _Username_flag;}
		}
		private string _Username =  "";
		public string Username{
			get{return _Username;}
			set{_Username = value; _Username_flag = true;}
		}

		private bool _Phonenum_flag= false;
		public bool Phonenum_flag{
			get{return _Phonenum_flag;}
		}
		private string _Phonenum =  "";
		public string Phonenum{
			get{return _Phonenum;}
			set{_Phonenum = value; _Phonenum_flag = true;}
		}

        private bool _Remark_flag = false;
        public bool Remark_flag
        {
            get { return _Remark_flag; }
        }
        private string _Remark = "";
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; _Remark_flag = true; }
        }

		#endregion
		//Messageuserid, Deptname, Username, Phonenum
		private string _entity_Table = "MessageUser";
		public string entity_Table {
			get{return _entity_Table;}
			set{_entity_Table = value;}
		}
	}
}

using System;

namespace SMSmanage.Entity
{
	public class SendMessageEntity
	{
		#region All Propertys Is Auto Created.
		private bool _Sendmessageid_flag= false;
		public bool Sendmessageid_flag{
			get{return _Sendmessageid_flag;}
		}
		private string _Sendmessageid =  "";
		public string Sendmessageid{
			get{return _Sendmessageid;}
			set{_Sendmessageid = value; _Sendmessageid_flag = true;}
		}
		private string _Sendmessageid_index = "";
		public string Sendmessageid_index {
			get{return _Sendmessageid_index;}
			set{_Sendmessageid_index = value;}
		}

		private bool _Senddept_flag= false;
		public bool Senddept_flag{
			get{return _Senddept_flag;}
		}
		private string _Senddept =  "";
		public string Senddept{
			get{return _Senddept;}
			set{_Senddept = value; _Senddept_flag = true;}
		}

		private bool _Senduser_flag= false;
		public bool Senduser_flag{
			get{return _Senduser_flag;}
		}
		private string _Senduser =  "";
		public string Senduser{
			get{return _Senduser;}
			set{_Senduser = value; _Senduser_flag = true;}
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

		private bool _Message_flag= false;
		public bool Message_flag{
			get{return _Message_flag;}
		}
		private string _Message =  "";
		public string Message{
			get{return _Message;}
			set{_Message = value; _Message_flag = true;}
		}

		private bool _Sendtime_flag= false;
		public bool Sendtime_flag{
			get{return _Sendtime_flag;}
		}
		private DateTime _Sendtime =new DateTime(1900, 1, 1);
		public DateTime Sendtime{
			get{return _Sendtime;}
			set{_Sendtime = value; _Sendtime_flag = true;}
		}

		private bool _State_flag= false;
		public bool State_flag{
			get{return _State_flag;}
		}
		private string _State =  "";
		public string State{
			get{return _State;}
			set{_State = value; _State_flag = true;}
		}

		private bool _Remark_flag= false;
		public bool Remark_flag{
			get{return _Remark_flag;}
		}
		private string _Remark =  "";
		public string Remark{
			get{return _Remark;}
			set{_Remark = value; _Remark_flag = true;}
		}

		#endregion
		//Sendmessageid, Senddept, Senduser, Phonenum, Message, 
		//Sendtime, State, Remark
		private string _entity_Table = "SendMessage";
		public string entity_Table {
			get{return _entity_Table;}
			set{_entity_Table = value;}
		}
	}
}

using System;

namespace SMSmanage.Entity
{
	public class EmergFileEntity
	{
		#region All Propertys Is Auto Created.
		private bool _Emergfileid_flag= false;
		public bool Emergfileid_flag{
			get{return _Emergfileid_flag;}
		}
		private string _Emergfileid =  "";
		public string Emergfileid{
			get{return _Emergfileid;}
			set{_Emergfileid = value; _Emergfileid_flag = true;}
		}
		private string _Emergfileid_index = "";
		public string Emergfileid_index {
			get{return _Emergfileid_index;}
			set{_Emergfileid_index = value;}
		}

		private bool _Fileid_flag= false;
		public bool Fileid_flag{
			get{return _Fileid_flag;}
		}
		private string _Fileid =  "";
		public string Fileid{
			get{return _Fileid;}
			set{_Fileid = value; _Fileid_flag = true;}
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

        private bool _Recount_flag = false;
        public bool Recount_flag
        {
            get { return _Recount_flag; }
        }
        private string _Recount = "";
        public string Recount
        {
            get { return _Recount; }
            set { _Recount = value; _Recount_flag = true; }
        }

        private bool _FileBoxId_flag = false;
        public bool FileBoxId_flag
        {
            get { return _FileBoxId_flag; }
        }
        private string _FileBoxId = "";
        public string FileBoxId
        {
            get { return _FileBoxId; }
            set { _FileBoxId = value; _FileBoxId_flag = true; }
        }


		#endregion
		//Emergfileid, Fileid, State
		private string _entity_Table = "EmergFile";
		public string entity_Table {
			get{return _entity_Table;}
			set{_entity_Table = value;}
		}
	}
}

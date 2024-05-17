using Infragistics.Controls.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO.Model
{
	public class USER
	{
		public int ID_User { get; set; }
		public string Pass { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string CCCD { get; set; }
		public string Position { get; set; }
		public string FullName { get; set; }
		public string DateCreate { get; set; } = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        public int Working_Days { get; set; }
		
		public USER() { }
		USER(int iD_User, string pass, string userName, string email, string phoneNumber, string cCCD, string position, int working_Days)
		{
			ID_User = iD_User;
			Pass = pass;
			UserName = userName;
			Email = email;
			PhoneNumber = phoneNumber;
			CCCD = cCCD;
			Position = position;
			Working_Days = working_Days;
		}
	}
}

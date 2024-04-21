using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO
{
	public class AccountDAO
	{
		private static AccountDAO instance;
		public static AccountDAO Instance
		{
			get
			{
				if (instance == null)
					instance = new AccountDAO();
				return instance;
			}
			private set => instance = value;
		}
		public AccountDAO() { }

		public DataTable Login(string username, string password)
		{
			string query = $"exec TM_USER_Get_All '{username}','{password}'";
			DataTable dt = DataProvider.Instance.ExecuteQuery(query);
			return dt;
		}
		public DataTable Get_All()
		{
			string query = $"EXEC  TM_USER_GetAll_By_Boss";
			DataTable dt = DataProvider.Instance.ExecuteQuery(query);
			return dt;
		}
		public int Insert(string id_User,string pass,string userName,string email, string phone_Number,string cccd,string position,int working_Day)
		{
			string query = $"EXEC TM_USER_Insert '{id_User}','{pass}','{userName}','{email}','{phone_Number}','{cccd}','{position}',{working_Day}";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}
		public int Update(string id_User, string pass, string userName, string email, string phone_Number, string cccd, string position, int working_Day)
		{
			string query = $"EXEC TM_USER_Update '{id_User}','{pass}','{userName}','{email}','{phone_Number}','{cccd}','{position}',{working_Day}";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}
	}
}

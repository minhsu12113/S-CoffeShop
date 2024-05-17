using CoffeShop.DAO.Model;
using System;
using System.Collections.Generic;
using System.Data;

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
		
		public int Insert(USER user)
		{
			string query = $"EXEC TM_USER_Insert '{user.Pass}','{user.UserName}','{user.Email}','{user.PhoneNumber}','{user.CCCD}', N'{user.Position}', {user.Working_Days}, N'{user.FullName}', N'{user.DateCreate}'";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}
		
		public int Update(USER user)
		{
			string query = $"EXEC TM_USER_Update '{user.ID_User}','{user.Pass}','{user.UserName}','{user.Email}','{user.PhoneNumber}','{user.CCCD}', N'{user.Position}', {user.Working_Days}, N'{user.FullName}'";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}

		public int Delete(USER user)
		{
			string query = $"EXEC TM_User_Delete '{user.ID_User}'";
			int data=DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}
	}
}

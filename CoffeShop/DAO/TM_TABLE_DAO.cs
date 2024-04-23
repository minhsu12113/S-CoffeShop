﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO
{
	public class TM_TABLE_DAO
	{
		private static TM_TABLE_DAO instance;
		public static TM_TABLE_DAO Instance
		{
			get
			{
				if (instance == null)
					instance = new TM_TABLE_DAO();
				return instance;
			}
			private set => instance = value;
		}
		private TM_TABLE_DAO() { }
		public DataTable Get_Table(int id_Area)
		{
			string query = $"exec TM_TABLE_GetAll {id_Area}";
			DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
			return dataTable;
		}
		public int Insert(TABLE table)
		{
			string query = $"EXEC TM_TABLE_Insert '{table.Table_Name}','{table.Status}','{table.ID_Area}'";
			int data=DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}
		public int Update_Status(TABLE table)
		{
			string query = $"EXEC TM_TABLE_Update_ChangeStatus '{table.ID_Table}','{table.Status}','{table.ID_Area}'";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}

	}
}

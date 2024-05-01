using CoffeShop.DAO.Model;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Model
{
    public class AreaModel : BindableBase
    {
        public AREA Data { get; set; } = new AREA();		 
		public int  Id
		{
			get { return Data.ID_Area; }
			set { Data.ID_Area = value; OnPropertyChanged(); }
		}

        public string Name
        {
            get { return Data.Area_Name; }
            set { Data.Area_Name = value; OnPropertyChanged(); }
        }

        public static AreaModel ParseArea(DataTable dt)
        {
            AreaModel area = null;
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    area = new AreaModel();
                    area.Id = int.Parse(dt.Rows[i]["ID_Area"].ToString());
                    area.Name = dt.Rows[i]["Area_Name"].ToString();
                }
            }
            return area;
        }

        public static List<AreaModel> ParseAreaList(DataTable dt)
        {
            List<AreaModel> areas = null;
            if (dt != null)
            {
                areas = new List<AreaModel>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var area = new AreaModel();
                    area.Id = int.Parse(dt.Rows[i]["ID_Area"].ToString());
                    area.Name = dt.Rows[i]["Area_Name"].ToString();
                    areas.Add(area);
                }
            }
            return areas;
        }
    }
}

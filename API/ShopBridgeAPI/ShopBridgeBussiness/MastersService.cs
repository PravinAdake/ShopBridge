using Newtonsoft.Json;
using ShopBridgeCommon;
using ShopBridgeDataAccess;
using ShopBridgeRepository;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ShopBridgeBussiness
{
    public class MastersService : IMasters
    {
        DBHelper dBHelper = new DBHelper();
        public string GetInventoryDetails(string reqtype)
        {
            string response = null;
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = new SqlParameter[1];
                prms[0] = new SqlParameter("@Type", reqtype);
                dt = dBHelper.GetTableFromSP("[Masters].[usp_Get_Category_SubCategory_Details]", prms);
                if (dt != null)
                {
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            response = JsonConvert.SerializeObject(dt);
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
            }
            return response;
        }
    }
}

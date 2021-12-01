using Newtonsoft.Json;
using ShopBridgeCommon;
using ShopBridgeDataAccess;
using ShopBridgeModel;
using ShopBridgeRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ShopBridgeBussiness
{
    public class TransationsService : ITransations
    {
        DBHelper dBHelper = new DBHelper();
        public string PostInventoryDetails(Insert_Inventory_Request request)
        {
            string response = null;
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = new SqlParameter[8];
                prms[0] = new SqlParameter("@T_ID", request.T_ID);
                prms[1] = new SqlParameter("@SubCat_ID", request.SubCat_ID);
                prms[2] = new SqlParameter("@Quantity", request.Quantity);
                prms[3] = new SqlParameter("@Unit_Price", request.Unit_Price);
                prms[4] = new SqlParameter("@Total_Price", request.Total_Price);
                prms[5] = new SqlParameter("@Product_Description", request.Product_Description);
                prms[6] = new SqlParameter("@Created_By", request.Created_By);
                prms[7] = new SqlParameter("@Type", request.Type);
                dt = dBHelper.GetTableFromSP("[Transactions].[usp_Insert_Inventory_Details]", prms);
                if (dt != null)
                {
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            response = JsonConvert.SerializeObject(dt.Rows[0]["Msg"].ToString());
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

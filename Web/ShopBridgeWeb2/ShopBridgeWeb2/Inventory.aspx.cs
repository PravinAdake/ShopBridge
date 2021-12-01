using Newtonsoft.Json;
using ShopBridgeWeb2.Model;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using System.Net.Http.Headers;
using ShopBridgeWeb2.AppCode;
using System.Web.UI.WebControls;

namespace ShopBridgeWeb2
{
    public partial class Inventory : System.Web.UI.Page
    {
        #region"GlobleDeclaration"
        string ShopBridgeBaseURL = System.Configuration.ConfigurationManager.AppSettings["ShopBridgeBaseURL"];
        #endregion

        #region"Page_Load"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindGrid();
                this.BindDropdown();
            }
        }
        #endregion

        #region"BindGrid"
        public void BindGrid()
        {
            DataTable dtMsg = new DataTable();
            dtMsg = BindData("Get_inventory_details");
            if (dtMsg != null)
            {
                if (dtMsg.Rows.Count > 0)
                {
                    grdInventory.DataSource = dtMsg;
                    grdInventory.DataBind();
                }
                else
                {
                    grdInventory.DataSource = null;
                    grdInventory.DataBind();
                }
            }
            else
            {
                grdInventory.DataSource = null;
                grdInventory.DataBind();
            }
        }
        #endregion

        #region"BindGrid"
        public void BindDropdown()
        {
            try
            {
                DataTable dtMsgddl = new DataTable();
                dtMsgddl = BindData("Get_category_details");
                if (dtMsgddl != null)
                {
                    if (dtMsgddl.Rows.Count > 0)
                    {
                        DataView view = new DataView(dtMsgddl);
                        DataTable dtMsgddlcategory = view.ToTable(true, "Cat_ID", "Cat_Name");
                        ddlCategory.DataTextField = "Cat_Name";
                        ddlCategory.DataValueField = "Cat_ID";
                        ddlCategory.DataSource = dtMsgddlcategory;
                        ddlCategory.DataBind();
                        ddlCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));

                        ddlSubCategory.DataTextField = "SubCat_Name";
                        ddlSubCategory.DataValueField = "SubCat_ID";
                        ddlSubCategory.DataSource = dtMsgddl;
                        ddlSubCategory.DataBind();
                        ddlSubCategory.Items.Insert(0, new ListItem("--Select SubCategory--", "0"));
                    }
                    else
                    {
                        ddlCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));
                        ddlSubCategory.Items.Insert(0, new ListItem("--Select SubCategory--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
            }
        }
        #endregion

        #region"BindData"
        public DataTable BindData(string strreqtype)
        {
            DataTable dtMsg = new DataTable();
            try
            {
                string reqtype = Convert.ToString(strreqtype);
                var result = JsonConvert.SerializeObject(reqtype);
                var content = new StringContent(result, Encoding.UTF8, "application/json");
                DataTable dt = new DataTable();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ShopBridgeBaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync(string.Format("Masters/GetInventoryDetails?reqtype=" + reqtype)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseresult = response.Content.ReadAsStringAsync().Result;
                        var Filter_result = JsonConvert.DeserializeObject(responseresult);
                        dtMsg = (DataTable)JsonConvert.DeserializeObject(Filter_result.ToString(), typeof(DataTable));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
            }
            return dtMsg;
        }
        #endregion

        #region"btnSubmit_Click"
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSubCategory.SelectedValue != "0" && txtProduct.Text != "" && txtUnitPrice.Text != "" && txtQuantity.Text != "")
                {
                    Actions("I", 0, Convert.ToInt16(ddlSubCategory.SelectedValue), Convert.ToInt64(txtUnitPrice.Text), Convert.ToString(txtProduct.Text), Convert.ToInt32(txtQuantity.Text), Convert.ToDecimal(Request.Form[txtTotal_Price.UniqueID]));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter mandetory fields to submit data! Please try after some time!!')", true);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
            }
            finally
            {
                Clear();
            }
        }
        #endregion

        #region"Actions"
        public void Actions(string ActionsName, Int64 TransactionId, int SubCat_ID = 0, Int64 Unit_Price = 0, string ProductDesc = "", Int32 Quantity = 0, decimal TotalPrice = 0)
        {
            try
            {
                Insert_Inventory_Request req = new Insert_Inventory_Request();
                req.T_ID = TransactionId;
                req.SubCat_ID = Convert.ToInt16(SubCat_ID);
                req.Unit_Price = Convert.ToInt64(Unit_Price);
                req.Product_Description = Convert.ToString(ProductDesc);
                req.Quantity = Convert.ToInt32(Quantity);
                req.Total_Price = Convert.ToDecimal(TotalPrice);
                req.Created_By = "Admin";
                req.Type = ActionsName;
                var result = JsonConvert.SerializeObject(req);
                var content = new StringContent(result, Encoding.UTF8, "application/json");
                DataTable dt = new DataTable();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ShopBridgeBaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync(string.Format("Transactions/PostInventoryDetails"), content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseresult = response.Content.ReadAsStringAsync().Result;
                        string Filter_result2 = responseresult.Substring(1, responseresult.Length - 2);
                        var Filter_result = JsonConvert.DeserializeObject(responseresult);
                        //DataTable dtMsg = (DataTable)JsonConvert.DeserializeObject(Filter_result.ToString(), typeof(DataTable));
                        if (responseresult != null)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Convert.ToString(Filter_result) + "')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error Ocured while submit data! Please try after some time!!')", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
            }
            finally
            {
                this.Clear();
                this.BindGrid();
            }
        }
        #endregion

        #region"Clear"
        public void Clear()
        {
            ddlCategory.SelectedValue = "0";
            ddlSubCategory.SelectedValue = "0";
            txtProduct.Text = "";
            txtQuantity.Text = "";
            txtTotal_Price.Text = "";
            txtUnitPrice.Text = "";

        }
        #endregion

        #region"grdInventory_RowEditing"
        protected void grdInventory_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            grdInventory.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }
        #endregion

        #region"grdInventory_RowCancelingEdit"
        protected void grdInventory_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            grdInventory.EditIndex = -1;
            this.BindGrid();
        }
        #endregion

        #region"grdInventory_PageIndexChanging"
        protected void grdInventory_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            grdInventory.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        #endregion

        #region"grdInventory_RowUpdating"
        protected void grdInventory_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = grdInventory.Rows[e.RowIndex];
                string transactionid = Convert.ToString(grdInventory.DataKeys[e.RowIndex].Values[0]);
                string product = (row.FindControl("txtProduct_Description") as TextBox).Text;
                string quantity = (row.FindControl("txtQuantity") as TextBox).Text;
                string unitprice = (row.FindControl("txtUnit_Price") as TextBox).Text;
                decimal totalpricecal = (Convert.ToDecimal(quantity) * Convert.ToDecimal(unitprice));
                if (transactionid != "0" && product != "" && unitprice != "" && quantity != "")
                {
                    Actions("U", Convert.ToInt64(transactionid), 0, Convert.ToInt64(unitprice), Convert.ToString(product), Convert.ToInt32(quantity), Convert.ToDecimal(totalpricecal));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter mandetory fields to submit data! Please try after some time!!')", true);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
            }
            finally
            {
                grdInventory.EditIndex = -1;
                this.BindGrid();
            }
        }
        #endregion

        #region"grdInventory_RowDeleting"
        protected void grdInventory_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            try
            {
                int TransactionId = Convert.ToInt32(grdInventory.DataKeys[e.RowIndex].Values[0]);
                Actions("D", TransactionId);
            }
            catch (Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
            }
            finally
            {
                this.BindGrid();
            }
        }
        #endregion
    }
}
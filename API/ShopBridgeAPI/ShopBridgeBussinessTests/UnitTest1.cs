using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopBridgeBussiness;
using ShopBridgeModel;

namespace ShopBridgeBussinessTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetInventoryDetails()
        {
            MastersService ms = new MastersService();
            var expecteddata = "[{\"Cat_ID\":1,\"Cat_Name\":\"Electronics\",\"SubCat_ID\":1,\"SubCat_Name\":\"Computer-Desktop\"},{\"Cat_ID\":1,\"Cat_Name\":\"Electronics\",\"SubCat_ID\":2,\"SubCat_Name\":\"Computer-Laptop\"}]";
            var res = ms.GetInventoryDetails("Get_category_details");
            Assert.AreEqual(res, expecteddata);
        }

        [TestMethod]
        public void TestPostInventoryDetails_Insert()
        {
            TransationsService ts = new TransationsService();
            Insert_Inventory_Request request=new Insert_Inventory_Request();
            request.T_ID = 1;
            request.SubCat_ID = 1;
            request.Quantity = 1;
            request.Unit_Price = 2;
            request.Total_Price = 2;
            request.Product_Description = "Product Details";
            request.Created_By = "Admin";
            request.Type = "I";
            var expecteddata = "\"Inventory record Inserted successfully.\"";
            var res = ts.PostInventoryDetails(request);
            Assert.AreEqual(res, expecteddata);
        }

        [TestMethod]
        public void TestPostInventoryDetails_Update()
        {
            TransationsService ts = new TransationsService();
            Insert_Inventory_Request request = new Insert_Inventory_Request();
            request.T_ID = 1;
            request.SubCat_ID = 1;
            request.Quantity = 1;
            request.Unit_Price = 2;
            request.Total_Price = 2;
            request.Product_Description = "Product Details";
            request.Created_By = "Admin";
            request.Type = "U";
            var expecteddata = "\"Inventory record Updated successfully.\"";
            var res = ts.PostInventoryDetails(request);
            Assert.AreEqual(res, expecteddata);
        }

        [TestMethod]
        public void TestPostInventoryDetails_Delete()
        {
            TransationsService ts = new TransationsService();
            Insert_Inventory_Request request = new Insert_Inventory_Request();
            request.T_ID = 1;
            request.SubCat_ID = 1;
            request.Quantity = 1;
            request.Unit_Price = 2;
            request.Total_Price = 2;
            request.Product_Description = "Product Details";
            request.Created_By = "Admin";
            request.Type = "D";
            var expecteddata = "\"Inventory record Deleted successfully.\"";
            var res = ts.PostInventoryDetails(request);
            Assert.AreEqual(res, expecteddata);
        }

        /// <summary>
        /// Failed Test Case ==> Pass Wrong Type
        /// </summary>

        [TestMethod]
        public void TestPostInventoryDetails_Delete2()
        {
            TransationsService ts = new TransationsService();
            Insert_Inventory_Request request = new Insert_Inventory_Request();
            request.T_ID = 1;
            request.SubCat_ID = 1;
            request.Quantity = 1;
            request.Unit_Price = 2;
            request.Total_Price = 2;
            request.Product_Description = "Product Details";
            request.Created_By = "Admin";
            request.Type = "I";
            var expecteddata = "\"Inventory record Deleted successfully.\"";
            var res = ts.PostInventoryDetails(request);
            Assert.AreEqual(res, expecteddata);
        }
    }
}

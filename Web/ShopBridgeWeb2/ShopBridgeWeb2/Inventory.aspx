<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="ShopBridgeWeb2.Inventory" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function CalTotal() {
            debugger;
            var qty = 0;
            var unitamt = 0;
            var totalamt = 0.00;
            if ($("#<%=txtUnitPrice.ClientID%>").val() != "" && $("#<%=txtQuantity.ClientID%>").val() != "") {
                qty = $("#<%=txtQuantity.ClientID%>").val();
                unitamt = $("#<%=txtUnitPrice.ClientID%>").val();
                totalamt = parseFloat(qty * unitamt);
                $("#<%=txtTotal_Price.ClientID%>").val(totalamt);
            }
            else {
                $("#<%=txtTotal_Price.ClientID%>").val(0);
            }

        }

        function ValidateFields() {
            if ($("#<%=ddlCategory.ClientID%>").val() == "0") {
                $("#<%=ddlCategory.ClientID%>").focus();
                alert("Please select Product Category");
                return false;
            }
            if ($("#<%=ddlSubCategory.ClientID%>").val() == "0") {
                $("#<%=ddlSubCategory.ClientID%>").focus();
                alert("Please select Product SubCategory");
                return false;
            }
            if ($("#<%=txtProduct.ClientID%>").val() == "") {
                $("#<%=txtProduct.ClientID%>").focus();
                alert("Please Enter Product Information");
                return false;
            }
            if ($("#<%=txtQuantity.ClientID%>").val() == "") {
                $("#<%=txtQuantity.ClientID%>").focus();
                alert("Please Enter Quantity");
                return false;
            }
            if ($("#<%=txtUnitPrice.ClientID%>").val() == "") {
                $("#<%=txtUnitPrice.ClientID%>").focus();
                alert("Please Enter Unit Price");
                return false;
            }
            if ($("#<%=txtTotal_Price.ClientID%>").val() == "") {
                $("#<%=txtTotal_Price.ClientID%>").focus();
                alert("Please Enter Total Price");
                return false;
            }
        }
    </script>
    <div class="container">
        <div class="row">
            <div class="col-sm-4">
                <h4>Category</h4>
                <asp:DropDownList ID="ddlCategory" runat="server" Width="100%" CssClass="from-control" ClientIDMode="Static"></asp:DropDownList>
            </div>
            <div class="col-sm-4">
                <h4>Sub Category</h4>
                <asp:DropDownList ID="ddlSubCategory" runat="server" Width="100%" CssClass="from-control" ClientIDMode="Static"></asp:DropDownList>
            </div>
            <div class="col-sm-4">
                <h4>Product Description</h4>
                <asp:TextBox ID="txtProduct" runat="server" Width="100%" CssClass="from-control" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-4">
                <h4>Quantity</h4>
                <asp:TextBox ID="txtQuantity" runat="server" Width="100%" CssClass="from-control" ClientIDMode="Static" onkeypress="return isNumber(event)" MaxLength="5"></asp:TextBox>
            </div>
            <div class="col-sm-4">
                <h4>Unit Price</h4>
                <asp:TextBox ID="txtUnitPrice" runat="server" Width="100%" CssClass="from-control" ClientIDMode="Static" onkeypress="return isNumber(event)" onblur="CalTotal()" MaxLength="10"></asp:TextBox>
            </div>
            <div class="col-sm-4">
                <h4>Price</h4>
                <asp:TextBox ID="txtTotal_Price" ReadOnly="true" runat="server" Width="100%" CssClass="from-control" ClientIDMode="Static" onkeypress="return isNumber(event)"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <h4>&nbsp;</h4>
            <asp:Button ID="btnSubmit" Text="Add" runat="server" CssClass="btn btn-primary" OnClientClick="return ValidateFields();" OnClick="btnSubmit_Click" />
        </div>
        <hr />
        <div class="row">
            <h4>&nbsp;</h4>
            <asp:GridView ID="grdInventory" CssClass="table table-striped" runat="server" AutoGenerateColumns="false" DataKeyNames="T_ID" OnRowEditing="grdInventory_RowEditing" OnRowCancelingEdit="grdInventory_RowCancelingEdit" PageSize="10" AllowPaging="true" OnPageIndexChanging="grdInventory_PageIndexChanging" OnRowUpdating="grdInventory_RowUpdating" OnRowDeleting="grdInventory_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="Category" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Cat_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Cat_Name") %>' Width="140"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" Sub Category" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblSubCategory" runat="server" Text='<%# Eval("SubCat_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblSubCategory" runat="server" Text='<%# Eval("SubCat_Name") %>' Width="140"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblUnit_Price" runat="server" Text='<%# Eval("Unit_Price") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUnit_Price" runat="server" Text='<%# Eval("Unit_Price") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Description" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblProduct_Description" runat="server" Text='<%# Eval("Product_Description") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtProduct_Description" runat="server" Text='<%# Eval("Product_Description") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Price" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_Price" runat="server" Text='<%# Eval("Total_Price") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="txtTotal_Price" runat="server" Text='<%# Eval("Total_Price") %>' Width="140"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ButtonType="Link" ShowEditButton="true"
                        ItemStyle-Width="150" />
                    <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Delete">
                        <ControlStyle CssClass="btn btn-danger btn-sm" />
                    </asp:ButtonField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

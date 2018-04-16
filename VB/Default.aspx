<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings: NorthwindConnectionString %>"
                SelectCommand="SELECT * FROM Products"
                UpdateCommand="UPDATE Products SET ProductSelection=@ProductSelection WHERE ProductID=@ProductID">
                <UpdateParameters>
                    <asp:Parameter Name="ProductSelection" Type="Boolean" />
                    <asp:Parameter Name="ProductID" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <dx:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="SqlDataSource1" KeyFieldName="ProductID" SettingsBehavior-AllowSelectByRowClick="true"
                OnSelectionChanged="ASPxGridView1_SelectionChanged">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0">
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="ProductID" VisibleIndex="1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ProductName" VisibleIndex="2"></dx:GridViewDataColumn>
                </Columns>
                <SettingsBehavior ProcessSelectionChangedOnServer="true" />
            </dx:ASPxGridView>
        </div>
    </form>
</body>
</html>
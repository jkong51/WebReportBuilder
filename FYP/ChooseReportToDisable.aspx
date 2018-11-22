<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ChooseReportToDisable.aspx.cs" Inherits="FYP.ChooseReportToDisable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .head {
            text-align: center;
        }

        .GridHeader {
            text-align: center !important;
            padding: 5px;
        }

        .grid {
            font-size: 20px;
        }

        .hiddencol {
            display: none;
        }

        .button {
            /*font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;*/
            font-family: 'lato', sans-serif;
            background-color: rgb(80, 142, 245);
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            right:0;
            margin-right:20px;
            position:absolute;
            margin-top:-50px;
            font-size: 16px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            border: 2px solid rgb(80, 142, 245);
        }
        .button:hover {
                background-color: white;
                color: rgb(80, 142, 245);
                cursor:pointer;
        }
        .GridPager a, .GridPager span
    {
            font-size:18px;
        display: block;
        height: 25px;
        width: 25px;
        font-weight: bold;
        text-align: center;
        text-decoration: none;
        vertical-align:central;
    }
    .GridPager a
    {
        background-color: #f5f5f5;
        color: #969696;
        border: 1px solid #969696;
    }
    .GridPager span
    {
        background-color: #A1DCF2;
        color: #000;
        border: 1px solid #3AC0F2;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="text-align:left;padding-left:20px;">
        <h3>Select report to disable</h3>    <asp:Button ID="Save" runat="server" Text="Save" CssClass="button" OnClick="Save_Click"/>

    </div>
    <div style="width:100%;padding:20px;">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FormNameConnectionString %>" SelectCommand="SELECT reportID, name, status, dateGenerated, description FROM Report WHERE (staffId = @staffId)">
            <SelectParameters>
                <asp:SessionParameter Name="staffId" SessionField="userId" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView CssClass="grid" AllowPaging="true" PagerSettings-Position="Top" PageSize="10" ID="GridView1" Width="100%" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" DataKeyNames="reportID" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound">
            <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
            <Columns>
                <asp:BoundField HeaderStyle-CssClass="GridHeader hiddencol" ItemStyle-CssClass="hiddencol" DataField="reportID" HeaderText="reportID" InsertVisible="False" ReadOnly="True" SortExpression="reportID" />
                <asp:BoundField HeaderStyle-CssClass="GridHeader hiddencol" ItemStyle-CssClass="hiddencol" DataField="status" HeaderText="status" InsertVisible="False" ReadOnly="True" SortExpression="status" />
                <asp:BoundField HeaderStyle-CssClass="GridHeader" DataField="name" HeaderText="Name" SortExpression="name" />
                <asp:BoundField HeaderStyle-CssClass="GridHeader" DataField="dateGenerated" HeaderText="Date Generated" SortExpression="dateGenerated"  ApplyFormatInEditMode="true" DataFormatString="{0:d}"/>
                <asp:BoundField HeaderStyle-CssClass="GridHeader" DataField="description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" SortExpression="description" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>Status</HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkDisable" runat="server"/>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#333333" BorderColor="#333333" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
    </div>

</asp:Content>

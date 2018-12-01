<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ChooseReportUpdate.aspx.cs" Inherits="FYP.ChooseReportUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .GridHeader {
            text-align: center !important;
            padding: 5px;
        }

        .grid {
            font-size: 20px;

        }
        .hiddencol { display: none; }
        .button{
            padding: 0;
border: none;
background: none;
margin-left:-60px;
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
        <h3>Select report for editing purposes</h3>

    </div>
    <div style="width:100%;padding:20px;">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FormNameConnectionString %>" SelectCommand="SELECT Report.reportID, Report.name, Report.dateGenerated, Report.description FROM Report INNER JOIN report_right ON Report.reportID = report_right.reportId WHERE (Report.status = 1) AND (report_right.staffId = @staffId) AND (report_right.rights LIKE '%E%')">
            <SelectParameters>
                <asp:SessionParameter Name="staffId" SessionField="userId" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView CssClass="grid" ID="GridView1" AllowPaging="true" PagerSettings-Position="Top" PagerStyle-CssClass="pagerStyle" Border="0" Width="100%" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" DataKeyNames="reportID" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
            <Columns>
                <asp:BoundField HeaderStyle-CssClass="GridHeader hiddencol" ItemStyle-CssClass="hiddencol" DataField="reportID" HeaderText="reportID" InsertVisible="False" ReadOnly="True" SortExpression="reportID" />
                <asp:BoundField HeaderStyle-CssClass="GridHeader" DataField="dateGenerated" HeaderText="Date Generated" SortExpression="dateGenerated"  ApplyFormatInEditMode="true" DataFormatString="{0:d}"/>
                <asp:BoundField HeaderStyle-CssClass="GridHeader" DataField="name" HeaderText="Name" SortExpression="name" />               
                <asp:BoundField HeaderStyle-CssClass="GridHeader" DataField="description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" SortExpression="description" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>Action</HeaderTemplate>
                    
                <ItemTemplate>
                <asp:Button ID="editReportBtn" CssClass="button" runat="server" Text="Edit" CausesValidation="false" OnClick="editReportBtn_Click"/>
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

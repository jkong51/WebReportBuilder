﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="RetrieveReport.aspx.cs" Inherits="FYP.Retrieve_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--    <script type="text/javascript">

</script>--%>
    <style type="text/css">
        .GridHeader {
            text-align: center !important;
            padding: 5px;
        }

        .GridHeader1 {
            width: 20%;
        }

        .GridHeader2 {
            width: 35%;
        }

        .GridHeader3 {
            width: 35%;
        }

        .GridHeader4 {
            width: 5%;
        }

        .grid {
            font-size: 20px;
        }

        .hiddencol {
            display: none;
        }

        .button {
            padding: 0;
            border: none;
            background: none;
        }

        .GridPager a, .GridPager span {
            font-size: 18px;
            display: block;
            height: 25px;
            width: 25px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
            vertical-align: central;
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }

        .GridPager span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }

        .grid td {
            border-right: 1px solid #ddd;
        }

        .descPadd {
            padding-left: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: left; padding-left: 20px;">
        <h3>Select report for viewing purposes</h3>
    </div>
    <div style="width: 100%; padding: 20px;">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FormNameConnectionString %>" SelectCommand="SELECT Report.reportID, Report.name, Report.dateGenerated, Report.description FROM Report INNER JOIN report_right ON Report.reportID = report_right.reportId WHERE (Report.status = 1) AND (report_right.staffId = @staffId) AND (report_right.rights LIKE '%R%')">
            <SelectParameters>
                <asp:SessionParameter Name="staffId" SessionField="userId" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="GridView1" PagerSettings-Position="Top" HeaderStyle-CssClass="GridHeader" AllowPaging="true" Width="100%" runat="server" CssClass="grid" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" DataKeyNames="reportID" DataSourceID="SqlDataSource1">
            <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
            <Columns>
                <asp:BoundField HeaderStyle-CssClass="GridHeader hiddencol" ItemStyle-CssClass="hiddencol" DataField="reportID" HeaderText="reportID" InsertVisible="False" ReadOnly="True" SortExpression="reportID" />
                <asp:BoundField HeaderStyle-CssClass="GridHeader GridHeader1" DataField="dateGenerated" HeaderText="Date Generated" SortExpression="dateGenerated" ApplyFormatInEditMode="true" DataFormatString="{0:d}" />
                <asp:BoundField HeaderStyle-CssClass="GridHeader GridHeader2" DataField="name" HeaderText="Name" SortExpression="name" />           
                <asp:BoundField HeaderStyle-CssClass="GridHeader GridHeader3" ItemStyle-CssClass="descPadd" DataField="description" HeaderText="Description" SortExpression="description" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderStyle-CssClass="GridHeader4" ShowHeader="False">
                    <HeaderTemplate>Action</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Button ID="viewReportBtn" CssClass="button" runat="server" Text="View" CausesValidation="false" OnClick="viewReportBtn_Click"  />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" CssClass="head" />
            <PagerStyle BackColor="#333333" BorderColor="#333333" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
    </div>

</asp:Content>

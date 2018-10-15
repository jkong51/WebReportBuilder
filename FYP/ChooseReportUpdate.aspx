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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:left;padding-left:20px;">
        <h3>Select report for editing purposes</h3>

    </div>
    <div style="width:100%;padding:20px;">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FormNameConnectionString %>" SelectCommand="SELECT DISTINCT Report.reportID, Report.name, Report.dateGenerated, Report.description
FROM                   Report INNER JOIN
                               report_right ON Report.reportID = report_right.reportId
WHERE                  (Report.status = 1) AND (Report.staffId = @staffId) OR
                               (Report.status = 1) AND (report_right.rights LIKE '%E%') AND (report_right.staffId = @staffId)">
            <SelectParameters>
                <asp:SessionParameter Name="staffId" SessionField="userId" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView CssClass="grid" ID="GridView1" Width="100%" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" DataKeyNames="reportID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField HeaderStyle-CssClass="GridHeader" DataField="reportID" HeaderText="reportID" InsertVisible="False" ReadOnly="True" SortExpression="reportID" />
                <asp:BoundField HeaderStyle-CssClass="GridHeader" DataField="name" HeaderText="name" SortExpression="name" />
                <asp:BoundField HeaderStyle-CssClass="GridHeader" DataField="dateGenerated" HeaderText="dateGenerated" SortExpression="dateGenerated" />
                <asp:BoundField HeaderStyle-CssClass="GridHeader" DataField="description" HeaderText="description" SortExpression="description" />
                <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                <asp:Button ID="editReportBtn" runat="server" Text="Edit" CausesValidation="false" OnClick="editReportBtn_Click"/>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
    </div>
</asp:Content>

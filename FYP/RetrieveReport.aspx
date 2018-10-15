<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="RetrieveReport.aspx.cs" Inherits="FYP.Retrieve_report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:left;padding-left:20px;background-color:white">
        <h3>Select report to show or update</h3>
    </div>
    <div style="width:100%;padding:20px;">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FormNameConnectionString %>" SelectCommand="SELECT reportID, name, dateGenerated, description FROM Report WHERE (staffId = @staffId) AND (status = 1)">
            <SelectParameters>
                <asp:SessionParameter Name="staffId" SessionField="userId" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="GridView1" Width="100%" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" DataKeyNames="reportID" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                <asp:Button ID="editReportBtn" runat="server" CausesValidation="false"
                 Text="Edit" />
                 <asp:Button ID="viewReportBtn" runat="server" CausesValidation="false"
                 Text="View" />
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="reportID" HeaderText="reportID" InsertVisible="False" ReadOnly="True" SortExpression="reportID" />
                <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                <asp:BoundField DataField="dateGenerated" HeaderText="dateGenerated" SortExpression="dateGenerated" />
                <asp:BoundField DataField="description" HeaderText="description" SortExpression="description" />
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

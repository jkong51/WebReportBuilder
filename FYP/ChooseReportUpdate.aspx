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
        <asp:GridView CssClass="grid" ID="GridView1" Width="100%" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" DataKeyNames="reportID" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField HeaderStyle-CssClass="GridHeader hiddencol" ItemStyle-CssClass="hiddencol" DataField="reportID" HeaderText="reportID" InsertVisible="False" ReadOnly="True" SortExpression="reportID" />
                <asp:BoundField HeaderStyle-CssClass="GridHeader" DataField="name" HeaderText="Name" SortExpression="name" />
                <asp:BoundField HeaderStyle-CssClass="GridHeader" DataField="dateGenerated" HeaderText="Date Generated" SortExpression="dateGenerated"  ApplyFormatInEditMode="true" DataFormatString="{0:d}"/>
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
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
    </div>
</asp:Content>

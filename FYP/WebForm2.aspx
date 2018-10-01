<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="FYP.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Label">Report Title: </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRptTitle" runat="server" ToolTip="Report title" placeholder="Report title"></asp:TextBox> <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Label">Report Description: </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRptDesc" runat="server" ToolTip="Report description" placeholder="Report description"></asp:TextBox> <br />
            </td>
        </tr>
    </table>
</div>
</asp:Content>

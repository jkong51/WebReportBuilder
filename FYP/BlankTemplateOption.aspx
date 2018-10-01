<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlankTemplateOption.aspx.cs" Inherits="FYP.BlankTemplateOption" %>
<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            height: 100%;
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
        }
        table{
            font-size:15px;
            padding:15px 15px;
        }

        .td1{
            width:200px;
            text-align:right;
            padding-right:30px;
        }
        .textbox{
            border-radius:10px 10px;
            font-size:15px;
            width:300px;
            padding:8px 8px;
            
        }
        .lstbox{
            border-radius:10px 10px;
            font-size:15px;
            width:300px;
            padding:8px 8px;
        }
        th{
            font-size:22px;
        }
        .chkbox input{
            width:15px;
            height:15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

    <table align="center">
        <tr>
            <th colspan="2">
                Header
            </th>
        </tr>
        <tr>
            <td class="td1">
                <asp:Label ID="Label1" runat="server" Text="Label"><strong>Report Title</strong></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRptTitle" CssClass="textbox" runat="server" ToolTip="Report title" placeholder="Report title"></asp:TextBox> <br />
            </td>
        </tr>
        <tr>
            <td class="td1">
                <asp:Label ID="Label2" runat="server" Text="Label"><strong>Report Description</strong></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRptDesc" CssClass="textbox" runat="server" ToolTip="Report description" placeholder="Report description"></asp:TextBox> <br />
            </td>
        </tr>
        <tr>
            <td class="td1">
                <asp:Label ID="Label3" runat="server" Text="Label"><strong>Show Date</strong></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="CheckBox1" CssClass="chkbox" runat="server" />
            </td>
        </tr>     
        <tr>
            <th colspan="2">
                Body
            </th>
        </tr>
        
        <tr>
            <td class="td1">
                <asp:Label ID="Label4" runat="server" Text="&lt;strong&gt;Select the form's displayed data&lt;/strong&gt;"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="lstbox" DataSourceID="SqlDataSource1" DataTextField="title" DataValueField="formId">
                    <asp:ListItem>Select Table</asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FormNameConnectionString %>" SelectCommand="SELECT [formId], [title] FROM [Form] WHERE ([staffId] = @staffId)">
                    <SelectParameters>
                        <asp:SessionParameter Name="staffId" SessionField="staffId" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <th colspan="2">
                Footer
            </th>
        </tr>
        <tr>
            <td class="td1">
                <asp:Label ID="Label5" runat="server" Text="Label"><strong>Show Page Number</strong></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="CheckBox2" CssClass="chkbox" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td1">
                <asp:Label ID="Label6" runat="server" Text="Label"><strong>Show Total Row</strong></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="CheckBox3" CssClass="chkbox" runat="server" />
            </td>
        </tr>
    </table>

</div>
    </form>
</body>
</html>

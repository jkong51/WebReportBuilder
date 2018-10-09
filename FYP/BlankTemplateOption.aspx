<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlankTemplateOption.aspx.cs" Inherits="FYP.BlankTemplateOption" %>
<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
 $("#addFilterBtn").click(function () {
     $("#filterTable").each(function () {
         var tds = '<tr>';
         jQuery.each($('tr:last td', this), function () {
             tds += '<td>' + $(this).html() + '</td>';
         });
         tds += '</tr>';
         if ($('tbody', this).length > 0) {
             $('tbody', this).append(tds);
         } else {
             $(this).append(tds);
         }
     });
});
    </script>
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
                <asp:Label ID="Label4" runat="server" Text="&lt;strong&gt;Select the form to be used&lt;/strong&gt;"></asp:Label>
            </td>
            <td>
                          <asp:DropDownList ID="DropDownList1" runat="server" CssClass="lstbox" DataSourceID="SqlDataSource1" DataTextField="title" DataValueField="formId" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                    <asp:ListItem>Select Table</asp:ListItem>
                </asp:DropDownList>
                          <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FormNameConnectionString %>" SelectCommand="SELECT formId, title FROM Form WHERE (staffId = @staffId)">
                <SelectParameters>
                        <asp:SessionParameter Name="staffId" SessionField="userId" />
                    </SelectParameters>
                          </asp:SqlDataSource>
                      </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="&lt;strong&gt;Select the form's displayed data&lt;/strong&gt;" Visible="false"></asp:Label>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" Visible="false">
                </asp:CheckBoxList>
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
    <table align="center" id="filterTable">
        <tr><th colspan="2">Choose Filters</th></tr>
        <tbody>
        <tr>
            <asp:PlaceHolder ID="filterPlaceholder" runat="server"/>
            <td>
                <asp:DropDownList ID="selectedItemDDL1" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="conditionsDDL1" runat="server"></asp:DropDownList>
                <asp:TextBox ID="filterBox1" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="addFilterBtn" runat="server" Text="Add Filter"/>
            </td>
        </tr>
            </tbody>
    </table>

</div>
    </form>
</body>
</html>

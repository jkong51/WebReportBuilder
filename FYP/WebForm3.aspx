<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="WebForm3.aspx.cs" Inherits="FYP.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
</head>
<body>

    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="false" Font-Names="Arial"
            Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B"
            HeaderStyle-BackColor="green" AllowPaging="true"
            OnPageIndexChanging="OnPaging">

            <Columns>

                <asp:BoundField ItemStyle-Width="150px" DataField="CustomerID"
                    HeaderText="CustomerID" />

                <asp:BoundField ItemStyle-Width="150px" DataField="City"
                    HeaderText="City" />

                <asp:BoundField ItemStyle-Width="150px" DataField="Country"
                    HeaderText="Country" />

                <asp:BoundField ItemStyle-Width="150px" DataField="PostalCode"
                    HeaderText="PostalCode" />

            </Columns>
            
        </asp:GridView>
        <asp:Button runat="server" Text="Button" OnClick="btnExportPDF_Click" />
    </form>
</body>
</html>

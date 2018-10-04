<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPlaceholder.aspx.cs" Inherits="FYP.LoginPlaceholder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- change back to homepage after testing-->
            <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Homepage.aspx" OnAuthenticate="Login1_Authenticate"></asp:Login>
        </div>
    </form>
</body>
</html>

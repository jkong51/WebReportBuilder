<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPlaceholder.aspx.cs" Inherits="FYP.LoginPlaceholder" %>


<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        body {
            height: 100%;
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            text-align: center;
            background-image: url(91b674b96a3cc7b5c542e0bcd432d52e.jpg);
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center;
            background-size: cover;
        }
        .lgcss{
            text-align:center;
            padding-left:475px;
            overflow-x: hidden;
            /*padding-top: 20px;*/
            
        }
        .loginCss{
            font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            width:400px;
            height:250px;
            text-align:center;
            font-size:20px;
            align-content:center;
            color:darkslategray;
            border: 1px solid #BFBFBF;
            background-color: white;
            box-shadow: 5px 10px 5px #aaaaaa;
            transition: all .2s ease-in-out;
            font-family: "Gill Sans", sans-serif;
            cursor: pointer;
        }
        .txtcss{
            font-size: 15px;
            width: 270px;
            padding: 8px 8px;
        }
        
        .lgbtncss{
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            background-color: rgb(80, 142, 245);
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            width: auto;
            -webkit-transition-duration: 0.4s;
            transition-duration: 0.4s;
            border: 2px solid rgb(80, 142, 245);
        }
        .lgbtncss:hover {
                background-color: white;
                color: rgb(80, 142, 245);
                cursor: pointer;
            }
        
    </style>
    <title></title>
</head>
<body>
    <header>
        <div>
            <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
            <img src="Tunku-Abdul-Rahman-University-College-TARC.png" width="250" height="100"/>
            <h1 style="cursor:default">i-Report Builder               
            </h1>
        </div>
        <div>
            
        </div>
    </header>
    <form id="form1" runat="server">
        <div class="lgcss" style="text-align:center;">
            <!-- change back to homepage after testing-->
            <asp:Login ID="Login1" 
                LoginButtonStyle-CssClass="lgbtncss" 
                UserNameLabelText="Username: "  
                runat="server" 
                CssClass="loginCss" 
                DestinationPageUrl="~/Homepage.aspx" 
                OnAuthenticate="Login1_Authenticate"
                TitleText="Login" 
                DisplayRememberMe="false" 
                TextBoxStyle-CssClass="txtcss">
                <LayoutTemplate>
                    <table align="center" cellpadding="18" style="border-collapse:collapse;padding:10px">
                        <tr>
                            <td>
                                <table cellpadding="8">
                                    <tr>
                                        <th style="font-weight:500;text-align:left;font-size:23px;">ACCOUNT LOGIN</th>
                                        
                                    </tr>
                                    <tr>
                                        <td style="text-align:left;font-weight:200;font-size:15px;">
                                            <div style="padding-bottom:10px">USERNAME</div>
                                            <asp:TextBox ID="UserName" placeholder="Please enter your username" runat="server" CssClass="txtcss"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left;font-weight:200;font-size:15px">
                                            <div style="padding-bottom:10px">PASSWORD</div>
                                            <asp:TextBox ID="Password" placeholder="Please enter your password" runat="server" CssClass="txtcss" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="text-align:center;font-weight:200;font-size:15px;color:red;width:200px">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="lgbtncss" Text="Log In" ValidationGroup="Login1" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <LoginButtonStyle CssClass="lgbtncss" />
                <TextBoxStyle CssClass="txtcss" />
            </asp:Login>
        </div>
    </form>
</body>
</html>

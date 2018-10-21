﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="FYP.ViewReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        body {
            height: 100%;
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            text-align: center;
            background: rgb(204,204,204);
            height: 100%;
        }
        page {
            background: white;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
            font-family: 'Times New Roman';
        }

            page[size="A4"] {
                width: 21cm;
                height: 29.7cm;
            }

        .reportHeader1 {
            font-size: 30px;
            font-variant: small-caps;
            text-transform: uppercase;
            font-weight: bold;
            
        }

        .reportHeader2 {
            font-size: 20px;
        }

        .Mouse {
            cursor: move;
        }

        .padding{
            padding:7px;
            border-radius: 10px 10px;
            font-size: 15px;

        }
        #reportContent{
            width:100%;
            /*padding:40px;*/
        }
        
        #sidebar {
            height: 100%;
            width: 23%;
            position: fixed;
            z-index: 1;
            top: 0;
            left: 0;
            background-color: white;
            overflow-x: hidden;
            padding-top: 0px;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
        }

            #sidebar table {
                width: 100%;
                height: 100%;
                z-index: 1;
                top: 0;

            }
            .button {
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            background-color: rgb(7,153,127);
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 20px;
            width: 100%;
            -webkit-transition-duration: 0.4s;
            transition-duration: 0.4s;
            border: 2px solid rgb(80, 142, 245);
        }

        .button:hover {
            background-color: white;
            color: rgb(80, 142, 245);
            cursor: pointer;
        }
        .rpttable{ 
            width: 90%; 
            background-color: #fff;     
        }
        .tableheader{
            background-color:rgb(230,230,230);
            padding-top:20px;
            text-transform:uppercase;

        }
        .rpttable td{ 
            border-left:none;
            border-right:none;
            border-color:rgb(230,230,230);
        }
        .rpttable th{ 
            vertical-align:bottom;
            padding-bottom:0px;
            padding-top:20px;
            border:none;
        }

    </style>
    <title>View Report</title>

</head>
<body>
    <form id="form1" runat="server">
        <div id="sidebar">
                        <table class="border">
                            <tr class="border">
                                <td style="font-size: 30px;" colspan="2">
                                    <img src="Tunku-Abdul-Rahman-University-College-TARC.png" width="180" height="70" /><br />
                                    i-Report Builder
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="vertical-align: bottom; height: 30px; padding: 5px 5px">
                                    <asp:Button runat="server" ID="BtnBack" class="button" Text="Back"/></td>
                            </tr>
                        </table>

                    </div>
        <div style="padding: 50px;padding-left: 350px" id="containment-wrapper">

            <page size="A4">
            <asp:Panel runat="server" ID="reportHeader" CssClass="reportHeaderClass">
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:Label ID="Label2" runat="server"></asp:Label>
                <asp:Label ID="Label3" runat="server"></asp:Label>
            </asp:Panel>

            <%-- Report Content (Table) --%>
            <div id="reportContent" style="padding-top:175px;padding-left:40px">
                <asp:GridView ID="reportGridView"  Border="0" CellPadding="6" HeaderStyle-CssClass="tableheader" runat="server" CssClass="rpttable">
                </asp:GridView>
            </div>
            <asp:Panel ID="reportFooter" runat="server">

            </asp:Panel>
        </page>

        </div>
    </form>
</body>
</html>

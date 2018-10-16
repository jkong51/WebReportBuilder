<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="FYP.ViewReport" %>

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
        .rpttable{ 
            width: 90%; 
            background-color: #fff;       
        }
        .tableheader{
            background-color:aqua;
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

    </style>
    <title>View Report</title>

</head>
<body>
    <form id="form1" runat="server">
        <%--<div id="sidebar">
                        <table class="border">
                            <tr class="border">
                                <td style="font-size: 30px;" colspan="2">
                                    <img src="Tunku-Abdul-Rahman-University-College-TARC.png" width="180" height="70" /><br />
                                    i-Report Builder
                                </td>
                            </tr>
                            <%--<tr class="border" style="border-bottom: none">
                                <td style="padding-top: 15px; vertical-align: central" colspan="2">
                                    <strong style="font-size: larger">Header & Footer</strong>
                                    <br />
                                    <table style="padding: 5px; text-align: left">
                                        <tr>
                                            <td>Font Style
                                            </td>
                                            <td>
                                                <asp:DropDownList ClientIDMode="Static"  ID="fontFamilyDrpDwnList" AutoPostBack="True" runat="server" Width="100%" OnSelectedIndexChanged="ChangeFont">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Report Title
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRptTitle" CssClass="padding" runat="server" onkeyup="document.getElementById('lblRptTitle').innerHTML=this.value;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Report Description
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRptDesc" CssClass="padding" runat="server" onkeyup="document.getElementById('lblRptDesc').innerHTML=this.value;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Table Content
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="Button1" class="button2" Text="Edit" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>--%>
                            <%--<tr>
                                <td colspan="2" style="vertical-align: bottom; height: 30px; padding: 5px 5px">
                                    <asp:Button runat="server" ID="BtnSave" class="button" Text="Save" OnClientClick="return confirm('Are you sure you want to submit?')" OnClick="BtnSave_Click"/></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="vertical-align: bottom; height: 30px; padding: 5px 5px">
                                    <asp:Button runat="server" ID="BtnCancel" class="button" Text="Cancel" OnClick="BtnCancel_Click" /></td>
                            </tr>--%>

<%--                        </table>

                    </div>--%>
        <div style="padding: 50px;" id="containment-wrapper">

            <page size="A4">
            <asp:Panel runat="server" ID="reportHeader" CssClass="reportHeaderClass">
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:Label ID="Label2" runat="server"></asp:Label>
                <asp:Label ID="Label3" runat="server"></asp:Label>
            </asp:Panel>

            <%-- Report Content (Table) --%>
            <div id="reportContent" style="padding-top:175px;padding-left:40px">
                <asp:GridView ID="reportGridView" runat="server" CssClass="rpttable" CellPadding="10" HeaderStyle-CssClass="tableheader">
                </asp:GridView>
            </div>
            <asp:Panel ID="reportFooter" runat="server">

            </asp:Panel>
        </page>

        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="FYP.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        $(function () {
            $(".draggable").draggable(
                { containment: "page", scroll: true },
                {
                    drag: function () {
                        var offset = $(this).offset();
                        var xPos = offset.left;
                        var yPos = offset.top;
                        $('#posX').text(xPos);
                        $('#posY').text(yPos);
                    }
                });

        });
    </script>
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
            background-color:white;
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
                                <asp:Label ID="lblRptTitle" CssClass="reportHeader1 draggable Mouse ui-widget-content"  runat="server">123123123123</asp:Label><br />
            <asp:Label ID="lblRptDesc" CssClass="reportHeader2 draggable Mouse" runat="server">12323123</asp:Label><br />
            <asp:Label ID="lblDate" CssClass="reportHeader2 draggable Mouse" runat="server">213123123</asp:Label>
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

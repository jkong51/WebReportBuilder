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
            padding:40px;
        }
        .rpttable{ 
            width: 90%; 
            background-color: #fff;       
        }
        .tableheader{
            background-color:aqua;
        }
    </style>
    <title>View Report</title>

</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 50px;" id="containment-wrapper">

            <page size="A4">
            <asp:Panel runat="server" ID="hiddenPanel">
                <asp:HiddenField ID="hiddenRptTitle" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hiddenRptDesc" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hiddenRptDate" runat="server"></asp:HiddenField>
            </asp:Panel>
            <asp:Panel runat="server" ID="reportHeader" CssClass="reportHeaderClass">
            <asp:Label ID="lblRptTitle" CssClass="reportHeader1 draggable Mouse ui-widget-content"  runat="server"></asp:Label><br />
            <asp:Label ID="lblRptDesc" CssClass="reportHeader2 draggable Mouse" runat="server"></asp:Label><br />
            <asp:Label ID="lblDate" CssClass="reportHeader2 draggable Mouse" runat="server"></asp:Label>
            </asp:Panel>
            <br />
            <br />
            <br />
            <%-- Report Content (Table) --%>
            <div id="reportContent">
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

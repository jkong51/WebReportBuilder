<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="FYP.ViewReport" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <style type="text/css">
        body {
            height: 100%;
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            text-align: center;
            height: 100%;
            background: rgb(204,204,204);
        }

        page {
            background: white;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            border: solid black 1px;
            /*box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);*/
            font-family: 'Times New Roman';
        }

            page[size="A4"] {
                width: 25cm;
                height: 34.7cm;
            }

        .reportHeader1 {
            font-size: 30px;
            font-variant: small-caps;
            text-transform: uppercase;
            font-weight: bold;
            display: inline-block;
        }

        .reportHeader2 {
            font-size: 20px;
            display: inline-block;
        }

        .Mouse {
            cursor: move;
        }

        .padding {
            padding: 7px;
            border-radius: 10px 10px;
            font-size: 15px;
        }

        .button {
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            color: black;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 20px;
            -webkit-transition-duration: 0.4s;
            transition-duration: 0.4s;
            z-index: 3;
            float: left;
            font-weight: 100;
            left: 0;
            width: auto;
        }

        .button2 {
            z-index: 3;
            float: right;
            font-weight: 100;
            right: 0;
            width: auto;
        }

        .rpttable {
            width: 90%;
            background-color: #fff;
            
        }

        .tableheader {
            background-color: rgb(230,230,230);
            padding-top: 20px;
            text-transform: uppercase;
        }

        .rpttable td {
            border-left: none;
            border-right: none;
            border-color: rgb(230,230,230);
        }

        .rpttable th {
            vertical-align: bottom;
            padding-bottom: 0px;
            padding-top: 20px;
            border: none;
        }

        .GridPager a, .GridPager span {
            display: block;
            height: 15px;
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
            padding: 3px;
        }

        .GridPager a {
            padding: 3px;
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }

        .GridPager span {
            padding: 3px;
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }
        #section-to-print{
            width:100%;
        }

        @media print {
            #section-to-print {
                position: absolute;
                margin-left:-208px;
                margin-top:-50px;
                width:100%;
            }
            #section-to-adjust{
                position: absolute;
                margin-top:-50px;
                width:500px;
            }
            #reportGridView 
            {
                page-break-after:always;
            }
            #reportGridView table
            {
                page-break-after:always;
            }
        }
    </style>
    <script>
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
        }
    </script>
    <title>View Report</title>

</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManger1" EnablePageMethods="true" runat="Server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <a href="RetrieveReport.aspx" class="button">&lt; &nbsp;&nbsp;Back</a>

        <button onclick="printDiv('printPDF')" class="button button2">Save</button>

        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Print" />

        <div id="printPDF" runat="server" style="padding-top: 50px">
            <div id="containment-wrapper">

                <page size="A4" id="pdf">                
          <div id="section-to-print">                    
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label><br />
              <div id="section-to-adjust">
                  <asp:Label ID="lblDate" runat="server" Text=""></asp:Label><br />
              </div>
                
          </div>


            <%-- Report Content (Table) --%>

            <div id="reportContent" style="padding-top:175px;padding-left:40px">
                <asp:UpdatePanel ID="updatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView ID="reportGridView" OnRowDataBound="reportGridView_RowDataBound" PagerSettings-Position="Top" PagerStyle-CssClass="pagerStyle" Border="0" CellPadding="6" HeaderStyle-CssClass="tableheader" runat="server" AllowPaging="false" CssClass="rpttable" PageSize="30" OnPageIndexChanging="reportGridView_PageIndexChanging">
                <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
                </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
                    <asp:Label ID="rowCount" runat="server" Text="Label"></asp:Label>
            <asp:Panel ID="reportFooter" runat="server">
            </asp:Panel>
        </page>


            </div>
        </div>

    </form>
</body>
</html>

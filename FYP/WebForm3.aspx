<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="FYP.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <script type="text/javascript">
        var x;
        document.onload = function getRowNumber() {
            x = document.getElementById("myTable").rows.length;
            document.getElementById("demo").innerHTML = "Found " + x + " tr elements in the table.";
        }
        //jQuery(document).ready(function() {
        //    var $mainTable = document.getElementById("myTable");
        //    var splitBy = 5;
        //    var rows = $mainTable.find ( "tr" ).slice( splitBy );
        //    var $secondTable = document.getElementById("myTable").parent().append("<table id='secondTable'><tbody></tbody></table>");
        //    $secondTable.find("tbody").append(rows);
        //    $mainTable.find ( "tr" ).slice( splitBy ).remove();
        //});
        function moreRow() {

            pageNumber = document.getElementById("myTable").rows.length / 20;
            rndpageNumber = Math.ceil(pageNumber);
            for (var i = 1; i < rndpageNumber; i++) {
                document.write("<page size='A4' id='page", i+1, "'<p class='footer' style='position:'>", i+1,"</p></page>");
            }

        }
    </script>
    <script type="text/javascript">
        // Run everything when the document loads.
        

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
</style>
    <title></title>
</head>
<body onload="myFunction();">
    <form id="form1" runat="server">
        <div style="padding: 50px;" id="containment-wrapper">
            <asp:HiddenField ID="rowNo" runat="server" />
            <page size="A4"> 
                
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                <p id="demo"></p>
            <br />
            <br />
            <br />
                <table id="myTable" border="1" style="font-size:20px;margin-left:100px">                   
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    <tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr><tr>
                        <td>1</td><td>1</td><td>1</td><td>1</td><td>1</td><td>1</td>
                    </tr>
                    
                </table>
            <%-- Report Content (Table) --%>
            <div id="reportContent">
                <asp:GridView ID="reportGridView" runat="server" CssClass="rpttable" CellPadding="10" HeaderStyle-CssClass="tableheader">
                </asp:GridView>
            </div>
            <asp:Panel ID="reportFooter" runat="server">

            </asp:Panel>                    
<%--        </asp:Panel>--%>
        </page>
            <script type="text/javascript">
                moreRow();
            </script>
            <%--<script type="text/javascript">
                document.onload = function splittable() {
                    //var mainTable = document.getElementById("myTable");
                    //var splitBy = 5;
                    //var row = mainTable.find("tr").slice(splitBy);
                    //var secondTable = (mainTable).parent().append("<table id='secondTable'><tbody></tbody></table>");
                    //secondTable.find("tbody").append(row);
                    //mainTable.find("tr").slice(splitBy).remove();

                    //var $mainTable = $("#myTable");
                    //var splitBy = 5;
                    ////$mainTable.find ( "tr" ).slice( splitBy ).css( "background-color", "red" );
                    //var rows = $mainTable.find("tr").slice(splitBy);
                    //var $secondTable = $("#myTable").parent().append("<table id='secondTable' style='border:solid 1px'><tbody></tbody></table>");
                    //$secondTable.find("tbody").append(rows);
                    //$mainTable.find("tr").slice(splitBy).remove();

                }
            </script>--%>
        </div>  
    </form>
</body>
</html>

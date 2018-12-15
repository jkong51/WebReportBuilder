<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm6.aspx.cs" Inherits="FYP.WebForm6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
        }
    </script>
    <style>
        @media print{
            #printMe{
                position:absolute;
                top:100px;
                left:100px;
            }
        }
    </style>
</head>




<body>
    <form id="form1" runat="server">
        <div>
            <h1>do not print this </h1>

            <div id='printMe' style="position:absolute;top:100px;left:100px">
                Print this only 
            </div>
            <button onclick="printDiv('printMe')">Print only the above div</button>
        </div>
    </form>
</body>
</html>

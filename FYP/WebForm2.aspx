<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="FYP.WebForm2" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <%--<script type="text/javascript">

        function textCounter(field, countfield, maxlimit) {

            if (field.value.length > maxlimit)
                field.value = field.value.substring(0, maxlimit);
            else
                countfield.value = maxlimit - field.value.length;
        }

    </script>--%>
    <script type="text/javascript">
function LimtCharacters(txtMsg, CharLength, indicator) {
chars = txtMsg.value.length;
document.getElementById(indicator).innerHTML = CharLength - chars;
if (chars > CharLength) {
txtMsg.value = txtMsg.value.substring(0, CharLength);
}
}
</script>
    <style type="text/css">
        .charleft{
            border:none;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p style="top:58px;left:746.5625px;position:absolute">Testing 1</p>
        <p style="top:0;left:0;margin-top:58px;margin-left:746.5625px;position:absolute">Testing 1</p>
    </form>
</body>
</html>

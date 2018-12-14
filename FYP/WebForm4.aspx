<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="FYP.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.8.23/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.23/themes/base/jquery-ui.css" type="text/css" media="all" />
    <script type="text/javascript">
        $(function () {
            $("#resizable").resizable();
        });
        $(function () {
            $("#test").draggable().resizable({
                resize: function (e, ui) {
                    console.log(ui.size);
                    ui.size.height
                    $('#width').text(ui.size.width);
                    $('#height').text(ui.size.height);
                },
                drag: function () {
                    var offset = $(this).offset();
                    var xPos = offset.left;
                    var yPos = offset.top;
                    $('#posX1').text('x: ' + xPos);
                    $('#posY1').text('y: ' + yPos);
                }
            });
            $("#test1").resizable();
            $('#test1').draggable(
                {
                    drag: function () {
                        var offset = $(this).offset();
                        var xPos = offset.left;
                        var yPos = offset.top;
                        $('#posX').text('x: ' + xPos);
                        $('#posY').text('y: ' + yPos);
                    }
                });
            $("#test1").resizable({ grid: [10, 10000] });
        });
        $('#body1').change(function () {
            document.getElementById('<%=HiddenLinePositionTop.ClientID%>').value = $('#posX').value;
            document.getElementById('<%=HiddenLinePositionLeft.ClientID%>').value = $('#posY').value;
        });
</script>
    <style type="text/css">
        #dragThis {
            width: 6em;
            height: 6em;
            padding: 0.5em;
            border: 3px solid #ccc;
            border-radius: 0 1em 1em 1em;
        }

        #test .ui-resizable-e {
            height: 100%;
            position: absolute;
            right: 0px;
            width: 10px;
        }

        #test .ui-resizable-s {
            bottom: 0px;
            height: 10px;
            position: absolute;
            width: 100%;
        }

        #test .ui-resizable-se {
            bottom: 0px;
            height: 10px;
            position: absolute;
            right: 0px;
            width: 10px;
        }
    </style>
</head>
<body id="body1">
    <form id="form1" runat="server">
        <div id="test1">
            <hr id="addline" />
        </div>
        <div id="current">
            Current Position:<br />
            Top:
            <asp:Label ID="posX" runat="server" Text="Label"></asp:Label><br />
            Left:
            <asp:Label ID="posY" runat="server" Text="Label"></asp:Label>
        </div>
        <div>
            Position After Postback<br />
            Top:
            <asp:Label ID="width" runat="server" Text="Label"></asp:Label><br />
            Left:
            <asp:Label ID="height" runat="server" Text="Label"></asp:Label>
        </div>
        <%--        <div>
            <asp:Button ID="add" runat="server" Text="Add more" OnClick="add_click" />
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
        </div>--%>
        <asp:HiddenField ID="HiddenLinePositionTop" runat="server" />
        <asp:HiddenField ID="HiddenLinePositionLeft" runat="server" />
    </form>

</body>
</html>

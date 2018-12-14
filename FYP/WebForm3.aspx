<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="FYP.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>jquery draggable and resizable example</title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.8.23/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.23/themes/base/jquery-ui.css" type="text/css" media="all" />



    <style type="text/css">
        #resizediv {
            width: 150px;
            height: 150px;
            padding: 0.5em;
            background: #EB5E00;
            color: #fff
        }
    </style>

    <%--    <script type="text/javascript">
        $(document).ready(function () {
            $("#resizediv").resizable();
            $("#resizediv").draggable();
        });
        $('#draggableHelper').draggable();
        $('#image').resizable();
    </script>--%>

    <%--    <style type="text/css">
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
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
        <%--<div id="resizediv">
            Move... or Resize....<br />
            Aspdotnet-Suresh .com
        </div>
        <div class="resizediv2">
            <img src="Tunku-Abdul-Rahman-University-College-TARC.png" />
        </div>
        <div id="draggableHelper" style="display: inline-block">
            <img id="image" src="http://www.google.com.br/images/srpr/logo3w.png" />
        </div>--%>

        <style type="text/css">
            #test {
                width: 160px;
                height: 10px;
                padding: 0;
                position: absolute;
                z-index: 3;
            }

                #test img {
                    width: 100%;
                    height: 100%;
                    cursor: move;
                }

            .test1 {
                width: 160px;
                height: 10px;
                padding: 0;
                position: absolute;
                z-index: 3;
                cursor: move;
            }

                .test1 img {
                    width: 100%;
                    height: 100%;
                    cursor: move;
                }

            #resizable {
                width: 150px;
                height: 150px;
                padding: 0.5em;
            }

                #resizable h3 {
                    text-align: center;
                    margin: 0;
                }
        </style>

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
                    }
                });
                $("#test").resizable({ grid: [1, 10000] });
            });
            $(function () {
                $(".test1").draggable().resizable({
                    resize: function (e, ui) {
                        console.log(ui.size);
                        ui.size.height
                        $('#width').text(ui.size.width);
                        $('#height').text(ui.size.height);
                    }
                });
                $(".test1").resizable({ grid: [10, 10000] });
            });

        </script>
        <script type="text/javascript">
            function imgSIZE() {
                var img = $(".background-image"),
                    theImage = new Image(),
                    pageHeader = $('.page-header'),
                    pageHeaderH = pageHeader.outerHeight(),
                    aspectRatio = img.width() / img.height();

                theImage.src = img.attr("src");
                var imgW = theImage.width;
                var imgH = theImage.height;

                $('.dimentions').text('Width: ' + imgW + ' Height: ' + imgH);
            }

            imgSIZE();

            //$(window).on('resize', function () {
            //    imgSIZE();
            //});
        </script>
        <div class="dimentions"></div>
        <%--<img class="background-image" src="https://myself-bbs.com/data/attachment/block/34/34672179e871a55293d6c47afd04ea46.jpg" alt="">--%>

        <div id="resizable" style="visibility: hidden" class="ui-widget-content">
            <h3 class="ui-widget-header">Resizable</h3>
        </div>
        <%--        <div id="test">
            <hr id="addline" />
        </div>--%>


        <%--<div id="test">
            <%--<img src="Tunku-Abdul-Rahman-University-College-TARC.png" alt="test" class="background-image" />--%>
        <%--            hello
        </div>--%>
        <%--        <div>
            <p>
                width:<span id='width'>100</span> height:<span id='height'>100</span>
            </p>
        </div>--%>

        <div>
            <asp:Button ID="add" runat="server" Text="Add more" OnClick="add_click" />
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
        </div>
        <%--<div>
            <asp:Panel ID="Panel2" runat="server"></asp:Panel>
            <asp:Button ID="Button1" runat="server" Text="Add more" OnClick="add_click" OnClientClick="return false" />

        </div>--%>
        <asp:DropDownList ID="DropDown1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDown1_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Value="a"></asp:ListItem>
            <asp:ListItem Value="b"></asp:ListItem>
        </asp:DropDownList>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="Label1" Text="test" runat="server" />
            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDown1" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </form>

</body>
</html>

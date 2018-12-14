<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="FYP.WebForm2" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript">
        function imagepreview(input) {
            if (input.files && input.files[0]) {

                var fildr = new FileReader();
                fildr.onload = function (e) {
                    $('#imgprw').attr('src', e.target.result);
                }
                fildr.readAsDataURL(input.files[0]);
            }
        }
    </script>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Resizable - Default functionality</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <style>
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
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $(".resizable").resizable().draggable();
        });

    </script>
</head>
<body>

    <%--<div id="resizable" class="ui-widget-content">
        <h3 class="ui-widget-header">Resizable</h3>
    </div>--%>
    <div>
        <input type="file" name="fileupload" onchange="imagepreview(this);" />
        <img id="imgprw" alt="image before upload" class="ui-widget-header resizable" width="100px" height="100px" />
    </div>
    <form id="form1" runat="server">
        <asp:FileUpload ID="ProductImage" runat="server" />
        <%--<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" />--%>
        <asp:TextBox runat="server" ID="txtProductName" CssClass="form-control" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProductName" ErrorMessage="The Product name field is required." />

    </form>
    <%-- <img src ='ShowImage.ashx?id=" + formEleList[i].eleTypeId + "' onclick='changeImage(this.id)' style='width: 150px; height: 150px' id='image" + temp + "'/>--%>
</body>
</html>

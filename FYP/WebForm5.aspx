<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="FYP.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        $(document).ready(function () {
            //Uncheck the CheckBox initially
            $('#chkHrVis').removeAttr('checked');
            // Initially, Hide the horizontal line when Web Form is loaded
            $('#hrLine').hide();
            $('#chkHrVis').change(function () {
                if (this.checked) {
                    $('#hrLine').show();
                }
                else {
                    $('#hrLine').hide();
                }
            });

            //Uncheck the CheckBox initially
            $('#chkImg').removeAttr('checked');
            // Initially, Hide the horizontal line when Web Form is loaded
            $('#fileupload').hide();
            $('#imgFrame').hide();
            $('.fileupload').hide();
            $('#chkImg').change(function () {
                if (this.checked) {
                    $('#fileupload').show();
                    $('.fileupload').show();
                    $('#imgFrame').show();
                }
                else {
                    $('#fileupload').hide();
                    $('.fileupload').hide();
                    $('#imgFrame').hide();
                }
            });
        });

    </script>
    <style type="text/css">
        body {
            height: 100%;
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            text-align: center;
            /*overflow-x:hidden;*/
        }

        #sidebar {

            height: 100%;
            width: 25%;
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
                height: 100%;
                z-index: 1;
                top: 0;
            }

        .border {
            border: rgb(204,204,204) solid 0.5px;
            border-left: none;
            border-right: none;
            border-top: none;
            border-collapse: collapse;
            width:100%;
        }

        .button {
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            background-color: rgb(80, 142, 245);
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
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

        .table1 {
            font-size: 15px;
            padding: 15px 15px;
        }

        .td1 {
            width: 10%;
            text-align: right;
            padding-right: 30px;
        }

        .lstbox {
            border-radius: 10px 10px;
            font-size: 15px;
            width: 300px;
            padding: 8px 8px;
        }

        .th1 {
            font-size: 22px;
        }

        .chkbox input {
            width: 15px;
            height: 15px;
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
                width: 26cm;
                height: 34.7cm;
            }

        .reportHeader1 {
            font-size: 30px;
            font-variant: small-caps;
            text-transform: uppercase;
            font-weight: bold;
            text-wrap: normal;
            /*position:absolute;*/
        }

        .reportHeader2 {
            font-size: 20px;
            text-wrap: normal;
            /*position:absolute;*/
        }

        .Mouse {
            cursor: move;
        }


        #reportContent {
            width: 100%;
            padding: 40px;
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

        .chkspacing input {
            width: 15px;
            height: 15px;
            cursor: pointer;
        }

            .chkspacing input + label {
                cursor: pointer;
            }

        .chkspacing label {
            margin-left: 10px;
            vertical-align: middle;
            padding: 1px;
            width: 120px;
            font-weight: normal;
            text-transform: capitalize;
        }

        #hrLine {
            position: absolute;
            width: 200px;
            cursor: move;
        }

            #hrLine .ui-resizable-e {
                height: 100%;
                position: absolute;
                right: 0px;
                width: 10px;
            }

            #hrLine .ui-resizable-s {
                bottom: 0px;
                height: 10px;
                position: absolute;
                width: 100%;
            }

            #hrLine .ui-resizable-se {
                bottom: 0px;
                height: 10px;
                position: absolute;
                right: 0px;
                width: 10px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="sidebar">
                <table class="border">
                    <tr class="border">
                        <td style="font-size: 30px;" colspan="2">
                            <div>
                                <img src="Tunku-Abdul-Rahman-University-College-TARC.png" width="180" height="70" /><br />
                                i-Report Builder
                            </div>
                        </td>
                    </tr>
                    <tr class="border" style="border-bottom: none">
                        <td style="vertical-align: central;width:100%"  colspan="2">
                            <table style="padding: 5px; text-align: left;width:100%">
                                <tr>
                                    <td class="td1">Font Style
                                    </td>
                                    <td>
                                        <asp:DropDownList ClientIDMode="Static" ID="fontFamilyDrpDwnList" AutoPostBack="True" runat="server" Width="70%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1">Show Line
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkHrVis" runat="server" CssClass="chkHrVis" />
                                    </td>
                                </tr>

                                <script type="text/javascript">
                                    function LimtCharacters(txtMsg, CharLength, indicator) {
                                        chars = txtMsg.value.length;

                                        document.getElementById(indicator).innerHTML = CharLength - chars + " lefts";
                                        if (chars > CharLength) {
                                            txtMsg.value = txtMsg.value.substring(0, CharLength);
                                        }
                                        if (chars == 0) {
                                            document.getElementById(indicator).innerHTML = " ";
                                        }
                                    }
                                </script>
                                <tr>
                                    <td class="td1">Report Title
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRptTitle" MaxLength="50" CssClass="padding" runat="server" onkeyup="document.getElementById('lblRptTitle').innerHTML=this.value;LimtCharacters(this,50,'lblcount');"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rptTitleRequired" runat="server" ErrorMessage="*" ToolTip="Required Field" ValidationGroup="form1" ControlToValidate="txtRptTitle"></asp:RequiredFieldValidator>
                                        <br />
                                        <label id="lblcount" style="font-weight: normal; font-size: smaller; color: gray"></label>
                                    </td>
                                        <asp:Literal ID="Literal1" runat="server" EnableViewState="False"></asp:Literal>
                                </tr>
                                <tr>
                                    <td class="td1">Report Description
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRptDesc" MaxLength="50" CssClass="padding" runat="server" onkeyup="document.getElementById('lblRptDesc').innerHTML=this.value;LimtCharacters(this,50,'lblcount2');"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rptDescRequired" runat="server" ErrorMessage="*" ToolTip="Required Field" ValidationGroup="form1" ControlToValidate="txtRptDesc"></asp:RequiredFieldValidator>
                                        <br />
                                        <label id="lblcount2" style="font-weight: normal; font-size: smaller; color: gray"></label>
                                    </td>
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </tr>
                                <tr>
                                    <td class="td1">Add Image
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkImg" runat="server" CssClass="chkHrVis" />
                                    </td>
                                </tr>
                                <%--<tr class="fileupload">
                                    <td class="td1">Image Url
                                    </td>
                                    <td>
                                        <input type="file" id="fileupload" name="fileupload" onchange="imagepreview(this);" />
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="2">
                                        <input type="file" id="fileupload" name="fileupload" onchange="imagepreview(this);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1">Table Content
                                    </td>
                                    <td>
                                        <button type="button" class="button btn btn-info btn-lg" data-toggle="modal" data-target="#myModal" style="border-radius: initial">Edit</button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="vertical-align: bottom; height: 30px; padding: 5px 5px">
                            <asp:Button runat="server" ID="BtnSave" class="button btnMargin" Text="Save" OnClientClick="return confirm('Are you sure you want to submit?')" ValidationGroup="form1" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="vertical-align: bottom; height: 30px; padding: 5px 5px">
                            <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="button btnMargin" /></td>
                    </tr>

                </table>
            </div>
        </div>
    </form>
</body>
</html>

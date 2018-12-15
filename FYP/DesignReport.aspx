<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesignReport.aspx.cs" Inherits="FYP.DesignReport" %>

<!DOCTYPE html>
<!-- 
    edit page use setStyle for positioning labels.
    bugs
    - distinct select feature
    - count does not go away after editing table
    -->
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link type="text/css" rel="stylesheet" href="StyleSheet1.css" />
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.3.1.min.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="css.css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <link rel="icon" href="icons8-business-report-50.ico" />
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
    <script>

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
            $("#hrLine").resizable();
            $('#hrLine').draggable(
                {
                    drag: function () {
                        var offset = $(this).offset();
                        var xPos = offset.left;
                        var yPos = offset.top;
                        //$('#posX').text('x: ' + xPos);
                        //$('#posY').text('y: ' + yPos);
                        $('#HiddenLinePositionTop').val(xPos);
                        $('#HiddenLinePositionLeft').val(yPos);

                    }
                });
            $("#hrLine").resizable({ grid: [10, 10000] });

            // get data from hiddenfield to be stored in db
            $('#<%=BtnSave.ClientID%>').click(function () {
                var lblTitle = $("#lblRptTitle");
                var lblDesc = $("#lblRptDesc");
                var lblDate = $("#lblDate");


                var positionImg = $('#imgFrame').position();
                if ($('#imgprw').attr('src') != null) {
                    //alert("Image exist");
                    //var hiddenImgHeight = ;
                    //var hiddenImgWidth = 
                    document.getElementById('<%=hiddenHeight.ClientID%>').value = $('#imgFrame').height();
                    document.getElementById('<%=hiddenWidth.ClientID%>').value = $('#imgFrame').width();
                    document.getElementById('<%=hiddenImage.ClientID%>').value = positionImg.left + "," + positionImg.top;
                }
                var positionTitle = lblTitle.position();
                var positionDesc = lblDesc.position();
                document.getElementById('<%=hiddenRptTitle.ClientID%>').value = positionTitle.left + "," + positionTitle.top;
                document.getElementById('<%=hiddenRptDesc.ClientID%>').value = positionDesc.left + "," + positionDesc.top;
                if (document.getElementById('<%=lblDate.ClientID%>').value != "") {
                    var positionDate = lblDate.position();
                    document.getElementById('<%=hiddenRptDate.ClientID%>').value = positionDate.left + "," + positionDate.top;
                }
            });
            <%=PostBackString %>
        });
        // update data everytime an object is moved.
        $(function () {
            $(".resizable").resizable().draggable();
            //$(document).ready(function () {
            //    $("#imgFrame").hide();
            //})
            //$('#fileUpload').change(function () {
            //    $("#imgFrame").show();
            //})
        });

    </script>
    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>--%>

    <%--Upload Image--%>
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

    <style type="text/css">
        body {
            height: 100%;
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            text-align: center;
            background: rgb(204,204,204);
            overflow-x:hidden;
            font-family: 'lato', sans-serif;
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

        .padding{
            padding:7px;
            font-size: 15px;
            width:95%;
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
    <title>i-Report Builder</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManger1" EnablePageMethods="true" runat="Server" EnablePartialRendering="true">
        </asp:ScriptManager>
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
                    <td style="vertical-align: central" colspan="2">
                        <table style="padding: 5px; text-align: left">
                            <tr>
                                <td class="td1">Font Style
                                </td>
                                <td>
                                    <asp:DropDownList ClientIDMode="Static" ID="fontFamilyDrpDwnList" AutoPostBack="True" runat="server" CssClass="padding" OnSelectedIndexChanged="ChangeFont">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="td1">Show Line
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkHrVis" runat="server" CssClass="chkHrVis chkbox" />
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
                                <td class="td1">
                                    <p style="margin-top:-8px">Report Title</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRptTitle" MaxLength="100" CssClass="padding" Width="90%" runat="server" onkeyup="document.getElementById('lblRptTitle').innerHTML=this.value;LimtCharacters(this,50,'lblcount');"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rptTitleRequired" runat="server" ErrorMessage="*" ToolTip="Report Title is required" ValidationGroup="form1" ControlToValidate="txtRptTitle"></asp:RequiredFieldValidator>
<%--                                    <label id="lblcount" style="font-weight: normal; font-size: smaller; color: gray"></label>--%>
                                    <asp:Literal ID="Literal1" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="td1">
                                    <p style="margin-top:-8px">Report Description</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRptDesc" MaxLength="100" CssClass="padding" Width="90%" runat="server" onkeyup="document.getElementById('lblRptDesc').innerHTML=this.value;LimtCharacters(this,50,'lblcount2');"></asp:TextBox>                                    
                                    <asp:RequiredFieldValidator ID="rptDescRequired" runat="server" ErrorMessage="*" ToolTip="Report Description is required" ValidationGroup="form1" ControlToValidate="txtRptDesc"></asp:RequiredFieldValidator>
                                    <%--<label id="lblcount2" style="font-weight: normal; font-size: smaller; color: gray"></label>--%>
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="td1">Add Image
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkImg" runat="server" CssClass="chkHrVis chkbox" />
                                </td>
                            </tr>
                            <tr class="fileupload" align="right">
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
                        <asp:Button runat="server" ID="BtnSave" class="button btnMargin" Text="Save" OnClientClick="return confirm('Are you sure you want to submit?')" OnClick="BtnSave_Click" ValidationGroup="form1" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="vertical-align: bottom; height: 30px; padding: 5px 5px">
                        <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="button btnMargin" OnClick="BtnCancel_Click" /></td>
                </tr>

            </table>

        </div>
        <div style="padding: 50px; padding-left: 350px" id="containment-wrapper">
            <script type="text/javascript">
                $(function () {
                    $("#imgFrame").resizable({

                        resize: function (e, ui) {
                            console.log(ui.size);
                            //$('#imgWidth').text(ui.size.width);
                            //$('#imgHeight').text(ui.size.height);
                        }
                    });
                    $("#imgFrame").resizable().draggable();
                    $("#imgFrame").draggable({
                        containment: "page", scroll: true,
                        helper: "ui-resizable-helper"
                    });

                });
            </script>
            <style type="text/css">
                #imgFrame {
                    width: 160px;
                    height: 90px;
                    padding: 0;
                    position: absolute;
                    z-index: 3;
                    resize: both;
                    border: none
                }

                    #imgFrame img {
                        width: 100%;
                        height: 100%;
                        z-index: 5;
                        overflow: hidden;
                        border: none;
                    }

                .ui-resizable-helper {
                    border: 0.1em dashed transparent;
                }
            </style>

            <page size="A4">                           
            <asp:Panel runat="server" ID="hiddenPanel">
                
                <%-- Hidden Field for report header --%>
                <asp:HiddenField ID="hiddenRptTitle" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hiddenRptDesc" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hiddenRptDate" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hiddenFormID" runat="server"></asp:HiddenField>

                <%-- Hidden Field for Image --%>
                <asp:HiddenField ID="hiddenWidth" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hiddenHeight" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hiddenImage" runat="server"></asp:HiddenField>

                <%-- Hidden Field for Horizontal Line --%>
                <asp:HiddenField ID="HiddenLinePositionTop" runat="server" />
                <asp:HiddenField ID="HiddenLinePositionLeft" runat="server" />
            </asp:Panel>
            <asp:Panel runat="server" ID="reportHeader" CssClass="reportHeaderClass">
            <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:Panel ID="imgFrame" runat="server" CssClass="ui-resizable-helper">
                    <asp:Image ID="imgprw" runat="server" CssClass="Mouse"></asp:Image>
                </asp:Panel>

                <asp:Panel ID="hrLine" runat="server" CssClass="ui-resizable-helper">
                    <hr id="addline" />
                </asp:Panel>
            
            <asp:Label ID="lblRptTitle" CssClass="reportHeader1 draggable Mouse"  runat="server"></asp:Label><br />
            <asp:Label ID="lblRptDesc"  CssClass="reportHeader2 draggable Mouse" runat="server"></asp:Label><br />
            <asp:Label ID="lblDate" CssClass="reportHeader2 draggable Mouse" runat="server"></asp:Label>
            </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>
            <br />
            <br />
            <br />
            <br />
            <%-- Report Content (Table) --%>
            <div id="reportContent">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                <asp:GridView ID="reportGridView" OnDataBound="reportGridView_DataBound" PagerSettings-Position="Top" EnableViewState="true" PagerStyle-CssClass="pagerStyle" Border="0" runat="server" CssClass="rpttable" CellPadding="6" HeaderStyle-CssClass="tableheader" OnRowDataBound="reportGridView_RowDataBound" AllowPaging="true" OnPageIndexChanging="reportGridView_PageIndexChanging" PageSize="30">               
                <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
                </asp:GridView>
                        <asp:Button ID="Next_Page" runat="server" OnClick="Button3_Click" Text="Display Next Page"></asp:Button>
                        </ContentTemplate>
                </asp:UpdatePanel>
            </div>                   
<%--        </asp:Panel>--%>
        </page>

        </div>
        <div class="container">
            <!-- Modal -->
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <asp:Panel ID="pnlControl" runat="server">
                            <asp:UpdatePanel ID="updatePanel" runat="server">
                                <ContentTemplate>
                                    <div class="modal-header">
                                        <div>
                                            <p style="font-size: 20px">
                                                <asp:Label ID="lblFormName" runat="server" Text="Label"></asp:Label>
                                            </p>
                                            <hr />
                                        </div>
                                        <table class="table1" align="center" style="width: 100%">
                                            <tr>
                                                <asp:Label ID="Label2" runat="server" Text="&lt;strong&gt;Select the column to be used&lt;/strong&gt;"></asp:Label>
                                            </tr>
                                            <tr>
                                                <td class="td1">
                                                    <asp:Label ID="Label7" runat="server" Text="&lt;strong&gt;Select the form's displayed data&lt;/strong&gt;" Visible="true"></asp:Label>
                                                </td>
                                                <td align="justify">
                                                    <asp:CheckBoxList ID="ColumnCbList" Width="100%" CssClass="chkspacing" runat="server" Visible="true" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" AutoPostBack="true" RepeatLayout="flow" RepeatColumns="2" RepeatDirection="Vertical">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td1">
                                                    <asp:Label ID="Label6" runat="server" Text="Label"><strong>Show Total Count</strong></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox3" CssClass="chkbox" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox3_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <%--<asp:PlaceHolder runat="server" ID="totalCount">--%>
                                            <tr>
                                                <td class="td1">
                                                    <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"><strong>Select data to be summed</strong></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="selectCount" runat="server" Visible="false"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <%--</asp:PlaceHolder>--%>
                                        </table>
                                        <div>
                                            <hr />
                                        </div>
                                        <asp:Button ID="Button1" runat="server" Text="Change" OnClick="Button1_Click" CssClass="button" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>

</html>

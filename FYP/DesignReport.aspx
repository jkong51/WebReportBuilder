﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesignReport.aspx.cs" Inherits="FYP.DesignReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.3.1.min.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="css.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    
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

            // get data from hiddenfield to be stored in db
            $('#<%=BtnSave.ClientID%>').click(function () {
                var lblTitle = $("#lblRptTitle");
                var lblDesc = $("#lblRptDesc");
                var lblDate = $("#lblDate");
                var positionTitle = lblTitle.position();
                var positionDesc = lblDesc.position();
                document.getElementById('<%=hiddenRptTitle.ClientID%>').value = positionTitle.left + "," + positionTitle.top;
                document.getElementById('<%=hiddenRptDesc.ClientID%>').value = positionDesc.left + "," + positionDesc.top;
                if (document.getElementById('<%=hiddenRptDate.ClientID%>').value == "") {
                    var positionDate = lblDate.position();
                    document.getElementById('<%=hiddenRptDate.ClientID%>').value = positionDate.left + "," + positionDate.top;
                }
            });
            <%=PostBackString %>
        });
        // update data everytime an object is moved.



        
        
    </script>
    <style type="text/css">
        body {
            height: 100%;
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            text-align: center;
            background: rgb(204,204,204);
            height: 100%;
        }

        #sidebar {
            height: 100%;
            width: 23%;
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
                width: 100%;
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
            width: 200px;
            text-align: right;
            padding-right: 30px;
        }

        .textbox {
            border-radius: 10px 10px;
            font-size: 15px;
            width: 300px;
            padding: 8px 8px;
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
                width: 21cm;
                height: 29.7cm;
            }

        .reportHeader1 {
            font-size: 30px;
            font-variant: small-caps;
            text-transform: uppercase;
            font-weight: bold;
            /*position:absolute;*/
        }

        .reportHeader2 {
            font-size: 20px;
            /*position:absolute;*/
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
    <title>i-Report Builder</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManger1" EnablePageMethods="true" runat="Server">
        </asp:ScriptManager>
<%--        <asp:UpdatePanel ID="updatePanel" runat="server">
                <ContentTemplate>--%>
                    <div id="sidebar">
                        <table class="border">
                            <tr class="border">
                                <td style="font-size: 30px;" colspan="2">
                                    <img src="Tunku-Abdul-Rahman-University-College-TARC.png" width="180" height="70" /><br />
                                    i-Report Builder
                                </td>
                            </tr>
                            <tr class="border" style="border-bottom: none">
                                <td style="padding-top: 15px; vertical-align: central" colspan="2">
                                    <strong style="font-size: larger">Header & Footer</strong>
                                    <br />
                                    <table style="padding: 5px; text-align: left">
                                        <tr>
                                            <td>Font Style
                                            </td>
                                            <td>
                                                <asp:DropDownList ClientIDMode="Static"  ID="fontFamilyDrpDwnList" AutoPostBack="True" runat="server" Width="100%" OnSelectedIndexChanged="ChangeFont">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Report Title
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRptTitle" CssClass="padding" runat="server" onkeyup="document.getElementById('lblRptTitle').innerHTML=this.value;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Report Description
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRptDesc" CssClass="padding" runat="server" onkeyup="document.getElementById('lblRptDesc').innerHTML=this.value;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Table Content
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
                                    <asp:Button runat="server" ID="BtnSave" class="button" Text="Save" OnClientClick="return confirm('Are you sure you want to submit?')" OnClick="BtnSave_Click"/></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="vertical-align: bottom; height: 30px; padding: 5px 5px">
                                    <asp:Button runat="server" ID="BtnCancel" class="button" Text="Cancel" OnClick="BtnCancel_Click" /></td>
                            </tr>

                        </table>

                    </div>
<%--                            </ContentTemplate>
            </asp:UpdatePanel>--%>
        <div style="padding: 50px; padding-left: 350px" id="containment-wrapper">
            
            <page size="A4"> 
                
            <asp:Panel runat="server" ID="hiddenPanel">
                <asp:HiddenField ID="hiddenRptTitle" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hiddenRptDesc" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hiddenRptDate" runat="server"></asp:HiddenField>
            </asp:Panel>

            <asp:Panel runat="server" ID="reportHeader" CssClass="reportHeaderClass">
            <asp:UpdatePanel ID="updatePanel1" runat="server">
                <ContentTemplate>
                <asp:Label ID="lblRptTitle" CssClass="reportHeader1 draggable Mouse ui-widget-content"  runat="server"></asp:Label><br />
            <asp:Label ID="lblRptDesc" CssClass="reportHeader2 draggable Mouse" runat="server"></asp:Label><br />
            <asp:Label ID="lblDate" CssClass="reportHeader2 draggable Mouse" runat="server"></asp:Label>
            </ContentTemplate>
            </asp:UpdatePanel>
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
                                        <p style="font-size: 20px">Select Report Content</p>
                                        <hr />
                                    </div>
                                    <table class="table1">
                                        <tr>
                                            <th colspan="2" style="padding-bottom: 20px" class="th1">BODY
                                            </th>
                                        </tr>                               
                                        <tr>
                                            <td class="td1">
                                                <asp:Label ID="Label4" runat="server" Text="&lt;strong&gt;Select the form to be used&lt;/strong&gt;"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="lstbox" DataSourceID="SqlDataSource1" DataTextField="title" DataValueField="formId" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem>Select Table</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FormNameConnectionString %>" SelectCommand="SELECT formId, title FROM Form WHERE (staffId = @staffId)">
                                                    <SelectParameters>
                                                        <asp:SessionParameter Name="staffId" SessionField="userId" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="&lt;strong&gt;Select the form's displayed data&lt;/strong&gt;" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" Visible="false" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="&lt;strong&gt;Choose Filters&lt;/strong&gt;"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Button ID="addFilterBtn" runat="server" Text="Add Filter" OnClick="AddFilterBtn_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                    <div>
                                        
                                        <hr />
                                    </div>
                                    <asp:Button ID="Button2" runat="server" Text="Done" CssClass="button" />
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

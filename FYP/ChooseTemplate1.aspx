<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ChooseTemplate1.aspx.cs" Inherits="FYP.ChooseTemplate1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.3.1.min.js"></script>
    <style type="text/css">
        .head {
            /*font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;*/
            font-family: 'lato', sans-serif;
            cursor: default;
            font-weight:bold;
        }

        .content {
            font-size: larger;
            /*font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;*/
            font-family: 'lato', sans-serif;
            cursor: pointer;
        }

        .border1 {
            border: 1px solid #BFBFBF;
            background-color: azure;
            box-shadow: 5px 10px 5px #aaaaaa;
            transition: all .2s ease-in-out;
            font-family: "Gill Sans", sans-serif;
            cursor: pointer;
        }

            .border1:hover {
                transform: scale(1.1);
            }

        .border2 {
            width: 300px;
            height: 300px;
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
            width: 95%;
            -webkit-transition-duration: 0.4s; /* Safari */
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

        th {
            font-size: 22px;
        }

        .chkbox input {
            width: 15px;
            height: 15px;
        }
    </style>
    <script type="text/javascript">
        function Count() {
            var i = document.getElementById("txtRptTitle").value.length;
            document.getElementById("remainingChr").innerHTML = 50 - i;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table align="center" style="margin-top: 80px">
            <tr>
                <td class="border1">
                    <table class="border2">
                        <tr>
                            <td>
                                <p style="font-size: 27px" class="head">BLANK TEMPLATE</p>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 60px 30px" class="content">A white sheet for decorate report content</td>
                        </tr>
                        <tr>
                            <td>
                                <button type="button" class="button btn btn-info btn-lg" data-toggle="modal" data-target="#myModal" style="border-radius: initial">Get Started</button>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td class="border1">
                    <table class="border2">
                        <tr>
                            <td>
                                <p style="font-size: 27px" class="head">EXIST TEMPLATE</p>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 60px 30px" class="content">Use pre-defined template to create report</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnExist" runat="server" Text="Get Started" CssClass="button" /></td>
                        </tr>
                    </table>
                </td>

            </tr>
        </table>
    </div>

    <div class="container">
        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <asp:ScriptManager ID="ScriptManger1" runat="Server">
                    </asp:ScriptManager>

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
                                            <th colspan="2" style="padding-bottom: 20px">HEADER
                                            </th>
                                        </tr>
                                        <tr>
                                            <td class="td1">
                                                <asp:Label ID="Label1" runat="server" Text="Label"><strong>Report Title</strong></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRptTitle" CssClass="textbox" runat="server" ToolTip="Report title" placeholder="Report title" MaxLength="50" onkeyup="Count()"></asp:TextBox><asp:Label ID="remainingChr" runat="server"></asp:Label>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td1">
                                                <asp:Label ID="Label2" runat="server" Text="Label"><strong>Report Description</strong></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRptDesc" CssClass="textbox" runat="server" ToolTip="Report description" placeholder="Report description" MaxLength="50"></asp:TextBox>
                                                <br />
                                            </td>
                                        </tr>

                                        <tr style="padding-bottom: 20px">
                                            <td class="td1">
                                                <asp:Label ID="Label3" runat="server" Text="Label"><strong>Show Date</strong></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkDate" CssClass="chkbox" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <th colspan="2" style="padding-bottom: 20px">BODY
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
                                            <th colspan="2" style="padding-bottom: 20px">FOOTER
                                            </th>
                                        </tr>
                                        <tr>
                                            <td class="td1">
                                                <asp:Label ID="Label6" runat="server" Text="Label"><strong>Show Total Count</strong></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckBox3" CssClass="chkbox" runat="server" OnCheckedChanged="CheckBox3_CheckedChanged"/>
                                            </td>
                                        </tr>
                                        <%--<asp:PlaceHolder runat="server" ID="totalCount">--%>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="selectCount" runat="server" Visible="false"></asp:DropDownList>
                                                </td>
                                            </tr>
                                        <%--</asp:PlaceHolder>--%>
                                    </table>
                                    <div>
                                        <hr />
                                    </div>
                                    <div>
                                        <asp:PlaceHolder id="filterTablePlaceHolder" Visible="false" runat="server">
                                        <table>
                                            <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="&lt;strong&gt;Choose Filters&lt;/strong&gt;"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                                    <td>
                                                        <asp:DropDownList ID="selectedItemDDL1" runat="server" OnSelectedIndexChanged="SelectedItemDDL1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <asp:DropDownList ID="conditionDDL" runat="server"></asp:DropDownList>
                                                        <asp:TextBox ID="filterBox1" runat="server"></asp:TextBox>
                                                    </td>
                                                <td>
                                                    <asp:Button ID="addFilter" runat="server" Text="Add Filter"/>
                                                </td>
                                        
                                        </tr>
                                       </table>
                                        </asp:PlaceHolder>
                                    </div>
                                    <asp:Button ID="Button1" runat="server" Text="Create" OnClick="Button1_Click" CssClass="button" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>

        </div>
        </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="FYP.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
                                                <asp:TextBox ID="txtRptTitle" CssClass="textbox" runat="server" ToolTip="Report title" placeholder="Report title"></asp:TextBox>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td1">
                                                <asp:Label ID="Label2" runat="server" Text="Label"><strong>Report Description</strong></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRptDesc" CssClass="textbox" runat="server" ToolTip="Report description" placeholder="Report description"></asp:TextBox>
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
                                                <asp:Label ID="Label5" runat="server" Text="Label"><strong>Show Page Number</strong></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckBox2" CssClass="chkbox" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td1">
                                                <asp:Label ID="Label6" runat="server" Text="Label"><strong>Show Total Count</strong></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckBox3" CssClass="chkbox" runat="server" />
                                            </td>
                                        </tr>
                                        <%--<asp:PlaceHolder runat="server" ID="totalCount">--%>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="selectCount" runat="server"></asp:DropDownList>
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
    </form>
</body>
</html>
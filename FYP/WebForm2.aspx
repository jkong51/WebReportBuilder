<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="FYP.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
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
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 50px; padding-left: 350px" id="containment-wrapper">
            
            <page size="A4"> 
                


            
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
</asp:Content>

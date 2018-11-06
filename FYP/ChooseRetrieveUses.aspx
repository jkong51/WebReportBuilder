<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ChooseRetrieveUses.aspx.cs" Inherits="FYP.ChooseRetrieveUses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .head {
            /*font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;*/
            font-family: 'lato', sans-serif;
            cursor: default;
            font-weight:bold;
        }

        .content {
            font-size: larger;
            font-family: 'lato', sans-serif;
            /*font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;*/
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


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
        <table align="center" style="margin-top: 80px">
            <tr>
                <td class="border1">
                    <table class="border2">
                        <tr>
                            <td>
                                <p style="font-size: 27px" class="head">VIEW REPORT</p>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 60px 30px" class="content">Get report content for viewing purposes</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnView" runat="server" Text="Get Started" CssClass="button" OnClick="btnView_Click" /></td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td class="border1">
                    <table class="border2">
                        <tr>
                            <td>
                                <p style="font-size: 27px" class="head">EDIT REPORT</p>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 60px 30px" class="content">Get report content for editing purposes</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnEdit" runat="server" Text="Get Started" CssClass="button" OnClick="btnEdit_Click" /></td>
                        </tr>
                    </table>
                </td>

            </tr>
        </table>
    </div>
</asp:Content>

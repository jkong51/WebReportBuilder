﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="FYP.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .head{           
            /*font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;*/
            font-family: 'lato', sans-serif;
            cursor:default;
            font-weight:bold;
        }
        
        .content{
            font-size:larger;
            /*font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;*/
            font-family: 'lato', sans-serif;
            cursor:pointer;
        }
        .border1 {
            border: 1px solid #BFBFBF;
            background-color: azure;
            box-shadow: 5px 10px 5px #aaaaaa;
            transition: all .2s ease-in-out;
            font-family: "Gill Sans", sans-serif;
            cursor:pointer;
        }
        .border1:hover{
            transform: scale(1.1);
        }
        .border2 {
            width: 300px;
            height: 300px;
        }
        .button {
            /*font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;*/
            font-family: 'lato', sans-serif;
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
                cursor:pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table align="center" style="margin-top:80px">
            <tr>
            <td class="border1">
                <table class="border2">
                    <tr>
                        <td>
                            <p style="font-size:27px" class="head">CREATE REPORT</p>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:60px 30px" class="content">Create your report using blank template or existing template</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnCreate" runat="server" Text="Get Started" CssClass="button" OnClick="btnCreate_Click" /></td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
           <td class="border1">
                <table class="border2">
                    <tr>
                        <td>
                            <p style="font-size:27px" class="head">RETRIEVE REPORT</p>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:60px 30px" class="content">Get report details so that you can update or view the report details</td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="btnUpdate" runat="server" Text="Get Started" CssClass="button" OnClick="BtnUpdate_Click"/></td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td class="border1">
                <table class="border2">
                    <tr>
                        <td>
                            <p style="font-size:27px" class="head">DISABLE REPORT</p>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:60px 30px" class="content">Disable the existing report that you think temporarily don't need</td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="btnDelete" runat="server" Text="Get Started" CssClass="button" OnClick="btnDelete_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
    </div>
</asp:Content>

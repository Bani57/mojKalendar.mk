<%@ Page Title="" Language="C#" MasterPageFile="~/Kalendar.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="mojKalendarfinal.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Login.css" rel="stylesheet" />
    <style type="text/css">
    .auto-style1 {
        width: 40%;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label CssClass="label" ID="lblError" runat="server">User login page</asp:Label>
    <table align="center" class="auto-style1">
    <tr>
        <td>Username</td>
        <td>
            <asp:TextBox ID="txtUsername" runat="server" ValidationGroup="group1" CssClass="textbox"></asp:TextBox>
        </td>
        <td rowspan="2">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" Display="None" ErrorMessage="Username field should not be empty" ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" Display="None" ErrorMessage="Password field should not be empty" ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Bold="True" ForeColor="#3498DB" ValidationGroup="group1" DisplayMode="List" />
        </td>
    </tr>
    <tr>
        <td>Password</td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" ValidationGroup="group1" CssClass="textbox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Log in" ValidationGroup="group1" CssClass="button" />
        </td>
        <td>
            <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Sign up" CssClass="button" />
        </td>
        <td></td>
    </tr>
</table>
</asp:Content>

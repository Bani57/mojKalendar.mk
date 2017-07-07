<%@ Page Title="" Language="C#" MasterPageFile="~/Kalendar.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="mojKalendarfinal.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Register.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .auto-style1 {
        width: 50%;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label CssClass="label" ID="StatusMessage" runat="server">Create your account</asp:Label>
    <table align="center" class="auto-style1">
    <tr>
        <td>Username</td>
        <td>
            <asp:TextBox CssClass="textbox" ID="txtUsername" runat="server" ValidationGroup="group1"></asp:TextBox>
        </td>
        <td rowspan="8">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" Display="None" ErrorMessage="Username field must not be empty" ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtUsername" Display="None" ErrorMessage="Username must contain from 4 to 20 characters" ValidationExpression="[A-Za-z0-9]{4,20}" ValidationGroup="group1"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFName" Display="None" ErrorMessage="First name field must not be empty" ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFName" Display="None" ErrorMessage="Name must contain only letters" ValidationExpression="[A-Za-z]+" ValidationGroup="group1"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLName" Display="None" ErrorMessage="Last name field must not be empty" ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtLName" Display="None" ErrorMessage="Last name must contain only letters" ValidationExpression="[A-Za-z]+" ValidationGroup="group1"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword" Display="None" ErrorMessage="Password field must not be empty" ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPassword" Display="None" ErrorMessage="Password must contain from 7 to 20 characters" ValidationExpression="\S{7,20}" ValidationGroup="group1"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtConfirmPassword" Display="None" ErrorMessage="Password confirmation field must not be empty" ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtConfirmPassword" ControlToValidate="txtPassword" Display="None" ErrorMessage="Passwords don`t match"></asp:CompareValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEmail" Display="None" ErrorMessage="Invalid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="group1"></asp:RegularExpressionValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Bold="True" ForeColor="#3498DB" ValidationGroup="group1" DisplayMode="List" />
        </td>
    </tr>
    <tr>
        <td>First name</td>
        <td>
            <asp:TextBox CssClass="textbox" ID="txtFName" runat="server" ValidationGroup="group1"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Last name</td>
        <td>
            <asp:TextBox CssClass="textbox" ID="txtLName" runat="server" ValidationGroup="group1"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Password</td>
        <td>
            <asp:TextBox CssClass="textbox" ID="txtPassword" runat="server" TextMode="Password" ValidationGroup="group1"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Confirm Password</td>
        <td>
            <asp:TextBox CssClass="textbox" ID="txtConfirmPassword" runat="server" TextMode="Password" ValidationGroup="group1"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>E-mail</td>
        <td>
            <asp:TextBox CssClass="textbox" ID="txtEmail" runat="server" ValidationGroup="group1"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Address</td>
        <td>
            <asp:TextBox CssClass="textbox" ID="txtAddress" runat="server" ValidationGroup="group1"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button CssClass="button" ID="btnSignUp" runat="server" OnClick="btnSignUp_Click" Text="Create account" ValidationGroup="group1" />
        </td>
        <td>
            <asp:Button CssClass="button" ID="btnLogin" runat="server" CausesValidation="False" OnClick="btnLogin_Click" Text="Back to login page" />
        </td>
    </tr>
</table>
</asp:Content>

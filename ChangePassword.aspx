<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="PMS.ChangePassword" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Change Password</title>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div class="change-password-section">
            <h2>Change Password</h2>
            <asp:Label ID="UsernameLabel" runat="server" Text="Username:"></asp:Label>
            <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Label ID="OldPasswordLabel" runat="server" Text="Old Password:"></asp:Label>
            <asp:TextBox ID="OldPasswordTextBox" runat="server" TextMode="Password" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Label ID="NewPasswordLabel" runat="server" Text="New Password:"></asp:Label>
            <asp:TextBox ID="NewPasswordTextBox" runat="server" TextMode="Password" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Label ID="ConfirmPasswordLabel" runat="server" Text="Confirm New Password:"></asp:Label>
            <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" TextMode="Password" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Button ID="ChangePasswordButton" runat="server" Text="Change Password" CssClass="form-button" OnClick="ChangePasswordButton_Click" />
        </div>
    </main>
</asp:Content>
 
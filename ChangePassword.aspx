<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="PMS.ChangePassword" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Change Password</title>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div class="change-password-section">
            <h2>Change Password</h2>
            <asp:Panel ID="panelChangePassword" runat="server">
                <asp:Label ID="lblErrorCurrentPassword" runat="server" Text="Current password is incorrect." ForeColor="Red" Visible="False" />
                <asp:Label ID="lblErrorNewPassword" runat="server" Text="New passwords do not match." ForeColor="Red" Visible="False" />
                <asp:Label ID="lblErrorPasswordChange" runat="server" Text="Password change failed." ForeColor="Red" Visible="False" />
                <asp:Label ID="lblSuccessPasswordChange" runat="server" Text="Password changed successfully." ForeColor="Green" Visible="False" />
 
                <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" Placeholder="Current Password" />
                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Placeholder="New Password" />
                <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" Placeholder="Confirm New Password" />
 
                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" OnClick="ChangePassword_Click" />
            </asp:Panel>
        </div>
    </main>
</asp:Content>

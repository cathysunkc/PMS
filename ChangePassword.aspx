<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="PMS.ChangePassword" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Change Password</title>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div class="change-password-section">
            <h2>Change Password</h2>
            <asp:Panel ID="panelChangePassword" runat="server">
                <div class="form-group">
                    <asp:Label ID="lblErrorCurrentPassword" runat="server" Text="Current password is incorrect." CssClass="error-msg" Visible="False" />
                    <asp:Label ID="lblErrorNewPassword" runat="server" Text="New passwords do not match." CssClass="error-msg" Visible="False" />
                    <asp:Label ID="lblErrorPasswordChange" runat="server" Text="Password change failed." CssClass="error-msg" Visible="False" />
                    <asp:Label ID="lblSuccessPasswordChange" runat="server" Text="Password changed successfully." CssClass="success-msg" Visible="False" />
                </div>

                <div class="form-group" style="margin-bottom: 15px;">
                    <asp:TextBox ID="txtUsername" runat="server" Placeholder="Username" CssClass="form-control" />
                </div>

                <div class="form-group" style="margin-bottom: 15px;">
                    <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" Placeholder="Current Password" CssClass="form-control" />
                </div>

                <div class="form-group" style="margin-bottom: 15px;">
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Placeholder="New Password" CssClass="form-control" />
                </div>

                <div class="form-group" style="margin-bottom: 15px;">
                    <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" Placeholder="Confirm New Password" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-primary" OnClick="ChangePassword_Click" />
                </div>
            </asp:Panel>
        </div>
    </main>
</asp:Content>

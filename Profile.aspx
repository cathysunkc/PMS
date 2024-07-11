<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="PMS.Profile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
            <h2><asp:Label ID="lblProfileNote" runat="server" Text="Profile"></asp:Label></h2>
        <asp:Panel ID="panelAccountInfo" runat="server">
            <div class="profile-section">
                <h4>Account Information</h4>
                <asp:Table ID="tbAccountInfo" runat="server">
                    <asp:TableRow>
                        <asp:TableCell Width="30%"><asp:Label ID="Label1" runat="server" Text="User ID:"></asp:Label></asp:TableCell>
                        <asp:TableCell Width="30%"><asp:Label ID="lblUserID" runat="server" Text="User ID"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="30%"><asp:Label ID="Label2" runat="server" Text="Full Name:"></asp:Label></asp:TableCell>
                        <asp:TableCell Width="30%"><asp:Label ID="lblFullName" runat="server" Text="Full Name"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="30%"><asp:Label ID="Label3" runat="server" Text="Email:"></asp:Label></asp:TableCell>
                        <asp:TableCell Width="30%"><asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <!-- Placeholder for listing management options -->
            </div>
        </asp:Panel>
        <asp:Panel ID="panelListing" runat="server" Visible="false">
            <div class="dashboard-section">
                <h4>Your Listing</h4><br/> <asp:Button ID="btnAddProperty" runat="server" Width="150px"  OnClick="AddProperty_Click" Text="Add Property" CssClass="form-button" />
                <!-- Placeholder for listing management options -->
            </div>
        </asp:Panel>

        <asp:Panel ID="panelMessages" runat="server">
            <div class="dashboard-section">
                <h4>Messages</h4>
                <!-- Placeholder for message overview -->
                <p>No new messages.</p>
            </div>
        </asp:Panel>
       

        
       <!-- Update User Panel -->
        <asp:Panel ID="panelUpdate" runat="server" Visible="false">
            <div class="update-section">
                <h2>Update User</h2>
                <asp:TextBox runat="server" ID="txtUpdateUserID" placeholder="User ID" CssClass="form-input" ReadOnly="true"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtUpdatePassword" placeholder="Password" CssClass="form-input" TextMode="Password"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtUpdateFirstName" placeholder="First Name" CssClass="form-input"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtUpdateLastName" placeholder="Last Name" CssClass="form-input"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtUpdateEmail" placeholder="Email" CssClass="form-input"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtUpdatePhone" placeholder="Phone" CssClass="form-input"></asp:TextBox>
                <asp:RadioButtonList runat="server" ID="rblUpdateRole">
                    <asp:ListItem Text="Client" Value="client"></asp:ListItem>
                    <asp:ListItem Text="Realtor" Value="realtor"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:Label runat="server" ID="lblErrorUpdateFieldsRequired" CssClass="error-msg" Visible="False">All fields are required.</asp:Label>
                <asp:Label runat="server" ID="lblErrorUpdateFail" CssClass="error-msg" Visible="False">Update failed.</asp:Label>
                <asp:Label runat="server" ID="lblUpdateSuccess" CssClass="success-msg" Visible="False">User updated successfully.</asp:Label>
                <asp:Button runat="server" Text="Update" ID="btnUpdate" CssClass="form-button" OnClick="UpdateUser_Click"></asp:Button>
            </div>
        </asp:Panel>

        <!-- Trigger Button for Update Panel -->
        <asp:Button runat="server" Text="Update Profile" ID="btnUpdateProfile" CssClass="form-button" OnClick="UpdateProfileLink_Click"></asp:Button>
     
        <asp:Button runat="server" Text="Delete Account" ID="btnDeleteAccount" CssClass="form-button" OnClick="DeleteAccount_Click" />
<asp:Label runat="server" ID="lblDeleteSuccess" CssClass="success-msg" Visible="False">Account successfully deleted.</asp:Label>
<asp:Label runat="server" ID="lblDeleteFail" CssClass="error-msg" Visible="False">Failed to delete the account.</asp:Label>

    </main>
</asp:Content>

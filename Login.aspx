<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PMS.Login" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Login</title>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <asp:Panel ID="panelLogin" runat="server">
            <div class="login-section">
                <h2>Login</h2>
                <asp:Label runat="server" CssClass="error-msg" ID="lblErrorLoginFail" Visible="False">Invalid Login ID or Password.</asp:Label>
                <asp:Label runat="server" CssClass="error-msg" ID="lblErrorNoLoginInfo" Visible="False">Please enter your User ID and Password.</asp:Label>
                <asp:TextBox runat="server" ID="txtLoginUserID" placeholder="User ID" CssClass="form-input"></asp:TextBox>
                <asp:TextBox runat="server" CssClass="form-input" placeholder="Password" ID="txtLoginPassword" TextMode="Password"></asp:TextBox>
                <asp:Button runat="server" Text="Login" ID="btnLogin" CssClass="form-button" OnClick="LoginSubmit_Click"></asp:Button>
                <br />
                No account yet? Sign Up <asp:LinkButton ID="lbnRegister" CssClass="link" runat="server" OnClick="RegisterLink_Click">here</asp:LinkButton>
                <br />
                Forgot password? <asp:HyperLink ID="ChangePasswordLink" runat="server" NavigateUrl="~/ChangePassword.aspx">Change Password</asp:HyperLink>
            </div>
        </asp:Panel>
        <asp:Panel ID="panelRegister" runat="server" Visible="false">
            <div class="register-section">
                <h2>Sign Up</h2>
                <asp:TextBox runat="server" ID="txtRegisterUserID" placeholder="User ID" CssClass="form-input"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtRegisterFirstName" placeholder="First Name" CssClass="form-input"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtRegisterLastName" placeholder="Last Name" CssClass="form-input"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtRegisterEmail" placeholder="Email" CssClass="form-input"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtRegisterPassword" placeholder="Password" CssClass="form-input" TextMode="Password"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtConfirmPassword" placeholder="Confirm Password" CssClass="form-input" TextMode="Password"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtRegisterPhone" placeholder="Phone" CssClass="form-input"></asp:TextBox>
                <asp:RadioButtonList runat="server" ID="rblRegisterRole">
                    <asp:ListItem Text="Client" Value="client"></asp:ListItem>
                    <asp:ListItem Text="Realtor" Value="realtor"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:Label runat="server" ID="lblErrorRegisterFieldsRequired" CssClass="error-msg" Visible="False">All fields are required.</asp:Label>
                <asp:Label runat="server" ID="lblErrorRegisterFail" CssClass="error-msg" Visible="False">Registration failed.</asp:Label>
                <asp:Label runat="server" ID="lblErrorPasswordMismatch" CssClass="error-msg" Visible="False">Passwords do not match.</asp:Label>
                <asp:Label runat="server" ID="lblErrorRoleRequired" CssClass="error-msg" Visible="False">Role is required.</asp:Label>
                <asp:Button runat="server" Text="Register" ID="btnRegister" CssClass="form-button" OnClick="RegisterSubmit_Click"></asp:Button>
                Already have an account? Login <asp:LinkButton ID="lbnLogin" CssClass="link" runat="server" OnClick="LoginLink_Click">here</asp:LinkButton>
            </div>
        </asp:Panel>
    </main>
</asp:Content>

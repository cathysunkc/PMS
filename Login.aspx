<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PMS.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
         <asp:Panel ID="panelLogin" runat="server">
            <div class="login-section">               
                <h2>Login</h2>
                
                    <asp:Label runat="server" CssClass="error-msg" ID="lblErrorLoginFail" Visible="False">Invalid Login ID or Password.</asp:Label>
                    <asp:label runat="server" CssClass="error-msg" ID="lblErrorNoLoginInfo" Visible="False">Please enter your User ID and Password.</asp:label>

                    <asp:TextBox runat="server" ID="txtLoginUserID" placeholder="User ID" CssClass="form-input" ></asp:TextBox>                    

                <asp:TextBox runat="server" CssClass="form-input" placeholder="Password" ID="txtLoginPassword" TextMode="Password"></asp:TextBox>
                    <asp:Button runat="server" Text="Login" ID="btnLogin" CssClass="form-button" OnClick="LoginSubmit_Click"></asp:Button>

                    No account yet? Sign Up <asp:LinkButton ID="lbnRegister" CssClass="link" runat="server" OnClick="RegisterLink_Click">here</asp:LinkButton>
                
            </div>
         </asp:Panel>
         <asp:Panel ID="panelRegister" runat="server" Visible="false">
            <div class="register-section">
                <h2>Sign Up</h2>

                <asp:Label ID="lblErrorRegisterFieldsRequired" runat="server" CssClass="error-msg" Text="All fields are required." Visible="false"></asp:Label>
                <asp:Label ID="lblErrorRegisterFail" runat="server" CssClass="error-msg" Text="Registration failed. Please try again." Visible="false"></asp:Label>
                <asp:Label ID="lblErrorPasswordMismatch" runat="server" CssClass="error-msg" Text="Passwords do not match." Visible="false"></asp:Label>

                    <asp:TextBox runat="server" ID="txtRegisterFirstName" placeholder="First Name" CssClass="form-input"></asp:TextBox>    
                <asp:TextBox runat="server" ID="txtRegisterLastName" placeholder="Last Name" CssClass="form-input"></asp:TextBox> 
                <asp:TextBox runat="server" ID="txtRegisterEmail" placeholder="Email" CssClass="form-input"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtRegisterPhone" placeholder="Phone" CssClass="form-input"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtRegisterUserID" placeholder="User ID" CssClass="form-input"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtRegisterPassword" placeholder="Password" CssClass="form-input" TextMode="Password"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtConfirmPassword"  placeholder="Confirm Password" CssClass="form-input" TextMode="Password"></asp:TextBox>





                <asp:DropDownList ID="ddlRegisterRole" runat="server" CssClass="form-input">
                    <asp:ListItem Value="" Text="Select Role"  />
                    <asp:ListItem Value="realtor">Realtor</asp:ListItem>
                    <asp:ListItem Value="client">Client</asp:ListItem>
                </asp:DropDownList>





                <asp:Button runat="server" Text="Register" ID="btnRegister" CssClass="form-button" OnClick="RegisterSubmit_Click"></asp:Button>


                Already had an account? Login <asp:LinkButton ID="lbnLogin" CssClass="link" runat="server" OnClick="LoginLink_Click">here</asp:LinkButton>
                
            </div>
        </asp:Panel>        
    </main>
</asp:Content>

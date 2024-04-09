<%@ Page Title="ContactRealtor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="PMS.ContactRealtor" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">   

              <h2><asp:Label ID="lblNote" runat="server" Text="Message"></asp:Label></h2>
            
                <asp:Label ID="lblSenderEmail" runat="server" Text="Your Email:"></asp:Label>                
                <asp:TextBox ID="txtSenderEmail" runat="server" class="form-input" ReadOnly=true></asp:TextBox>
                <asp:Label ID="lblRecipientEmail" runat="server" Text="Recipient Email:"></asp:Label><br/>
                <asp:TextBox ID="txtRecipientEmail" runat="server" class="form-input"></asp:TextBox>
                <label for="message">Your Message:</label>
                <textarea id="message" name="message" class="form-input" required></textarea>
                <input type="submit" value="Send" class="form-button">            

    </main>
</asp:Content>

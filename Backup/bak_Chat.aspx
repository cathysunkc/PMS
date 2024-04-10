<%@ Page Title="Chat" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="bak_Chat.aspx.cs" Inherits="PMS.Chat" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">

            <h2>Chat with a Realtor</h2>
            <br>
            <!-- Placeholder for more chat messages -->
            <div id="chat-box" class="chat-container">
                <div class="chat-message realtor-message">
                    <p><strong>Realtor:</strong> Hello! How can I help you today?</p>
                </div>
                <!-- Placeholder for user message -->
                <div class="chat-message user-message">
                    <p><strong>You:</strong> I'm interested in the downtown apartment listing.</p>
                </div>
                <!-- Additional messages would follow the same pattern -->
            </div>

            <form id="chat-form">
                <input type="text" id="chat-message" class="form-input" placeholder="Type your message here..." required>
                <input type="submit" value="Send" class="form-button">
            </form>

    </main>
</asp:Content>

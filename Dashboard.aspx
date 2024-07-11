<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="PMS.Dashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
            <h2><asp:Label ID="lblDashboardNote" runat="server" Text="Dashboard"></asp:Label></h2>
        <asp:Panel ID="panelAccountInfo" runat="server">
            <div class="dashboard-section">
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
        <asp:Panel ID="panelReports" runat="server" Visible="false">
            <div class="dashboard-section">
                <h4>Reports</h4>
                <!-- Placeholder for analytics/metrics -->
                <p>To be implemented.</p>
            </div>
        </asp:Panel>    
            
    </main>
</asp:Content>

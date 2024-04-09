<%@ Page Title="View Property" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewProperty.aspx.cs" Inherits="PMS.ViewProperty" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">

            <h2>View Property</h2>
                <table>
                    <tr><td style="padding:10px"><h4><asp:Label ID="lblAddress01" runat="server" Text="Address"></asp:Label></h4></td></tr>
                    <tr><td style="padding:10px"><asp:Image ID="imgProperty" runat="server" Width="100%"></asp:Image></td></tr>
                </table>
                <table style="width: 80%">
                    <tr>
                        <td style="padding:10px; width: 50%"><h5><asp:Label ID="lblPrice" ForeColor="gold" runat="server" Text="Price"></asp:Label></h5></td>
                        <td style="text-align:right"><h5><asp:Label ID="lblArea"  runat="server" Text="Area"></asp:Label></h5></td></tr>                
                    <tr>
                        <td style="padding:10px">
                            <asp:Label ID="lblAddress02" runat="server" Text="Address"></asp:Label><br>
                            <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label> <asp:Label ID="lblZipCode" runat="server" Text="Zip Code"></asp:Label>
                        </td>
                    </tr>                       
                </table>
                <hr/>
                <table style="width: 80%">
                    <tr><td style="padding:10px; width: 80%"><asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label></td></tr>
                </table>
                <hr/>
                <table style="width: 80%">
                    <tr style="padding:20px">
                        <td style="padding-left:10px;width: 25%"><label style="font-weight:bold">Property Type:</label></td>
                        <td style="padding-left:10px;width: 25%"><label style="font-weight:bold">Parking Type:</label></td>
                        <td style="padding-left:10px;width: 25%"><label style="font-weight:bold">Bed Number:</label></td>
                        <td style="padding-left:10px; width: 25%"><label style="font-weight:bold">Bath Number:</label></td>
                    </tr>
                    <tr style="padding:20px">
                        <td style="padding-left:10px;width: 25%"><asp:Label ID="lblPropertyType" runat="server" Text="Property Type"></asp:Label></td>
                        <td style="padding-left:10px;width: 25%"><asp:Label ID="lblParkingType" runat="server" Text="Parking Type"></asp:Label></td>
                        <td style="padding-left:10px;width: 25%"><asp:Label ID="lblBedNum" runat="server" Text="Bed Number"></asp:Label></td>
                        <td style="padding-left:10px;width: 25%"><asp:Label ID="lblBathNum" runat="server" Text="Bath Number"></asp:Label></td>
                    </tr>
                </table>
                <hr/>
                <table style="width: 80%">
                    <tr style="padding:20px">
                        <td style="padding:10px; width: 25%"><asp:Label ID="Label1" Font-Bold="true" runat="server" Text="Available On:"></asp:Label><br/>
                            <asp:Label ID="lblAvailableOn" runat="server" Text="Available On"></asp:Label>
                        </td>
                        <td style="padding-left:10px;width: 25%"></td>
                        <td style="padding-left:10px;width: 25%"></td>
                        <td style="padding-left:10px;width: 25%">
                            <asp:Button ID="btnContactRealtor" runat="server" OnClick="ContactReatlor_Click" CssClass="form-button"  Text="Contact Realtor" />
                            <asp:Button ID="btnEditProperty" runat="server" CssClass="form-button"  Text="Edit Property"/>
                        </td>
                    </tr>                     
                </table>

    </main>
</asp:Content>

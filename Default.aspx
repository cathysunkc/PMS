<%@ Page Title="Default"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PMS.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
            <section class="map-container">
                <h2>Find Properties Near You</h2>
                <p>Explore the map to find properties in your desired area.</p>
                <!-- Placeholder for the map -->
                <img src="https://i.stack.imgur.com/F9v3y.png" alt="Map Placeholder" style="max-width:100%; height:auto;">
            </section>

            <section class="listings-container">
                <h2>Featured Listings</h2>
                    
                    <asp:GridView runat="server" CellPadding="10" ID="gridProperty" CssClass="listing" ShowHeader="False" GridLines="None" AutoGenerateColumns="False">
                        <Columns> 
                        <asp:TemplateField>
                            <ItemTemplate> 
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("address")%>' Font-Size="X-Large"  /><br>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("price", "{0:c}") %>' Font-Size="X-Large"  /><br>
                                <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# "ViewProperty?id=" + Eval("property_id")%>' runat="server"><asp:Image ID="Image1" runat="server" style="max-width:100%; height:auto;" ImageUrl='<%#Eval("image_path")%>'/></asp:HyperLink><br/>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("description")%>' />
                            </ItemTemplate> 
                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                
              </section>
    </main>
</asp:Content>

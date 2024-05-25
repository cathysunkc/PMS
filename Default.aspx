<%@ Page Title="Default"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PMS.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
            <section class="map-container">
                <h2>Find Properties Near You</h2>
                <p>Explore the map to find properties in your desired area.</p>
                <!-- Placeholder for the map -->
                <script src="https://use.fontawesome.com/releases/v6.2.0/js/all.js"></script>                               
                <div id="googleMap" style="width:100%;height:500px;"></div>
                <script type="text/javascript" src="Scripts/map.js"></script>
                <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCdCFuqERJyLjpKCjXdokfdPWGk-Niu8Pk&callback=myMap"></script>
                </section>
                <section class="listings-container">
                <h2>Featured Listings</h2>                    
                    <asp:GridView runat="server" CellPadding="10" ID="gridProperty" CssClass="listing" ShowHeader="False" GridLines="None" AutoGenerateColumns="False">
                        <Columns> 
                        <asp:TemplateField>
                            <ItemTemplate> 
                                <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("address")%>' Font-Size="X-Large"  /><br>
                                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:c}") %>' Font-Size="X-Large"  /><br>
                                <asp:HyperLink ID="hlkProperty" NavigateUrl='<%# "ViewProperty?id=" + Eval("property_id")%>' runat="server">
                                <asp:Image ID="imgPropertyImage" runat="server" style="max-width:100%; height:auto;" ImageUrl='<%# "~/Images/" + Eval("property_id") + "/" + Eval("property_id") + "01.jpg" %>'/></asp:HyperLink><br/>
                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("description")%>' />
                            </ItemTemplate> 
                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>                
              </section>
    </main>
</asp:Content>

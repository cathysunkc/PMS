<%@ Page Title="Index" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PMS.Index" %>

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
                <div class="listing">
                    <h3>Luxury Downtown Apartment $450,000</h3>
                    <img src="https://robbreport.com/wp-content/uploads/2022/08/The-Grand-by-Gehry_Great-Room_Image-Courtesy-of-Peter-Christiansen-Valli-for-The-Grand-by-Gehry.jpg" alt="Luxury Downtown Apartment" style="max-width:100%; height:auto;">
                    <p>Spacious 2-bedroom apartment in the heart of the city. Modern amenities, close to public transport.</p>
                    <!-- Contact Realtor link for this listing -->
                    <a href="\ContactRealtor" class="form-button">Contact Realtor</a>
                </div>
                <div class="listing">
                    <h3>Cozy Suburban House $580,495</h3>
                    <img src="https://www.idesignarch.com/wp-content/uploads/2686-Eagleridge-Drive-Coquitlam_1.jpg" alt="Cozy Suburban House" style="max-width:100%; height:auto;">
                    <p>A charming 4-bedroom house in a quiet neighborhood. Perfect for families.</p>
                    <!-- Contact Realtor link for this listing -->
                    <a href="\ContactRealtor" class="form-button">Contact Realtor</a>
                </div>
                <!-- Additional listings can be added here -->
            </section>
    </main>
</asp:Content>

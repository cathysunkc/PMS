<%@ Page Title="AddProperty" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProperty.aspx.cs" Inherits="PMS.AddProperty" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2>Add New Property</h2>
        <br/>
        <%-- Changed form to runat="server" for server-side handling --%>
        <asp:Panel ID="addPropertyPanel" runat="server">
            <label for="propertyName">Property Address:</label>
            <input type="text" id="propertyName" name="propertyName" class="form-input" required>

            <label for="propertyCity">City:</label>
            <input type="text" id="txtCity" name="propertyCity" class="form-input" required>

            <label for="propertyZip">Zip Code:</label>
            <input type="text" id="txtZipCode" name="propertyZip" class="form-input" required>

            <label for="propertyType">Property Type:</label>
            <input type="text" id="txtPropertyType" name="propertyType" class="form-input" required> 

            <label for="description">Description:</label>
            <textarea id="description" name="description" class="form-input" required></textarea>

            <label for="area">Area:</label>
            <input type="text" id="area" name="area" class="form-input" required>

            <label for="bedNum">Bed Number:</label>
            <input type="number" id="bedNum" name="bedNum" class="form-input" required>

            <label for="bathNum">Bath Number:</label>
            <input type="number" id="bathNum" name="bathNum" class="form-input" required>

            <label for="parkingType">Parking Type:</label>
            <input type="text" id="txtParkingType" name="parkingType" class="form-input" required> 

            <!-- Grouped radio buttons with GroupName -->
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:RadioButton ID="RadioButton1" GroupName="transactionType" Text="For Rent" runat="server" />
                <asp:RadioButton ID="RadioButton2" GroupName="transactionType" Text="For Sale" runat="server" />
            </div>

            <label for="price">Price ($):</label>
            <input type="text" id="price" name="price" class="form-input" required>

            <label for="date">Available Date:</label><br/>
            <input type="date" id="availableDate" name="availableDate" class="form-input" required>
                
            <label for="images">Upload Images:</label>
            <input type="file" id="images" name="images" class="form-input" multiple>

            <!-- Changed submit button to ASP.NET Button control -->
            <asp:Button ID="submitBtn" Text="Submit" OnClick="SubmitBtn_Click" runat="server" CssClass="form-button"/>
        </asp:Panel>
    </main>
</asp:Content>

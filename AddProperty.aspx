<%@ Page Title="AddProperty" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProperty.aspx.cs" Inherits="PMS.AddProperty" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">

            <h2>Add New Property</h2>
            <br/>
                <label for="propertyName">Property Address:</label>
                <input type="text" id="propertyName" name="propertyName" class="form-input" required>
                <label for="propertyCity">City:</label>
                <input type="text" id="txtCity" name="propertyName" class="form-input" required>
                <label for="propertyCity">Zip Code:</label>
                <input type="text" id="txtZipCode" name="propertyName" class="form-input" required>
                <label for="bedNum">Property Type:</label>
                <input type="text" id="txtPropertyType" name="propertyName" class="form-input" required> 
                <label for="description">Description:</label>
                <textarea id="description" name="description" class="form-input" required></textarea>
                 <label for="bedNum">Area:</label>
                <input type="text" id="area" name="price" class="form-input" required>
                 <label for="bedNum">Bed Number:</label>
                <input type="number" id="bedNum" name="price" class="form-input" required>
                <label for="bedNum">Bath Number:</label>
                <input type="number" id="bathNum" name="price" class="form-input" required>
                <label for="bedNum">Parking Type:</label>
                <input type="text" id="txtParkingType" name="propertyName" class="form-input" required> 
                <div style="padding-top: 20px; padding-bottom: 20px"><asp:RadioButton ID="RadioButton1" Width="100px" Text="For Rent" runat="server" />
                <asp:RadioButton ID="RadioButton2" Width="100px" Text="  For Sale" runat="server" /></div>
                <label for="price">Price ($):</label>
                <input  id="price" name="price" class="form-input" required>
                <label for="date">Available Date:</label><br/>                
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-input" ></asp:TextBox>        
                <label for="images">Upload Images:</label>
                <input type="file" id="images" name="images" class="form-input" multiple>
                <input type="submit" value="Submit" class="form-button">

    </main>
</asp:Content>

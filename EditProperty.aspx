﻿<%@ Page Title="Edit Property" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="EditProperty.aspx.cs" Inherits="PMS.EditProperty" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
       .image-container {
            position: relative;
            display: inline-block;
        }

        .image-container img {
            width: 100%;
            height: auto;
        }

        .overlay-text {
            position: absolute;
            top: -5px;
            left: 1%;
            /* transform: translate(-50%, -50%); */
            background-color: #5E2D79;
            color: #fff;
            padding: 10px 10px;
            font-size: 20px;
            text-align: center;
        }

         .overlay-delete {
            position: relative;
            border: none;
            cursor: pointer;
            top: -2em;
            left: 7.4em;               
         }

        .overlay-previous {
            position: relative;
            cursor: pointer;
            bottom: 2em;
            right: 2em;
       
        }
        .overlay-next {
            position: relative;
            cursor: pointer;
            bottom: 2em;
            right: 2.2em;
        }
        input[type=file]::file-selector-button {        
        
          border-radius: 3px;
          border-color:  #D3CBFF;
          background-color: #D3CBFF;
          width: 10em;
          transition: 1s;
          color: #2C003E;
          cursor: pointer;
        }

        #MainContent_btnAddImage {
            border-radius: 3px;
            background-color: #D3CBFF;
            width: 8em;
            transition: 1s;
            color: #2C003E;
            cursor: pointer;
        }
        

        #loading {
            position: fixed;
            display: block;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            text-align: center;
            opacity: 0.4;
            background-color: black;
            z-index: 99;
        }

        #loading-image {
            position: absolute;
            top: 50%;
            left: 50%;
            
        }

    </style>
    <main aria-labelledby="title">
        <h2>Edit Property</h2>
        <br/>
         
        <asp:Panel ID="editPropertyPanel" runat="server">
                             
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-msg" Visible="False"></asp:Label><br/>
            <asp:FileUpload ID="FileUpload" runat="server" /><asp:Button ID="btnAddImage" OnClientClick="WaitDialog()" CausesValidation="false" OnClick="btnAddImage_Click"  runat="server" Text="Add Image" CssClass="AddImage"  />

            <div id="ImageGallery" style="overflow:auto; width:100%; margin-top: 1em; border-color: #D3CBFF; border-width: 5px; display:inline-block;">
                
                <asp:ListView ID="listImages" runat="server" GroupItemCount="6" ShowHeader="False" AutoGenerateColumns="False" >
                    <LayoutTemplate>
                    <table id="table1">
                      <tr runat="server" id="groupPlaceholder">
                      </tr>
                    </table>
                    </LayoutTemplate>
                      <GroupTemplate>
                        <tr runat="server" id="tableRow">
                          <td runat="server" id="itemPlaceholder" />
                        </tr>   
                      </GroupTemplate>
                     <ItemTemplate>
                        <td runat="server" style="width:160px; height: 160px; vertical-align: top; padding: 3px">
                            <div class="image-container">
                                <img src='<%#Eval("FilePath") + "?time=" + DateTime.UtcNow %>' alt='<%#Eval("FilePath") %>' style="cursor:pointer;width: 150px;height:140px;background-position: center center;  background-repeat: no-repeat;" />
                                <div class="overlay-text"><%#Eval("Index") %></div>
                                 <asp:ImageButton OnClientClick="WaitDialog()" CausesValidation="false" ImageUrl="~/Icons/trash-bin.png" CssClass="overlay-delete" ID="deleteImageButton" runat="server" OnCommand="deleteImageButton_Command" CommandArgument='<%#Eval("Index").ToString() %>' width="30px" height="30px" ></asp:ImageButton> 
                                 <asp:ImageButton OnClientClick="WaitDialog()" CausesValidation="false" ImageUrl="~/Icons/previous-arrow.png" CssClass="overlay-previous" ID="movePreviousButton" runat="server" OnCommand="movePreviousButton_Command" CommandArgument='<%#Eval("Index").ToString() %>' Visible='<%# Eval("Index").ToString() != "1" ? true : false  %>'  width="30px" height="30px" ></asp:ImageButton> 
                                 <asp:ImageButton OnClientClick="WaitDialog()" CausesValidation="false" ImageUrl="~/Icons/next-arrow.png" CssClass="overlay-next" ID="moveNextButton" runat="server" OnCommand="moveNextButton_Command" CommandArgument='<%#Eval("Index").ToString() %>' Visible='<%# Eval("Index").ToString() ==  Session["ImageListCount"].ToString() ? false : true  %>' width="30px" height="30px" ></asp:ImageButton> 
                            </div>    
                        </td>
                      </ItemTemplate>                   
                </asp:ListView>  
             </div>
             <div id="loading" style="display: none; width: 100%; height: 100%">
                    <img src="Icons/loading.gif" id="loading-image" style="height: 64px; width: 70px" alt="Loading..." />                
            </div>

            <script>
                function WaitDialog() {
                    var mywait = document.getElementById("loading")
                    mywait.style.display = 'block';
                }
            </script>
            <br />
            <asp:RequiredFieldValidator ID="rfvPropertyName" runat="server" ControlToValidate="txtPropertyName" ErrorMessage="Property Address is required." CssClass="error-msg"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblPropertyName" runat="server" Text="Property Address:"></asp:Label>
            <br />
            <asp:TextBox ID="txtPropertyName" runat="server" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Label ID="lblErrorPropertyName" runat="server" CssClass="error-msg" Visible="False"></asp:Label>
            <br />

            <asp:RequiredFieldValidator ID="rfvPropertyCity" runat="server" ControlToValidate="txtPropertyCity" ErrorMessage="City is required." CssClass="error-msg"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblPropertyCity" runat="server" Text="City:"></asp:Label>
            <br />
            <asp:TextBox ID="txtPropertyCity" runat="server" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Label ID="lblErrorPropertyCity" runat="server" CssClass="error-msg" Visible="False"></asp:Label>
            <br />

            <asp:RequiredFieldValidator ID="rfvPropertyZip" runat="server" ControlToValidate="txtPropertyZip" ErrorMessage="Zip Code is required." CssClass="error-msg"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblPropertyZip" runat="server" Text="Zip Code:"></asp:Label>
            <br />
            <asp:TextBox ID="txtPropertyZip" runat="server" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Label ID="lblErrorPropertyZip" runat="server" CssClass="error-msg" Visible="False"></asp:Label>
            <br />

            <asp:RequiredFieldValidator ID="rfvPropertyType" runat="server" ControlToValidate="txtPropertyType" ErrorMessage="Property Type is required." CssClass="error-msg"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblPropertyType" runat="server" Text="Property Type:"></asp:Label>
            <br />
            <asp:TextBox ID="txtPropertyType" runat="server" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Label ID="lblErrorPropertyType" runat="server" CssClass="error-msg" Visible="False"></asp:Label>
            <br />

            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Description is required." CssClass="error-msg"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label>
            <br />
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Label ID="lblErrorDescription" runat="server" CssClass="error-msg" Visible="False"></asp:Label>
            <br />

            <asp:RequiredFieldValidator ID="rfvArea" runat="server" ControlToValidate="txtArea" ErrorMessage="Area is required." CssClass="error-msg"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblArea" runat="server" Text="Area:"></asp:Label>
            <br />
            <asp:TextBox ID="txtArea" runat="server" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Label ID="lblErrorArea" runat="server" CssClass="error-msg" Visible="False"></asp:Label>
            <br />

            <asp:RequiredFieldValidator ID="rfvBedNum" runat="server" ControlToValidate="txtBedNum" ErrorMessage="Bed Number is required." CssClass="error-msg"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblBedNum" runat="server" Text="Bed Number:"></asp:Label>
            <br />
            <asp:TextBox ID="txtBedNum" runat="server" CssClass="form-input" TextMode="Number"></asp:TextBox>
            <br />
            <asp:Label ID="lblErrorBedNum" runat="server" CssClass="error-msg" Visible="False"></asp:Label>
            <br />

            <asp:RequiredFieldValidator ID="rfvBathNum" runat="server" ControlToValidate="txtBathNum" ErrorMessage="Bath Number is required." CssClass="error-msg"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblBathNum" runat="server" Text="Bath Number:"></asp:Label>
            <br />
            <asp:TextBox ID="txtBathNum" runat="server" CssClass="form-input" TextMode="Number"></asp:TextBox>
            <br />
            <asp:Label ID="lblErrorBathNum" runat="server" CssClass="error-msg" Visible="False"></asp:Label>
            <br />

            <asp:RequiredFieldValidator ID="rfvParkingType" runat="server" ControlToValidate="txtParkingType" ErrorMessage="Parking Type is required." CssClass="error-msg"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblParkingType" runat="server" Text="Parking Type:"></asp:Label>
            <br />
            <asp:TextBox ID="txtParkingType" runat="server" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Label ID="lblErrorParkingType" runat="server" CssClass="error-msg" Visible="False"></asp:Label>
            <br />

            <asp:CustomValidator ID="cvTransactionType" runat="server" ClientValidationFunction="ValidateTransactionType" OnServerValidate="ValidateTransactionType" ErrorMessage="Transaction Type is required." CssClass="error-msg"></asp:CustomValidator>
            <br />
            <asp:Label ID="lblTransactionType" runat="server" Text="Transaction Type:"></asp:Label>
            <br />
            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="TransactionType" Text="For Rent" />
            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="TransactionType" Text="For Sale" />
            <br />
            <asp:Label ID="lblErrorTransactionType" runat="server" CssClass="error-msg" Visible="False"></asp:Label>
            <br />

            <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice" ErrorMessage="Price is required." CssClass="error-msg"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblPrice" runat="server" Text="Price ($):"></asp:Label>
            <br />
            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Label ID="lblErrorPrice" runat="server" CssClass="error-msg" Visible="False"></asp:Label>
            <br />

            <asp:RequiredFieldValidator ID="rfvAvailableDate" runat="server" ControlToValidate="txtAvailableDate" ErrorMessage="Available Date is required." CssClass="error-msg"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblAvailableDate" runat="server" Text="Available Date:"></asp:Label>
            <br />
            <asp:TextBox ID="txtAvailableDate" runat="server" TextMode="Date" CssClass="form-input"></asp:TextBox>
            <br />
            <asp:Label ID="lblErrorAvailableDate" runat="server" CssClass="error-msg" Visible="False"></asp:Label>
            <br />
           
           
           
            <asp:Button ID="btnSave" Text="Save Changes" OnClick="SaveChanges_Click" runat="server" CssClass="form-button"/>
            <br />
        </asp:Panel>

        <script type="text/javascript">
            function ValidateTransactionType(source, args) {
                var radioButton1 = document.getElementById('<%= RadioButton1.ClientID %>');
                var radioButton2 = document.getElementById('<%= RadioButton2.ClientID %>');
                args.IsValid = radioButton1.checked || radioButton2.checked;
            }
           
        </script>
    </main>
</asp:Content>

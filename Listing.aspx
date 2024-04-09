﻿<%@ Page Title="Listing" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="PMS.Listing" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" style="min-height:300px">
            <h2>Property Listings</h2>
            <br>
            <!-- Listing 1 -->
            <asp:Panel ID="panelAddListing" runat="server" Visible="false">
                 <div class="add-listing-cta">
                 <p>Have a property to list?&nbsp;&nbsp;<asp:Button ID="btnAddProperty" Width="150px" runat="server" OnClick="AddProperty_Click" Text="Add Property" CssClass="form-button" /></p>
             </div>
            </asp:Panel>
             <asp:Panel ID="panelSearch" runat="server">
                 <asp:Table ID="Table2" runat="server" CellPadding="10" Width="80%">
                     <asp:TableRow>
                         <asp:TableCell Width="30%">
                             <asp:DropDownList ID="ddlTransactionType" AutoPostBack="True"  OnSelectedIndexChanged="TransactionType_SelectedIndexChanged" CssClass="form-input" runat="server"></asp:DropDownList></asp:TableCell>
                         <asp:TableCell Width="25%">
                             <asp:DropDownList ID="ddlBedNum" AutoPostBack="True"  OnSelectedIndexChanged="BedNum_SelectedIndexChanged" CssClass="form-input" runat="server"></asp:DropDownList></asp:TableCell>
                         <asp:TableCell Width="25%">
                             <asp:DropDownList ID="ddlBathNum" AutoPostBack="True"  OnSelectedIndexChanged="BathNum_SelectedIndexChanged" CssClass="form-input" runat="server"></asp:DropDownList>
                         </asp:TableCell>
                         <asp:TableCell Width="20%">
                           <asp:Button ID="btnReset" CssClass="form-button" runat="server" OnClick="Reset_Click" Text="Reset" Width="80px" /></asp:TableCell>

                     </asp:TableRow>
                 </asp:Table>
                 <asp:Label ID="lblNoPropertyFound" runat="server" Text="No Property Found."></asp:Label>
             </asp:Panel>
            <br>
            
             <asp:ListView ID="listProperty" GroupItemCount="3"  ShowHeader="False" runat="server" AutoGenerateColumns="False">
            <LayoutTemplate>
                <table runat="server" id="table1">
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
                <td runat="server" style="width: 30%; vertical-align: top; padding: 20px">
                 <asp:Label ID="Label1" runat="server" Text='<%#Eval("address")%>' Font-Size="X-Large"  /><br/>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("price", "{0:c}") %>' Font-Size="X-Large"  /> <asp:Label ID="Label3" runat="server" Text='<%# Eval("transaction_type").ToString() == "R" ? "/Monthly" : "" %>' Font-Size="X-Large"  /><br/>
                 <div style="min-height: 200px"><a href='<%# "ViewProperty?id=" + Eval("property_id")%>'><asp:Image ID="Image1" style="max-width:100%; height:auto;" runat="server" ImageUrl='<%#Eval("image_path")%>'/></a></div> 
                 <asp:Label ID="Label4" runat="server" Text='<%#Eval("description")%>' />
                 </div>
             </td>
          </ItemTemplate>
        </asp:ListView>      
    </main>
</asp:Content>

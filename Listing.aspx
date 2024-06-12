<%@ Page Title="Listing" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="PMS.Listing" %>

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
                 <asp:Table ID="Table2" runat="server" CellPadding="10" Width="100%">
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
                     <asp:TableRow>
                         <asp:TableCell ColumnSpan="4" Width="100%" HorizontalAlign="Left">
                             Sort By: &nbsp;<asp:DropDownList ID="ddlSortType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSortType_SelectedIndexChanged" CssClass="form-input" Width="25%"></asp:DropDownList>
                         </asp:TableCell>
                     </asp:TableRow>
                 </asp:Table>
                 <asp:Label ID="lblNoPropertyFound" runat="server" Text="No Property Found."></asp:Label>
             </asp:Panel>
            <br>
             <asp:ListView ID="listProperty" GroupItemCount="3"  ShowHeader="False" runat="server" AutoGenerateColumns="False" OnPagePropertiesChanging="listProperty_PagePropertiesChanging">
            <LayoutTemplate>
                <table runat="server" id="table1">
                  <tr runat="server" id="groupPlaceholder">
                  </tr>
                </table>
               <div style="text-align:center">
                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="listProperty" PageSize="9">
                            <Fields>       
                               <asp:NumericPagerField ButtonType="Link" />
                            </Fields>
                        </asp:DataPager>
               </div>
              </LayoutTemplate>
              <GroupTemplate>
                <tr runat="server" id="tableRow">
                  <td runat="server" id="itemPlaceholder" />
                </tr>
              </GroupTemplate>            
             <ItemTemplate>
                <td runat="server" style="width: 30%; vertical-align: top; padding: 15px">
                 <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("address")%>' style="white-space:nowrap" Font-Size="X-Large"  /><br/>
                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:c}") %>' Font-Size="X-Large"  /> <asp:Label ID="Label3" runat="server" Text='<%# Eval("transaction_type").ToString() == "R" ? "/Monthly" : "" %>' Font-Size="X-Large"  /><br/>
                 <div style="min-height: 200px"><a href='<%# "ViewProperty?id=" + Eval("property_id")%>'>
                     <asp:Image ID="imgPropertyImage" style="max-width:100%; height:200px; background-position: center center;
background-repeat: no-repeat;" runat="server" ImageUrl='<%# "~/Images/" + Eval("property_id") + "/" + Eval("property_id")  + "01.jpg" %>'/></a></div> 
                 <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("description") %> ' />
                 </div>
             </td>
          </ItemTemplate>
                 
        </asp:ListView>  
       
    </main>
</asp:Content>

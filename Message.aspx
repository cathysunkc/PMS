﻿<%@ Page Title="ContactRealtor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="PMS.ContactRealtor" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">   

        <!-- Edited by Wilson -->
        <h2><asp:Label ID="lblNote" runat="server" Text="Message"></asp:Label></h2>

        <asp:Table ID="Table2" CssClass="chattestcontainer" runat="server" CellPadding="10" Width="100%">
            <asp:TableRow>
                <asp:TableCell Width="30%">
                    <asp:ListView ID="listMessage" ShowHeader="False" runat="server" AutoGenerateColumns="True">
                        <LayoutTemplate>
                            <table id="Table3" runat="server" CellPadding="10" class="chat-box" >
                                <tr runat="server" id="itemPlaceholder" class="chat-container" />
                            </table>
                        </LayoutTemplate>

                        <ItemTemplate>
                            <tr id="tableRow" runat="server" >

                                <td runat="server" class='<%# Eval("recipent.UserID").ToString() == Session["UserID"].ToString() && Eval("is_checked").ToString() == "False" ? "uncheck-msgGroup" : "" %>'>
                                    <asp:LinkButton ID="lbnRegister" runat="server" class='<%# Eval("is_checked").ToString() == "True" ? "" : "uncheck-msgGroupbutton" %>' CommandArgument='<%# Eval("property.PropertyID")%>'  OnCommand="Message_Click">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("property.Address").ToString().Length > 25 ? Eval("property.Address").ToString().Substring(0,25) : Eval("property.Address") %>' Font-Size="medium"  /><br/>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("sender.UserID").ToString() == Session["UserID"].ToString() ? Eval("recipent.FirstName").ToString()+" "+Eval("recipent.LastName").ToString() : Eval("sender.FirstName").ToString()+" "+Eval("sender.LastName").ToString() %>' Font-Size="medium"  /> <br />
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("content").ToString().Length > 45 ? Eval("content").ToString().Substring(0,45) : Eval("content") %>' Font-Size="x-small"  /><br/>
                                        <asp:Label ID="Label53" runat="server" Text='<%# Eval("is_checked") %>' Font-Size="x-small" />
                                    </asp:LinkButton>
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:ListView> 
                </asp:TableCell>
                <asp:TableCell Width="70%" VerticalAlign="Top">
                    <h4><asp:Label runat="server" ID="panelSelectMessageNull" CssClass="" Visible="True">Please select message to communicate</asp:Label></h4>
                    <asp:Panel ID="panelSelectMessage" runat="server" Visible="false">
                        <div class="chat-container" style="height: 300px; width:100%; overflow-y: auto;">
                            <!-- Hidden 
                            <asp:Label ID="lblSenderEmail" runat="server" Text="Your Email:"></asp:Label>                
                            <asp:TextBox ID="txtSenderEmail" runat="server" class="form-input" ReadOnly=true></asp:TextBox>
                            <asp:Label ID="lblRecipientEmail" runat="server" Text="Recipient Email:"></asp:Label><br/>
                            <asp:TextBox ID="txtRecipientEmail" runat="server" class="form-input"></asp:TextBox>
                            -->
                            <h4><asp:Label runat="server" ID="gridMessageNull" CssClass="" Visible="False">Please enter message to communicate</asp:Label></h4>
                            <asp:GridView runat="server" CellPadding="10" ID="gridMessage" CssClass="listing" ShowHeader="False" GridLines="None" AutoGenerateColumns="False">
                                <Columns> 
                                <asp:TemplateField>
                                    <ItemTemplate > 
                                        <asp:Panel runat="server" class='<%# Eval("recipent.UserID").ToString() == Session["UserID"].ToString() ? "user-message" : "another-message" %>  '>
                                            <asp:Label ID="Label4" runat="server" CssClass='<%# Eval("recipent.UserID").ToString() == Session["UserID"].ToString() && Eval("is_checked").ToString() == "False" ? "uncheck-message" : "" %>' Text='<%# Eval("content") %>' Font-Size="medium"  /><br/>                              
                                        </asp:Panel>
                                    </ItemTemplate> 
                                </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>

                        <div>
                            <asp:hiddenfield ID="sendMessage" Value="" runat="server"/>
                            <label for="txtMessage">Your Message:</label>
                            <asp:TextBox runat="server" ID="txtMessage" placeholder="Input here" CssClass="form-input" MaxLength="200" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            <asp:Button ID="btnSend" CssClass="form-button" runat="server" OnClick="Send_Click" Text="Send" Width="80px" />
                        </div>
                    </asp:Panel>

                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </main>
</asp:Content>

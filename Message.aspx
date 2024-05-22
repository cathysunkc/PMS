﻿<%@ Page Title="ContactRealtor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="PMS.ContactRealtor" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">   

        <!-- Edited by Wilson -->
        <h2><asp:Label ID="lblNote" runat="server" Text="Message"></asp:Label></h2>

        <asp:Table ID="Table2" runat="server" CellPadding="10" Width="100%">
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

                                <td runat="server">
                                    <asp:LinkButton ID="lbnRegister" runat="server" CssClass="chat-message" CommandArgument='<%# Eval("property.PropertyID")%>'  OnCommand="Message_Click">
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("property.Address")%>' Font-Size="medium"  /><br/>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("sender.UserID").ToString() == Session["UserID"].ToString() ? Eval("recipent.FirstName").ToString()+" "+Eval("recipent.LastName").ToString() : Eval("sender.FirstName").ToString()+" "+Eval("sender.LastName").ToString() %>' Font-Size="medium"  /> <br />
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("content").ToString().Substring(0,40) %>' Font-Size="x-small"  /><br/>
                                    </asp:LinkButton>
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:ListView> 
                </asp:TableCell>
                <asp:TableCell Width="70%" VerticalAlign="Top">
                    <div class="chat-container" style="height: 300px; width:100%; overflow-y: auto;">
                        <!-- Hidden 
                        <asp:Label ID="lblSenderEmail" runat="server" Text="Your Email:"></asp:Label>                
                        <asp:TextBox ID="txtSenderEmail" runat="server" class="form-input" ReadOnly=true></asp:TextBox>
                        <asp:Label ID="lblRecipientEmail" runat="server" Text="Recipient Email:"></asp:Label><br/>
                        <asp:TextBox ID="txtRecipientEmail" runat="server" class="form-input"></asp:TextBox>
                        -->
                        <asp:GridView runat="server" CellPadding="10" ID="gridMessage" CssClass="listing" ShowHeader="False" GridLines="None" AutoGenerateColumns="False">
                        <Columns> 
                        <asp:TemplateField>
                            <ItemTemplate > 
                                <asp:Panel runat="server" class='<%# Eval("sender.UserID").ToString() == Session["UserID"].ToString() ? "another-message" : "user-message" %>'>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("property.Address")%>' Font-Size="medium"  /><br/>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("sender.UserID").ToString() == Session["UserID"].ToString() ? Eval("recipent.FirstName").ToString()+" "+Eval("recipent.LastName").ToString() : Eval("sender.FirstName").ToString()+" "+Eval("sender.LastName").ToString() %>' Font-Size="medium"  /> <br />
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("content").ToString().Substring(0,40) %>' Font-Size="x-small"  /><br/>
                                </asp:Panel>
                            </ItemTemplate> 
                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    </div>
                    <div>
                        <label for="message">Your Message:</label>
                        <textarea id="message" name="message" class="form-input" required></textarea>
                        <input type="submit" value="Send" class="form-button"> 
                    </div>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </main>
</asp:Content>

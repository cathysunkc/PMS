<%@ Page Title="ContactRealtor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="PMS.ContactRealtor" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">   

        <!-- Edited by Wilson -->
        <h2><asp:Label ID="lblNote" runat="server" Text="Message"></asp:Label></h2>

        <asp:Table ID="Table2" runat="server" CellPadding="10" Width="100%">
            <asp:TableRow>
                <asp:TableCell Width="30%">
                    <asp:ListView ID="listMessage" ShowHeader="False" runat="server" AutoGenerateColumns="True">
                        <LayoutTemplate>
                            <div id="chat-box">
                                <table runat="server" id="table1">
                                    <div class="chat-container">
                                        <tr runat="server" id="itemPlaceholder" />
                                    </div>
                                </table>
                            </div>
                        </LayoutTemplate>

                        <ItemTemplate>
                            <tr style="vertical-align: top; padding: 20px; cursor: pointer;" onclick="Message_Click(<%# Eval("message_id") %>);">
             
                                <td>
                                    <div ID="panelMessageBox" class="chat-message" >
            
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("property.Address")%>' Font-Size="medium"  /><br/>
                                             <asp:Label ID="Label2" runat="server" Text='<%# Eval("sender.UserID").ToString() == Session["UserID"].ToString() ? Eval("recipent.FirstName").ToString()+" "+Eval("recipent.LastName").ToString() : Eval("sender.FirstName").ToString()+" "+Eval("sender.LastName").ToString() %>' Font-Size="medium"  /> 
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("content").ToString().Substring(0,40) %>' Font-Size="x-small"  /><br/>
                                
                                    </div>
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:ListView> 
                </asp:TableCell>
                <asp:TableCell Width="70%" VerticalAlign="Top">
                    <div style="overflow-y: auto;">
                        <asp:Label ID="lblSenderEmail" runat="server" Text="Your Email:"></asp:Label>                
                        <asp:TextBox ID="txtSenderEmail" runat="server" class="form-input" ReadOnly=true></asp:TextBox>
                        <asp:Label ID="lblRecipientEmail" runat="server" Text="Recipient Email:"></asp:Label><br/>
                        <asp:TextBox ID="txtRecipientEmail" runat="server" class="form-input"></asp:TextBox>
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

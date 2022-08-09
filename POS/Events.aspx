<%@ Page Title="Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="POS.Events" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- script type="text/javascript" src="Scripts\demo.js"></!-->

    
    <h2>Assign Blocks</h2>
    <div class="row">
        <div class="col-md-4">
            <h3>Choose Event</h3>
            <updatepanel>
                <asp:DropDownList ID="halls" runat="server" AutoPostBack="true" OnSelectedIndexChanged="halls_SelectedIndexChanged" ></asp:DropDownList><br />     
                <asp:DropDownList ID="blocks" runat="server" AutoPostBack="true" OnSelectedIndexChanged="blocks_SelectedIndexChanged"></asp:DropDownList><br />   
            </updatepanel>
            
            <asp:TextBox ID="ticketPrice" placeholder="Enter Ticket Price for selected Block" runat="server" Enabled="False"></asp:TextBox> <br />
            <asp:Button ID="asg" runat="server" Text="Assign" OnClick="asg_Click" />
        </div>
        <div class="col-md-4">
            <img id="hall" src="./" runat="server" />
            
        </div>

        
    </div>
  

</asp:Content>
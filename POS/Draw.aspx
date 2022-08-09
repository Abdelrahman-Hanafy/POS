<%@ Page Title="Draw" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Draw.aspx.cs" Inherits="POS.About" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="Scripts\demo.js"></script>

    
    <h2>New Event</h2>
    <div class="row">
        <div class="col-md-4">
            <h3>Event Details</h3>
            <asp:Label ID="Label1" runat="server" Text="Choose ur Hall"></asp:Label><br>
            <asp:DropDownList ID="halls" runat="server" AutoPostBack="true" OnSelectedIndexChanged="halls_SelectedIndexChanged"></asp:DropDownList> <br>

            <asp:TextBox ID="name" placeholder="Name of the Event" runat="server"></asp:TextBox><br>
            <asp:Calendar ID="date" runat="server"></asp:Calendar><br>
            <asp:TextBox ID="time" placeholder="Time Event Satrt 24 hour system" runat="server"></asp:TextBox><br>
            <asp:TextBox ID="duration" placeholder="Duration of Event in Hours" runat="server"></asp:TextBox><br>

            <asp:Button ID="add" runat="server" Text="Add Event" OnClick="add_Click" />

        </div>
        <div class="col-md-4">

            <asp:ImageMap ID="selectedHall" runat="server" ></asp:ImageMap>
        </div>

        
    </div>
  
    
</asp:Content>

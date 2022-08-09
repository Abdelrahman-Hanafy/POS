<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="POS._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    

    <div class="jumbotron">
        <h1>Point of sale</h1>
        
    </div>

    <div class="row">
        <div class="col-md-4">

            <h2>Choose Event</h2>

            <asp:DropDownList ID="Halls" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Halls_SelectedIndexChanged"></asp:DropDownList>
            
            <asp:Label ID="tick" runat="server" Text="0 Tickts to book"></asp:Label>
            <canvas id="cav" width="800" height="800" runat="server"> </canvas>
            
        </div>
        
    </div>

    <script type="text/javascript" src="Scripts\grid.js"></script>
    <script type="text/javascript" src="Scripts\demo.js"></script>

</asp:Content>

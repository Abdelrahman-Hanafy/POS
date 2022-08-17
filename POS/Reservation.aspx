<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reservation.aspx.cs" Inherits="POS._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    

    <div class="jumbotron">
        <h1>Point of sale</h1>
        
    </div>

    <div class="row">
        <div class="col-md-4">

            <h3>Choose Event</h3>

            <asp:DropDownList ID="Halls" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Halls_SelectedIndexChanged"></asp:DropDownList>
            <br /><br />
            <asp:TextBox ID="name" Placeholder="Your Full Name" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="Mail" Placeholder="example@gamil.com" runat="server"></asp:TextBox><br />
            <asp:Label ID="tick" runat="server" Text="Choose Event first"></asp:Label> <br />
            <asp:Label ID="price" runat="server" Text="Choose Event first"></asp:Label> <br />
            <input id="reserve" type="button" onclick="onReserve()" value="Reserve" />
            
        </div>

        <div class="col-md-4">

            <h3>Choose Your Seats</h3>
            <canvas id="cav" width="800" height="800" runat="server"> </canvas>
            
        </div>
        
    </div>
    
    <script type="text/javascript">  

            var SIZE = "<%=ConfigurationManager.AppSettings["SIZE"].ToString() %>"
    </script> 

    <script type="text/javascript" src="Scripts\grid.js"></script>
    <script type="text/javascript" src="Scripts\reserve.js"></script>

</asp:Content>

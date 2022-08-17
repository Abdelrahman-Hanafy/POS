<%@ Page Title="Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pricing.aspx.cs" Inherits="POS.Events" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- script type="text/javascript" src="Scripts\demo.js"></!-->

    
    <h2>Assign Blocks</h2>
    <div class="row">
        <div class="col-md-4">
            <h3>Choose Event</h3>
            <updatepanel>
                <asp:DropDownList ID="events" runat="server" AutoPostBack="true" OnSelectedIndexChanged="events_SelectedIndexChanged" ></asp:DropDownList><br />     
            </updatepanel>
            
            <asp:TextBox ID="ticketPrice" placeholder="Enter Ticket Price for selected Block" runat="server"></asp:TextBox> <br />
            <input id="reserve" type="button" onclick="onPricing()" value="Assign" />

        </div>

        <div class="col-md-4">
            <canvas id="cav" width="800" height="800" runat="server"> </canvas>
        </div>
        
    </div>
    
        <script type="text/javascript">  

            var SIZE = "<%=ConfigurationManager.AppSettings["SIZE"].ToString() %>"
        </script> 
    <script type="text/javascript" src="Scripts\grid.js"></script>
    <script type="text/javascript" src="Scripts\price.js"></script>
</asp:Content>
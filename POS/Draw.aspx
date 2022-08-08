<%@ Page Title="Draw" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Draw.aspx.cs" Inherits="POS.About" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="Scripts\demo.js"></script>

    
    <h2>List Draw Hall</h2>
    <div class="row">
        <div class="col-md-4">
            <h2>Hall Design</h2>
            <asp:DropDownList ID="halls" runat="server" AutoPostBack="true" OnSelectedIndexChanged="halls_SelectedIndexChanged"></asp:DropDownList>          
        </div>
        <div class="col-md-4">
                
            <asp:ImageMap ID="selectedHall" runat="server" OnClick="selectedHall_Click"></asp:ImageMap>
            <canvas id="cav" width="400" height="400" runat="server"> </canvas>
        </div>

        
    </div>
  
    <div class="row">
        <div class="col-md-4">
            <img ID="Image1" runat="server" />        
        </div>
        
    </div>

</asp:Content>

<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="POS._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Point of sale</h1>
        
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Choose Hall</h2>
            <asp:DropDownList ID="Halls" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Halls_SelectedIndexChanged"></asp:DropDownList>
            
            <asp:Panel ID="Panel1" runat="server" BorderStyle="solid" BorderWidth="1px"></asp:Panel>
        </div>
        
    </div>

</asp:Content>

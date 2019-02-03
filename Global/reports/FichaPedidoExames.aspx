<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageClean.master" AutoEventWireup="true" CodeFile="FichaPedidoExames.aspx.cs" Inherits="Global_reports_FichaPedidoExames" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <center>
        <div style="position:absolute;font-size:9pt;color:green;">SIGADM</div>
        <hr>
        <h1>Congregação Cristã no Brasil</h1>
        <h3 style="margin-top:-20px;">Estado do Rio Grande do Norte</h3>
        <h1>Pedido de teste / Exame de músicos</h1>
        <hr> 
    </center>

    Data: <asp:Label ID="lblData" runat="server" Text=""></asp:Label> <br /><br />
    Aos irmãos Anciães. A paz de Deus. Amém.<br /><br />
    Através desta, apresentamos o irmão: <asp:Label ID="lblNome" runat="server" Text="" Font-Bold="true"></asp:Label><br />
    da casa de oração da(o): <asp:Label ID="lblCasaOracao" runat="server" Text="" Font-Bold="true"></asp:Label>, 
    para ingressar na orquestra com o instrumento: <asp:Label ID="lblInstrumento" runat="server" Text="" Font-Bold="true"></asp:Label>, 
    pois cumpriu todo o programa mínimo de estudos e poderá prestar o: <asp:Label ID="lblTipoExame" runat="server" Text="" Font-Bold="true"></asp:Label>

</asp:Content>


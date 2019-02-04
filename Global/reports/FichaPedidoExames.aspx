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
    pois cumpriu todo o programa mínimo de estudos e poderá prestar o: <asp:Label ID="lblTipoExame" runat="server" Text="" Font-Bold="true"></asp:Label>.<br /><br />
    Deus vos abençoe. Amém.

    <hr />

    <center><b>Assinaturas</b></center><br />
    <b>Ancião:</b><br />
    ________________________________________________________________________________________________<br /><br />
    <b>Cooperador:</b><br />
    ________________________________________________________________________________________________<br /><br />
    <b>Encarregado:</b><br />
    ________________________________________________________________________________________________<br /><br />
   
    <hr />
     <center><h1>Atendimento do teste / exame</h1></center>
    <hr> 

    <br />
    <b>Resultado: </b> Apto <asp:Image ID="Image1" runat="server" ImageUrl="~/img/quadrado.jpg" Width="15px" /> | Inapto <asp:Image ID="Image2" runat="server" ImageUrl="~/img/quadrado.jpg" Width="15px" /><br /><br />

    Teste / Exame realizado em: ______ / ______ / ________ <br /><br />

    <hr />

    <center><b>Assinaturas</b></center><br />
    <b>Ancião do atendimento:</b><br />
    ________________________________________________________________________________________________<br /><br />
    <b>Regional responsável:</b><br />
    ________________________________________________________________________________________________<br /><br />

    
</asp:Content>


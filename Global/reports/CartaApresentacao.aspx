<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageClean.master" AutoEventWireup="true" CodeFile="CartaApresentacao.aspx.cs" Inherits="Global_reports_CartaApresentacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body {
            background-color: #ffffff;
        } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <div style="position:absolute;font-size:9pt;color:green;">SIGADM</div>
        <hr>
        <h1>Congregação Cristã no Brasil</h1>
        <h3 style="margin-top:-20px;">Estado do Rio Grande do Norte</h3>
        <h1>Carta de Apresentação de Candidato(a)</h1>
        <hr> 
    </center>
        <center><b>Ao Ministério Espiritual e Musical</b></center><br />
        A paz de Deus.<br />
        Estamos apresentando o irmão(ã) abaixo discriminado para o início dos estudos no GEM - Grupo de Estudos Musicais.
    <hr />
    Nome: <asp:Label ID="lblNome" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Data de Nascimento: <asp:Label ID="lblDataNascimento" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Batizado: <asp:Label ID="lblBatizado" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Endereço: <asp:Label ID="lblEndereco" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Bairro: <asp:Label ID="lblBairro" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Cidade: <asp:Label ID="lblCidade" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Comum Congregação: <asp:Label ID="lblComum" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Instrumento: <asp:Label ID="lblInstrumento" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Possui Instrumento: <asp:Label ID="lblPossuiInstrumento" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Telefone: <asp:Label ID="lblTelefone" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Email: <asp:Label ID="lblEmail" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    <br />
    <div style="text-align:right;">
    <b>Data de Impressão:</b> <asp:Label ID="lblDataImpressao" runat="server" Text="---"></asp:Label><br /></div>
    <hr />
    <center><b>Assinaturas</b></center><br />
    <b>Ancião:</b><br />
    ________________________________________________________________________________________________<br /><br />
    <b>Cooperador:</b><br />
    ________________________________________________________________________________________________<br /><br />
    <b>Encarregado Local:</b><br />
    ________________________________________________________________________________________________<br /><br />
    <b>Encarregado Regional:</b><br />
    ________________________________________________________________________________________________<br /><br />
    <b>Examinadora:</b><br />
    ________________________________________________________________________________________________<br /><br />
    <b>Instrutor(a):</b><br />
    ________________________________________________________________________________________________<br /><br />
    <hr />
    <center><b>Resultado:</b> Aceito <asp:Image ID="Image1" runat="server" ImageUrl="~/img/quadrado.jpg" Width="15px" /> | Não Aceito <asp:Image ID="Image2" runat="server" ImageUrl="~/img/quadrado.jpg" Width="15px" /></center>

    <hr />
    <b>Observações:</b>
</asp:Content>


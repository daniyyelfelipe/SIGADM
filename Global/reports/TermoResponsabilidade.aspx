<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TermoResponsabilidade.aspx.cs" Inherits="Global_reports_TermoResponsabilidade" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
        <div style="position:absolute;font-size:9pt;color:green;">SIGADM</div>
        <hr>
        <h1>Congregação Cristã no Brasil</h1>
        <h3 style="margin-top:-20px;">Estado do Rio Grande do Norte</h3>
        <h1>Termo de Responsabilidade</h1>
        <hr> 
        Declaro encontrar em minhas mãos para louvar a DEUS, um instrumento musical, abaixo relacionado, pertencente ao patrimônio da Congregação Cristã no Brasil do Estado do Rio Grande do Norte.        
        <p>Comprometo-me a desfrutar deste instrumento obedecendo as seguintes condições:</p>
        <ul style="text-align:left;"> 
            <li> Enquanto morar neste Estado;
            <li> Enquanto não conseguir comprar meu instrumento;
            <li> Se eu permanecer com vida e saúde física;
            <li> Se permanecer fiel à doutrina da Congregação;
            <li> Ser freqüente nos cultos e ensaios musicais;
            <li> A manutenção do instrumento ficará por minha conta;
            <li> Em caso de morte, o instrumento deverá ser devolvido à Congregação.          
        </ul>
        Fico ciente que se não obedecer aos itens acima, sofrerei a pena de perder o direito de usufruir do instrumento, obrigando-me a devolvê-lo imediatamente quando solicitado pelo Encarregado Regional responsável pela comum congregação.
        <hr>
        <div style="text-align:left;">
        <b>Instrumento:</b> <asp:Label ID="lblInstrumento" runat="server" Text="---"></asp:Label><br />
        <b>Tonalidade:</b> <asp:Label ID="lblTonalidade" runat="server" Text="---"></asp:Label><br />
        <b>Nº Tombamento:</b> <asp:Label ID="lblTombamento" runat="server" Text="---"></asp:Label><br />	
        <b>Nome:</b> <asp:Label ID="lblNome" runat="server" Text="---"></asp:Label><br />	
        <b>Endereço:</b> <asp:Label ID="lblEndereco" runat="server" Text="---"></asp:Label><br />	
        <b>Cidade:</b> <asp:Label ID="lblCidade" runat="server" Text="---"></asp:Label><br />	
        <b>Congregação/Código:</b> <asp:Label ID="lblCongregacao" runat="server" Text="---"></asp:Label><br />	
        <b>Telefone:</b> <asp:Label ID="lblTelefone" runat="server" Text="---"></asp:Label><br />	
        <b>E-mail:</b> <asp:Label ID="lblEmail" runat="server" Text="---"></asp:Label><br />
        <b>Encarregado Responsável:</b> <asp:Label ID="lblEncarregado" runat="server" Text="---"></asp:Label><br />
        <b>Data:</b> <asp:Label ID="lblData" runat="server" Text="---"></asp:Label><br />
        </div>
        <hr />
        <br />
        <div style="float:left;width:50%;">
        _________________________________________ <br />
        <asp:Label ID="lblAssRecebedor" runat="server" Text="Recebedor(responsável)"></asp:Label>
        </div>
        <div style="float:left;width:50%;">
        _________________________________________ <br />
        <asp:Label ID="lblAssEntregador" runat="server" Text="Recebedor(responsável)"></asp:Label>
        </div>
        <br /><br /><br />
        <hr />
        SIGADM
    </center>
    </div>
    </form>
</body>
</html>

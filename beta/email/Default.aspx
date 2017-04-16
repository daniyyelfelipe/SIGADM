<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="beta_email_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <br />
    <center>
        <asp:TextBox ID="txtPara" runat="server" CssClass="txt" required TextMode="Email" placeholder="para"></asp:TextBox>
        <asp:TextBox ID="txtAssunto" runat="server" CssClass="txt" required placeholder="assunto"></asp:TextBox>
        <asp:TextBox ID="txtMensagem" runat="server" CssClass="txt" required TextMode="MultiLine" placeholder="mensagem"></asp:TextBox>
        <asp:Button ID="btnEnviar" runat="server" Text="Enviar Email" OnClick="btnEnviar_Click"></asp:Button>
        <br />
        <asp:Button ID="btnPrivado" runat="server" Text="Enviar Privado" OnClick="btnPrivado_Click"></asp:Button>
        <br />
        <asp:Button ID="btnCadastro" runat="server" Text="Enviar Cadastro" OnClick="btnCadastro_Click"></asp:Button>
    </center>
</asp:Content>


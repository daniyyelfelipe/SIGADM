<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="PassSystem.aspx.cs" Inherits="Tools_PassSystem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="fieldset">
        <legend>Codificar Senha</legend>
        Senha: <asp:TextBox ID="txtSenha1" runat="server" CssClass="txt" Width="400px"></asp:TextBox><br />
        Senha Codificada: <asp:TextBox ID="txtSenhaCodificada1" runat="server" CssClass="txt" ReadOnly="true" Width="400px"></asp:TextBox><br />
        <asp:Button ID="btnCodificar" runat="server" Text="Codificar Senha" CssClass="btn1" OnClick="btnCodificar_Click" />
    </fieldset>
    <fieldset class="fieldset">
        <legend>Decodificar Senha</legend>
        Senha Codificada: <asp:TextBox ID="txtSenhaCodificada2" runat="server" CssClass="txt" Width="400px"></asp:TextBox><br />
        Senha: <asp:TextBox ID="txtSenha2" runat="server" CssClass="txt" ReadOnly="true" Width="400px"></asp:TextBox><br />
        <asp:Button ID="btnDecodificar" runat="server" Text="Decodificar Senha" CssClass="btn1" OnClick="btnDecodificar_Click" />
    </fieldset>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="CadastroUser.aspx.cs" Inherits="Cadastro_CadastroUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <fieldset class="fieldsetDefault" style="width:100%;">
        <h3>Cadastro de usuários</h3>
        <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="ddlDefault" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoUsuario_SelectedIndexChanged"></asp:DropDownList>
        <asp:DropDownList ID="ddlOficializado" runat="server" CssClass="ddlDefault" Visible="false">
            <asp:ListItem Text="Oficializado(a)" Value="1"></asp:ListItem>
            <asp:ListItem Text="Não Oficializado(a)" Value="0"></asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="ddlInstrumento" runat="server" CssClass="ddlDefault"></asp:DropDownList>
        <asp:DropDownList ID="ddlCidade" runat="server" CssClass="ddlDefault" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="ddlIgreja" runat="server" CssClass="ddlDefault" Visible="false"></asp:DropDownList>
        <br /><br />
        <asp:TextBox ID="txtNome" runat="server" placeholder="nome completo" CssClass="txt" Height="35px" required MaxLength="25"></asp:TextBox>
        <asp:TextBox ID="txtEmail" runat="server" placeholder="email" CssClass="txt" Height="35px" required MaxLength="35"></asp:TextBox>
        <asp:TextBox ID="txtSenha" runat="server" placeholder="senha" CssClass="txt" Height="35px" TextMode="Password" required MaxLength="35"></asp:TextBox>
            <br /><br /><br /><br />
        <fieldset class="fieldsetDefault" style="width:200px;">
            <legend>Foto para o Perfil</legend>
        <asp:FileUpload ID="FileUpload" runat="server"/>
            </fieldset>
        <br />
        <a href='javascript:history.back(1)'>Voltar</a> |  
        <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="btn1" OnClick="btnCadastrar_Click"></asp:Button>        
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </fieldset>
    </center>
    </div>
            </center>
</asp:Content>


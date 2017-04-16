<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cadastro.aspx.cs" Inherits="Cadastro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SIGADM - CADASTRO DE USUARIOS</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <center>
    <div style="width:auto;">
    <center>
        <fieldset class="fieldsetDefault" style="width:60%;">
        <h3>Cadastro de usuário</h3>
        <asp:TextBox ID="txtNome" runat="server" placeholder="nome completo" CssClass="txt" Height="35px" required></asp:TextBox>
        <asp:TextBox ID="txtEmail" runat="server" placeholder="email" CssClass="txt" Height="35px" required></asp:TextBox>
        <asp:TextBox ID="txtSenha" runat="server" placeholder="senha" CssClass="txt" Height="35px" TextMode="Password" required></asp:TextBox>
        <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="ddlDefault"></asp:DropDownList>
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
    </form>
</body>
</html>


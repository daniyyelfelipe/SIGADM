<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPass.aspx.cs" Inherits="ForgotPass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SIGADM - Esqueci a senha</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divPedido" runat="server">
        Digite seu email, então você receberá um email na sua caixa de entrada para resetar a sua senha.<br />
        Se você não receber o email, entre em contato com o suporte regional.<br /><br />
        <fieldset class="fieldsetDefault">
            Email: <asp:TextBox ID="txtEmail" runat="server" Height="35px" required MaxLength="35" placeholder="email" CssClass="txt"></asp:TextBox>
            <asp:Button ID="btnForgotPass" runat="server" Text="Enviar email" CssClass="btn1" OnClick="btnForgotPass_Click" />
        </fieldset>
    </div>
    </form>
</body>
</html>

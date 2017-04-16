<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SIGADM - SISTEMA DE GESTÃO DAS ATIVIDADES DO DEPARTAMENTO MUSICAL</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript">
        function abrirPopup(URL, W, H) {

            var width = W;
            var height = H;
            var left = 99;
            var top = 99;

            window.open(URL, 'janela', 'width=' + width + ', height=' + height + ', top=' + top + ', left=' + left + ', scrollbars=no, status=no, toolbar=no, location=no, directories=no, menubar=no, resizable=no, fullscreen=no');

        }
    </script>

</head>
<body style="background-color:rgba(0, 191, 96,0.04);">
    <form id="form1" runat="server">
        <br />
        <br />
    <div>
    <center>
        <img src="img/logo1.png" width="300px" />
    </center>
        <center>
            <fieldset style="width:20%;" class="fieldset">
                <div style="width:100%;">
                    <asp:TextBox ID="txtUsuario" runat="server" placeholder="email do usuário" TextMode="Email" CssClass="txt" Height="35px" Width="90%" required></asp:TextBox><br />
                    <asp:TextBox ID="txtSenha" runat="server" placeholder="senha" CssClass="txt" Height="35px" Width="90%" TextMode="Password" required></asp:TextBox><br />
                    <div style="text-align:right;">
                        <asp:Button ID="btnLogin" runat="server" Text="Entrar" Width="40%" CssClass="btn1" OnClick="btnLogin_Click"></asp:Button>
                    </div>
                </div>
            </fieldset>
            <asp:Label ID="lblErroLogin" runat="server" Text="" ForeColor="Red"></asp:Label>
        </center>
        <br />
        <br />
        <center>
            <%--<a href="cadastro/Cadastro.aspx">Cadastro</a> |--%> <a href="#">Política de privacidade</a> | <a href="#">Termos de uso</a> | <asp:LinkButton ID="lbForgotPass" runat="server" Text="Esqueci a senha" OnClick="lbForgotPass_Click"></asp:LinkButton>
        </center>
    </div>  
        <div>
            <center>
                <asp:Label ID="lblVersion" runat="server" Text="" ForeColor="Gray" Font-Size="Smaller" Visible="true"></asp:Label>
            </center>
        </div>
    </form>
</body>
</html>


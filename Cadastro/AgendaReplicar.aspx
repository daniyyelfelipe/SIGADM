<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgendaReplicar.aspx.cs" Inherits="Cadastro_AgendaReplicar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
        Replicar dados de um mês anterior <br />
           <fieldset class="fieldsetDefault"">
               Mês/Ano a Replicar: <asp:TextBox ID="txtMesReplicar" runat="server" placeholder="mês/ano" CssClass="txt" required></asp:TextBox><br />
               Mês/Ano que receberá os eventos: <asp:TextBox ID="txtMesReceber" runat="server" placeholder="mês/ano" CssClass="txt" required></asp:TextBox><br />
               <asp:Button ID="btnConfirmarReplicar" runat="server" Text="Confirmar" OnClick="btnConfirmarReplicar_Click"></asp:Button>
           </fieldset>
        </center>
    </div>
    </form>
</body>
</html>

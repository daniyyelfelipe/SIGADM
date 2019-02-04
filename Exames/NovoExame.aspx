<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageClean.master" AutoEventWireup="true" CodeFile="NovoExame.aspx.cs" Inherits="Exames_NovoExame" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        //atualiza a janela que abriu esse popup
        window.onunload = refreshParent;
        function refreshParent() {
            window.opener.location.replace("/Exames/");
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="fieldsetDefault">
        <legend>Dados do novo Exame</legend>
        Tipo de Exame: <asp:DropDownList ID="ddlExameTipo" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
        Localidade: <asp:DropDownList ID="ddlLocalidade" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
        Data da Abertura: <asp:TextBox ID="txtDataAbertura" runat="server" CssClass="txt"></asp:TextBox><br />
        <cc1:CalendarExtender ID="clexDataAbertura" runat="server" TargetControlID="txtDataAbertura" Format="dd/MM/yyyy" />
        Hora de Abertura: <asp:TextBox ID="txtHoraAbertura" runat="server" CssClass="txt" placeholder="00:00"></asp:TextBox><br /><br /><br /><br /><br />
        <asp:Button ID="btnAbrirExame" runat="server" Text="Abrir Exame" OnClick="btnAbrirExame_Click" CssClass="btn1" />
    </fieldset>
</asp:Content>


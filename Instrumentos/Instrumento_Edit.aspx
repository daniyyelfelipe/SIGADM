<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageClean.master" AutoEventWireup="true" CodeFile="Instrumento_Edit.aspx.cs" Inherits="Instrumentos_Instrumento_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%;">
    <fieldset class="fieldsetDefault" style="width:95%;float:left;">
        <legend>Alteração de Bem Patrimônial</legend>
        <center align="left">
            <div style="float:left;">
            Instrumento:<br />
            <asp:DropDownList ID="ddlDescricao" runat="server" CssClass="ddlDefault" Width="200px"></asp:DropDownList><br />
            Ano de Fabricação:<br />
            <asp:TextBox ID="txtAnoFabricacao" MaxLength="4" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Numero de Tombamento:<br />
            <asp:TextBox ID="txtNumeroTombamento" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Tonalidade:<br />
            <asp:TextBox ID="txtTonalidade" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Caracteristicas:<br />
            <asp:TextBox ID="txtCaracteristicas" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Marca:<br />
            <asp:TextBox ID="txtMarca" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Componentes:<br />
            <asp:TextBox ID="txtComponentes" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Natureza da Aquisição:<br />
            <asp:DropDownList ID="ddlNatureza" runat="server" CssClass="ddlDefault" Width="200px"  AutoPostBack="true"></asp:DropDownList><br />
                </div>
            <div style="float:left;margin-left:20px;">
            <%--COMPRA--%>
            <div id="divCompra" runat="server">
            Data da Compra:<br />
            <asp:TextBox ID="txtDtCompra" onKeyUp="mascara_data(this)" MaxLength="10"  runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Valor R$:<br />
            <asp:TextBox ID="txtValor" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Numero da Nota Fiscal:<br />
            <asp:TextBox ID="txtNumeroNota" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Empresa:<br />
            <asp:TextBox ID="txtEmpresa" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
                </div>
            <%--DOACAO--%>
            <div id="divDoacao" runat="server">
            Origem da Doação:<br />
            <asp:TextBox ID="txtOrigemDoacao" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Data da Doação:<br />
            <asp:TextBox ID="txtDtDoacao" onKeyUp="mascara_data(this)" MaxLength="10"  runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
                </div>

             Data da Entrada no Estoque:<br />
            <asp:TextBox ID="txtDtEntrada" onKeyUp="mascara_data(this)" MaxLength="10"  runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Congregação de Estoque:<br />
            <asp:DropDownList ID="ddlCongregacao" runat="server" CssClass="ddlDefault" Width="200px"></asp:DropDownList><br />
            Observações:<br />
            <asp:TextBox ID="txtObs" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
        </center>
        <center>
            <asp:Button ID="btnCadastrar" runat="server" Text="Alterar" CssClass="btn1" /></asp:Button>
            <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
</asp:Content>


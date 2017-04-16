<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="PedidoInstrumento.aspx.cs" Inherits="Formularios_Patrimonio_PedidoInstrumento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <h3>
            Formulário para pedido de Instrumento
        </h3>
    </center>
    <center>
        <fieldset class="fieldsetDefault" align="left" style="width:300px;">
            Nome completo do recebedor do Instrumento: <br />
            <asp:TextBox ID="txtRecebedor" runat="server" pleceholder="" CssClass="txt" required></asp:TextBox><br />
            Endereço Completo:<br /> 
            <asp:TextBox ID="txtEndereço" runat="server" pleceholder="" CssClass="txt" required></asp:TextBox><br />
            <%--Cidade: <br />
            <asp:DropDownList ID="ddlCidade" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />--%>
            Comum Congregação - Cidade: <br />
            <asp:DropDownList ID="ddlCongregação" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
            Telefone: <br />
            <asp:TextBox ID="txtTelefone" runat="server" pleceholder="" CssClass="txt" required></asp:TextBox><br />
            Email: <br />
            <asp:TextBox ID="txtEmail" runat="server" pleceholder="" CssClass="txt"></asp:TextBox><br />
            Instrumento: <br />
            <asp:DropDownList ID="ddlInstrumento" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
            Tonalidade: <br />
            <asp:DropDownList ID="ddlTonalidade" runat="server" CssClass="ddlDefault">
                <asp:ListItem Enabled="true" Text="Dó" Value="Dó"></asp:ListItem>
                <asp:ListItem Enabled="true" Text="Si Bemol" Value="Si Bemol"></asp:ListItem>
                <asp:ListItem Enabled="true" Text="Mi Bemol" Value="Mi Bemol"></asp:ListItem>
                <asp:ListItem Enabled="true" Text="Fá" Value="Fá"></asp:ListItem>
            </asp:DropDownList><br />
            Encarregado Responsável: <br />
            <asp:TextBox ID="txtEncarregado" runat="server" pleceholder="" CssClass="txt" required></asp:TextBox><br />
            <%--<asp:DropDownList ID="ddlEncarregado" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />--%>
            Início dos Estudos: <br />
            <asp:TextBox ID="txtDataInicio" onKeyUp="mascara_data(this)" runat="server" pleceholder="" CssClass="txt" required></asp:TextBox><br />
            <div align="right">
                <asp:Button ID="btnGravar" runat="server" Text="Realizar Pedido" CssClass="btn1" OnClick="btnGravar_Click"></asp:Button>
            </div>
        </fieldset>
    </center>
</asp:Content>


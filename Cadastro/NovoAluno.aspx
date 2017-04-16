<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageClean.master" AutoEventWireup="true" CodeFile="NovoAluno.aspx.cs" Inherits="Cadastro_NovoAluno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="fieldsetDefault" style="width:15%;">
            <legend>Dados do Candidato</legend>
            <asp:TextBox ID="txtNome" runat="server" CssClass="txt" placeholder="Nome" Width="150px" MaxLength="25"></asp:TextBox>
            <asp:TextBox ID="txtDtNascimento" runat="server" onKeyUp="mascara_data(this)" CssClass="txt" placeholder="Dt.Nascimento" Width="150px"></asp:TextBox>
            <fieldset class="fieldsetDefault" style="width:80%;height:40px;">
                <legend>Batizado?</legend>
            <asp:RadioButtonList ID="rbBatizado" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbBatizado_SelectedIndexChanged">
                <asp:ListItem Enabled="true" Selected="True" Text="Sim" Value="True"></asp:ListItem> 
                <asp:ListItem Enabled="true" Selected="false" Text="Não" Value="False"></asp:ListItem> 
            </asp:RadioButtonList>
                </fieldset>
            <asp:TextBox ID="txtDtBatismo" runat="server" onKeyUp="mascara_data(this)" CssClass="txt" placeholder="Dt.Batismo" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtEndereco" runat="server" CssClass="txt" placeholder="Endereço" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtBairro" runat="server" CssClass="txt" placeholder="Bairro" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtCidade" runat="server" CssClass="txt" placeholder="Cidade" Width="150px"></asp:TextBox>
            <fieldset class="fieldsetDefault" style="width:80%">
                <legend>Comum Congregação</legend>
            <asp:DropDownList ID="ddlComum" runat="server" CssClass="ddlDefault" Width="200px"></asp:DropDownList>
                </fieldset>
            <fieldset class="fieldsetDefault" style="width:80%">
                <legend>Instrumento</legend>
            <asp:DropDownList ID="ddlInstrumento" runat="server" CssClass="ddlDefault"></asp:DropDownList>
                </fieldset>
                <fieldset class="fieldsetDefault" style="width:80%;height:40px;">
                <legend>Tem instrumento?</legend>
            <asp:RadioButtonList ID="rbTemInstrumento" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Enabled="true" Selected="True" Text="Sim" Value="True"></asp:ListItem> 
                <asp:ListItem Enabled="true" Selected="false" Text="Não" Value="False"></asp:ListItem> 
            </asp:RadioButtonList>
                </fieldset>
            <asp:TextBox ID="txtTelefone" runat="server" CssClass="txt" placeholder="Telefone" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtemail" runat="server" CssClass="txt" placeholder="Email" Width="150px" required type="email"></asp:TextBox><br />
            <asp:Button ID="btnEditar" runat="server" Text="Salvar Dados" CssClass="btn1" OnClick="btnEditar_Click"/>
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="btn1" OnClick="btnCadastrar_Click"></asp:Button>
        </fieldset>
</asp:Content>


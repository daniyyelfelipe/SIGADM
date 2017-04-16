<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Estatisticas_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <fieldset style="float:left;width:20%;height:300px;">
        <legend>Usuários Cadastrados</legend>
        <%--<asp:Label ID="lblAlunosCadastrados" runat="server" Text="Total de Alunos Cadastrados: "></asp:Label><br />
        <asp:Label ID="lblAlunosMatriculados" runat="server" Text="Total de Alunos Matriculados: "></asp:Label><br />
        <br />
        <br />
        <asp:Label ID="lblGemsCadastradas" runat="server" Text="Total de GEM's Cadastradas: "></asp:Label><br />
        <br />
        <br />
        <asp:Label ID="lblEncarregadosRegionaisCadastrados" runat="server" Text="Total de Encarregados Regionais Cadastrados: "></asp:Label><br />
        <asp:Label ID="lblEncarregadosLocaisCadastrados" runat="server" Text="Total de Encarregados Locais Cadastrados: "></asp:Label><br />
        <asp:Label ID="lblInstrutoresCadastrados" runat="server" Text="Total de Instrutores Cadastrados: "></asp:Label><br />
        <br />
        <br />
        <asp:Label ID="lblTotalUsuarios" runat="server" Text="Total de Usuários Cadastrados: "></asp:Label><br />--%>
        <div style="overflow:scroll;overflow-x:hidden;height:290px">
            <asp:GridView ID="gvUsuarios" runat="server"></asp:GridView>
        </div>
    </fieldset>    
    
    <fieldset style="float:left;width:20%;height:300px;">
        <legend>Candidatos por Cidade</legend>
        <div style="overflow:scroll;overflow-x:hidden;height:290px">
         <asp:GridView ID="gvCandidatosCidade" runat="server"></asp:GridView>
        </div>
    </fieldset>
    <fieldset style="float:left;width:20%;height:300px;">
        <legend>Candidatos por Instrumento</legend>
        <div style="overflow:scroll;overflow-x:hidden;height:290px">
        <asp:GridView ID="gvCandidatosInstrumento" runat="server"></asp:GridView>
        </div>
    </fieldset>
    <fieldset style="float:left;width:20%;height:300px;">
        <legend>GEMs Ativas por Comum</legend>
        <div style="overflow:scroll;overflow-x:hidden;height:290px">
            <asp:GridView ID="gvGemAtiva" runat="server"></asp:GridView>
        </div>
    </fieldset>
    <fieldset style="float:left;width:20%;height:300px;">
       <legend>Alunos matriculados por Instrumento</legend>
        <center>
        Total de Alunos Matriculados: <asp:Label ID="lblTotalMatriculados" runat="server" Text="---" Font-Bold="true"></asp:Label>
        </center>
        <br />
        <div style="overflow:scroll;overflow-x:hidden;height:250px">
        <asp:GridView ID="gvAlunosInstrumento" runat="server"></asp:GridView>
        </div>
    </fieldset>
    <fieldset style="float:left;width:20%;height:300px;">
        <legend>Ações Realizadas no Mês</legend>
        <div style="overflow:scroll;overflow-x:hidden;height:290px">
            <asp:GridView ID="gvAcoes" runat="server"></asp:GridView>
        </div>
    </fieldset>    
    
    <fieldset style="float:left;width:20%;height:300px;">
        <legend>---</legend>
    </fieldset>
    <fieldset style="float:left;width:20%;height:300px;">
        <legend>---</legend>
    </fieldset>
    
</asp:Content>


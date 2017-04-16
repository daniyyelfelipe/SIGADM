<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageClean.master" AutoEventWireup="true" CodeFile="HistoricoAluno.aspx.cs" Inherits="Global_reports_HistoricoAluno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body {
            background-color: #ffffff;
        } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <div style="position:absolute;font-size:9pt;color:green;">SIGADM</div>
        <hr>
        <h1>Congregação Cristã no Brasil</h1>
        <h3 style="margin-top:-20px;">Estado do Rio Grande do Norte</h3>
        <h1>Histórico do Aluno</h1>
        <hr> 
    </center>
    <div style="width:700px;height:200px;">
        <div style="float:left;">
            <fieldset class="fieldsetDefault" style="float:left;">
                <asp:Image ID="imgFotoAluno" ImageUrl="~/data/user/default/img/perfil.jpg" runat="server" Width="180px" Height="200px"/>
            </fieldset>
            <fieldset class="fieldsetDefault" style="height:205px;float:left;width:430px;">
                <center><div style="background-color:lightgray;">DADOS PESSOAIS</div></center>
                Nome: <asp:Label ID="lblNome" runat="server" Text="---" Font-Bold="true"></asp:Label> <br />
                Data de nascimento: <asp:Label ID="lblDataNasc" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
                Nome da mãe: <asp:Label ID="lblNomeMae" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
                Nome do pai: <asp:Label ID="lblNomePai" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
                Endereço: <asp:Label ID="lblEndereco" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
                Bairro: <asp:Label ID="lblBairro" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
                Municipio: <asp:Label ID="lblMunicipio" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
                Comum congregação: <asp:Label ID="lblComum" runat="server" Text="---" Font-Bold="true"></asp:Label>
            </fieldset>
        </div>
    </div>
    <br />
    <br />
    <hr />
    <center><div style="background-color:lightgray;">INFORMAÇÕES GERAIS</div></center>
    Instrumento: <asp:Label ID="lblInstrumento" runat="server" Text="---" Font-Bold="true"></asp:Label> <br />
    Ano letivo inicial: <asp:Label ID="lblAnoInicial" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Ano letivo final: <asp:Label ID="lblAnoFinal" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Ultima GEM frequentada: <asp:Label ID="lblUltimaGem" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Instrutor(a): <asp:Label ID="lblInstrutor" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Encarregado Local: <asp:Label ID="lblEncarregadoLocal" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Examinadora: <asp:Label ID="lblExaminadora" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    Encarregado regional: <asp:Label ID="lblEncarregadoRegional" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    <br />
    <hr />
    <div id="dvDadosAcademicos" runat="server">
    <center><div style="background-color:lightgray;">INDICES GERADOS PELO SISTEMA</div></center>
    <br />
    <div style="width:670px;height:200px;margin-bottom:50px;">
        <div>
            <fieldset class="fieldsetDefault" style="background-color:lightgray;width:80px;height:100px;float:left;">
                <center>
                    <asp:Label ID="lblIndicePresencas" runat="server" Text="---" Font-Bold="true" Font-Size="15pt" ForeColor="DarkGreen"></asp:Label>
                    de presença na GEM
                </center>
            </fieldset>
            <fieldset class="fieldsetDefault" style="background-color:lightgray;width:80px;height:100px;float:left;">
                <center>
                    <asp:Label ID="lblIndiceHinosSoprano" runat="server" Text="---" Font-Bold="true" Font-Size="15pt" ForeColor="DarkGreen"></asp:Label>
                    de hinos passados no soprano
                </center>
            </fieldset>
            <fieldset class="fieldsetDefault" style="background-color:lightgray;width:80px;height:100px;float:left;">
                <center>
                    <asp:Label ID="lblIndiceHinosContralto" runat="server" Text="---" Font-Bold="true" Font-Size="15pt" ForeColor="DarkGreen"></asp:Label>
                    de hinos passados no contralto
                </center>
            </fieldset>
            <fieldset class="fieldsetDefault" style="background-color:lightgray;width:80px;height:100px;float:left;">
                <center>
                    <asp:Label ID="lblIndiceHinosTenor" runat="server" Text="---" Font-Bold="true" Font-Size="15pt" ForeColor="DarkGreen"></asp:Label>
                    de hinos passados no tenor
                </center>
            </fieldset>
            <fieldset class="fieldsetDefault" style="background-color:lightgray;width:80px;height:100px;float:left;">
                <center>
                    <asp:Label ID="lblIndiceHinosBaixo" runat="server" Text="---" Font-Bold="true" Font-Size="15pt" ForeColor="DarkGreen"></asp:Label>
                    de hinos passados no baixo
                </center>
            </fieldset>
            <fieldset class="fieldsetDefault" style="background-color:lightgray;width:80px;height:100px;">
                <center>
                    <asp:Label ID="lblIndiceHinosPedaleira" runat="server" Text="---" Font-Bold="true" Font-Size="15pt" ForeColor="DarkGreen"></asp:Label>
                    de hinos passados na pedaleira
                </center>
            </fieldset>
            <fieldset class="fieldsetDefault" style="background-color:lightgray;width:80px;height:100px;float:left;">
                <center>
                    <asp:Label ID="Label1" runat="server" Text="---" Font-Bold="true" Font-Size="15pt" ForeColor="DarkGreen"></asp:Label>
                    de método passado
                </center>
            </fieldset>
            <fieldset class="fieldsetDefault" style="background-color:lightgray;width:80px;height:100px;float:left;">
                <center>
                    <asp:Label ID="Label6" runat="server" Text="---" Font-Bold="true" Font-Size="15pt" ForeColor="DarkGreen"></asp:Label>
                    de teoria passada
                </center>
            </fieldset>
            <fieldset class="fieldsetDefault" style="background-color:lightgray;width:80px;height:100px;float:left;">
                <center>
                    <asp:Label ID="Label7" runat="server" Text="---" Font-Bold="true" Font-Size="15pt" ForeColor="DarkGreen"></asp:Label>
                    do programa mínimo
                </center>
            </fieldset>
            <fieldset class="fieldsetDefault" style="background-color:lightgray;width:80px;height:100px;float:left;">
                <center>
                    <asp:Label ID="Label8" runat="server" Text="---" Font-Bold="true" Font-Size="15pt" ForeColor="DarkGreen"></asp:Label>
                    ----
                </center>
            </fieldset>
            <fieldset class="fieldsetDefault" style="background-color:lightgray;width:80px;height:100px;float:left;">
                <center>
                    <asp:Label ID="Label9" runat="server" Text="---" Font-Bold="true" Font-Size="15pt" ForeColor="DarkGreen"></asp:Label>
                    ----
                </center>
            </fieldset>
            <fieldset class="fieldsetDefault" style="background-color:lightgray;width:80px;height:100px;">
                <center>
                    <asp:Label ID="Label10" runat="server" Text="---" Font-Bold="true" Font-Size="15pt" ForeColor="DarkGreen"></asp:Label>
                    ----
                </center>
            </fieldset>
        </div>
    </div>
    <br />
    <hr />
    <center><div style="background-color:lightgray;">TESTES REALIZADOS</div></center>
    <div style="width:670px;height:200px;">
    </div>
    <br />
    <hr />
    
    <center><div style="background-color:lightgray;">HINOS PASSADOS</div></center>
    <div style="width:670px;height:80px;">
        Soprano: <asp:Label ID="lblHinosSoprano" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
        Contralto: <asp:Label ID="lblHinosContralto" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
        Tenor: <asp:Label ID="lblHinosTenor" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
        Baixo: <asp:Label ID="lblHinosBaixo" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
        Pedaleira: <asp:Label ID="lblHinosPedaleira" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    </div>
    <br />
    <hr />
    <center><div style="background-color:lightgray;">MÉTODO PASSADO</div></center>
    <div style="width:670px;height:200px;">
    </div>
    <br />
    <hr />
    <center><div style="background-color:lightgray;">LIÇÕES DE TEORÍA</div></center>
    <div style="width:670px;height:200px;">
    </div>
    <br />
    <hr />
    <center><div style="background-color:lightgray;">PRESENÇA NA GEM</div></center>
    <div style="width:670px;height:200px;">
        Presenças: <asp:Label ID="lblPresencas" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
        Faltas: <asp:Label ID="lblFaltas" runat="server" Text="---" Font-Bold="true"></asp:Label><br />
    </div>
    <br />
    <hr />
        </div>
</asp:Content>


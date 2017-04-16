<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="SolicitacaoPreTeste.aspx.cs" Inherits="Formularios_alunos_SolicitacaoPreTeste" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../../js/jquery-1.2.6.pack.js"></script>
    <script type="text/javascript" src="../../js/jquery.click-calendario-1.0.js"></script>
    <link href="../../css/jquery.click-calendario-1.0.css" rel="stylesheet" type="text/css"/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        

        <fieldset class="fieldset" style="">
            <legend>Dados do aluno</legend>
            <asp:Label ID="Label1" runat="server" Text="Nome do Aluno"></asp:Label>
            <asp:TextBox ID="txtNome" runat="server" CssClass="txt" Width="200px"></asp:TextBox>
            <asp:ImageButton ID="btnPesquisa" runat="server" ImageUrl="~/img/search.png" Width="30px" ToolTip="Pesquisar Aluno" OnClick="btnPesquisa_Click"></asp:ImageButton>
        </fieldset>
        <fieldset class="fieldset" style="">
            <legend>Dados da Pesquisa</legend>

            <div style="overflow:scroll;height:300px;"
                    <asp:GridView ID="gvPesquisa" OnRowCommand="gvPesquisa_RowCommand" runat="server" BackColor="White" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum aluno Encontrado." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center">
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />

            <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>                                    
                                    <asp:ImageButton ID="btnChoose" Text="Escolher Aluno" runat="server" CausesValidation="false"
                                        CommandName="Escolher" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/choose1.png"
                                        ToolTip="Escolher Aluno" PostBackUrl="#" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
           </Columns>
        </asp:GridView>
                </div>

        </fieldset>
    </center>
    <iframe width=174 height=189 name="gToday:normal:agenda.js" id="gToday:normal:agenda.js" src="../../js/calendar1/ipopeng.htm" scrolling="no" frameborder="0" style="visibility:visible; z-index:999; position:absolute; top:-500px; left:-500px;"></iframe>
</asp:Content>


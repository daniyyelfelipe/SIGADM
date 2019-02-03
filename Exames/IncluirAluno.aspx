<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageClean.master" AutoEventWireup="true" CodeFile="IncluirAluno.aspx.cs" Inherits="Exames_IncluirAluno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/jscript">

        function ConfirmaInclusao() {
            return confirm("Confirma a inclusão do aluno no exame?");
        }
        function ConfirmaExclusao() {
            return confirm("Confirma a exclusão do aluno no exame?");
        }
</script>
<script type="text/javascript">
    function ReloadParent() {
        window.opener.location.reload();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="fieldset">
        <legend>Inclusão de Alunos no exame <asp:Label ID="lblExame" runat="server" Text=""></asp:Label></legend>
        <fieldset class="fieldset" style="">
            <legend>Pesquisa de Alunos para a inclusão</legend>
            <asp:Label ID="Label1" runat="server" Text="Nome do Aluno"></asp:Label>
            <asp:TextBox ID="txtNome" runat="server" CssClass="txt" Width="200px"></asp:TextBox>
            <asp:ImageButton ID="btnPesquisa" runat="server" ImageUrl="~/img/search.png" 
                Width="30px" ToolTip="Pesquisar Aluno" OnClick="btnPesquisa_Click" /></asp:ImageButton>
        </fieldset>
        <fieldset class="fieldset" style="">
            <legend>Resultados da Pesquisa</legend>

            <div style="overflow:scroll;height:100px;">
                    <asp:GridView ID="gvPesquisa" runat="server" BackColor="White" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum aluno Encontrado." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" OnRowCommand="gvPesquisa_RowCommand">
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
                                    <asp:ImageButton ID="btnIncluir" Text="Incluir Aluno no Exame" runat="server" CausesValidation="false"
                                        CommandName="incluir" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/choose1.png"
                                        ToolTip="Incluir Aluno no Exame" width="20px" OnClientClick="return ConfirmaInclusao();"/>
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
           </Columns>
            </asp:GridView>
                </div>

        </fieldset>
        <fieldset class="fieldset">
        <legend>Alunos já incluidos no Exame</legend>
        <div style="overflow:scroll;height:280px;">
                    <asp:GridView ID="gvIncluidos" runat="server" BackColor="White" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum aluno incluido nesse Exame." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" OnRowCommand="gvIncluidos_RowCommand">
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
                                    <asp:ImageButton ID="btnChoose" Text="Excluir aluno do Exame" runat="server" CausesValidation="false"
                                        CommandName="excluir" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/excluir.png"
                                        ToolTip="Excluir aluno do Exame" width="20px" OnClientClick="return ConfirmaExclusao();"/>
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
           </Columns>
        </asp:GridView>
                </div>
    </fieldset>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageClean.master" AutoEventWireup="true" CodeFile="Matricula.aspx.cs" Inherits="GEM_Matricula" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/jscript">

        function ConfirmaMatricula()
        {
            return confirm("ATENÇÃO!! A matricula dos alunos na GEM só pode ser efetuada após a assinatura da carta de apresentação por parte do ministério local. Você confirma que imprimiu e que o ministério local assinou devidamente a carta de apresentação desse aluno?");
        }

        function ConfirmaDeletarMatricula() {
            return confirm("ATENÇÃO!! Deseja realmente excluir a matricula do aluno neste GEM? Essa operação não pode ser desfeita.");
        }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="fieldset">
        <legend>Matricula de novos alunos na GEM de <asp:Label ID="lblGem1" runat="server" Text=""></asp:Label></legend>
        <fieldset class="fieldset" style="">
            <legend>Pesquisa de Alunos para a Matrícula</legend>
            <asp:Label ID="Label1" runat="server" Text="Nome do Aluno"></asp:Label>
            <asp:TextBox ID="txtNome" runat="server" CssClass="txt" Width="200px"></asp:TextBox>
            <asp:ImageButton ID="btnPesquisa" runat="server" ImageUrl="~/img/search.png" 
                Width="30px" ToolTip="Pesquisar Aluno" OnClick="btnPesquisa_Click"></asp:ImageButton>
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
                                    <asp:ImageButton ID="btnMatricula" Text="Matricular Aluno" runat="server" CausesValidation="false"
                                        CommandName="Matricular" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/choose1.png"
                                        ToolTip="Matricular Aluno" width="20px" OnClientClick="return ConfirmaMatricula();"/>
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
    </fieldset>
    <fieldset class="fieldset">
        <legend>Alunos já matriculados na GEM de <asp:Label ID="lblGem" runat="server" Text=""></asp:Label></legend>
        <div style="overflow:scroll;height:280px;">
                    <asp:GridView ID="gvMatriculados" runat="server" BackColor="White" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum aluno matriculado nesta GEM." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" OnRowCommand="gvMatriculados_RowCommand">
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
                                    <asp:ImageButton ID="btnChoose" Text="Excluir matricula do Aluno" runat="server" CausesValidation="false"
                                        CommandName="excluir" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/excluir.png"
                                        ToolTip="Excluir Matricula" width="20px" OnClientClick="return ConfirmaDeletarMatricula()"/>
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


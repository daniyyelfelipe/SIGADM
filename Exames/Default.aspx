<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Exames_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/jscript">

        function ConfirmaExclusao() {
            return confirm("Confirma a Exclusão do Exame?");
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <fieldset class="fieldsetDefault">
        <legend>Criação de Exames</legend>
        <asp:Button ID="btnNewExame" runat="server" Text="Abrir novo Exame" CssClass="btn2" OnClick="btnNewExame_Click" />
    </fieldset>
    <fieldset class="fieldsetDefault">
        <legend>Lançamento de Resultados</legend>
        <div style="overflow:scroll;height:400px;"
                    <asp:GridView ID="gvExames" runat="server" BackColor="White" OnRowCommand="gvExames_RowCommand"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum exame está disponível pra você." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" Width="1050px">
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
                                    <%--<asp:ImageButton ID="btnAction" Text="Ações do Item" runat="server" CausesValidation="false"
                                        CommandName="Alterar" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/action1.png"
                                        ToolTip="Ações do Item" PostBackUrl="#" width="20px" />--%>
                                    <asp:ImageButton ID="btnExcluir" Text="Excluir" runat="server" CausesValidation="false"
                                        CommandName="excluir" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/excluir.png"
                                        ToolTip="Excluir Exame" PostBackUrl="#" width="20px" OnClientClick="return ConfirmaExclusao();" />
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton ID="btnIncluir" Text="Incluir/Excluir alunos no Exame" runat="server" CausesValidation="false"
                                        CommandName="incluir" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/incluir.png"
                                        ToolTip="Incluir/Excluir alunos no Exame" PostBackUrl="#" width="20px" OnClientClick="return ConfirmaInclusao();" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton ID="btnLancarResultado" Text="Lançar resultados do Exame" runat="server" CausesValidation="false"
                                        CommandName="resultados" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/lancar.png"
                                        ToolTip="Lançar resultados do Exame" PostBackUrl="#" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        <%--<asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton ID="btnRelatorio" Text="Imprimir relatório do Exame" runat="server" CausesValidation="false"
                                        CommandName="relatorio" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/print1.png"
                                        ToolTip="Imprimir relatório do Exame" PostBackUrl="#" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>--%>
           </Columns>
        </asp:GridView>
                </div>
    </fieldset>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="Alunos.aspx.cs" Inherits="Cadastro_Alunos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/jscript">

         function ConfirmaDel() {
             return confirm("ATENÇÃO!! Ao excluir o Candidato também será excluido o usuário. Você confirma a exclusão do candidato?");
         }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <div style="width:1200px;height:600px;margin-left:10px;">        

            <br />
            <fieldset class="fieldsetDefault" style="width:89%;">
                <legend>Cadastro de novo candidato</legend>
                <asp:Button ID="btnNovo" runat="server" Text="Novo Candidato" CssClass="btn2" OnClick="btnNovo_Click"></asp:Button>
                </fieldset>
            <fieldset class="fieldsetDefault" style="width:89%;">
                <legend>Pesquisa de candidatos</legend>
                Nome: <asp:TextBox ID="txtNomeP" CssClass="txt" runat="server"></asp:TextBox>
                Email: <asp:TextBox ID="txtEmailP" CssClass="txt" runat="server" type="email"></asp:TextBox>
                Comum: <asp:TextBox ID="txtComumP" CssClass="txt" runat="server"></asp:TextBox>
                Cidade: <asp:TextBox ID="txtCidadeP" CssClass="txt" runat="server"></asp:TextBox>
                Instrumento: <asp:TextBox ID="txtInstrumentoP" CssClass="txt" runat="server"></asp:TextBox>
                <asp:Button ID="btnPesquisa" runat="server" Text="Pesquisar" CssClass="btn1" OnClick="btnPesquisa_Click"></asp:Button>
                <hr />
                <asp:Label ID="lblTotalCandidatos" runat="server" Text="Total de Candidatos cadastrados: 0" ForeColor="Gray"></asp:Label>
            </fieldset>

            <fieldset class="fieldsetDefault" style="width:89%;height:525px;">
                <legend>Resultados da Pesquisa</legend>
                <div style="overflow:scroll;height:515px;"
                    <asp:GridView ID="gvAlunos" runat="server" BackColor="White" OnRowDataBound="gvAlunos_RowDataBound"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum candidato encontrado com esses dados." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" OnRowCommand="gvAlunos_RowCommand" Width="1050px">
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
                                        CommandName="Excluir" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/excluir.png"
                                        ToolTip="Excluir Candidato" PostBackUrl="#" width="20px" OnClientClick="return ConfirmaDel();" />
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton ID="btnEditar" Text="Editar dados do Candidato" runat="server" CausesValidation="false"
                                        CommandName="editar" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/edit.png"
                                        ToolTip="Editar dados do Candidato" PostBackUrl="#" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton ID="btnCarta" Text="Imprimir Carta de Apresentação" runat="server" CausesValidation="false"
                                        CommandName="carta" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/print1.png"
                                        ToolTip="Imprimir Carta de Apresentação" PostBackUrl="#" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton ID="btnHistorico" Text="Imprimir Historico do aluno" runat="server" CausesValidation="false"
                                        CommandName="historico" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/hitorico1.png"
                                        ToolTip="Imprimir Historico do aluno" PostBackUrl="#" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
           </Columns>
        </asp:GridView>
                </div>
            </fieldset>

        </div>
    </center>
</asp:Content>


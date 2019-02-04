<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageClean.master" AutoEventWireup="true" CodeFile="LancarResultado.aspx.cs" Inherits="Exames_LancarResultado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/jscript">

        function ConfirmaConsolidar() {
            return confirm("ATENÇÃO!! A consolidação do teste/exame impede que os resultados para os alunos sejam alterados. Confirma a consolidação do teste/exame?");
        }
</script>
<script type="text/javascript">
    //atualiza a janela que abriu esse popup
    window.onunload = refreshParent;
    function refreshParent() {
        window.opener.location.replace("/Exames/");
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset id="fdsDadosExame" runat="server" class="fieldset">
        <legend>Dados do Exame</legend>
        <div style="text-align:left;">
            Status: <asp:Label ID="lblStatusExame" runat="server" Text=""></asp:Label>
        </div>
        <div style="text-align:right;margin-top:-23px;">
            <asp:Button ID="btlConsolidarExame" runat="server" Text="Consolidar Teste/Exame" CssClass="btn2" OnClick="btlConsolidarExame_Click" OnClientClick="return ConfirmaConsolidar();" />
        </div>
    </fieldset>
    <fieldset id="fdsAlunosAvaliacao" runat="server" class="fieldset" style="">
        <legend>
            <asp:Label ID="lblQuantAlunos" runat="server" Text=""></asp:Label>Aluno(s) para avaliação
        </legend>

        <div style="overflow:scroll;height:480px"
                    <asp:GridView ID="gvAlunosAvaliados" runat="server" BackColor="White" OnRowCommand="gvAlunosAvaliados_RowCommand"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum aluno matriculado nesse exame." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" Width="930px">
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
                                    <asp:ImageButton ID="btnLancarResultado" Text="Lançar resultados do Exame" runat="server" CausesValidation="false"
                                        CommandName="resultados" CommandArgument='<%# Eval("ALUNO") %>' ImageUrl="~/img/lancar.png"
                                        ToolTip="Lançar resultados do Exame" PostBackUrl="#" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton ID="btnImprimirPedido" Text="Imprimir ficha de pedido de testes e exames" runat="server" CausesValidation="false"
                                        CommandName="ficha" CommandArgument='<%# Eval("ALUNO") %>' ImageUrl="~/img/print1.png"
                                        ToolTip="Imprimir ficha de pedido de testes e exames" PostBackUrl="#" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        
           </Columns>
        </asp:GridView>
                </div>

    </fieldset>
    <fieldset id="fdsAlunoResultado" runat="server" class="fieldset" style="" visible="false">
        <legend>
            Resultados do exame para o aluno: 
            <asp:Label ID="lblIdAlunoLancamento" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblAlunoLancamento" runat="server" Text=""></asp:Label>
        </legend>
        Resultado: <asp:DropDownList ID="ddlResultado" runat="server" CssClass="ddlDefault"></asp:DropDownList>
        Observações: <asp:TextBox ID="txtObs" runat="server" CssClass="txt"></asp:TextBox>
        <asp:Button ID="btlLancarResultadoAluno" runat="server" Text="Lançar Resultado" CssClass="btn1" OnClick="btlLancarResultadoAluno_Click" />
    </fieldset>
    <div id="divHlBackResultado" runat="server" visible="false" style="margin:10px 10px 10px 10px;border-style: solid; border-color:beige;width:250px;text-align:center;">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/img/seta1.png" Width="20px" CssClass="imgSetaBack"/><asp:HyperLink ID="hlBackResultado" runat="server">Voltar para listagem de alunos</asp:HyperLink>
    </div>
</asp:Content>


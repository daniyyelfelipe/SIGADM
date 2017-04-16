<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="PedidosDeInstrumentosRealizados.aspx.cs" Inherits="Formularios_Patrimonio_PedidosDeInstrumentosRealizados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
         <fieldset class="fieldsetDefault" style="width:80%;height:525px;">
                <legend>Pedidos de Instrumentos Realizados</legend>
                <div style="overflow:scroll;height:515px;"
                <asp:GridView ID="gvPedidos" runat="server" BackColor="White" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum pedido Realizado." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" OnRowCommand="gvPedidos_RowCommand">
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
                        <%--<asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton ID="btnAlterar" Text="Alterar" runat="server" CausesValidation="false"
                                        CommandName="Alterar" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/edit.png"
                                        ToolTip="Editar Registro" PostBackUrl="#" width="20px" />
                                    <asp:ImageButton ID="btnExcluir" Text="Excluir" runat="server" CausesValidation="false"
                                        CommandName="Excluir" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/excluir.png"
                                        ToolTip="Excluir Registro" PostBackUrl="#" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton ID="btnTermo" Text="Gerar termo de Responsabilidade" runat="server" CausesValidation="false"
                                        CommandName="termo" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/report2.png"
                                        ToolTip="Gerar termo de Responsabilidade" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
           </Columns>
        </asp:GridView>
            </fieldset>
    </center>
</asp:Content>


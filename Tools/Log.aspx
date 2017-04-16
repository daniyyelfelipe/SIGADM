<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="Log.aspx.cs" Inherits="Tools_Log" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div style="overflow:scroll;height:530px;">
        <asp:GridView ID="gvLog" runat="server" BackColor="White" 
                            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                            CellPadding="4" GridLines="Both" EmptyDataText="Nenhuma GEM nova ou ativa neste momento." 
                            RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" Font-Size="11">
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
                                            <asp:ImageButton ID="btnAction" Text="Ações do Item" runat="server" CausesValidation="false"
                                                CommandName="Matricula" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/notebook2.png"
                                                ToolTip="Visualizar/Efetuar Matrículas" width="25px" />
                                        </center>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                    HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <center>
                                            <asp:ImageButton ID="btnAction1" Text="Ações do Item" runat="server" CausesValidation="false"
                                                CommandName="Academico" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/academico.png"
                                                ToolTip="Lançar dados Acadêmicos" width="30px" />
                                        </center>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>--%>
                   </Columns>
                </asp:GridView>
    </div>
</asp:Content>


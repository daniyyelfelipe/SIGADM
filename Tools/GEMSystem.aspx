﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="GEMSystem.aspx.cs" Inherits="Tools_GEMSystem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <fieldset class="fieldset">
        <legend>Dados para pesquisa</legend>
        ID GEM: <asp:TextBox ID="txtIdGem" runat="server" CssClass="txt" Width="300px"></asp:TextBox><br />
        Congregacão: <asp:TextBox ID="txtCongregacao" runat="server" CssClass="txt" Width="300px"></asp:TextBox><br />
        <asp:Button ID="btnPesquisa" runat="server" Text="Pesquisar" OnClick="btnPesquisa_Click" CssClass="btn1"/>
    </fieldset>
    <fieldset class="fieldset">
        <legend>Resultados da pesquisa</legend>
        <div style="overflow:scroll;height:120px;"
                    <asp:GridView ID="gvGEM" runat="server" BackColor="White" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhumma GEM encontrada." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" OnRowCommand="gvGEM_RowCommand">
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
                                    <asp:ImageButton ID="btnEdit" Text="Editar" runat="server" CausesValidation="false"
                                        CommandName="editar" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/edit.png"
                                        ToolTip="Editar GEM" PostBackUrl="#" width="20px" />
                                    <asp:ImageButton ID="btnExcluir" Text="Excluir" runat="server" CausesValidation="false"
                                        CommandName="excluir" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/excluir.png"
                                        ToolTip="Excluir GEM" PostBackUrl="#" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
           </Columns>
        </asp:GridView>
                </div>
    </fieldset>
    <fieldset id="fdsEditarGem" class="fieldset" runat="server" visible="false">
        <legend>Editar GEM</legend>
        ID: <asp:Label ID="lblId" runat="server" Text=""></asp:Label><br />
        REGIONAL: <asp:DropDownList ID="ddlRegional" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
        LOCAL: <asp:DropDownList ID="ddlLocal" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
        INSTRUTOR: <asp:DropDownList ID="ddlInstrutor" runat="server" CssClass="ddlDefault"></asp:DropDownList><br /><br />
        <asp:Button ID="brnEditar" runat="server" Text="Atualizar Informações" OnClick="brnEditar_Click" CssClass="btn1" />
    </fieldset>
</asp:Content>


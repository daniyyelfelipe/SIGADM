<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="UserSystem.aspx.cs" Inherits="Tools_UserSystem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="fieldset">
        <legend>Dados para pesquisa</legend>
        Nome: <asp:TextBox ID="txtNome" runat="server" CssClass="txt" Width="300px"></asp:TextBox><br />
        Email: <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" Width="300px" TextMode="Email"></asp:TextBox><br />
        <asp:Button ID="btnPesquisa" runat="server" Text="Pesquisar" OnClick="btnPesquisa_Click" CssClass="btn1"/>
    </fieldset>
    <fieldset class="fieldset">
        <legend>Resultados da pesquisa</legend>
        <div style="overflow:scroll;height:120px;"
                    <asp:GridView ID="gvUser" runat="server" BackColor="White" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum usuário encontrado." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" OnRowCommand="gvUser_RowCommand">
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
                                    <asp:ImageButton ID="btnAction" Text="Editar usuário" runat="server" CausesValidation="false"
                                        CommandName="Alterar" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/edit.png"
                                        ToolTip="Editar usuário" PostBackUrl="#" width="20px" />
                                    <asp:ImageButton ID="btnExcluir" Text="Excluir" runat="server" CausesValidation="false"
                                        CommandName="Excluir" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/excluir.png"
                                        ToolTip="Excluir usuário" PostBackUrl="#" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
           </Columns>
        </asp:GridView>
                </div>
    </fieldset>
     <fieldset class="fieldset" id="fdsAtualizar" runat="server" visible="false">
        <legend>Editar Usuário</legend>
         Id: <asp:Label ID="lblId" runat="server" Text=""></asp:Label><br />
         Email: <asp:TextBox ID="txtEmailA" runat="server" CssClass="txt" Width="300px"></asp:TextBox><br />        
         Status: <asp:DropDownList ID="ddlStatusA" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
         Tipo do usuário: <asp:DropDownList ID="ddlTipoA" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
         Matrícula: <asp:TextBox ID="txtMatriculaA" runat="server" CssClass="txt" Width="300px"></asp:TextBox><br />

         <asp:Button ID="btnResetSenha" runat="server" Text="Resetar Senha" CssClass="btn1" OnClick="btnResetSenha_Click" />
         <asp:Button ID="btnAtualizar" runat="server" Text="Atualizar Dados" CssClass="btn1" OnClick="btnAtualizar_Click" />
    </fieldset>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="Cidades.aspx.cs" Inherits="Cadastro_Cidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <center>
    <fieldset class="fieldsetDefault" style="width:60%">
        <center>
            <asp:TextBox ID="txtNome" runat="server" placeholder="Nome da Cidade" required CssClass="txt"></asp:TextBox>
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ddlDefault"></asp:DropDownList>
            <asp:DropDownList ID="ddlRegiao" runat="server" CssClass="ddlDefault"></asp:DropDownList>
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" OnClick="btnCadastrar_Click" CssClass="btn1"></asp:Button>
        </center>
    </fieldset>
        </center>
    <br />
    <center>
    <fieldset class="fieldsetDefault" style="width:60%">
        <div class="esconderBarraHoriz" style="overflow:scroll;height:400px;">
        <asp:GridView ID="gvCidades" runat="server" BackColor="White" BorderColor="#336666" 
            BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Both" EmptyDataText="Nenhum bem Cadastrado." 
            RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" Width="650px">
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
                                    <asp:ImageButton ID="btnAlterar" Text="Alterar" runat="server" CausesValidation="false"
                                        CommandName="Alterar" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/edit.png"
                                        ToolTip="Editar Registro" PostBackUrl="#" width="20px" />
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
</asp:Content>


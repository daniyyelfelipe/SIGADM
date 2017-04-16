<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="Cadastro.aspx.cs" Inherits="GEM_Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div style="width:1350px;height:550px;">
        <fieldset runat="server" id="fdsNew" class="fieldset" style="float:left;width:250px;height:460px;">
        <legend>Dados da nova GEM</legend>
        <center>
            Cidade:<br />
            <asp:DropDownList ID="ddlCidade" runat="server" CssClass="ddlDefault" OnTextChanged="ddlCidade_TextChanged" AutoPostBack="true"></asp:DropDownList><br />
            Igreja:<br />
            <asp:DropDownList ID="ddlComum" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
            Período:<br />
            <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
            Turma:<br />
            <asp:DropDownList ID="ddlTurma" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
            Nipe:<br />
            <asp:DropDownList ID="ddlNipe" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
            Regional/Examinadora responsável:<br />
            <asp:DropDownList ID="ddlRegional" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
            Local responsável:<br />
            <asp:DropDownList ID="ddlLocal" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
            Instrutor(a) responsável:<br />
            <asp:DropDownList ID="ddlInstrutor" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="btn1" OnClick="btnCadastrar_Click"></asp:Button>
        </center>
    </fieldset>

        <fieldset runat="server" id="fdsGems" class="fieldset" style="float:left;height:460px;width:65%;">
        <legend>GEM's novas ou ativas que você tem acesso</legend>
        <center>
            
            <fieldset runat="server" id="FieldsetSearchGens" class="fieldset" style="float:left;width:97%;height:60px;">
                    <legend>Ferramentas de pesquisa</legend>
                    Congregação:
                    <asp:TextBox ID="txtGemSearch" CssClass="txt" runat="server"></asp:TextBox>
                    <asp:Button ID="btnGemSearch" runat="server" Text="Pesquisar" CssClass="btn1" OnClick="btnGemSearch_Click"></asp:Button><br />
                    <asp:Label ID="lblQuantidadeGems" ForeColor="Gray" runat="server" Text=""></asp:Label>
            </fieldset>
            <div style="overflow:scroll;height:370px;width:100%;"
                
                <asp:GridView ID="gvGEM" runat="server" BackColor="White" 
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                        CellPadding="4" GridLines="Both" EmptyDataText="Nenhuma GEM nova ou ativa neste momento." 
                        RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" OnRowCommand="gvGEM_RowCommand" Font-Size="11" OnRowDataBound="gvGEM_RowDataBound">
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
                            </asp:TemplateField>
               </Columns>
            </asp:GridView>
                </div>
        </center>
    </fieldset>
    </div>
</asp:Content>


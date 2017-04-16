<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Instrumentos_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function mascara_data(campo) { if (campo.value.length == 2) { campo.value += '/'; } if (campo.value.length == 5) { campo.value += '/'; } }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<center><h2>Patrimônio - Cadastro e Pesquisa</h2></center>--%>
    <br />
    <div style="width:100%;margin-left:90px;">
    <fieldset class="fieldsetDefault" style="width:35%;float:left;">
        <legend>Cadastro de Bem Patrimônial</legend>
        <center align="left">
            <div style="float:left;">
            Instrumento:<br />
            <asp:DropDownList ID="ddlDescricao" runat="server" CssClass="ddlDefault" Width="200px"></asp:DropDownList><br />
            Ano de Fabricação:<br />
            <asp:TextBox ID="txtAnoFabricacao" MaxLength="4" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Numero de Tombamento:<br />
            <asp:TextBox ID="txtNumeroTombamento" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Tonalidade:<br />
            <asp:TextBox ID="txtTonalidade" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Caracteristicas:<br />
            <asp:TextBox ID="txtCaracteristicas" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Marca:<br />
            <asp:TextBox ID="txtMarca" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Componentes:<br />
            <asp:TextBox ID="txtComponentes" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Natureza da Aquisição:<br />
            <asp:DropDownList ID="ddlNatureza" runat="server" CssClass="ddlDefault" Width="200px" OnSelectedIndexChanged="ddlNatureza_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
                </div>
            <div style="float:left;margin-left:20px;">
            <%--COMPRA--%>
            <div id="divCompra" runat="server">
            Data da Compra:<br />
            <asp:TextBox ID="txtDtCompra" onKeyUp="mascara_data(this)" MaxLength="10"  runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Valor R$:<br />
            <asp:TextBox ID="txtValor" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Numero da Nota Fiscal:<br />
            <asp:TextBox ID="txtNumeroNota" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Empresa:<br />
            <asp:TextBox ID="txtEmpresa" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
                </div>
            <%--DOACAO--%>
            <div id="divDoacao" runat="server">
            Origem da Doação:<br />
            <asp:TextBox ID="txtOrigemDoacao" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Data da Doação:<br />
            <asp:TextBox ID="txtDtDoacao" onKeyUp="mascara_data(this)" MaxLength="10"  runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
                </div>

             Data da Entrada no Estoque:<br />
            <asp:TextBox ID="txtDtEntrada" onKeyUp="mascara_data(this)" MaxLength="10"  runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
            Congregação de Estoque:<br />
            <asp:DropDownList ID="ddlCongregacao" runat="server" CssClass="ddlDefault" Width="200px"></asp:DropDownList><br />
            Observações:<br />
            <asp:TextBox ID="txtObs" runat="server" CssClass="txt" Width="200px"></asp:TextBox><br />
        </center>
        <center>
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="btn1" OnClick="btnCadastrar_Click"></asp:Button>
            <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
        </center>
    </fieldset>
    <fieldset class="fieldsetDefault" style="width:40%;float:left;margin-left:20px;height:410px;">
        <legend>Consulta e Edição - <asp:Label ID="lblQuant" runat="server" Text=""></asp:Label></legend>
         <div style="overflow:scroll;height:400px;"
        <asp:GridView ID="gvBens" runat="server" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Both" 
            EmptyDataText="Nenhum bem Cadastrado." RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" OnRowCommand="gvBens_RowCommand">
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
                                    <asp:ImageButton ID="btnAlterar" Text="Alterar" runat="server" CausesValidation="False"
                                        CommandName="alterar" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/edit.png"
                                        ToolTip="Editar Registro" width="20px" />
                                    <%--<asp:LinkButton runat="server"><asp:Image runat="server" url="~/img/edit.png" 
                                        ImageUrl="~/img/edit.png" width="20px" CommandName="Alterar" CommandArgument='<%# Eval("ENTRADA") %>'
                                        ToolTip="Editar Registro"></asp:Image></asp:LinkButton>--%>
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
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="Agenda.aspx.cs" Inherits="Cadastro_Agenda" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset id="fdsDadosEvento" runat="server" class="fieldsetDefault" style="width:98%;">
        <legend>Dados do novo compromisso</legend>
        <center>
            Data: <asp:TextBox ID="txtdata" runat="server" CssClass="txt" placeholder="Dia/Mês/Ano"></asp:TextBox>
            <cc1:CalendarExtender ID="clexData" runat="server" TargetControlID="txtdata" Format="dd/MM/yyyy" />
            Hora: <asp:TextBox ID="txtHora" runat="server" CssClass="txt" placeholder="09:00"></asp:TextBox>
            Tipo: <asp:DropDownList ID="ddlTIpo" runat="server" CssClass="ddlDefault"></asp:DropDownList><br />
            Cidade: <asp:DropDownList ID="ddlCidade" runat="server" CssClass="ddlDefault" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            Igreja: <asp:DropDownList ID="ddlIgreja" runat="server" CssClass="ddlDefault"></asp:DropDownList>
            Encarregado: <asp:DropDownList ID="ddlEncarregado" runat="server" CssClass="ddlDefault"></asp:DropDownList>
            Telefone: <asp:TextBox ID="txtTel" runat="server" CssClass="txt"></asp:TextBox><br />
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar Compromisso" CssClass="btn1" OnClick="btnCadastrar_Click" />
            <asp:Button ID="btnReplicar" runat="server" Text="Replicar Eventos" CssClass="btn1" OnClick="btnReplicar_Click" />
        </center>        

    </fieldset>
    <fieldset id="fdsDataUpdate" runat="server" visible="false" class="fieldsetDefault" style="width:98%;">
        <legend>Dados do novo compromisso</legend>
        <center>
            <h3><asp:Label ID="lblEventoUpdate" runat="server" Text="---"></asp:Label></h3>
            Data atualizada: <asp:TextBox ID="txtDateUpdate" runat="server" CssClass="txt" placeholder="Dia/Mês/Ano"></asp:TextBox>
            <cc1:CalendarExtender ID="clexDataUpdate" runat="server" TargetControlID="txtDateUpdate" Format="dd/MM/yyyy" />
            Hora atualizada: <asp:TextBox ID="txtHoraUpdate" runat="server" CssClass="txt" placeholder="09:00"></asp:TextBox>
            <asp:Button ID="btnUpdateDate" runat="server" Text="Atualizar Data" CssClass="btn1" OnClick="btnUpdateDate_Click" />
            <asp:Button ID="btnCancelarUpdateDate" runat="server" Text="Cancelar" CssClass="btn1" OnClick="btnCancelarUpdateDate_Click" />
            <asp:HiddenField ID="hfIdEventoUpdateDate" runat="server"></asp:HiddenField>
        </center>        

    </fieldset>
    <fieldset class="fieldsetDefault" style="width:98%;height:350px;">
        <legend>Compromissos Cadastrados</legend>
        <center>
        <div style="overflow:scroll;height:330px;"
                    <asp:GridView ID="gvCompromissos" runat="server" BackColor="White" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum compromisso cadastrado." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" OnRowCommand="gvCompromissos_RowCommand">
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
                                    <asp:ImageButton ID="btnUbdateDate" Text="Atualizar Data" runat="server" CausesValidation="false"
                                        CommandName="updateDate" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/calendar1.png"
                                        ToolTip="Atualizar Data" width="20px"/>
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>                                    
                                    <asp:ImageButton ID="btnEdit" Text="Editar dados do Compromisso" runat="server" CausesValidation="false"
                                        CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/edit.png"
                                        ToolTip="Editar dados do Compromisso" width="20px"/>
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>                                    
                                    <asp:ImageButton ID="btnDel" Text="Excluir Compromisso" runat="server" CausesValidation="false"
                                        CommandName="del" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/excluir.png"
                                        ToolTip="Excluir Compromisso" width="20px"/>
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
</asp:Content>


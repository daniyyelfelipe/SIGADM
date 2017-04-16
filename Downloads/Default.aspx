<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Downloads_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
           <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>        
    <br />
    <center><asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label></center>
    <fieldset style="width: 100%;" class="fieldset" runat="server" id="fsUpload">
        <legend>Sistema de Upload de arquivos</legend>
        <%--<center><h3>Sistema de Upload de arquivos</h3></center>--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <center>
                     <table style="border-right: #00bf60 2px solid; border-top: #00bf60 2px solid; border-left: #00bf60 2px solid;
                    border-bottom: #00bf60 2px solid">
                    <tbody>
                        <tr>
                            <td style="font-size: 15pt; color: white; background-color: #00bf60" align="center"
                                colspan="3">
                                Sistema de UPLOAD</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <asp:Label ID="lbl" runat="server"></asp:Label>
                                <asp:Label ID="Label1" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:FileUpload ID="FileUpload" runat="server"></asp:FileUpload>
                            </td>
                            <td valign="top">
                                <asp:Button ID="btnUpload" OnClick="btnUpload_Click" runat="server" Text="Upload"></asp:Button></td>
                            <td colspan="1" rowspan="1">
                                <asp:Image ID="ImageView" runat="server" Width="100" Height="100"></asp:Image></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <asp:Button ID="btnProcess" OnClick="btnProcess_Click" runat="server" Text="Process Data" Visible="false">
                                </asp:Button>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>
                                        <img src="../img/saving.gif" style="width: 43px; height: 39px" alt="" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </tbody>
                </table>
                </center>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnProcess" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:PostBackTrigger ControlID="btnUpload"></asp:PostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
        <br />
        </center>
    </fieldset>

    <fieldset style="width:100%;" class="fieldset">
        <legend>Pesquisa de Arquivos</legend>
        <center>
            <asp:TextBox ID="txtPesquisa" runat="server" CssClass="txt" placeholder="Termo para pesquisa"></asp:TextBox>
            <asp:Button ID="btnPesquisa" runat="server" Text="Pesquisar" CssClass="btn1" OnClick="btnPesquisa_Click"></asp:Button>
        </center>
    </fieldset>

    <fieldset style="width:100%;" class="fieldset">
        <legend>Arquivos diponíveis para download</legend>
        <%--<center><h3>Arquivos diponíveis para download</h3></center>--%>
        <center>
            <asp:GridView ID="gvDownloads" runat="server" BackColor="White" 
            OnRowCommand="gvDownloads_RowCommand" AutoGenerateColumns="false" OnRowDataBound="gvDownloads_RowDataBound"
            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
            CellPadding="4" GridLines="Both" EmptyDataText="Nenhum arquivo para download." 
            RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center">
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
                                    <asp:ImageButton ID="btnDownload" Text="download" runat="server" CausesValidation="false"
                                        CommandName="download" CommandArgument='<%# Eval("ARQUIVO") %>' ImageUrl="~/img/download1.png"
                                        ToolTip="Baixar Arquivo" PostBackUrl="#" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField> 
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton ID="btnDel" Text="deletar" runat="server" CausesValidation="false"
                                        CommandName="deletar" CommandArgument='<%# Eval("ARQUIVO") %>' ImageUrl="~/img/excluir.png"
                                        ToolTip="Deletar Arquivo" PostBackUrl="#" width="20px" Visible='<%# Eval("DEL") %>'/>
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="100px" HeaderText="Nome do Arquivo">
                            <ItemTemplate>
                                <center>
                                    <asp:Label runat="server" Text='<%# Eval("ARQUIVO") %>'></asp:Label>
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>   
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="100px" HeaderText="Tamanho">
                            <ItemTemplate>
                                <center>
                                    <asp:Label runat="server" Text='<%# Eval("TAMANHO") %>'></asp:Label>
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>    
                                                
           </Columns>
        </asp:GridView>
        </center>
    </fieldset>
</asp:Content>


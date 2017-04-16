<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="teia.aspx.cs" Inherits="teia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <%--<fieldset class="fieldsetDefault">
            <div style="overflow:scroll;height:500px;">
                <asp:GridView ID="gvTeia" runat="server" EmptyDataText="Não aconteceu nada na TEIA." CellPadding="4" GridLines="Horizontal" RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Left" RowStyle-CssClass="teiaRowStyle" OnRowDataBound="gvTeia_RowDataBound" ShowHeader="false"></asp:GridView>
            </div>
        </fieldset>--%>
        <fieldset class="fieldsetDefault">
            <div style="overflow:scroll;height:500px;overflow-x: hidden;">
                <asp:Repeater runat="server" ID="rpTeia">
                    <ItemTemplate>
                        <div class="teiaRowStyle">                            
                            <asp:HyperLink ID="HyperLink3" runat="server" Text='<%# Eval("USUARIO") %>' ToolTip="Ver perfil do Usuário" NavigateUrl='<%# Eval("PERFIL") %>'/>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("QUANDO") %>'></asp:Label>
                            <div align="left" style="margin-top:10px;">
                                <div id="divMensagem">
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("MENSAGEM") %>' ForeColor="ForestGreen" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:HyperLink ID="HyperLink1" runat="server" Text="Comentar" ImageUrl="~/img/comment.png" ImageWidth="30px" ToolTip="Comentar Ação" NavigateUrl="#"></asp:HyperLink>
                                <asp:HyperLink ID="HyperLink2" runat="server" Text="Curtir" ImageUrl="~/img/like1.png" ImageWidth="25px" ToolTip="Curtir Ação" NavigateUrl="#"></asp:HyperLink>                                
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("VISIB") %>' ForeColor="GrayText"></asp:Label>
                            </div>
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </fieldset>
    </center>
</asp:Content>


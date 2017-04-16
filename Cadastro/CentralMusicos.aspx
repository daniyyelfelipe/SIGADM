<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="CentralMusicos.aspx.cs" Inherits="Cadastro_CentralMusicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br />
    <fieldset class="fieldset">        
        <legend>Músicos cadastrados - Total: <asp:Label ID="lblTotalMusicos" runat="server" Text=""></asp:Label></legend>
        <asp:Repeater runat="server" ID="rpMusicos">
                    <ItemTemplate>
                        <div style="width:400px;float:left;border-style:solid;border-color:green;margin:10px 10px 10px 10px;">
                            <div class="divImgMusicos">
                            <asp:Image ID="imgFoto" runat="server" ImageUrl='<%# Eval("imgUrl") %>' Width="100px" />
                            </div>
                            <div style="float:left;margin-left:5px;">
                            <asp:Label ID="lblNome" runat="server" Text='<%# Eval("nome") %>' Font-Size="Small"></asp:Label><br />
                            <asp:Label ID="lblComum" runat="server" Text='<%# Eval("comum") %>' Font-Size="Smaller"></asp:Label><br />
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label><br />
                            <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("estado") %>' ForeColor="Gray" Font-Size="10pt"></asp:Label><br />
                            <asp:HyperLink ID="hlPerfil" runat="server" NavigateUrl='<%# Eval("urlPerfil") %>'>Ver perfil</asp:HyperLink>
                            </div>
                        </div>
                    </ItemTemplate>
        </asp:Repeater>
    </fieldset>
    <br />


</asp:Content>


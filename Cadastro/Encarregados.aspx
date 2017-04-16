﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="Encarregados.aspx.cs" Inherits="Cadastro_Encarregados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <fieldset class="fieldset">        
        <legend>Anciães Cadastrados - Total: <asp:Label ID="lblTotalAnciaes" runat="server" Text=""></asp:Label></legend>
        <asp:Repeater runat="server" ID="rpAnciaes">
                    <ItemTemplate>
                        <div style="width:400px;float:left;border-style:solid;border-color:green;margin:10px 10px 10px 10px;">
                            <div class="divImgColaboradores">
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
    <fieldset class="fieldset">        
        <legend>Encarregados Regionais Cadastrados - Total: <asp:Label ID="lblTotalRegionais" runat="server" Text=""></asp:Label></legend>
        <asp:Repeater runat="server" ID="rpRegionais">
                    <ItemTemplate>
                        <div style="width:400px;float:left;border-style:solid;border-color:green;margin:10px 10px 10px 10px;">
                            <div class="divImgColaboradores">
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
    <fieldset class="fieldset">
        <legend>Examinadoras Cadastradas - Total: <asp:Label ID="lblTotalExaminadoras" runat="server" Text="" /></legend>
        <asp:Repeater runat="server" ID="rpExaminadoras">
                    <ItemTemplate>
                        <div style="width:400px;float:left;border-style:solid;border-color:green;margin:10px 10px 10px 10px;">
                            <div class="divImgColaboradores">
                            <asp:Image ID="imgFoto" runat="server" ImageUrl='<%# Eval("imgUrl") %>' Height="100px" />
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
    <fieldset class="fieldset">
        <legend>Secretários Cadastrados - Total: <asp:Label ID="lblTotalSecretarios" runat="server" Text="" /></legend>
        <asp:Repeater runat="server" ID="rpSecretarios">
                    <ItemTemplate>
                        <div style="width:400px;float:left;border-style:solid;border-color:green;margin:10px 10px 10px 10px;">
                            <div class="divImgColaboradores">
                            <asp:Image ID="imgFoto" runat="server" ImageUrl='<%# Eval("imgUrl") %>' Height="100px" />
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
    <fieldset class="fieldset">
        <legend>Encarregados Locais Cadastrados - Total: <asp:Label ID="lblTotalLocais" runat="server" Text="" /></legend>
        <asp:Repeater runat="server" ID="rpLocais">
                    <ItemTemplate>
                        <div style="width:400px;float:left;border-style:solid;border-color:green;margin:10px 10px 10px 10px;">
                            <div class="divImgColaboradores">
                            <asp:Image ID="imgFoto" runat="server" ImageUrl='<%# Eval("imgUrl") %>' Height="100px" />
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
    <fieldset class="fieldset">
        <legend>Instrutores Cadastrados - Total: <asp:Label ID="lblTotalInstrutores" runat="server" Text="" /></legend>
        <asp:Repeater runat="server" ID="rpInstrutores">
                    <ItemTemplate>
                        <div style="width:400px;float:left;border-style:solid;border-color:green;margin:10px 10px 10px 10px;">
                            <div class="divImgColaboradores">
                            <asp:Image ID="imgFoto" runat="server" ImageUrl='<%# Eval("imgUrl") %>' Height="100px" />
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
    <fieldset class="fieldset">
        <legend>Instrutoras Cadastradas - Total: <asp:Label ID="lblTotalInstrutoras" runat="server" Text="" /></legend>
        <asp:Repeater runat="server" ID="rpInstrutoras">
                    <ItemTemplate>
                        <div style="width:400px;float:left;border-style:solid;border-color:green;margin:10px 10px 10px 10px;">
                            <div class="divImgColaboradores">
                            <asp:Image ID="imgFoto" runat="server" ImageUrl='<%# Eval("imgUrl") %>' Height="100px" />
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
</asp:Content>


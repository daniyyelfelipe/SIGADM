<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageClean.master" AutoEventWireup="true" CodeFile="PrevisualizarPostImage.aspx.cs" Inherits="Perfil_PrevisualizarPostImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset id="fdsPerfilPostVideo" runat="server" class="fieldsetDefault">
        <center>
            <asp:Label ID="lblDescricaoTemp" runat="server" Text="Descricao" ForeColor="ForestGreen" Font-Bold="true"></asp:Label><br />
            <asp:Image ID="imgTemp" runat="server" CssClass="imgPostTeia"/>        
        </center>
    </fieldset>
    <asp:Button ID="btnPerfilPostImagePost" runat="server" Text="Postar Imagem" CssClass="btn1" OnClick="btnPerfilPostImagePost_Click"/>
</asp:Content>


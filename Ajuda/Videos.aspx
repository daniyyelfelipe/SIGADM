<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="Videos.aspx.cs" Inherits="Ajuda_Videos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    
    <div style="width:1350px;height:600px;float:left;">
        <fieldset class="fieldsetDefault" style="width:350px;height:450px;float:left;">
            <legend>Listagem de Videos</legend>
            <div style="width:340px;height:440px;overflow:scroll;">
                 <asp:Repeater runat="server" ID="rpListagem">
                            <ItemTemplate>
                                <div>    
                                    <asp:HiddenField ID="hfIdReproducao" Value='<%# Eval("IDREPRODU") %>' runat="server" />
                                    <h4><asp:HyperLink ID="hlVideo" runat="server" NavigateUrl='<%# Eval("IDREPRODULINK") %>' Text='<%# Eval("TITULO") %>'></asp:HyperLink></h4>                                                  
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
            </div>
        </fieldset>
        <fieldset class="fieldsetDefault" style="width:560px;height:450px;float:left;">
            <legend>Video Selecionado</legend>
            <br />
            <br />
            <asp:Literal ID="ltIframeTop" runat="server"></asp:Literal>
            <center>
                <h4><asp:Label ID="lblTituloVideo" runat="server" Text=""></asp:Label></h4>
            </center>
        </fieldset>

        <fieldset class="fieldsetDefault" style="width:300px;height:450px;float:left;">
            <legend>Comentários</legend>
            <center>Em desenvolvimento...</center>
        </fieldset>
    </div>
</asp:Content>


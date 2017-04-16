<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="true" CodeFile="~/Perfil/Default.aspx.cs" Inherits="Perfil_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type ="text/javascript">
        function ContarPost(campo) {
            if ((300 - campo.value.length) >= 0) {
                document.getElementById("labelPost").style.color = "Gray";
                document.getElementById("labelPost").innerHTML = "Faltam " + (300 - campo.value.length) + " Caracteres.";
            }
            else {
                document.getElementById("labelPost").style.color = "Red";
                document.getElementById("labelPost").innerHTML = "Passaram " + (campo.value.length - 300) + " Caracteres. O texto não será postado!";
            }
        }
    </script>

    <script type="text/jscript">

        function ConfirmaDelPost() {
            return confirm("ATENÇÃO!! Deseja realmente excluir o post na sua teia? Essa operação não poderá ser desfeita!");
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="margin-left:6%;width:1350px;">
        <fieldset id="fdsPerfilDados" runat="server" class="fieldsetDefault" style="width:40%;float:left;">
        <legend>Dados do usuário</legend>
        <div class="divImgPerfil">
            <asp:Image ID="imgPerfil" runat="server" Width="200px" />
        </div>
        <div style="float:left;margin-left:10px;">
            <div id="divEditarNome" runat="server" visible="false" style="margin-bottom:-20px;">
                <asp:TextBox ID="txtNomeEdit" runat="server" CssClass="txt" Width="200px" MaxLength="25"></asp:TextBox><asp:Button ID="btnSalvarNome" runat="server" Text="Salvar" CssClass="btn1" OnClick="btnSalvarNome_Click" />
            </div>
            <asp:Label ID="lblNome" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label> <asp:LinkButton ID="lbEditarNome" runat="server" OnClick="lbEditarNome_Click">editar nome</asp:LinkButton><br />
            <asp:Label ID="lblCargo" runat="server" Text="" ForeColor="Red"></asp:Label> <asp:HyperLink ID="hlEditCargo" NavigateUrl="#" runat="server">editar cargo</asp:HyperLink> <br />
            <asp:Label ID="lblComum" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label><br /> <asp:LinkButton ID="lbEditarComum" runat="server" OnClick="lbEditarComum_Click">editar comum</asp:LinkButton><br />            
            <asp:Label ID="lblEmail" runat="server" Text="" ForeColor="Red"></asp:Label><br />
            <asp:LinkButton ID="lbEditarSenha" runat="server" OnClick="lbEditarSenha_Click">Alterar Senha</asp:LinkButton><br />
            <div id="divEditarSenha" runat="server" visible="false">
                <center>
                    <asp:TextBox ID="txtNovaSenha" runat="server" CssClass="txt" placeholder="Nova Senha" TextMode="Password">
                    </asp:TextBox><asp:Button ID="btnSalvarNovaSenha" runat="server" Text="Salvar Senha" CssClass="btn1" OnClick="btnSalvarNovaSenha_Click"></asp:Button>
                </center>
            </div>
            <%--<asp:ImageButton ID="imbEditarImagem" runat="server" ImageUrl="~/img/imageEdit.png" Width="30px" ToolTip="Editar Imagem do Usuário" />--%>
            <fieldset id="fildsetUpload" runat="server" class="fieldsetDefault" style="margin-top:10px;">
                <asp:FileUpload ID="FileUpload" runat="server" Width="150px"/>
                <asp:Button ID="btnEditImg" runat="server" Text="Alterar Imagem" OnClick="btnEditImg_Click" CssClass="btn1" />
            </fieldset>
            <div><center><asp:Label ID="lblUltimaVisita" runat="server" Text="" ForeColor="Gray" Font-Size="Small"></asp:Label></center></div>
        </div>
            <div id="divEditarComum" runat="server" visible="false">
                <center>
                    <asp:DropDownList ID="ddlComumEdit" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnSalvarComum" runat="server" Text="Salvar Comum" CssClass="btn1" OnClick="btnSalvarComum_Click"></asp:Button>
                </center>
            </div>
    </fieldset>

        <fieldset id="fdsPerfilTeia" runat="server" class="fieldsetDefault" style="width:40%;float:left;margin-left:10px;">
        <legend>TEIA</legend>
        <div style="overflow:scroll;height:470px;overflow-x: hidden;">
            <asp:Label ID="lblNoPosts" runat="server" Text="" ForeColor="Gray"></asp:Label>
                <asp:Repeater runat="server" ID="rpTeia">
                    <ItemTemplate>
                        <div class="teiaRowStyle">                            
                            <asp:HyperLink ID="HyperLink3" runat="server" Text='<%# Eval("USUARIO") %>' Font-Size="Small" ToolTip="Ver perfil do Usuário" NavigateUrl='<%# Eval("PERFIL") %>'/>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("ACAO") %>'></asp:Label>
                            <div style="margin-top:10px;">
                                <div id="divMensagem" style="margin-bottom:5px;">
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("MENSAGEM") %>' ForeColor="ForestGreen" Font-Bold="true"></asp:Label><br />
                                    <center>
                                    <a href='<%# Eval("imgPostHref") %>' target="_blank"><asp:Image ID="imgPostTeia" runat="server" CssClass="imgPostTeia" Visible='<%# Eval("imgPostVisible") %>' ImageUrl='<%# Eval("imgPostUrl") %>' ToolTip="Clique para visualizar a Imagem."/></a>
                                    </center>
                                </div>
                                <asp:LinkButton ID="lkbAnswer" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lkbAnswer_Command" Visible='<%# Eval("VCOMENT") %>' ToolTip="Responder"><img src="../img/comment.png" width="25px" /></asp:LinkButton>
                                <asp:LinkButton ID="lkbCurtir" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="imgbCurtir_Command" Visible='<%# Eval("VLIKE") %>' ToolTip="Curtir"><img src="../img/like1.png" width="25px" /></asp:LinkButton>
                                <%--<asp:ImageButton ID="imgbCurtir" runat="server" ToolTip="Curtir" ImageUrl="~/img/like1.png" Width="25px" CommandArgument='<%# Eval("ID") %>' OnCommand="imgbCurtir_Command" Visible='<%# Eval("VLIKE") %>'/>--%>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("VISIB") %>' ForeColor="GrayText"></asp:Label>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("CURTIR") %>' ForeColor="Gray" ToolTip='<%# Eval("CURTIU") %>'></asp:Label>
                                <div id="divBtnExcluir" runat="server" style="float:right;margin-top:15px;"  Visible="false">
                                    <%--<asp:LinkButton ID="lkbExcluir" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="imgbCurtir_Command" Visible="false" ToolTip="Excluir Post"><img src="../img/excluir.png" width="20px" onclick="imgbExcluir_Command"/></asp:LinkButton>--%>
                                    <asp:ImageButton ID="imgbExcluir" runat="server" ToolTip="Excluir Post" ImageUrl="~/img/excluir.png" Width="20px" CommandArgument='<%# Eval("ID") %>' OnCommand="imgbExcluir_Command" Visible='<%# Eval("VDEL") %>' OnClientClick="return ConfirmaDelPost()"/>
                                </div>
                            </div>
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
    </fieldset>

            <fieldset id="fdsPerfilPost" runat="server" class="fieldsetDefault" style="width: 40%; float: left; margin-top: -265px">
                <legend>Postar na TEIA</legend>
                <center>
                <fieldset runat="server" class="fieldsetDefault" style="float: left;height:50px;">
                    <legend>Texto</legend>
                    <asp:ImageButton ID="imbPostText" ImageUrl="~/img/postTexto1.png" Width="40px" runat="server" ToolTip="Postar Texto" OnClick="imbPostText_Click" />
                </fieldset>
                <fieldset runat="server" class="fieldsetDefault" style="float: left;height:50px;">
                    <legend>Imagem</legend>
                    <asp:ImageButton ID="imbPostImage" ImageUrl="~/img/postImage1.png" Width="40px" runat="server" ToolTip="Postar Imagem" OnClick="imbPostImage_Click" />
                </fieldset>
                <fieldset runat="server" class="fieldsetDefault" style="float: left;height:50px;">
                    <legend>Vídeo</legend>
                    <asp:ImageButton ID="imbPostVideo" ImageUrl="~/img/postVideo1.png" Width="40px" runat="server" ToolTip="Postar Vídeo" OnClick="imbPostVideo_Click" />
                </fieldset>
                <fieldset runat="server" class="fieldsetDefault" style="float: left;height:50px;">
                    <legend>Arquivo</legend>
                    <asp:ImageButton ID="imbPostArquivo" ImageUrl="~/img/file1.png" Width="40px" runat="server" ToolTip="Postar Vídeo" OnClick="imbPostArquivo_Click" />
                </fieldset>
                </center>
            </fieldset>

        <fieldset id="fdsPerfilPostTexto" runat="server" class="fieldsetDefault" style="width:40%;float:left;margin-top:-150px" visible="false">
        <legend>Postar Texto</legend>
            <div style="margin-top:-10px;margin-right:34px;" align="right">
            <label id="labelPost" style="color:gray;font-size:10pt;">Faltam 300 Caracteres.</label>
                </div>
        <asp:TextBox ID="txtPost" runat="server" MaxLength="300" onKeyUp="ContarPost(this)" TextMode="MultiLine" CssClass="txt" 
            Width="500px" Height="100px" ForeColor="ForestGreen" Font-Bold="true" placeholder="Texto com no máximo 300 caracteres"></asp:TextBox><br />
            <div id="dvPrivado" runat="server" visible="false">
                Mensagem para:
                <asp:TextBox ID="txtPrivado" runat="server" CssClass="txt" Width="170px"></asp:TextBox>
                <cc1:AutoCompleteExtender ServiceMethod="AutoCompletePrivate"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="txtPrivado"
                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                </cc1:AutoCompleteExtender>
            </div>
        Visibilidade: <asp:DropDownList ID="ddlVisibilidade" runat="server" CssClass="ddlDefault" AutoPostBack="true" OnSelectedIndexChanged="ddlVisibilidade_SelectedIndexChanged">
        </asp:DropDownList> | <asp:Button ID="btnpostar" runat="server" Text="Postar" CssClass="btn1" OnClick="btnpostar_Click" />
            <asp:CheckBox ID="cbAviso" runat="server" Text="Aviso?" Visible="false" ToolTip="CUIDADO! ao selecionar isso, todos os usuários receberão um email de aviso!"/>
            
    </fieldset> 
            
            <fieldset id="fdsPerfilPostImage" runat="server" class="fieldsetDefault" style="width: 40%; float: left; margin-top: -150px" visible="false">
                <legend>Postar Imagem</legend>
                <asp:FileUpload ID="fuPerfilPostImage" runat="server" Width="150px" />
                <asp:Button ID="BtnPerfilPostImageVisualizar" runat="server" Text="Pré-visualizar Post" CssClass="btn1" OnClick="BtnPerfilPostImageVisualizar_Click"/><br />               
                Descrição: <asp:TextBox ID="txtPerfilPostImageDescricao" runat="server" CssClass="txt" Width="400px"></asp:TextBox>  
                 <fieldset id="fdsPerfilPostImagePreview" runat="server" class="fieldsetDefault" visible="false">                    
                    <asp:Label ID="lblDescricaoTemp" runat="server" Text="Descricao" ForeColor="ForestGreen" Font-Bold="true"></asp:Label><br />
                     <center>
                    <asp:Image ID="imgTemp" runat="server" CssClass="imgPostTeia"/>        
                    </center>
                </fieldset>                
                <asp:Button ID="BtnPerfilPostImagePost" runat="server" Text="Postar Imagem" CssClass="btn1" OnClick="BtnPerfilPostImagePost_Click" Visible="false"/>
            </fieldset>

            <fieldset id="fdsPerfilPostVideo" runat="server" class="fieldsetDefault" style="width: 40%; float: left; margin-top: -150px" visible="false">
                <legend>Postar Vídeo</legend>                
                Link do Youtube: <asp:TextBox ID="txtPerfilPostVideoUrl" runat="server" CssClass="txt" Width="400px"></asp:TextBox><br />
                Descrição: <asp:TextBox ID="txtPerfilPostVideoDescricao" runat="server" CssClass="txt" Width="400px"></asp:TextBox><br />
                <asp:Button ID="btnPerfilPostVideoPost" runat="server" Text="Postar Vídeo" CssClass="btn1" />

            </fieldset>
        </div>
</asp:Content>

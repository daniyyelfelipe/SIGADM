<%@ page title="" language="C#" masterpagefile="~/MasterPageHome.master" autoeventwireup="true" inherits="Home" CodeFile="~/Home.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div style="margin-top:0px;">

        <fieldset class="fieldsetDefault" style="float:left;height:480px;width:97.5%;">
        <legend>Módulos do Sistema</legend>
            <%--<div style="overflow:scroll;overflow-x: hidden;height:460px;padding:5px 5px 5px 5px;">
                <p>A paz de Deus a todos, o SIGADM foi idealizado para que possamos sempre melhorar e facilitar o trabalho de todos 
                os envolvidos na parte musical, como também proporcionar um melhor controle por parte do administrativo e dos encarregados.
                    </p> 
                <p>A ideia do sistema foi sempre colocar em um mesmo local, e em um mesmo sistema, todos os documentos 
                e mecanismos necessários para se ter as informações desejadas com facilidade, rapidez e principalmente com confiança de 
                guarda, pois teremos todas as informações cadastradas em um só lugar, facilitando assim o acesso de todos.
                    </p>
                <p>Com este sistema poderemos ter acesso de qualquer computador aos grupos GEMs, aos perfis de alunos, ao 
                desenvolvimento individual e coletivo dos candidatos, aos documentos que serão gerados de forma automática como as 
                cartas de testes e pedidos de pré-testes, bem como todo o histórico do aluno. Os pedidos de instrumentos serão feitos 
                também pelo sistema, através de cada encarregado, e assim teremos uma facilidade de controle maior e melhor na distribuição. 
                No tocante ao nosso patrimônio, poderemos tirar relatórios de instrumentos e saber toda a ficha de onde se encontra o 
                instrumento cedido pela igreja, podendo este relatório ser retirado por igreja, aluno ou instrumento.
                    </p>
                <p>Sendo assim, além dessas facilidades acima descritas, existem muitas outras que descobriremos somente com a utilização do sistema no dia a 
                dia, pois o processo de construção de um sistema como esse demanda trabalho e adaptação para a nossa realidade, e graças a 
                Deus ele nos preparou os caminhos para termos essa inovação em nosso estado, e isso com toda a certeza facilitará a vida de 
                todos. O início poderá ser um pouco trabalhoso por não estarmos acostumados com mudanças, mas este sistema já é uma realidade 
                e seus benefícios já estão sendo colhidos, aproveitem com dedicação e sabedoria. Deus abençoe a todos.
                    </p>
            </div>--%>

            <%--<asp:Calendar ID="clAgenda" runat="server" Width="550px" Height="450px" BackColor="White" 
                BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" 
                Font-Size="9pt" ForeColor="Black" NextPrevFormat="ShortMonth" OnSelectionChanged="clAgenda_SelectionChanged" OnDayRender="clAgenda_DayRender">
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                <DayStyle BackColor="#CCCCCC" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="DarkGray" ForeColor="White" />
                <TitleStyle BackColor="#00bf60" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" BorderColor="#00a550" />
                <TodayDayStyle BackColor="DimGray" ForeColor="White" />
            </asp:Calendar>

            <center>
                <asp:Label ID="Label5" runat="server" Text="Ensaio Regional" ForeColor="Blue"></asp:Label> | <asp:Label ID="Label6" runat="server" Text="Ensaio Local" ForeColor="Green"></asp:Label> | 
                <asp:Label ID="Label7" runat="server" Text="Reuniões" ForeColor="Red"></asp:Label> | <asp:Label ID="Label10" runat="server" Text="Treinamentos" ForeColor="Purple"></asp:Label> | <asp:Label ID="Label8" runat="server" Text="Exames" ForeColor="Orange"></asp:Label> | 
                <asp:Label ID="Label9" runat="server" Text="Hoje" ForeColor="DimGray"></asp:Label>
            </center>--%>

            <div class="imgBtnModuloBox">
                <br />
                    <asp:ImageButton ID="moduloGem" runat="server" CssClass="imgBtnModuloBoxImg" ImageUrl="~/img/academico.png" Width="100px" PostBackUrl="~/GEM/Cadastro.aspx" /><br />
                    <br />
                    GEM
            </div>
            <div class="imgBtnModuloBox">
                    <asp:ImageButton ID="moduloExames" runat="server" CssClass="imgBtnModuloBoxImg" ImageUrl="~/img/moduloExames1.png" Width="100px" PostBackUrl="~/Exames/Default.aspx" /><br />
                    Testes e Exames
            </div>
            <div class="imgBtnModuloBox">
                    <asp:ImageButton ID="moduloDownloads" runat="server" CssClass="imgBtnModuloBoxImg" ImageUrl="~/img/download1.png" Width="100px" PostBackUrl="~/Downloads/Default.aspx" /><br />
                    Downloads
            </div>
            <div class="imgBtnModuloBox">
                    <asp:ImageButton ID="moduloCadastroUsuarios" runat="server" CssClass="imgBtnModuloBoxImg" ImageUrl="~/img/cadastroUsuario1.png" Width="100px" PostBackUrl="~/Cadastro/CadastroUser.aspx" /><br />
                    Usuários
            </div>

    </fieldset>

        <fieldset class="fieldsetDefault" style="width:40%;float:left;margin-left:10px;height:480px;visibility:hidden;">
        <legend>TEIA Global</legend>
        <div style="overflow:scroll;height:470px;overflow-x: hidden;">
                <asp:Repeater runat="server" ID="rpTeia">
                    <ItemTemplate>
                        <div class="teiaRowStyle" style='<%# Eval("STYLE") %>'>                            
                            <asp:HyperLink ID="HyperLink3" runat="server" Text='<%# Eval("USUARIO") %>' Font-Size="Small" ToolTip="Ver perfil do Usuário" NavigateUrl='<%# Eval("PERFIL") %>'/>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("ACAO") %>'></asp:Label>
                            <div align="left" style="margin-top:10px;">
                                <div id="divMensagem" style="margin-bottom:5px;">
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("MENSAGEM") %>' ForeColor="ForestGreen" Font-Bold="true"></asp:Label>
                                    <center>
                                    <a href='<%# Eval("imgPostHref") %>' target="_blank"><asp:Image ID="imgPostTeia" runat="server" CssClass="imgPostTeia" Visible='<%# Eval("imgPostVisible") %>' ImageUrl='<%# Eval("imgPostUrl") %>' ToolTip="Clique para visualizar a Imagem."/></a>
                                    </center>
                                </div>
                                <asp:HyperLink ID="HyperLink1" runat="server" Text="Comentar" ImageUrl="~/img/comment.png" ImageWidth="30px" ToolTip="Comentar Ação" NavigateUrl="#" Visible='<%# Eval("VCOMENT") %>'></asp:HyperLink>
                                <asp:LinkButton ID="lkbCurtir" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lkbCurtir_Command" Visible='<%# Eval("VLIKE") %>' ToolTip="Curtir"><img src="img/like1.png" width="25px" /></asp:LinkButton>
                                <%--<asp:ImageButton ID="imgbCurtir" runat="server" ToolTip="Curtir" ImageUrl="~/img/like1.png" Width="25px" CommandArgument='<%# Eval("ID") %>' Visible='<%# Eval("VLIKE") %>' />--%>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("VISIB") %>' ForeColor="GrayText"></asp:Label>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("CURTIR") %>' ToolTip='<%# Eval("CURTIU") %>' ForeColor="Gray"></asp:Label>
                            </div>
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
    </fieldset>
    </div>
</asp:Content>


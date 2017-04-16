<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageClean.master" AutoEventWireup="true" CodeFile="Academico.aspx.cs" Inherits="GEM_Academico" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/jscript">

        function ConfirmaDel() {
            return confirm("ATENÇÃO!! Deseja realmente excluir o registro de teoria? Essa operação não poderá ser desfeita!");
        }
        function ConfirmaLancarPresenca() {
            return confirm("ATENÇÃO!! Deseja realmente lançar as presenças? O registro só pode ser feito uma vez a cada data. E somente o encarregado local ou regional poderá re-lançar as presenças eletrônicas!");
        }
        function ConfirmaDelPresenca() {
            return confirm("ATENÇÃO!! Deseja realmente excluir o registro de presença dessa data? Essa operação não poderá ser desfeita!");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:MultiView ID="mtv" runat="server">

        <asp:View ID="viewHome" runat="server">
            <center><h3>Lançamento de dados Acadêmicos</h3></center>
            <hr />
            <fieldset class="fieldset" style="width:95%;">
                        <legend>Alunos matriculados na GEM de <asp:Label ID="lblGem" runat="server" Text=""></asp:Label></legend>
                <div style="overflow:scroll;height:280px;"
                    <asp:GridView ID="gvMatriculados" runat="server" BackColor="White" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum aluno matriculado nesta GEM." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" Width="100%" OnRowCommand="gvMatriculados_RowCommand">
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
                <%--<asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>                                    
                                    <asp:ImageButton ID="btnNotas" Text="Lançar Notas" runat="server" CausesValidation="false"
                                        CommandName="notas" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/notas1.png"
                                        ToolTip="Lançar Notas" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>--%>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>                                    
                                    <asp:ImageButton ID="btnHinos" Text="Lançar Hinos" runat="server" CausesValidation="false"
                                        CommandName="hinos" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/hinos1.png"
                                        ToolTip="Lançar Hinos" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                <%--<asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>                                    
                                    <asp:ImageButton ID="btnSolfejo" Text="Lançar Solfejo" runat="server" CausesValidation="false"
                                        CommandName="solfejo" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/solfejo1.png"
                                        ToolTip="Lançar Solfejo" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>--%>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <center>                                    
                                    <asp:ImageButton ID="btnInstrumento" Text="Lançar lições de Instrumento" runat="server" CausesValidation="false"
                                        CommandName="metodo" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/instrumento1.png"
                                        ToolTip="Lançar lições de Instrumento" width="20px" />
                                </center>
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
           </Columns>
        </asp:GridView>
                </div>
            </fieldset>
            <hr />
            <div>
                <div style="margin:10px 10px 10px 10px;float:left;"> 
                    <fieldset class="fieldset" style="width:140px;">
                        <legend>Presença Eletronica</legend>      
                        <center>         
                        <asp:ImageButton ID="btnPresenca" runat="server" ImageUrl="~/img/presenca1.png" Width="100px" OnClick="btnPresenca_Click"/>
                        </center>
                    </fieldset>
                </div>
                <div style="margin:10px 10px 10px 10px;float:left;">   
                     <fieldset class="fieldset" style="width:120px;">
                        <legend>Teoria e MTS</legend>                
                    <asp:ImageButton ID="btnMts" runat="server" ImageUrl="~/img/solfejo1.png" Width="100px" OnClick="btnMts_Click" />                    
                     </fieldset>
                </div>
                <%--<div style="margin:10px 10px 10px 10px;float:left;">    
                     <fieldset class="fieldset" style="width:120px;">
                        <legend>Hinos</legend>               
                    <asp:ImageButton ID="btnHinos" runat="server" ImageUrl="~/img/hinos1.png" Width="100px" />
                    </fieldset>
                </div>
                <div style="margin:10px 10px 10px 10px;float:left;">  
                     <fieldset class="fieldset" style="width:120px;">
                        <legend>Solfejo</legend>                 
                    <asp:ImageButton ID="btnNotas" runat="server" ImageUrl="~/img/notas1.png" Width="100px" />
                    </fieldset>
                </div>--%>
               <%-- <div style="margin:10px 10px 10px 10px;float:left;">   
                     <fieldset class="fieldset" style="width:120px;">
                        <legend>Instrumento</legend>                
                    <asp:ImageButton ID="btnInstrumento" runat="server" ImageUrl="~/img/instrumento1.png" Width="100px" />
                    </fieldset>
                </div>--%>
            </div>
            <div style="margin-top:200px;"><hr /></div>
            
        </asp:View>





        <asp:View ID="viewPresenca" runat="server">

            <center>
                <h3><asp:Label ID="lblPresenca" runat="server" Text="---"></asp:Label></h3>
            </center>

            <fieldset class="fieldset" >
                    <legend>Dados da presença eletrônica</legend>
                
                <asp:TextBox ID="txtPresencaData" CssClass="txt" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="clexTxtPresencaData" runat="server" TargetControlID="txtPresencaData" Format="dd/MM/yyyy" />
                <asp:Button ID="btnLancarPresenca" runat="server" CssClass="btn1" Text="Lançar presença eletrônica" OnClick="btnLancarPresenca_Click" OnClientClick="return ConfirmaLancarPresenca();"/>
                <asp:Label ID="lblPermicaoRelancar" runat="server" Text="---"></asp:Label>
                <%--<asp:Button ID="btnGravarPresencas" CssClass="btn1" runat="server" Text="Gravar presenças" OnClick="btnGravarPresencas_Click" />--%>
            </fieldset>
            <hr />

            <fieldset class="fieldset" >
                    <legend>Alunos da GEM aptos a receber presença</legend>
                <div style="overflow:scroll;height:150px;">

                         <asp:Repeater runat="server" ID="rpPresenca">
                            <ItemTemplate>
                                <div>           
                                        <asp:HiddenField ID="hfAlunoID" Value='<%# Eval("ALUNOID") %>' runat="server" />
                                        <asp:HiddenField ID="hfGemID" Value='<%# Eval("GEMID") %>' runat="server" />
                                                              
                                        <asp:CheckBox ID="cb1" runat="server" Text=" | " Checked="true" Enabled="true" />
                                                                       
                                        <asp:Label ID="lblPrecensaNome" runat="server" Text='<%# Eval("NOME") %>'></asp:Label>
                                                                                    
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                </div>
                
            </fieldset>

            <fieldset class="fieldsetDefault">
                <legend>Presenças lançadas</legend>
                <div style="overflow:scroll;height:150px;">
                    <asp:GridView ID="gvPresencaLancada" runat="server" BackColor="White" 
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" AutoGenerateColumns="true"
                        CellPadding="4" GridLines="Both" EmptyDataText="Nenhuma presença registrada nesta GEM." 
                        RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" Width="100%" OnRowCommand="gvPresencaLancada_RowCommand">
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
                                        <%--<asp:ImageButton ID="btnAction" Text="Ações do Item" runat="server" CausesValidation="false"
                                            CommandName="Alterar" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/action1.png"
                                            ToolTip="Ações do Item" PostBackUrl="#" width="20px" />--%>
                                        <asp:ImageButton ID="btnExcluir" Text="Excluir" runat="server" CausesValidation="false"
                                            CommandName="Excluir" CommandArgument='<%# Eval("DATA") %>' ImageUrl="~/img/excluir.png"
                                            ToolTip="Excluir lançamento de presença" PostBackUrl="#" width="20px" OnClientClick="return ConfirmaDelPresenca();" />
                                </ItemTemplate>
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                            </asp:TemplateField>
               </Columns>
            </asp:GridView>
        </div>
            </fieldset>


            <hr />
                <div style="margin:10px 10px 10px 10px;border-style: solid; border-color:beige;width:150px;">
                    <asp:HyperLink ID="hlBackPresenca" runat="server">Voltar para Acadêmico</asp:HyperLink>
                </div>
        </asp:View>






        <asp:View ID="viewMts" runat="server">
            <div>
                <fieldset class="fieldset" >
                    <legend>Assuntos do MTS apresentados na GEM</legend>
                    Data: <asp:TextBox ID="txtDataMts" CssClass="txt" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="clexTxtDataMts" runat="server" TargetControlID="txtDataMts" Format="dd/MM/yyyy" />
                    | Módulo: <asp:DropDownList ID="ddlModuloMts" runat="server" CssClass="ddlDefault" AutoPostBack="true" OnSelectedIndexChanged="ddlModuloMts_SelectedIndexChanged"></asp:DropDownList>
                    <%--| Aula: <asp:DropDownList ID="ddlAulaMts" runat="server" CssClass="ddlDefault" AutoPostBack="true" OnSelectedIndexChanged="ddlAulaMts_SelectedIndexChanged"></asp:DropDownList>--%>
                    | Assunto Abordado: <asp:DropDownList ID="ddlAssuntosMts" CssClass="ddlDefault" runat="server"></asp:DropDownList>
                </fieldset>
                <asp:Button ID="btnLancarMts" runat="server" Text="Lançar Dados" CssClass="btn1" OnClick="btnLancarMts_Click" />    

                <hr />
                <fieldset class="fieldset" >
                    <legend>Histórico de assuntos do MTS apresentados na GEM</legend>
                    <div style="height:370px;overflow:scroll">

                       <asp:GridView ID="gvTeoriaMts" runat="server" BackColor="White" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" AutoGenerateColumns="true"
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum assunto registrado nesta GEM." 
                    RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" Width="100%" OnRowCommand="gvTeoriaMts_RowCommand">
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
                                    <%--<asp:ImageButton ID="btnAction" Text="Ações do Item" runat="server" CausesValidation="false"
                                        CommandName="Alterar" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/action1.png"
                                        ToolTip="Ações do Item" PostBackUrl="#" width="20px" />--%>
                                    <asp:ImageButton ID="btnExcluir" Text="Excluir" runat="server" CausesValidation="false"
                                        CommandName="Excluir" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/excluir.png"
                                        ToolTip="Excluir Assunto" PostBackUrl="#" width="20px" OnClientClick="return ConfirmaDel();" />
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
           </Columns>
        </asp:GridView>

                    </div>
                </fieldset>
                <hr />
                <div style="margin:10px 10px 10px 10px;border-style: solid; border-color:beige;width:150px;">
                    <asp:HyperLink ID="hlBackMts" runat="server">Voltar para Acadêmico</asp:HyperLink>
                </div>
            </div>
        </asp:View>




        <asp:View ID="viewHinos" runat="server">            
            <div style="width:1000px;height:450px">
            <div style="float:left;width:700px;">
            <fieldset class="fieldset">    
        <asp:HiddenField ID="hfAlunoID" runat="server" />    
        <legend><asp:Label ID="lblTotalHinosPassados" runat="server" Text=""></asp:Label></legend>
            <div style="overflow: scroll; height: 400px;">
                <asp:Repeater runat="server" ID="rpHinos">
                    <ItemTemplate>
                        <div style="width: 100px; float: left; border-style: solid; border-color: <%# Eval("bColor") %>; margin: 10px 10px 10px 10px;">
                             <div style="float: left;">
                                <asp:HiddenField ID="hf1" Value='<%# Eval("numero") %>' runat="server" />
                                <asp:CheckBox ID="cb1" runat="server" Text='<%# Eval("hino") %>' Checked='<%# Eval("ck") %>' Enabled='<%# Eval("en") %>' />
                             </div>
                            <%--<div style="float: left;">
                                <asp:Label ID="lblHino" runat="server" Text='<%# Eval("numero") %>'></asp:Label><br />
                            </div>   --%>                        
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </fieldset>
            </div>
            <div style="float:left;">
            <fieldset class="fieldset">
                <legend>Histórico de Lançamentos</legend>
                <div style="overflow:scroll;height:400px;width:260px;"
                    <asp:GridView ID="gvHinos" runat="server" BackColor="White" OnRowDataBound="gvHinos_RowDataBound"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" OnRowCommand="gvHinos_RowCommand"
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhum hino passado inda." 
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
                                    <asp:ImageButton ID="btnExcluir" Text="Excluir" runat="server" CausesValidation="false"
                                        CommandName="Excluir" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/excluir.png"
                                        ToolTip="Excluir Registro" PostBackUrl="#" width="20px" />
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
           </Columns>
        </asp:GridView>
                </div>
            </fieldset>
            </div>
            <fieldset class="fieldset" style="width:97%;">
                <legend>Lançar dados</legend>
                Data:
                <asp:TextBox ID="txtData" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="clexData" runat="server" TargetControlID="txtData" Format="dd/MM/yyyy" />
                Voz: <asp:DropDownList ID="ddlVozHino" CssClass="ddlDefault" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVozHino_SelectedIndexChanged"></asp:DropDownList>
                <asp:Button ID="btnLancarHinos" runat="server" Text="Lançar Hinos" CssClass="btn1" OnClick="btnLancarHinos_Click" />
            </fieldset>
            </div>
            <div style="margin:10px 10px 10px 10px;border-style: solid; border-color:beige;width:150px;">
                <asp:HyperLink ID="hlBack" runat="server">Voltar para Acadêmico</asp:HyperLink>
            </div>
        </asp:View>


         <asp:View ID="viewMetodo" runat="server">
             <fieldset id="fdsMetodoDefault" runat="server" class="fieldset">
                 <h3>
                     <asp:Label ID="lblAlunoMetodo" runat="server" Text="---"></asp:Label></h3>
                <legend>Lançamento e visualização de lições de instrumento</legend>
                 <div style="float:left;width:50%;">
                     <fieldset id="Fieldset1" class="fieldset">
                          <legend>Lançar dados de Lições de Instrumento</legend>
                    Data: 
                    <asp:TextBox ID="txtDataMetodo" CssClass="txt" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="clexDataMetodo" runat="server" TargetControlID="txtDataMetodo" Format="dd/MM/yyyy" />
                     <br />
                     Método usado: 
                     <asp:DropDownList ID="ddlMetodos" runat="server" CssClass="ddlDefault" Width="395px"></asp:DropDownList>
                     <asp:ImageButton ID="imbtnAddMethod" ImageUrl="~/img/plus1.png" Width="25px" ToolTip="Adicionar novo método" runat="server" OnClick="imbtnAddMethod_Click" />
                     <br />
                     Pagina: 
                    <asp:TextBox ID="txtMetodoPagina" CssClass="txt" runat="server" Width="195px"></asp:TextBox>
                     Lição: 
                    <asp:TextBox ID="txtMetodoLicao" CssClass="txt" runat="server" Width="195px"></asp:TextBox>
                     <br />
                     Observações: 
                    <asp:TextBox ID="txtMetodoObs" CssClass="txt" TextMode="MultiLine" Width="399px" Height="100px" runat="server"></asp:TextBox>
                    <br /> 
                    <asp:Button ID="btnLancarMetodo" runat="server" Text="Lançar dados" CssClass="btn1" OnClick="btnLancarMetodo_Click" />
                        </fieldset>
                  </div>
                 <div style="float:left;width:50%;">
                     <fieldset id="Fieldset2" class="fieldset">
                          <legend>Lições de Instrumento lançadas</legend>
                            
                         <div style="overflow:scroll;height:400px;width:100%;">
                            <asp:GridView ID="gvMetodoLancado" runat="server" BackColor="White" OnRowCommand="gvMetodoLancado_RowCommand" OnRowDataBound="gvMetodoLancado_RowDataBound" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" GridLines="Both" EmptyDataText="Nenhuma lição de método passada ainda." 
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
                                    <asp:ImageButton ID="btnExcluir" Text="Excluir" runat="server" CausesValidation="false"
                                        CommandName="Excluir" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/img/excluir.png"
                                        ToolTip="Excluir Registro" PostBackUrl="#" width="20px" />
                            </ItemTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
           </Columns>
        </asp:GridView>
    </div>


                         </fieldset>
                 </div>
             </fieldset>
             <fieldset id="fdsAddMetodo" runat="server" visible="false" class="fieldset">
                <legend>Dados do novo método</legend>
                 
                 Descrição do método: <asp:TextBox ID="txtNewMetodoDescricao" Width="200px" runat="server" CssClass="txt"></asp:TextBox>
                 Instrumento: <asp:DropDownList ID="ddlNewMetodoInstrumento" CssClass="ddlDefault" runat="server"></asp:DropDownList>
                 <asp:Button ID="btnNewMetodoAdd" runat="server" Text="Adicionar método" CssClass="btn1" OnClick="btnNewMetodoAdd_Click" />
            </fieldset>
            <div style="margin:10px 10px 10px 10px;border-style: solid; border-color:beige;width:150px;">
                <asp:HyperLink ID="hlBackMetodo" runat="server">Voltar para Acadêmico</asp:HyperLink>
            </div>
         </asp:View>


    </asp:MultiView>
</asp:Content>


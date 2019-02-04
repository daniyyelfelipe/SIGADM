using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageHome : System.Web.UI.MasterPage
{
    BD bd = new BD();
    App app = new App();

    protected void Page_Load(object sender, EventArgs e)
    {

        //passa as tags dos usuarios
        //-----------------------------------------------
        var usuarios = (from p in bd.db._users
                        select p).ToList();

        ArrayList arrayList = new ArrayList();

        for (int i = 0; i < usuarios.Count; i++)
        {
            arrayList.Add(usuarios[i].nome);
        }

        //Atribuindo os valores ao HiddenField
        HiddenField1.Value = ArrayListToString(arrayList);

        //-----------------------------------------------


        if (Session["usuarioLogado"] != null)
        {
            _user usuario = new _user();
            usuario = (_user)Session["usuarioLogado"];

            lblLogadoComo.Text = "Bem vindo " + usuario.nome;   
        }
        else
        {
            Response.Redirect("~/Logoff.aspx");
        }

        if (!IsPostBack)
        {
            CarregarForm();
        }

        MontaMenu();

        lblVersion.Text = app.GetVersion();
    }

    private string ArrayListToString(ArrayList arrayList)
    {
        //Sainda a ser atribuida no HiddenField
        string saidaArray = "";

        for (int i = 0; i <= arrayList.Count - 1; i++)
        {
            //Verificando se é o primeiro item a ser tratado
            if (i > 0)
            {
                //Delimitador
                saidaArray += "~";
            }
            saidaArray += arrayList[i].ToString();
        }
        return saidaArray;
    }

    private void CarregarForm()
    {
        try
        {
            _user usuarioLogado = (_user)Session["usuarioLogado"];
            imgbPerfil.ImageUrl = "~/data/user/" + usuarioLogado.id + "/img/perfil.jpg";
            //imgbPerfil.PostBackUrl = "~/perfil/?user=" + usuarioLogado.id;
        }
        catch
        {

        }
    }

    protected void imgbLogoff_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbPerfil_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            _user usuarioLogado = (_user)Session["usuarioLogado"];
            Response.Redirect("~/perfil/?user=" + usuarioLogado.id);
        }
        catch
        {

        }
    }

    private void MontaMenu()
    {
        try
        {
            _user usuario = new _user();
            usuario = (_user)Session["usuarioLogado"];


            //declaração dos menus
            //-------------------------------------------------------------------------------------
            //cadastros
            MenuItem miCadastros = new MenuItem("Cadastros", "", "", "#");
            miCadastros.Selectable = false;
            MenuItem miCadastrosEnsino = new MenuItem("Ensino", "", "", "#");
            MenuItem miCadastrosEnsinoGEM = new MenuItem("GEM", "", "", "#");
            MenuItem miCadastrosEnsinoAlunos = new MenuItem("Alunos", "", "", "~/Cadastro/Alunos.aspx");

            //cadastros - setor
            MenuItem miSetor = new MenuItem("Central de Colaboradores", "", "", "~/cadastro/encarregados.aspx");
            MenuItem miSetorMusicos = new MenuItem("Central de Músicos e Organistas", "", "", "~/cadastro/CentralMusicos.aspx");
            MenuItem miSetorInstrutores = new MenuItem("Instrutores", "", "", "#");
            MenuItem miSetorEncarregados = new MenuItem("Colaboradores", "", "", "~/cadastro/encarregados.aspx");
            MenuItem miSetorExaminadoras = new MenuItem("Examinadoras", "", "", "#");
            //MenuItem miSetorMetodos = new MenuItem("Métodos", "", "", "#");


            //cadastros - patrimonio            
            MenuItem miPatrimonio = new MenuItem("Patrimonio", "", "", "#");
            MenuItem miPatrimonioInstrumentos = new MenuItem("Instrumentos", "", "", "~/instrumentos/Default.aspx");

            //cadastros - regional            
            MenuItem miRegional = new MenuItem("Regional", "", "", "#");
            MenuItem miRegionalCongregacoes = new MenuItem("Congregações", "", "", "~/Cadastro/Congregacoes.aspx");
            MenuItem miRegionalCidades = new MenuItem("Cidades", "", "", "~/Cadastro/Cidades.aspx");

            //cadastros - agenda
            MenuItem miAgendaDoSetor = new MenuItem("Agenda do Setor", "", "", "~/Cadastro/Agenda.aspx");

            //GEM
            MenuItem miGEM = new MenuItem("GEM", "", "", "#");
            miGEM.Selectable = false;
            MenuItem miGEMCadastro = new MenuItem("Cadastro/Matrícula/Acadêmico", "", "", "~/gem/cadastro.aspx");
            MenuItem miGEMRelatorios = new MenuItem("Relatórios", "", "", "");
            MenuItem miGEMRelatoriosResumoGEM = new MenuItem("Resumo da GEM", "", "", "");
            //MenuItem miGEMRelatoriosHistoricoAluno = new MenuItem("Histórico do Aluno", "", "", "");
            MenuItem miGEMConsolidacao = new MenuItem("Consolidação de Turmas", "", "", "#");
            MenuItem miGEMAlunos = new MenuItem("Central de Candidatos", "", "", "~/Cadastro/Alunos.aspx");
            //MenuItem miGEMControleAcademico = new MenuItem("Controle Acadêmico", "", "", "#");

            //EXAMES
            MenuItem miExames = new MenuItem("Testes e Exames", "", "", "~/Exames/");
            //miExames.Selectable = false;
            MenuItem miExamesAbrir = new MenuItem("Abrir/Resultados", "", "", "~/Exames/");

            //movimentaçoes           
            MenuItem miMovimentacoes = new MenuItem("Movimentações", "", "", "#");
            MenuItem miMovimentacoesInstrumentos = new MenuItem("Instrumentos", "", "", "#");
            MenuItem miMovimentacoesPedidosRealizados = new MenuItem("Consulta de Pedidos", "", "", "~/Formularios/Patrimonio/PedidosDeInstrumentosRealizados.aspx");

            //reunioes           
            MenuItem miReunioes = new MenuItem("Reuniões", "", "", "#");

            //formularios           
            MenuItem miFormularios = new MenuItem("Formulários", "", "", "#");
            MenuItem miFormulariosPedidoINstrumento = new MenuItem("Pedido de Instrumento", "", "", "~/Formularios/Patrimonio/PedidoInstrumento.aspx");
            MenuItem miFormulariosTermoResponsabilidade = new MenuItem("Termo de Responsabilidade", "", "", "~/Formularios/Patrimonio/TermoResponsabilidade.aspx");
            MenuItem miFormulariosPedidoPreTeste = new MenuItem("Solicitação de Pré-Teste", "", "", "~/Formularios/alunos/SolicitacaoPreTeste.aspx");
            
            //downloads           
            MenuItem miDownloads = new MenuItem("Downloads", "", "", "~/Downloads/");

            //configurações           
            MenuItem miConfiguracoes = new MenuItem("Configurações", "", "", "#");
            miConfiguracoes.Selectable = false;
            MenuItem miConfiguracoesCadastroUsuarios = new MenuItem("Cadastro de Usuarios", "", "", "~/cadastro/CadastroUser.aspx");

            //estatisticas
            MenuItem miEstatisticas = new MenuItem("Estatisticas", "", "", "~/estatisticas/");

            //Developer Toos
            MenuItem miTools = new MenuItem("Developer Tools", "", "", "#");
            miTools.Selectable = false;
            MenuItem miToolsPassSystem = new MenuItem("PassSystem", "", "", "~/Tools/PassSystem.aspx");
            MenuItem miToolsUserSystem = new MenuItem("UserSystem", "", "", "~/Tools/UserSystem.aspx");
            MenuItem miToolsLog = new MenuItem("Log", "", "", "~/Tools/Log.aspx");

            //Ajuda
            MenuItem miAjuda = new MenuItem("Ajuda", "", "", "#");
            miAjuda.Selectable = false;
            MenuItem miAjudaVideos = new MenuItem("Videos de Treinamento", "", "", "~/Ajuda/Videos.aspx");

            //Exercicio
            var exercicio = (from p in bd.db._Variaveis where p.descricao == "exercicioAtual" select p.value).Single();
            MenuItem miExercicio = new MenuItem("<font color='yellow'><b>Exercício " + exercicio + "</b></font>", "", "", "#");
            miExercicio.Selectable = false;
            miExercicio.ToolTip = "Exercicio atual do sistema.";

            //-------------------------------------------------------------------------------------

            //montagem dos menus
            //-------------------------------------------------------------------------------------
            //cadastros
            //miCadastros.ChildItems.Add(miCadastrosEnsino);
            miCadastrosEnsino.ChildItems.Add(miCadastrosEnsinoGEM);
            miCadastrosEnsino.ChildItems.Add(miCadastrosEnsinoAlunos);

            //GEM
            miGEM.ChildItems.Add(miGEMCadastro);
            miGEMRelatorios.ChildItems.Add(miGEMRelatoriosResumoGEM);
            //miGEMRelatorios.ChildItems.Add(miGEMRelatoriosHistoricoAluno);
            //menu liberado para desenvolvedores, secretarios e regionais
            if (usuario.tipoID == 8 || usuario.tipoID == 9 || usuario.tipoID == 1)
            {
                miGEM.ChildItems.Add(miGEMRelatorios);
                miGEM.ChildItems.Add(miGEMConsolidacao);
            }

            miGEM.ChildItems.Add(miGEMAlunos);
            //miGEM.ChildItems.Add(miGEMControleAcademico);

            //EXAMES
            //miExames.ChildItems.Add(miExamesAbrir);

            //setor
            miCadastros.ChildItems.Add(miSetor);
            miCadastros.ChildItems.Add(miSetorMusicos);
            //miSetor.ChildItems.Add(miSetorInstrutores);
            //miSetor.ChildItems.Add(miSetorEncarregados);
            //miSetor.ChildItems.Add(miSetorExaminadoras);
            //miSetor.ChildItems.Add(miSetorMetodos);

            //patrimonio
             //secretários e desenvolvedor
            if (usuario.tipoID == 9 || usuario.tipoID == 8)
            {
                miCadastros.ChildItems.Add(miPatrimonio);
                miPatrimonio.ChildItems.Add(miPatrimonioInstrumentos);
            }

            //regional
            //secretários e desenvolvedor
            if (usuario.tipoID == 9 || usuario.tipoID == 8)
            {
                miCadastros.ChildItems.Add(miRegional);
                miRegional.ChildItems.Add(miRegionalCidades);
                miRegional.ChildItems.Add(miRegionalCongregacoes);
            }

            //agenda
            //secretários e desenvolvedor
            if (usuario.tipoID == 9 || usuario.tipoID == 8)
            {
                miCadastros.ChildItems.Add(miAgendaDoSetor);
            }

            //movimentações
            miMovimentacoes.ChildItems.Add(miMovimentacoesInstrumentos);
            miMovimentacoesInstrumentos.ChildItems.Add(miMovimentacoesPedidosRealizados);

            //formularios
            miFormularios.ChildItems.Add(miFormulariosPedidoINstrumento);
            //miFormularios.ChildItems.Add(miFormulariosTermoResponsabilidade);
            miFormularios.ChildItems.Add(miFormulariosPedidoPreTeste);

            //configurações
            miConfiguracoes.ChildItems.Add(miConfiguracoesCadastroUsuarios);

            //Tools
            miTools.ChildItems.Add(miEstatisticas);
            miTools.ChildItems.Add(miToolsPassSystem);
            miTools.ChildItems.Add(miToolsUserSystem);
            miTools.ChildItems.Add(miToolsLog);

            //ajuda
            miAjuda.ChildItems.Add(miAjudaVideos);

            //-------------------------------------------------------------------------------------

            //seleção dos menus por tipo de usuario
            //-------------------------------------------------------------------------------------

            //musico e organista
            if (usuario.tipoID == 10 || usuario.tipoID == 11)
            {
                NavigationMenu.Items.Add(miSetorMusicos);
                NavigationMenu.Items.Add(miDownloads);
                NavigationMenu.Items.Add(miExercicio);
            }
            //encarregados locais
            if (usuario.tipoID == 2)
            {
                NavigationMenu.Items.Add(miCadastros);
                NavigationMenu.Items.Add(miGEM);
                NavigationMenu.Items.Add(miExames);
                NavigationMenu.Items.Add(miDownloads);
                //NavigationMenu.Items.Add(miFormularios);
                //NavigationMenu.Items.Add(miMovimentacoesPedidosRealizados);
                //NavigationMenu.Items.Add(miConfiguracoes);
                NavigationMenu.Items.Add(miEstatisticas);
                NavigationMenu.Items.Add(miAjuda);
                NavigationMenu.Items.Add(miExercicio);
            }
            //desenvolvedor
            if (usuario.tipoID == 8)
            {
                NavigationMenu.Items.Add(miCadastros);
                NavigationMenu.Items.Add(miGEM);
                NavigationMenu.Items.Add(miExames);
                //NavigationMenu.Items.Add(miMovimentacoes);
                //NavigationMenu.Items.Add(miFormularios);
                NavigationMenu.Items.Add(miDownloads);
                //NavigationMenu.Items.Add(miConfiguracoes);
                //NavigationMenu.Items.Add(miEstatisticas);
                NavigationMenu.Items.Add(miAjuda);
                NavigationMenu.Items.Add(miTools);
                NavigationMenu.Items.Add(miExercicio);
            }
            //secretários
            if (usuario.tipoID == 9)
            {
                NavigationMenu.Items.Add(miCadastros);
                NavigationMenu.Items.Add(miGEM);
                NavigationMenu.Items.Add(miExames);
                //NavigationMenu.Items.Add(miMovimentacoes);
                //NavigationMenu.Items.Add(miFormularios);
                NavigationMenu.Items.Add(miDownloads);
                //NavigationMenu.Items.Add(miConfiguracoes);
                NavigationMenu.Items.Add(miEstatisticas);
                NavigationMenu.Items.Add(miAjuda);
                NavigationMenu.Items.Add(miExercicio);
            }
            //encarregado regional
            if (usuario.tipoID == 1)
            {
                NavigationMenu.Items.Add(miCadastros);
                NavigationMenu.Items.Add(miGEM);
                NavigationMenu.Items.Add(miExames);
                NavigationMenu.Items.Add(miDownloads);
                //NavigationMenu.Items.Add(miFormularios);
                //NavigationMenu.Items.Add(miMovimentacoesPedidosRealizados);
                //NavigationMenu.Items.Add(miConfiguracoes);
                NavigationMenu.Items.Add(miEstatisticas);
                NavigationMenu.Items.Add(miAjuda);
                NavigationMenu.Items.Add(miExercicio);
            }
            //examinadora
            if (usuario.tipoID == 4)
            {
                NavigationMenu.Items.Add(miCadastros);
                NavigationMenu.Items.Add(miGEM);
                NavigationMenu.Items.Add(miExames);
                NavigationMenu.Items.Add(miDownloads);
                //NavigationMenu.Items.Add(miConfiguracoes);
                NavigationMenu.Items.Add(miEstatisticas);
                NavigationMenu.Items.Add(miAjuda);
                NavigationMenu.Items.Add(miExercicio);
            }
            //instrutor
            if (usuario.tipoID == 3)
            {
                NavigationMenu.Items.Add(miCadastros);
                NavigationMenu.Items.Add(miGEM);
                //NavigationMenu.Items.Add(miExames);
                NavigationMenu.Items.Add(miDownloads);
                NavigationMenu.Items.Add(miAjuda);
                NavigationMenu.Items.Add(miExercicio);
            }
            //instrutora
            if (usuario.tipoID == 12)
            {
                NavigationMenu.Items.Add(miCadastros);
                NavigationMenu.Items.Add(miGEM);
                NavigationMenu.Items.Add(miExames);
                NavigationMenu.Items.Add(miDownloads);
                NavigationMenu.Items.Add(miAjuda);
                NavigationMenu.Items.Add(miExercicio);
            }
            //aluno
            if (usuario.tipoID == 5)
            {
                NavigationMenu.Items.Add(miDownloads);
            }
            //Ancião
            if (usuario.tipoID == 6)
            {
                NavigationMenu.Items.Add(miCadastros);
                NavigationMenu.Items.Add(miGEM);
                NavigationMenu.Items.Add(miExames);
                NavigationMenu.Items.Add(miDownloads);
                NavigationMenu.Items.Add(miEstatisticas);
                //NavigationMenu.Items.Add(miFormularios);                
                //NavigationMenu.Items.Add(miMovimentacoesPedidosRealizados);
                NavigationMenu.Items.Add(miAjuda);
                NavigationMenu.Items.Add(miExercicio);
            }

            //-------------------------------------------------------------------------------------
        }
        catch
        {

        }
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnGoSearch_Click(object sender, EventArgs e)
    {
        try
        {
            var userSearch = (from p in bd.db._users
                              where p.nome == txtSearch.Text.Trim()
                              select p.id).Single();

            Response.Redirect("~/perfil/?user=" + userSearch);
        }
        catch
        {

        }
    }
}

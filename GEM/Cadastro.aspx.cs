using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GEM_Cadastro : System.Web.UI.Page
{
    BD bd = new BD();
    Log log = new Log();
    Guard guard = new Guard();
    _user usuarioLogado;
    App app = new App();
    Global gl = new Global();

    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogado = (_user)Session["usuarioLogado"];

        if (!IsPostBack)
        {
            CarregaForm();
            ddlCidade_TextChanged(sender, e);
            CarregaGems();
        }
    }
    private void CarregaForm()
    {
        try
        {
            var cidades = (from p in bd.db._municipios
                           orderby p.descricao
                           select p.descricao).ToList();
            ddlCidade.DataSource = cidades;
            ddlCidade.DataBind();

            var periodo = (from p in bd.db._GEM_Periodos
                           orderby p.descricao
                           select p.descricao).ToList();
            ddlPeriodo.DataSource = periodo;
            ddlPeriodo.DataBind();

            var turma = (from p in bd.db._GEM_Turmas
                           orderby p.descricao
                           select p.descricao).ToList();
            ddlTurma.DataSource = turma;
            ddlTurma.DataBind();

            var nipes = (from p in bd.db._instrumentoNipes
                         orderby p.descricao
                         select p.descricao).ToList();
            ddlNipe.DataSource = nipes;
            ddlNipe.DataBind();

            var regional = (from p in bd.db._users
                            where p.tipoID == 1 || p.tipoID == 4
                            && p.statusID == 1
                            orderby p.nome
                            select p.nome).ToList();
            ddlRegional.DataSource = regional;
            ddlRegional.DataBind();

            var local = (from p in bd.db._users
                         where p.tipoID == 2
                         && p.statusID == 1
                         orderby p.nome
                         select p.nome).ToList();
            ddlLocal.DataSource = local;
            ddlLocal.DataBind();

            var instrutor = (from p in bd.db._users
                             where p.tipoID == 3 || p.tipoID == 12
                             && p.statusID == 1
                             orderby p.nome
                         select p.nome).ToList();
            ddlInstrutor.DataSource = instrutor;
            ddlInstrutor.DataBind();

            //Cadastro de GEMs aberto para desenvolvedor, secretarios, regional e examinadoras
            if (usuarioLogado.tipoID == 8 || usuarioLogado.tipoID == 9 || usuarioLogado.tipoID == 1 || usuarioLogado.tipoID == 4)
            {
                
            }
            else
            {
                fdsNew.Style.Add("visibility", "hidden;");
                fdsGems.Style.Add("width", "95%;");
                fdsGems.Style.Add("position", "absolute;"); 
            }
            
        }
        catch
        {

        }
    }
    protected void ddlCidade_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var comum = (from p in bd.db._igrejas
                         where p._municipio.descricao == ddlCidade.SelectedValue
                         orderby p.descricao
                         select p.descricao).ToList();
            ddlComum.DataSource = comum;
            ddlComum.DataBind();

            
        }
        catch
        {

        } 
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        try
        {
            var testeGem = (from p in bd.db._GEMs 
                            where p._municipio.descricao == ddlCidade.SelectedValue 
                            && p._igreja.descricao == ddlComum.SelectedValue 
                            && p._instrumentoNipe.descricao == ddlNipe.SelectedValue
                            && p._GEM_Periodo.descricao == ddlPeriodo.SelectedValue
                            && p._GEM_Turma.descricao == ddlTurma.SelectedValue
                            select p).ToList();

            if (testeGem.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Já existe uma GEM cadastrada para essa comum congregação com o mesmo período, turma e nipe. Contate o setor de suporte para mais informações!');", true);
            }
            else
            {
                var gem = new _GEM();
                gem.cidadeID = (from p in bd.db._municipios where p.descricao == ddlCidade.SelectedValue select p.id).Single();
                gem.comumID = (from p in bd.db._igrejas where p.descricao == ddlComum.SelectedValue && p._municipio.descricao == ddlCidade.SelectedValue select p.id).Single();
                gem.periodoID = (from p in bd.db._GEM_Periodos where p.descricao == ddlPeriodo.SelectedValue select p.ID).Single();
                gem.turmaID = (from p in bd.db._GEM_Turmas where p.descricao == ddlTurma.SelectedValue select p.ID).Single();
                gem.regionalID = (from p in bd.db._users where p.nome == ddlRegional.SelectedValue && p.statusID == 1 select p.id).Single();
                gem.localID = (from p in bd.db._users where p.nome == ddlLocal.SelectedValue && p.statusID == 1 select p.id).Single();
                gem.instrutorID = (from p in bd.db._users where p.nome == ddlInstrutor.SelectedValue && p.statusID == 1 select p.id).Single();
                gem.nipeID = (from p in bd.db._instrumentoNipes where p.descricao == ddlNipe.SelectedValue select p.id).Single();
                gem.status = 1;
                gem.dataCriacao = app.DateTimeCorrigido();

                bd.db._GEMs.InsertOnSubmit(gem);
                bd.db.SubmitChanges();

                CarregaGems();

                log.AdicionarEntrada(25, usuarioLogado.id, 6, "", 1, 0);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('GEM cadastrada com sucesso!!');", true);
            }
        }
        catch(Exception er1)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + er1.Message + "');", true);
        }
    }

    private void CarregaGems()
    {
        try
        {
            //mostra todas as GEMs ativas ou cadastradas apenas para, secretarios e anciões //regionais 
            if (usuarioLogado.tipoID == 9 || usuarioLogado.tipoID == 6)//|| usuarioLogado.tipoID == 1 )
            {
                var GEM = (from p in bd.db._GEMs
                           where p.status == 1 || p.status == 2
                           select new
                           {
                               ID = p.Id,
                               CIDADE = p._municipio.descricao,
                               CONGREGACAO = p._igreja.descricao,
                               PERIODO = p._GEM_Periodo.descricao,
                               T = p._GEM_Turma.descricao,
                               NIPE = p._instrumentoNipe.descricao,
                               REGIONAL = gl.FirstWords(p._user2.nome,2),
                               LOCAL = gl.FirstWords(p._user1.nome, 2),
                               INSTRUTOR = gl.FirstWords(p._user.nome, 2),
                               STATUS = p._GEM_Status.descricao,
                               //MATRICULAS = (from g in bd.db._GEM_Matriculas where g.gemID == p.Id select g).ToList().Count()
                           }).ToList();
                gvGEM.DataSource = GEM;
                gvGEM.DataBind();

                lblQuantidadeGems.Text = "Você pode visualizar " + GEM.Count().ToString() +" GEMs.";
            }
            // mostra todas as GEMs inclusive inativas apenas para desenvolvedores
            else if (usuarioLogado.tipoID == 8)
            {
                var GEM = (from p in bd.db._GEMs
                           where p.status == 1 || p.status == 2 //ativas ou cadastradas
                           select new
                           {
                               ID = p.Id,
                               CIDADE = p._municipio.descricao,
                               CONGREGACAO = p._igreja.descricao,
                               PERIODO = p._GEM_Periodo.descricao,
                               T = p._GEM_Turma.descricao,
                               NIPE = p._instrumentoNipe.descricao,
                               REGIONAL = gl.FirstWords(p._user2.nome, 2),
                               LOCAL = gl.FirstWords(p._user1.nome, 2),
                               INSTRUTOR = gl.FirstWords(p._user.nome, 2),
                               STATUS = p._GEM_Status.descricao,
                               //STATUSCOLOR = (p._GEM_Status.descricao.Contains("ativa")) ? "YellowGreen" : "Gray"
                               //MATRICULAS = (from g in bd.db._GEM_Matriculas where g.gemID == p.Id select g).ToList().Count()
                           }).ToList();
                gvGEM.DataSource = GEM;
                gvGEM.DataBind();

                lblQuantidadeGems.Text = "Você pode visualizar " + GEM.Count().ToString() + " GEMs.";
            }
            //apenas GEMs ativas ou cadastradas e que ele participa, para os demais
            else
            {
                var GEM = (from p in bd.db._GEMs
                           where p.localID == usuarioLogado.id 
                           || p.instrutorID == usuarioLogado.id
                           || p.regionalID == usuarioLogado.id
                           //&& p.status == 1 || p.status == 2
                           select new
                           {
                               ID = p.Id,
                               CIDADE = p._municipio.descricao,
                               CONGREGACAO = p._igreja.descricao,
                               PERIODO = p._GEM_Periodo.descricao,
                               T = p._GEM_Turma.descricao,
                               NIPE = p._instrumentoNipe.descricao,
                               REGIONAL = p._user2.nome,
                               LOCAL = p._user1.nome,
                               INSTRUTOR = p._user.nome,
                               STATUS = p._GEM_Status.descricao,
                               //MATRICULAS = 0
                           }).ToList();
                gvGEM.DataSource = GEM;
                gvGEM.DataBind();

                lblQuantidadeGems.Text = "Você pode visualizar " + GEM.Count().ToString() + " GEMs.";
            }


        }
        catch
        {

        } 
    }
    protected void gvGEM_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Matricula")
            {
                String s = "<script language='javascript'> javascript:abrirMatricula('Matricula.aspx?idg=" + e.CommandArgument.ToString() + "'); </script>";
                Page.RegisterClientScriptBlock("Print", s);
            }
            if (e.CommandName == "Academico")
            {
                //checa se a gem esta ativa
                var gem = (from p in bd.db._GEMs
                           where p.Id == int.Parse(e.CommandArgument.ToString())
                           select p).Single();
                if (gem.status == 2)
                {
                    String s = "<script language='javascript'> javascript:abrirAcademico('Academico.aspx?idg=" + e.CommandArgument.ToString() + "'); </script>";
                    Page.RegisterClientScriptBlock("Print", s);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('A GEM selecionada não esta ativa ou não tem alunos matriculados! ');", true);
                }
            }
        }
        catch
        {

        }
    }
    protected void btnGemSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //mostra todas as GEMs ativas ou cadastradas apenas para, secretarios e anciões //regionais 
            if (usuarioLogado.tipoID == 9 || usuarioLogado.tipoID == 6)//|| usuarioLogado.tipoID == 1 )
            {
                var GEM = (from p in bd.db._GEMs
                           where p._igreja.descricao.Contains(txtGemSearch.Text)
                           //&& p.status == 1 || p.status == 2
                           select new
                           {
                               ID = p.Id,
                               CIDADE = p._municipio.descricao,
                               CONGREGACAO = p._igreja.descricao,
                               PERIODO = p._GEM_Periodo.descricao,
                               T = p._GEM_Turma.descricao,
                               NIPE = p._instrumentoNipe.descricao,
                               REGIONAL = gl.FirstWords(p._user2.nome,2),
                               LOCAL = gl.FirstWords(p._user1.nome, 2),
                               INSTRUTOR = gl.FirstWords(p._user.nome, 2),
                               STATUS = p._GEM_Status.descricao,
                               //MATRICULAS = (from g in bd.db._GEM_Matriculas where g.gemID == p.Id select g).ToList().Count()
                           }).ToList();
                gvGEM.DataSource = GEM;
                gvGEM.DataBind();

                lblQuantidadeGems.Text = "Foram encontradas " + GEM.Count().ToString() + " GEMs na pesquisa.";
            }
            // mostra todas as GEMs inclusive inativas apenas para desenvolvedores
            else if (usuarioLogado.tipoID == 8)
            {
                var GEM = (from p in bd.db._GEMs
                           where p._igreja.descricao.Contains(txtGemSearch.Text)
                           //&& p.status == 1 || p.status == 2 //ativas ou cadastradas
                           select new
                           {
                               ID = p.Id,
                               CIDADE = p._municipio.descricao,
                               CONGREGACAO = p._igreja.descricao,
                               PERIODO = p._GEM_Periodo.descricao,
                               T = p._GEM_Turma.descricao,
                               NIPE = p._instrumentoNipe.descricao,
                               REGIONAL = gl.FirstWords(p._user2.nome, 2),
                               LOCAL = gl.FirstWords(p._user1.nome, 2),
                               INSTRUTOR = gl.FirstWords(p._user.nome, 2),
                               STATUS = p._GEM_Status.descricao,
                               //MATRICULAS = (from g in bd.db._GEM_Matriculas where g.gemID == p.Id select g).ToList().Count()
                           }).ToList();
                gvGEM.DataSource = GEM;
                gvGEM.DataBind();

                lblQuantidadeGems.Text = "Foram encontradas " + GEM.Count().ToString() + " GEMs na pesquisa.";
            }
            //apenas GEMs ativas ou cadastradas e que ele participa, para os demais
            else
            {
                var GEM = (from p in bd.db._GEMs
                           where p._igreja.descricao.Contains(txtGemSearch.Text)
                           && p.localID == usuarioLogado.id
                           || p.instrutorID == usuarioLogado.id
                           || p.regionalID == usuarioLogado.id
                           //&& p.status == 1 || p.status == 2 
                           select new
                           {
                               ID = p.Id,
                               CIDADE = p._municipio.descricao,
                               CONGREGACAO = p._igreja.descricao,
                               PERIODO = p._GEM_Periodo.descricao,
                               T = p._GEM_Turma.descricao,
                               NIPE = p._instrumentoNipe.descricao,
                               REGIONAL = p._user2.nome,
                               LOCAL = p._user1.nome,
                               INSTRUTOR = p._user.nome,
                               STATUS = p._GEM_Status.descricao,
                               //MATRICULAS = 0
                           }).ToList();
                gvGEM.DataSource = GEM;
                gvGEM.DataBind();

                lblQuantidadeGems.Text = "Foram encontradas " + GEM.Count().ToString() + " GEMs na pesquisa.";
            }
        }
        catch { }
    }
    protected void gvGEM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Muda a cor do row conforme o status
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[11].Text.Contains("Ativa"))
                {
                    //e.Row.Cells[9].Font.Bold = true;
                    e.Row.BackColor = Color.GreenYellow;
                }
                else if (e.Row.Cells[11].Text.Contains("Cadastrada"))
                {
                    //e.Row.Cells[9].Font.Bold = true;
                    e.Row.BackColor = Color.LightGray;
                }
                else if (e.Row.Cells[11].Text.Contains("Inativa"))
                {
                    //e.Row.Cells[9].Font.Bold = true;
                    e.Row.BackColor = Color.Yellow;
                }
            }
        }
    }
}
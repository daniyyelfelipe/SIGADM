using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_GEMSystem : System.Web.UI.Page
{
    BD bd = new BD();
    Guard guard = new Guard();
    Log log = new Log();
    _user usuario = new _user();
    Email email = new Email();
    Global gl = new Global();
    protected void Page_Load(object sender, EventArgs e)
    {
        //controle de login
        if (Session["usuarioLogado"] != null)
        {
            usuario = (_user)Session["usuarioLogado"];
        }
        else
        {
            Response.Redirect("~/Logoff.aspx");
        }
    }
    protected void btnPesquisa_Click(object sender, EventArgs e)
    {
        try
        {
            var gem = (from p in bd.db._GEMs
                            select new
                            {
                                ID = p.Id,
                                CIDADE = p._municipio.descricao,
                                COMUM = p._igreja.descricao,
                                PERIODO = p._GEM_Periodo.descricao,
                                TURMA = p._GEM_Turma.descricao,
                                NIPE = p._instrumentoNipe.descricao,
                                REGIONAL = gl.FirstWords(p._user2.nome, 5),
                                LOCAL = gl.FirstWords(p._user1.nome, 5),
                                INSTRUTOR = gl.FirstWords(p._user.nome, 5),
                                STATUS = p._GEM_Status.descricao

                            }).OrderBy(x => x.ID).ToList();


            var filter = gem;

            if (!string.IsNullOrEmpty(txtIdGem.Text))
            {
                filter = filter.Where(x => x.ID.ToString().Contains(txtIdGem.Text.Trim().ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(txtCongregacao.Text))
            {
                filter = filter.Where(x => x.CIDADE.Contains(txtCongregacao.Text.Trim().ToUpper())).ToList();
            }

            gvGEM.DataSource = filter;
            gvGEM.DataBind();

            fdsEditarGem.Visible = false;
        }
        catch
        {

        }
    }
    protected void gvGEM_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "editar")
            {
                //String s = "<script language='javascript'> javascript:abrirMatricula('Matricula.aspx?idg=" + e.CommandArgument.ToString() + "'); </script>";
                //Page.RegisterClientScriptBlock("Print", s);

                var gem = (from p in bd.db._GEMs
                               where p.Id ==  int.Parse(e.CommandArgument.ToString())
                               select p).Single();

                lblId.Text = gem.Id.ToString();


                var regionais = (from p in bd.db._users
                                 where p.statusID == 1
                                 && p.tipoID == 1 || p.tipoID == 4 //regional e examinadora
                                 orderby p.nome
                                 select p.nome).ToList();

                var locais = (from p in bd.db._users
                                 where p.statusID == 1
                                 && p.tipoID == 2 || p.tipoID == 12 //local e instrutora
                                 orderby p.nome
                                 select p.nome).ToList();

                var instrutor = (from p in bd.db._users
                                 where p.statusID == 1
                                 && p.tipoID == 3 || p.tipoID == 12 //instrutor e instrutora
                                 orderby p.nome
                                 select p.nome).ToList();

                ddlRegional.DataSource = regionais;
                ddlRegional.DataBind();
                ddlRegional.SelectedValue = gem._user2.nome;

                ddlLocal.DataSource = locais;
                ddlLocal.DataBind();
                ddlLocal.SelectedValue = gem._user1.nome;

                ddlInstrutor.DataSource = instrutor;
                ddlInstrutor.DataBind();
                ddlInstrutor.SelectedValue = gem._user.nome;

                fdsEditarGem.Visible = true;


            }
            if (e.CommandName == "excluir")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Função em desenvolvimento! ');", true);
            }
        }
        catch
        {

        }
    }
    protected void brnEditar_Click(object sender, EventArgs e)
    {
        try
        {
            var gem = (from p in bd.db._GEMs
                       where p.Id == int.Parse(lblId.Text.Trim())
                       select p).Single();

            var regional = (from p in bd.db._users
                            where p.nome == ddlRegional.SelectedValue
                            select p.id).Single();

            var local = (from p in bd.db._users
                            where p.nome == ddlLocal.SelectedValue
                            select p.id).Single();

            var instrutor = (from p in bd.db._users
                            where p.nome == ddlInstrutor.SelectedValue
                            select p.id).Single();


            gem.regionalID = regional;
            gem.localID = local;
            gem.instrutorID = instrutor;

            bd.db.SubmitChanges();

            log.AdicionarEntrada(50, usuario.id, 6, "", 1, 0);

            fdsEditarGem.Visible = false;

            btnPesquisa_Click(sender, e);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Dados da GEM alterados com sucesso!');", true);
        }
        catch
        {

        }
    }
}
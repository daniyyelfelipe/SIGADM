using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Exames_Default : System.Web.UI.Page
{
    BD bd = new BD();
    _user usuarioLogado;
    Log log = new Log();

    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogado = (_user)Session["usuarioLogado"];

        if (!IsPostBack)
        {
            CarregaForm();
        }
    }
    private void CarregaForm()
    {
        try
        {
            var exames = (from e in bd.db._exames
                          join u in bd.db._users
                          on e.usuarioID equals u.id
                          where e.statusID == 1
                          select new
                          {
                              ID = e.id,
                              TIPO = e._exame_tipo.descricao,
                              DATA = String.Format("{0:dd/MM/yyyy}", e.dataAbertura.Value.Date),
                              HORA = e.horaAbertura.Value,
                              ALUNOS = (from p in bd.db._exame_lancamentos where p.exameID == e.id select p.id).Count()
                          }).ToList();

            gvExames.DataSource = exames;
            gvExames.DataBind();
        }
        catch { }
    }
    protected void btnNewExame_Click(object sender, EventArgs e)
    {
        String s = "<script language='javascript'> javascript:abrirPopup('NovoExame.aspx',600,300); </script>";
        Page.RegisterClientScriptBlock("Print", s);
    }
    protected void gvExames_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "excluir")
            {
                var verifica = (from p in bd.db._exame_lancamentos
                                where p.exameID == int.Parse(e.CommandArgument.ToString())
                                select p).ToList();

                if (verifica.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Exame com alunos incluídos não pode ser deletado!!');", true);
                }
                else
                {
                    var exame = (from p in bd.db._exames
                                 where p.id == int.Parse(e.CommandArgument.ToString())
                                 select p).Single();

                    bd.db._exames.DeleteOnSubmit(exame);
                    bd.db.SubmitChanges();

                    log.AdicionarEntrada(46, usuarioLogado.id, 6, "", 1, 0);

                    CarregaForm();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Exame deletado com sucesso!!');", true);

                }
            }
            else if (e.CommandName == "incluir")
            {
                String s = "<script language='javascript'> javascript:abrirPopup('IncluirAluno.aspx?exameid="+e.CommandArgument.ToString()+"',1000,600); </script>";
                Page.RegisterClientScriptBlock("Print", s);
            }
            else if (e.CommandName == "resultados")
            {

            }
            else if (e.CommandName == "relatorio")
            {

            }
        }
        catch(Exception er4)
        {
            ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('"+er4.Message+"');", true);        
        }
    }
}
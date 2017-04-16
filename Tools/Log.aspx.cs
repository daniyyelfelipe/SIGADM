using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Log : System.Web.UI.Page
{
    BD bd = new BD();
    Log log = new Log();
    _user usuarioLogado;

    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogado = (_user)Session["usuarioLogado"];

        CarregaDados();
    }

    private void CarregaDados()
    {
        try
        {
            var log = (from p in bd.db._Logs
                       where p.usuarioID != 3
                       orderby p.dataHora descending
                       select new
                       {
                           ACAO = p._LogAcao.descricao,
                           DATETIME = p.dataHora,
                           USUARIO = p._user.nome,
                           VISIBILIDADE = p._LogVisibilidade.descricao,
                           MENSAGEM = p.mensagem,
                           PARA = (p.privadoUserID == 0) ? "0" : (from u in bd.db._users where u.id == p.privadoUserID select u.nome).Single(),
                           TIPO = p._LogPostTipo.descricao

                       }).Take(300).ToList();

            gvLog.DataSource = log;
            gvLog.DataBind();
        }
        catch { }

    }
}
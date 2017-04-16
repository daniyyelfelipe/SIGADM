using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class teia : System.Web.UI.Page
{
    BD bd = new BD();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregaForm();
        }
    }
    private void CarregaForm()
    {
        try
        {
            var teia = (from p in bd.db._Logs
                        where p.visibilidadeID == 1
                        select new 
                        {
                            USUARIO = p._user.nome,
                            PERFIL = "~/perfil/?user=" + p._user.id,
                            //FEZ = p._LogAcao.menssagem,
                            DATA = p.dataHora,
                            VISIB = "Visibilidade: " + p._LogVisibilidade.descricao,
                            MENSAGEM = p.mensagem,
                            QUANDO = " " + p._LogAcao.menssagem +  " no dia " + p.dataHora.Value.Day + " do " + p.dataHora.Value.Month + " de " + p.dataHora.Value.Year + " ás " + p.dataHora.Value.Hour + ":" + p.dataHora.Value.Minute + ":" + p.dataHora.Value.Second
                        }).OrderByDescending(x => x.DATA).ToList().Take(20);
            //gvTeia.DataSource = teia;            
            //gvTeia.DataBind();

            rpTeia.DataSource = teia;
            rpTeia.DataBind();
        }
        catch(Exception e3)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('"+e3.Message+"');", true);
        }
    }
    protected void gvTeia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[0].Visible = false;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Ajuda_Videos : System.Web.UI.Page
{
    BD bd = new BD();

    string idReproducao = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        idReproducao = Request.QueryString["v"];

        CarregaDefault(idReproducao);
    }
    private void CarregaDefault(string idReproducao)
    {
        try
        {
            var listagemVideos = (from p in bd.db._videoTreinamentos
                                  select new
                                  {
                                      IDREPRODU = p.idReproducao,
                                      TITULO = p.titulo,
                                      IDREPRODULINK = "Videos.aspx?v=" + p.idReproducao
                                  }).ToList();
            rpListagem.DataSource = listagemVideos;
            rpListagem.DataBind();

            if (idReproducao != null)
            {
                ltIframeTop.Text = "<iframe width='560' height='315' src='https://www.youtube.com/embed/" + idReproducao + "?rel=0' frameborder='0' allowfullscreen></iframe>";

                var videoSelecionado = (from p in bd.db._videoTreinamentos
                                        where p.idReproducao == idReproducao
                                        select p).Single();

                lblTituloVideo.Text = videoSelecionado.titulo;
            }
            else
            {
                ltIframeTop.Text = "<iframe width='560' height='315' src='https://www.youtube.com/embed/7lC3XpImaa0?rel=0' frameborder='0' allowfullscreen></iframe>";

                idReproducao = "7lC3XpImaa0";

                var videoSelecionado = (from p in bd.db._videoTreinamentos
                                        where p.idReproducao == idReproducao
                                        select p).Single();

                lblTituloVideo.Text = videoSelecionado.titulo;
            }

        }
        catch
        {

        }
    }
}
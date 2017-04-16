using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cadastro_CentralMusicos : System.Web.UI.Page
{
    BD bd = new BD();
    Global g = new Global();

    protected void Page_Load(object sender, EventArgs e)
    {
        CarregaForm();
    }

    private void CarregaForm()
    {
        try
        {
            var musicos = (from p in bd.db._users
                           where p.tipoID == 10 || p.tipoID == 11
                           && p.statusID == 1
                           orderby p.nome
                           select new
                           {
                               id = p.id,
                               imgUrl = "~/data/user/" + p.id + "/img/perfil.jpg",
                               nome = p.nome,
                               comum = (p.cidadeID != null) ? p._municipio.descricao + " - " + p._igreja.descricao : "---",
                               email = p.email,
                               urlPerfil = "~/perfil/?user=" + p.id,
                               estado = g.LastLogin(p.id)
                           }).ToList();

            rpMusicos.DataSource = musicos;
            rpMusicos.DataBind();

            lblTotalMusicos.Text = musicos.Count.ToString();
        }
        catch
        { }
    }
}
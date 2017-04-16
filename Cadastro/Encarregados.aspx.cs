using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cadastro_Encarregados : System.Web.UI.Page
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
            var encarregadosRegionais = (from p in bd.db._users                                         
                                         where p.tipoID == 1
                                         && p.statusID == 1
                                         orderby p.nome
                                         select new
                                         {
                                             id = p.id,
                                             imgUrl = "~/data/user/"+p.id+"/img/perfil.jpg",
                                             //nome = (p.nome.Length > 38) ? p.nome.Substring(0, 28) + "..." : p.nome,
                                             nome = p.nome,
                                             comum = (p.cidadeID != null) ? p._municipio.descricao + " - " + p._igreja.descricao : "---",
                                             email = p.email,
                                             urlPerfil = "~/perfil/?user="+p.id,
                                             estado = g.LastLogin(p.id)
                                         }).ToList();

            rpRegionais.DataSource = encarregadosRegionais;
            rpRegionais.DataBind();

            var encarregadosLocais = (from p in bd.db._users
                                         where p.tipoID == 2
                                         && p.statusID == 1
                                         && p.id != 7221
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

            rpLocais.DataSource = encarregadosLocais;
            rpLocais.DataBind();

            var examinadoras = (from p in bd.db._users
                                      where p.tipoID == 4
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

            rpExaminadoras.DataSource = examinadoras;
            rpExaminadoras.DataBind();

            var instrutores = (from p in bd.db._users
                                where p.tipoID == 3
                                && p.statusID == 1
                                && p.id != 7220
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

            rpInstrutores.DataSource = instrutores;
            rpInstrutores.DataBind();

            var instrutoras = (from p in bd.db._users
                               where p.tipoID == 12
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

            rpInstrutoras.DataSource = instrutoras;
            rpInstrutoras.DataBind();

            var anciaes = (from p in bd.db._users
                               where p.tipoID == 6
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

            rpAnciaes.DataSource = anciaes;
            rpAnciaes.DataBind();

            var secretarios = (from p in bd.db._users
                           where p.tipoID == 9
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

            rpSecretarios.DataSource = secretarios;
            rpSecretarios.DataBind();

            lblTotalRegionais.Text = encarregadosRegionais.Count.ToString();
            lblTotalExaminadoras.Text = examinadoras.Count.ToString();
            lblTotalLocais.Text = encarregadosLocais.Count.ToString();
            lblTotalInstrutores.Text = instrutores.Count.ToString();
            lblTotalInstrutoras.Text = instrutoras.Count.ToString();
            lblTotalAnciaes.Text = anciaes.Count.ToString();
            lblTotalSecretarios.Text = secretarios.Count.ToString();
                                
        }
        catch
        {

        }
    }
}
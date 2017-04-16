using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Perfil_PrevisualizarPostImage : System.Web.UI.Page
{
    BD bd = new BD();
    Log log = new Log();
    Guard guard = new Guard();
    _user usuarioLogado;
    string privado, visibilidade, descricao = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            usuarioLogado = (_user)Session["usuarioLogado"];


            descricao = Request.QueryString["desc"].ToString();
            lblDescricaoTemp.Text = descricao;

            imgTemp.ImageUrl = "~/data/user/"+usuarioLogado.id.ToString()+"/img/tempPostImage.jpg";

            privado = Request.QueryString["p"].ToString();
            visibilidade = Request.QueryString["v"].ToString();

        }
        catch
        {

        }
    }
    protected void btnPerfilPostImagePost_Click(object sender, EventArgs e)
    {
        try
        {            
                if (privado.Length > 0)
                {
                    var privadoUser = (from p in bd.db._users
                                   where p.nome == privado
                                   select p).ToList();

                    if (privadoUser.Count > 0)
                    {
                        log.AdicionarEntrada(29, usuarioLogado.id, (from p in bd.db._LogVisibilidades where p.descricao == visibilidade select p.id).Single(), descricao, 2, privadoUser[0].id);
                    }
                }
                else
                {
                    log.AdicionarEntrada(29, usuarioLogado.id, (from p in bd.db._LogVisibilidades where p.descricao == visibilidade select p.id).Single(), descricao, 2, 0);
                }


                var post = (from p in bd.db._Logs
                            where p.acaoID == 29
                            && p.usuarioID == usuarioLogado.id
                            select p.id).Last();

                string pathTemp = Server.MapPath("~/data/user/" + usuarioLogado.id + "/img/");
                string path = Server.MapPath("~/data/post/" + post.ToString() + "/");
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }

                File.Copy(pathTemp + "tempPostImage.jpg", path + post.ToString() + ".jpg");
                File.Delete(pathTemp + "tempPostImage.jpg");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Imagem Postada com Sucesso! Clique para fechar a Janela.');", true);
        }
        catch
        {

        }
    }
}
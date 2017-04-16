using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Perfil_Default : System.Web.UI.Page
{
    string userID;
    BD bd = new BD();
    Log log = new Log();
    App app = new App();
    _user usuarioLogado;
    Email email = new Email();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CarregaID();
            EscondeCampos();

            if (!IsPostBack)
            {
                CarregaPerfil();
            }
        }
        catch (Exception er4)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + er4.Message + "');", true);
        }
    }

    private void EscondeCampos()
    {

        if (int.Parse(userID) == usuarioLogado.id)
        {
            //MOSTRA O BTN EXCLUIR SE U USUARIO ACESSAR SEU PERFIL
            for (int i = 0; i < rpTeia.Items.Count; i++)
            {
                rpTeia.Items[i].FindControl("divBtnExcluir").Visible = true;
            }
        }
        else
        {
            // esconde elementos da pagina de perfil se o susario visualizar pagina de outros
            fildsetUpload.Visible = false;
            fdsPerfilPost.Visible = false;
            btnEditImg.Visible = false;
            FileUpload.Visible = false;
            lbEditarNome.Visible = false;
            hlEditCargo.Visible = false;
            lbEditarComum.Visible = false;
            lbEditarSenha.Visible = false;
        }

        if (usuarioLogado.tipoID == 8 || usuarioLogado.tipoID == 9)
        {
            cbAviso.Visible = true;
        }
    }

    private void CarregaID()
    {
        userID = null;
        usuarioLogado = (_user)Session["usuarioLogado"];

        if (Request.QueryString["user"] == null || Request.QueryString["user"] == string.Empty)
        {
            userID = usuarioLogado.id.ToString();
        }
        else
        {
            userID = Request.QueryString["user"];
        }
    }

    private void CarregaPerfil()
    {
        try
        {
            CarregaID();
            EscondeCampos();

            var usuario = (from p in bd.db._users
                           where p.id == int.Parse(userID)
                           select p).Single();

            lblNome.Text = usuario.nome;
            lblCargo.Text = usuario._userTipo.descricao;
            if (usuario.cidadeID != null)
            {
                lblComum.Text = usuario._municipio.descricao + " - " + usuario._igreja.descricao;
            }
            else
            {
                lblComum.Text = "a definir...";
            }
            lblEmail.Text = usuario.email;
            imgPerfil.ImageUrl = "~/data/user/" + userID + "/img/perfil.jpg";

            try
            {
                if (usuarioLogado.id == int.Parse(userID))
                {

                    var teia = (from p in bd.db._Logs
                                where 
                                (p.postTipoID == 2) //post
                                && (p._LogAcao.id == 13 || p._LogAcao.id == 29) //post de mensagem ou post de imagem
                                && (p.usuarioID == int.Parse(userID)) //usuário está no seu perfil
                                ||
                                (p.postTipoID == 2) //post
                                && (p._LogAcao.id == 13 || p._LogAcao.id == 29) //post de mensagem ou post de imagem
                                && (p._LogVisibilidade.userTipoID.ToString().Contains(usuarioLogado.tipoID.ToString()) == true)//tipo do usuário esteja na visibilidade
                                ||
                                (p.postTipoID == 2) //post
                                && (p._LogAcao.id == 13 || p._LogAcao.id == 29) //post de mensagem ou post de imagem
                                && (p._LogVisibilidade.userTipoID.ToString().Contains("p") == true)//post privado
                                && (p.privadoUserID == int.Parse(userID))//poste para esse usuário
                                select new
                                {
                                    ID = p.id,
                                    USUARIO = p._user.nome,
                                    PERFIL = "~/perfil/?user=" + p._user.id,
                                    DATA = p.dataHora,
                                    VISIB = "Visibilidade: " + p._LogVisibilidade.descricao,
                                    CURTIR = ((from c in bd.db._LogCurtirs
                                               where c.logID == p.id
                                               select p).ToList().Count() != 0) ? " | " + (from c in bd.db._LogCurtirs
                                                                                           where c.logID == p.id
                                                                                           select p).ToList().Count() + " curtiram" : "",
                                    CURTIU = string.Join(" - ", (from c in bd.db._LogCurtirs where c.logID == p.id select c._user.nome).ToList()),
                                    MENSAGEM = (p.acaoID == 13 || p.acaoID == 29) ? p.mensagem 
                                    : (p.acaoID == 17) ? (from c in bd.db._LogCurtirs where c.logID == p.postReferenteID select c._Log.mensagem).Single() 
                                    : "Mensagem Bloqueada",
                                    ACAO = (p.acaoID == 13 && p.privadoUserID != 0) ? " " + p._LogAcao.menssagem + " para " + bd.db._users.Where(x => x.id == p.privadoUserID).Single().nome + " no dia "
                                    + p.dataHora.Value.Day + " do " + p.dataHora.Value.Month
                                    + " de " + p.dataHora.Value.Year + " ás "
                                    + p.dataHora.Value.Hour + ":"
                                    + p.dataHora.Value.Minute + ":"
                                    + p.dataHora.Value.Second :
                                    (p.acaoID == 13) ? " " + p._LogAcao.menssagem + " no dia "
                                    + p.dataHora.Value.Day + " do " + p.dataHora.Value.Month
                                    + " de " + p.dataHora.Value.Year + " ás "
                                    + p.dataHora.Value.Hour + ":"
                                    + p.dataHora.Value.Minute + ":"
                                    + p.dataHora.Value.Second :
                                    (p.acaoID == 17) ? " " + p._LogAcao.menssagem + " de " +
                                    (from c in bd.db._LogCurtirs where c.logID == p.postReferenteID select c._Log._user.nome).Single()
                                    + " em " + p.dataHora.Value.Day + " do " + p.dataHora.Value.Month
                                    + " de " + p.dataHora.Value.Year + " ás "
                                    + p.dataHora.Value.Hour + ":"
                                    + p.dataHora.Value.Minute + ":"
                                    + p.dataHora.Value.Second :
                                    (p.acaoID == 29) ? " " + p._LogAcao.menssagem + " no dia "
                                    + p.dataHora.Value.Day + " do " + p.dataHora.Value.Month
                                    + " de " + p.dataHora.Value.Year + " ás "
                                    + p.dataHora.Value.Hour + ":"
                                    + p.dataHora.Value.Minute + ":"
                                    + p.dataHora.Value.Second
                                    : "Post Bloqueado!",
                                    VLIKE = (p.usuarioID == usuarioLogado.id) ? false 
                                    : (p.acaoID == 13) ? true
                                    : (p.acaoID == 29) ? true 
                                    : (p.acaoID == 17) ? false 
                                    : false,
                                    VCOMENT = (p.usuarioID.Value == int.Parse(userID)) ? false : true,
                                    VDEL = (p.usuarioID.Value == int.Parse(userID)) ? true : false,
                                    imgPostVisible = (p.acaoID == 29) ? true : false,
                                    imgPostUrl = "~/data/post/" + p.id.ToString() + "/" + p.id.ToString() + ".jpg",
                                    imgPostHref = "../data/post/" + p.id.ToString() + "/" + p.id.ToString() + ".jpg"
                                }).OrderByDescending(x => x.DATA).ToList().Take(30);

                    rpTeia.DataSource = teia;
                    rpTeia.DataBind();

                    if (teia.Count() < 1)
                    {
                        lblNoPosts.Text = "nada postado nessa TEIA...";
                    }
                    else
                    {
                        lblNoPosts.Text = string.Empty;
                    }

                    var visibilidade = (from p in bd.db._LogVisibilidades
                                        select p.descricao).ToList();
                    ddlVisibilidade.DataSource = visibilidade;
                    ddlVisibilidade.DataBind();


                    txtPost.MaxLength = 300;

                    if (!IsPostBack)
                    {
                        txtPost.Text = string.Empty;
                    }

                    EscondeCampos();
                }
                else
                {
                    var teia = (from p in bd.db._Logs
                                where
                                (p.usuarioID == int.Parse(userID))
                                && p.postTipoID == 2 //postar
                                && (p._LogAcao.id == 13 || p._LogAcao.id == 29)//mensagem ou imagem
                                && (p._LogVisibilidade.userTipoID.ToString().Contains(usuarioLogado.tipoID.ToString()) == true) //visibilidade contenha o tipo do usuário
                                ||
                                (p.postTipoID == 2) //post
                                && (p._LogAcao.id == 13 || p._LogAcao.id == 29) //post de mensagem ou post de imagem
                                && (p._LogVisibilidade.userTipoID.ToString().Contains("p") == true)//post privado
                                && (p.privadoUserID == int.Parse(userID))//poste para esse usuário
                                && (p.usuarioID == usuarioLogado.id)//postado por quem esta vendo                                
                                
                                select new
                                {
                                    ID = p.id,
                                    USUARIO = p._user.nome,
                                    PERFIL = "~/perfil/?user=" + p._user.id,
                                    DATA = p.dataHora,
                                    VISIB = "Visibilidade: " + p._LogVisibilidade.descricao,
                                    CURTIR = ((from c in bd.db._LogCurtirs
                                               where c.logID == p.id
                                               select p).ToList().Count() != 0) ? " | " + (from c in bd.db._LogCurtirs
                                                                                           where c.logID == p.id
                                                                                           select p).ToList().Count() + " curtiram" : "",
                                    CURTIU = string.Join(" - ", (from c in bd.db._LogCurtirs where c.logID == p.id select c._user.nome).ToList()),
                                    MENSAGEM = (p.acaoID == 13 || p.acaoID == 29) ? p.mensagem : 
                                    (p.acaoID == 17) ? (from c in bd.db._LogCurtirs where c.logID == p.postReferenteID select c._Log.mensagem).Single() 
                                    : "Mensagem Bloqueada",
                                    ACAO = (p.acaoID == 13 && p.privadoUserID != 0) ? " " + p._LogAcao.menssagem + " para " + bd.db._users.Where(x => x.id == p.privadoUserID).Single().nome + " no dia "
                                    + p.dataHora.Value.Day + " do " + p.dataHora.Value.Month
                                    + " de " + p.dataHora.Value.Year + " ás "
                                    + p.dataHora.Value.Hour + ":"
                                    + p.dataHora.Value.Minute + ":"
                                    + p.dataHora.Value.Second :
                                    (p.acaoID == 13) ? " " + p._LogAcao.menssagem + " no dia "
                                    + p.dataHora.Value.Day + " do " + p.dataHora.Value.Month
                                    + " de " + p.dataHora.Value.Year + " ás "
                                    + p.dataHora.Value.Hour + ":"
                                    + p.dataHora.Value.Minute + ":"
                                    + p.dataHora.Value.Second :
                                    (p.acaoID == 17) ? " " + p._LogAcao.menssagem + " de " +
                                    (from c in bd.db._LogCurtirs where c.logID == p.postReferenteID select c._Log._user.nome).Single()
                                    + " em " + p.dataHora.Value.Day + " do " + p.dataHora.Value.Month
                                    + " de " + p.dataHora.Value.Year + " ás "
                                    + p.dataHora.Value.Hour + ":"
                                    + p.dataHora.Value.Minute + ":"
                                    + p.dataHora.Value.Second :
                                    (p.acaoID == 29) ? " " + p._LogAcao.menssagem + " no dia "
                                    + p.dataHora.Value.Day + " do " + p.dataHora.Value.Month
                                    + " de " + p.dataHora.Value.Year + " ás "
                                    + p.dataHora.Value.Hour + ":"
                                    + p.dataHora.Value.Minute + ":"
                                    + p.dataHora.Value.Second
                                    : "Post Bloqueado!",
                                    VLIKE = (p.usuarioID == usuarioLogado.id) ? false 
                                    : (p.acaoID == 13) ? true
                                    : (p.acaoID == 29) ? true
                                    : (p.acaoID == 17) ? false 
                                    : false,
                                    VCOMENT = false,
                                    VDEL = false,
                                    imgPostVisible = (p.acaoID == 29) ? true : false,
                                    imgPostUrl = "~/data/post/" + p.id.ToString() + "/" + p.id.ToString() + ".jpg",
                                    imgPostHref = "../data/post/" + p.id.ToString() + "/" + p.id.ToString() + ".jpg"
                                }).OrderByDescending(x => x.DATA).ToList().Take(30);

                    rpTeia.DataSource = teia;
                    rpTeia.DataBind();

                    if (teia.Count() < 1)
                    {
                        lblNoPosts.Text = "nada postado nessa TEIA...";
                    }
                    else
                    {
                        lblNoPosts.Text = string.Empty;
                    }

                    var visibilidade = (from p in bd.db._LogVisibilidades
                                        select p.descricao).ToList();
                    ddlVisibilidade.DataSource = visibilidade;
                    ddlVisibilidade.DataBind();


                    txtPost.MaxLength = 300;

                    if (!IsPostBack)
                    {
                        txtPost.Text = string.Empty;
                    }

                    EscondeCampos();
                }

            }
            catch (Exception e3)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + e3.Message + "');", true);
            }

            var ultimoLogin = (from p in bd.db._Logs
                               where p.acaoID == 1 && p.usuarioID == int.Parse(userID)
                               select p.dataHora).ToList();
            if (ultimoLogin.Count > 0)
            {
                if (ultimoLogin[ultimoLogin.Count - 1].Value.Date == DateTime.Now.Date)
                {
                    lblUltimaVisita.Text = "Visto por ultimo hoje às " + String.Format("{0:HH:mm:ss}", ultimoLogin[ultimoLogin.Count - 1].Value);
                }
                else if (ultimoLogin[ultimoLogin.Count - 1].Value.Date == DateTime.Now.Date.AddDays(-1))
                {
                    lblUltimaVisita.Text = "Visto por ultimo ontem às " + String.Format("{0:HH:mm:ss}", ultimoLogin[ultimoLogin.Count - 1].Value);
                }
                else
                {
                    lblUltimaVisita.Text = "Visto por ultimo em " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", ultimoLogin[ultimoLogin.Count - 1].Value);
                }
            }
            else
            {
                lblUltimaVisita.Text = "Nunca entrou no sistema :(";
            }
        }
        catch
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + e4.Message + "');", true);
        }
    }

    protected void btnpostar_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPost.Text != string.Empty)
            {
                string mystring = txtPost.Text.Trim();
                Regex urlRx = new Regex(@"(?<url>(http:[/][/]|https:[/][/]|www.)([a-z]|[A-Z]|[0-9]|[/.]|[~])*)", RegexOptions.IgnoreCase);
                MatchCollection matches = urlRx.Matches(mystring);
                foreach (Match match in matches)
                {
                    var url = match.Groups["url"].Value;
                    mystring = mystring.Replace(url, string.Format("<a href=\"{0}\" Target=\"_blank\">{0}</a>", url));
                }

                if (txtPrivado.Text.Length > 0)
                {
                    var privado = (from p in bd.db._users
                                   where p.nome == txtPrivado.Text
                                   select p).ToList();

                    if (privado.Count > 0)
                    {
                        log.AdicionarEntrada(13, usuarioLogado.id, (from p in bd.db._LogVisibilidades where p.descricao == ddlVisibilidade.SelectedValue select p.id).Single(), mystring, 2, privado[0].id);

                        email.SendEmailPrivateMensage(usuarioLogado.nome, privado[0].email);

                        txtPost.Text = string.Empty;
                        CarregaPerfil();

                        txtPrivado.Text = string.Empty;
                        dvPrivado.Visible = false;

                        fdsPerfilPostTexto.Visible = false;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('O usuário que você digitou não existe! Tente usar a autocomplementação para o nome do usuário!');", true);
                    }
                }
                else
                {

                    log.AdicionarEntrada(13, usuarioLogado.id, (from p in bd.db._LogVisibilidades where p.descricao == ddlVisibilidade.SelectedValue select p.id).Single(), mystring, 2, 0);

                    if (cbAviso.Checked)
                    {
                        email.SendEmailAvisoAll(usuarioLogado.nome);
                    }

                    txtPost.Text = string.Empty;
                    CarregaPerfil();

                    fdsPerfilPostTexto.Visible = false;

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Post na TEIA realizado com Sucesso!');", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Você deve digitar uma mensagem para postar!');", true);
            }
        }
        catch
        {

        }
    }

    protected void imgbExcluir_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string id = e.CommandArgument.ToString();

            //deletas as curtidas no post
            var curtir = bd.db._LogCurtirs.Where(x => x.logID == int.Parse(id)).ToList();

            for (int i = 0; i < curtir.Count; i++)
            {
                bd.db._LogCurtirs.DeleteOnSubmit(curtir[i]);
            }

            //deleta o post
            var post = (from p in bd.db._Logs
                        where p.id == int.Parse(id)
                        select p).Single();

            try//deleta a imagem do post
            {
                if (post.acaoID == 29)
                {
                    string path = Server.MapPath("~/data/post/" + post.id.ToString() + "/" + post.id.ToString() + ".jpg");
                    File.Delete(path);
                }
            }
            catch
            {

            }

            bd.db._Logs.DeleteOnSubmit(post);
            bd.db.SubmitChanges();

            

            log.AdicionarEntrada(15, usuarioLogado.id, 6, "", 1, 0);

            CarregaPerfil();

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Post deletado com Sucesso!');", true);
        }
        catch
        {

        }
    }

    protected void btnEditImg_Click(object sender, EventArgs e)
    {
        try
        {
            string path = Server.MapPath("~/data/user/" + usuarioLogado.id + "/img/");
            string defaultPath = Server.MapPath("~/data/user/default/img/");
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                if (FileUpload.FileName.Length > 1)
                {
                    FileUpload.SaveAs(path + "perfil1.jpg");

                    //carregando a imagem de exemplo
                    using (Bitmap img = new Bitmap(path + "perfil1.jpg"))
                    {
                        //CodecInfo para imagens Jpeg
                        ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(enc => enc.FormatID == ImageFormat.Jpeg.Guid);
                        //EncoderParameters que vai setar o nível de qualidade (compressão)
                        EncoderParameters imgParams = new EncoderParameters(1);
                        //Qualidade em 0L = máximo de compressão
                        imgParams.Param = new[] { new EncoderParameter(Encoder.Quality, 50L) };
                        //Salvar a imagem a imagem
                        img.Save(path + "perfil.jpg", codec, imgParams);
                    }

                    File.Delete(path + "perfil1.jpg");
                }
                else
                {
                    File.Copy(defaultPath + "perfil.jpg", path + "perfil.jpg");
                }
            }
            else
            {
                dir.Create();
                //FileUpload.SaveAs(path + FileUpload.FileName);
                if (FileUpload.FileName.Length > 1)
                {
                    FileUpload.SaveAs(path + "perfil1.jpg");

                    //carregando a imagem de exemplo
                    using (Bitmap img = new Bitmap(path + "perfil1.jpg"))
                    {
                        //CodecInfo para imagens Jpeg
                        ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(enc => enc.FormatID == ImageFormat.Jpeg.Guid);
                        //EncoderParameters que vai setar o nível de qualidade (compressão)
                        EncoderParameters imgParams = new EncoderParameters(1);
                        //Qualidade em 0L = máximo de compressão
                        imgParams.Param = new[] { new EncoderParameter(Encoder.Quality, 50L) };
                        //Salvar a imagem a imagem
                        img.Save(path + "perfil.jpg", codec, imgParams);
                    }

                    File.Delete(path + "perfil1.jpg");
                }
                else
                {
                    File.Copy(defaultPath + "perfil.jpg", path + "perfil.jpg");
                }
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Imagem do perfil alterada com sucesso!!');", true);

            log.AdicionarEntrada(16, usuarioLogado.id, 6, "", 1, 0);

            CarregaPerfil();

        }
        catch(Exception e2)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + e2.Message + "');", true);
        }
    }

    protected void imgbCurtir_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string id = e.CommandArgument.ToString();

            var curtidas = (from p in bd.db._LogCurtirs
                            where p.logID == int.Parse(id) && p.userID == usuarioLogado.id
                            select p).ToList();
            if (curtidas.Count < 1)
            {

                _LogCurtir curtir = new _LogCurtir();
                curtir.dataHora = app.DateTimeCorrigido();
                curtir.logID = int.Parse(id);
                curtir.userID = usuarioLogado.id;

                bd.db._LogCurtirs.InsertOnSubmit(curtir);
                bd.db.SubmitChanges();

                log.AdicionarEntrada(17, usuarioLogado.id, 1, "", 2, 0);

                CarregaPerfil();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Você já curtiu esse post!');", true);
            }
        }
        catch
        {

        }
    }
    protected void lbEditarSenha_Click(object sender, EventArgs e)
    {
        divEditarSenha.Visible = true;
        lblUltimaVisita.Visible = false;
    }
    protected void btnSalvarNovaSenha_Click(object sender, EventArgs e)
    {
        try
        {
            Guard guard = new Guard();
            string senha = guard.EncriptaSenha(txtNovaSenha.Text.Trim());

            var usu = (from p in bd.db._users
                       where p.id == usuarioLogado.id
                       select p).Single();

            usu.senha = senha;
            bd.db.SubmitChanges();

            txtNovaSenha.Text = string.Empty;
            divEditarSenha.Visible = false;

            log.AdicionarEntrada(21, usuarioLogado.id, 6, "", 1, 0);

            lblUltimaVisita.Visible = true;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Senha alterada com Sucesso!');", true);
        }
        catch
        {

        }
    }


    protected void lbEditarComum_Click(object sender, EventArgs e)
    {
        try
        {
            var comum = (from p in bd.db._igrejas
                         orderby p._municipio.descricao
                         select p._municipio.descricao + " - " + p.descricao).ToList();
            ddlComumEdit.DataSource = comum;
            ddlComumEdit.DataBind();

            divEditarComum.Visible = true;

            fdsPerfilDados.Style.Add("height", "40%");
            fdsPerfilPost.Visible = false;
            fdsPerfilTeia.Visible = false;
        }
        catch
        {

        }
    }
    protected void btnSalvarComum_Click(object sender, EventArgs e)
    {
        try
        {
            string[] comum = ddlComumEdit.SelectedValue.Split('-');

            var user = (from p in bd.db._users
                        where p.id == usuarioLogado.id
                        select p).Single();

            user.igrejaID = (from p in bd.db._igrejas where p.descricao == comum[1].Trim() && p._municipio.descricao == comum[0].Trim() select p.id).Single();
            user.cidadeID = (from p in bd.db._municipios where p.descricao == comum[0].Trim() select p.id).Single();

            bd.db.SubmitChanges();

            divEditarComum.Visible = false;

            fdsPerfilDados.Style.Add("height", "");
            fdsPerfilPost.Visible = true;
            fdsPerfilTeia.Visible = true;

            CarregaPerfil();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Dados atualizados com Sucesso!!');", true);
        }
        catch
        {

        }
    }
    protected void ddlVisibilidade_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlVisibilidade.SelectedValue == "Privado")
            {
                txtPrivado.Focus();
                dvPrivado.Visible = true;
            }
            else
            {
                dvPrivado.Visible = false;
            }
        }
        catch
        {

        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> AutoCompletePrivate(string prefixText, int count)
    {
        try
        {
            BD bd = new BD();
            var users = (from p in bd.db._users
                         where p.nome.Contains(prefixText)
                         select p.nome).ToList();
            return users;
        }
        catch
        {
            return null;
        }

    }
    protected void lbEditarNome_Click(object sender, EventArgs e)
    {
        lblNome.Visible = false;
        lbEditarNome.Visible = false;

        txtNomeEdit.Text = lblNome.Text;
        divEditarNome.Visible = true;
        txtNomeEdit.Focus();
    }
    protected void btnSalvarNome_Click(object sender, EventArgs e)
    {
        try
        {
            var usuarioNomeEdit = (from p in bd.db._users
                                   where p.id == usuarioLogado.id
                                   select p).Single();
            usuarioNomeEdit.nome = txtNomeEdit.Text.Trim().ToUpper();
            bd.db.SubmitChanges();

            CarregaPerfil();

            divEditarNome.Visible = false;
            lblNome.Visible = true;
            lbEditarNome.Visible = true;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Nome atualizado com Sucesso!!');", true);
        }
        catch
        {

        }
    }
    protected void imbPostText_Click(object sender, ImageClickEventArgs e)
    {
        //fdsPerfilPost.Visible = false;
        fdsPerfilPostTexto.Visible = true;
        fdsPerfilPostImage.Visible = false;
        fdsPerfilPostVideo.Visible = false;
    }
    protected void imbPostImage_Click(object sender, ImageClickEventArgs e)
    {
        fdsPerfilPostTexto.Visible = false;
        fdsPerfilPostImage.Visible = true;
        fdsPerfilPostVideo.Visible = false;
    }
    protected void imbPostVideo_Click(object sender, ImageClickEventArgs e)
    {
        //fdsPerfilPostTexto.Visible = false;
        //fdsPerfilPostImage.Visible = false;
        //fdsPerfilPostVideo.Visible = true;

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Funcionalidade ainda não liberada pelo desenvolvedor!');", true);
    }
    protected void BtnPerfilPostImageVisualizar_Click(object sender, EventArgs e)
    {
        try
        {
            string path = Server.MapPath("~/data/user/" + usuarioLogado.id + "/img/");
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                if (fuPerfilPostImage.FileName.Length > 1)
                {
                    fuPerfilPostImage.SaveAs(path + "tempPostImage1.jpg");

                    //carregando a imagem de exemplo
                    using (Bitmap img = new Bitmap(path + "tempPostImage1.jpg"))
                    {
                        //CodecInfo para imagens Jpeg
                        ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(enc => enc.FormatID == ImageFormat.Jpeg.Guid);
                        //EncoderParameters que vai setar o nível de qualidade (compressão)
                        EncoderParameters imgParams = new EncoderParameters(1);
                        //Qualidade em 0L = máximo de compressão
                        imgParams.Param = new[] { new EncoderParameter(Encoder.Quality, 50L) };
                        //Salvar a imagem a imagem
                        img.Save(path + "tempPostImage.jpg", codec, imgParams);
                    }

                    File.Delete(path + "tempPostImage1.jpg");

                    //String s = "<script language='javascript'> javascript:abrirPrevisualizarPostImage('PrevisualizarPostImage.aspx?desc=" + txtPerfilPostImageDescricao.Text.Trim() + "'); </script>";
                    //Page.RegisterClientScriptBlock("Print", s);

                    lblDescricaoTemp.Text = txtPerfilPostImageDescricao.Text.Trim();
                    imgTemp.ImageUrl = "~/data/user/" + usuarioLogado.id.ToString() + "/img/tempPostImage.jpg";
                    fdsPerfilPostImagePreview.Visible = true;
                    BtnPerfilPostImagePost.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Você deve selecionar uma Imagem!');", true);
                }
            }
            else
            {
                dir.Create();
                //FileUpload.SaveAs(path + FileUpload.FileName);
                if (fuPerfilPostImage.FileName.Length > 1)
                {
                    fuPerfilPostImage.SaveAs(path + "tempPostImage1.jpg");

                    //carregando a imagem de exemplo
                    using (Bitmap img = new Bitmap(path + "tempPostImage1.jpg"))
                    {
                        //CodecInfo para imagens Jpeg
                        ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(enc => enc.FormatID == ImageFormat.Jpeg.Guid);
                        //EncoderParameters que vai setar o nível de qualidade (compressão)
                        EncoderParameters imgParams = new EncoderParameters(1);
                        //Qualidade em 0L = máximo de compressão
                        imgParams.Param = new[] { new EncoderParameter(Encoder.Quality, 50L) };
                        //Salvar a imagem a imagem
                        img.Save(path + "tempPostImage.jpg", codec, imgParams);
                    }

                    File.Delete(path + "tempPostImage1.jpg");


                    //String s = "<script language='javascript'> javascript:abrirPrevisualizarPostImage('PrevisualizarPostImage.aspx?desc=" + txtPerfilPostImageDescricao.Text.Trim() + "'); </script>";
                    //Page.RegisterClientScriptBlock("Print", s);

                    lblDescricaoTemp.Text = txtPerfilPostImageDescricao.Text.Trim();
                    imgTemp.ImageUrl = "~/data/user/" + usuarioLogado.id.ToString() + "/img/tempPostImage.jpg";
                    fdsPerfilPostImagePreview.Visible = true;
                    BtnPerfilPostImagePost.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Você deve selecionar uma Imagem!');", true);
                }
            }            
        }
        catch(Exception e21)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('"+e21.Message+"');", true);
        }
    }
    protected void BtnPerfilPostImagePost_Click(object sender, EventArgs e)
    {
        try
        {
                if (txtPrivado.Text.Length > 0)
                {
                    var privado = (from p in bd.db._users
                                   where p.nome == txtPrivado.Text
                                   select p).ToList();

                    if (privado.Count > 0)
                    {
                        log.AdicionarEntrada(29, usuarioLogado.id, (from p in bd.db._LogVisibilidades where p.descricao == ddlVisibilidade.SelectedValue select p.id).Single(), txtPerfilPostImageDescricao.Text.Trim(), 2, privado[0].id);
                    }
                }
                else
                {
                    log.AdicionarEntrada(29, usuarioLogado.id, (from p in bd.db._LogVisibilidades where p.descricao == ddlVisibilidade.SelectedValue select p.id).Single(), txtPerfilPostImageDescricao.Text.Trim(), 2, 0);
                }


                var post = (from p in bd.db._Logs
                            where p.acaoID == 29
                            && p.usuarioID == usuarioLogado.id
                            select p.id).ToList().Last();

                string pathTemp = Server.MapPath("~/data/user/" + usuarioLogado.id + "/img/");
                string path = Server.MapPath("~/data/post/" + post.ToString() + "/");
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }

                File.Copy(pathTemp + "tempPostImage.jpg", path + post.ToString() + ".jpg");
                File.Delete(pathTemp + "tempPostImage.jpg");

                fdsPerfilPostImagePreview.Visible = false;
                BtnPerfilPostImagePost.Visible = false;
                txtPerfilPostImageDescricao.Text = string.Empty;

                CarregaPerfil();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Imagem Postada com Sucesso!');", true);

        }
        catch(Exception er43)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('"+er43.Message+"');", true);
        }
    }
    protected void imbPostArquivo_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Funcionalidade ainda não liberada pelo desenvolvedor!');", true);
    }
    protected void lkbAnswer_Command(object sender, CommandEventArgs e)
    {
        try
        {
            //responde a um post na teia de forma privada

            int postId = int.Parse(e.CommandArgument.ToString());

            var post = (from p in bd.db._Logs
                        where p.id == postId
                        select p).Single();



            fdsPerfilPostTexto.Visible = true;
            fdsPerfilPostImage.Visible = false;
            fdsPerfilPostVideo.Visible = false;

            ddlVisibilidade.SelectedValue = "Privado";
            dvPrivado.Visible = true;
            txtPrivado.Text = post._user.nome;
            
            
            txtPost.Focus();


        }
        catch
        {

        }
    }
}
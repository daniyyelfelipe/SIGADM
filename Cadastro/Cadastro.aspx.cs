using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public partial class Cadastro : System.Web.UI.Page
{
    Guard guard = new Guard();
    BD bd = new BD();
    Log log = new Log();
    _user usuario = new _user();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtNome.Focus();
            CarregaTipos();
        }

        if (Session["usuarioLogado"] != null)
        {
            usuario = (_user)Session["usuarioLogado"];
        }
        else
        {
            Response.Redirect("~/Logoff.aspx");
        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        Cadastrar();
    }
    private void Cadastrar()
    {
        try
        {
            var testaUser = (from p in bd.db._users where p.email == txtEmail.Text.Trim() select p).ToList();

            if (testaUser.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Email já cadastrado no sistema! Entre em contato com o suporte.');", true);
            }
            else
            {
                _user newUser = new _user();
                newUser.email = txtEmail.Text.Trim();
                newUser.nome = txtNome.Text.Trim();
                newUser.senha = guard.EncriptaSenha(txtSenha.Text.Trim());
                newUser.statusID = 1;
                newUser.tipoID = (from p in bd.db._userTipos where p.descricao == ddlTipoUsuario.SelectedValue select p.id).Single();

                bd.db._users.InsertOnSubmit(newUser);
                bd.db.SubmitChanges();

                string path = Server.MapPath("~/data/user/" + (from p in bd.db._users where p.email == txtEmail.Text.Trim() select p.id).Single().ToString() + "/img/");
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
                

                log.AdicionarEntrada(9, (from p in bd.db._users where p.email == txtEmail.Text.Trim() select p.id).Single(),6,"",1, 0);

                txtEmail.Text = string.Empty;
                txtNome.Text = string.Empty;
                txtSenha.Text = string.Empty;
                txtNome.Focus();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Cadastro efetuado com Sucesso!');", true);
            }

        }
        catch(Exception er4)
        {
            lblError.Text = er4.Message;
        }
    }
    private void CarregaTipos()
    {
        try
        {
            if (usuario.tipoID == 8 || usuario.tipoID == 9)
            {
                var tipos = (from p in bd.db._userTipos
                             select p.descricao).ToList();
                ddlTipoUsuario.DataSource = tipos;
                ddlTipoUsuario.DataBind();

                ddlTipoUsuario.Items.Add("Selecione o cargo:");
                ddlTipoUsuario.SelectedValue = "Selecione o cargo:";
            }
            else if (usuario.tipoID == 1)
            {
                var tipos = (from p in bd.db._userTipos
                             where p.id == 2 && p.id == 3 && p.id == 5
                             select p.descricao).ToList();
                ddlTipoUsuario.DataSource = tipos;
                ddlTipoUsuario.DataBind();

                ddlTipoUsuario.Items.Add("Selecione o cargo:");
                ddlTipoUsuario.SelectedValue = "Selecione o cargo:";
            }
            else if (usuario.tipoID == 2)
            {
                var tipos = (from p in bd.db._userTipos
                             where p.id == 3 && p.id == 5
                             select p.descricao).ToList();
                ddlTipoUsuario.DataSource = tipos;
                ddlTipoUsuario.DataBind();

                ddlTipoUsuario.Items.Add("Selecione o cargo:");
                ddlTipoUsuario.SelectedValue = "Selecione o cargo:";
            }
            
        }
        catch
        {

        }
    }
}
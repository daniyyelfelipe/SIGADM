using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public partial class Cadastro_CadastroUser : System.Web.UI.Page
{
    Guard guard = new Guard();
    BD bd = new BD();
    Log log = new Log();
    _user usuario = new _user();
    Email email = new Email();

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
                newUser.email = txtEmail.Text.Trim().ToLower();
                newUser.nome = txtNome.Text.Trim().ToUpper();
                newUser.senha = guard.EncriptaSenha(txtSenha.Text.Trim());
                newUser.statusID = 1;
                newUser.tipoID = (from p in bd.db._userTipos where p.descricao == ddlTipoUsuario.SelectedValue select p.id).Single();

                // Instrumento
                if (ddlInstrumento.SelectedValue == "Não é musico.")
                {
                    //????
                }
                else if (ddlInstrumento.SelectedValue == "Selecione o instrumento:")
                {
                    //????
                }
                else
                {
                    newUser.instrumentoID = (from p in bd.db._instrumentos where p.descricao == ddlInstrumento.SelectedValue select p.id).Single();
                }

                // Cidade e Comum
                if (ddlCidade.SelectedValue != "Selecione uma Cidade:")
                {
                    if (ddlIgreja.SelectedValue != "Selecione uma Comum:")
                    {
                        newUser.cidadeID = (from p in bd.db._municipios where p.descricao == ddlCidade.SelectedValue select p.id).Single();
                        newUser.igrejaID = (from p in bd.db._igrejas where p.descricao == ddlIgreja.SelectedValue && p._municipio.id == newUser.cidadeID select p.id).Single();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Você deve selecionar uma comum!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Você deve selecionar uma cidade!');", true);
                }

                newUser.oficializado = ddlOficializado.SelectedValue.ToString();

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


                log.AdicionarEntrada(9, (from p in bd.db._users where p.email == txtEmail.Text.Trim() select p.id).Single(), 6, "", 1, 0);

                email.SendEmailSignUp(txtEmail.Text.Trim(), txtSenha.Text.Trim(), ddlTipoUsuario.SelectedValue);

                // Cleaning the form
                txtEmail.Text = string.Empty;
                txtNome.Text = string.Empty;
                txtSenha.Text = string.Empty;
                txtNome.Focus();
                ddlCidade.SelectedValue = "Selecione uma Cidade:";
                ddlIgreja.SelectedValue = "Selecione uma Comum:";
                ddlIgreja.Visible = false;
                


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Cadastro efetuado com Sucesso!');", true);
            }

        }
        catch (Exception er4)
        {
            lblError.Text = er4.Message;
        }
    }
    private void CarregaTipos()
    {
        try
        {
            usuario = (_user)Session["usuarioLogado"];

            //desenvolvedor
            if (usuario.tipoID == 8)
            {
                var tipos = (from p in bd.db._userTipos
                             select p.descricao).ToList();
                ddlTipoUsuario.DataSource = tipos;
                ddlTipoUsuario.DataBind();

                ddlTipoUsuario.Items.Add("Selecione o cargo:");
                ddlTipoUsuario.SelectedValue = "Selecione o cargo:";
            }
            //secretarios
            if (usuario.tipoID == 9)
            {
                var tipos = (from p in bd.db._userTipos
                             where p.id == 1 || p.id == 2 || p.id == 3 || p.id == 4 || p.id == 6 || p.id == 7 || p.id == 9 || p.id == 10 || p.id == 11 || p.id == 12
                             select p.descricao).ToList();
                ddlTipoUsuario.DataSource = tipos;
                ddlTipoUsuario.DataBind();

                ddlTipoUsuario.Items.Add("Selecione o cargo:");
                ddlTipoUsuario.SelectedValue = "Selecione o cargo:";
            }
            //encarregado regional
            else if (usuario.tipoID == 1)
            {
                var tipos = (from p in bd.db._userTipos
                             where p.id == 2 || p.id == 3 || p.id == 10 || p.id == 11
                             select p.descricao).ToList();
                ddlTipoUsuario.DataSource = tipos;
                ddlTipoUsuario.DataBind();

                ddlTipoUsuario.Items.Add("Selecione o cargo:");
                ddlTipoUsuario.SelectedValue = "Selecione o cargo:";
            }
            //encarregado local
            else if (usuario.tipoID == 2)
            {
                var tipos = (from p in bd.db._userTipos
                             where p.id == 3 || p.id == 10 || p.id == 11
                             select p.descricao).ToList();
                ddlTipoUsuario.DataSource = tipos;
                ddlTipoUsuario.DataBind();

                ddlTipoUsuario.Items.Add("Selecione o cargo:");
                ddlTipoUsuario.SelectedValue = "Selecione o cargo:";
            }
            //examinadoras
            else if (usuario.tipoID == 4)
            {
                var tipos = (from p in bd.db._userTipos
                             where p.id == 12 || p.id == 10 || p.id == 11
                             select p.descricao).ToList();
                ddlTipoUsuario.DataSource = tipos;
                ddlTipoUsuario.DataBind();

                ddlTipoUsuario.Items.Add("Selecione o cargo:");
                ddlTipoUsuario.SelectedValue = "Selecione o cargo:";
            }

            var instrumentos = (from p in bd.db._instrumentos
                                select p.descricao).ToList();
            ddlInstrumento.DataSource = instrumentos;
            ddlInstrumento.DataBind();
            ddlInstrumento.Items.Add("Selecione o instrumento:");
            ddlInstrumento.Items.Add("Não é musico.");
            ddlInstrumento.SelectedValue = "Selecione o instrumento:";

            var cidades = (from p in bd.db._municipios
                           select p.descricao).ToList();
            ddlCidade.DataSource = cidades;
            ddlCidade.DataBind();
            ddlCidade.Items.Add("Selecione uma Cidade:");
            ddlCidade.SelectedValue = "Selecione uma Cidade:";
        }
        catch
        {

        }
    }
    protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCidade.SelectedValue != "Selecione uma Cidade:")
        {
            ddlIgreja.Visible = true;

            var igrejas = (from p in bd.db._igrejas
                           where p._municipio.descricao == ddlCidade.SelectedValue
                           select p.descricao).ToList();
            ddlIgreja.DataSource = igrejas;
            ddlIgreja.DataBind();
            ddlIgreja.Items.Add("Selecione uma Comum:");
            ddlIgreja.SelectedValue = "Selecione uma Comum:";
        }
        else
        {
            ddlIgreja.Visible = false;
        }
    }
    protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTipoUsuario.SelectedValue == "Músico" || ddlTipoUsuario.SelectedValue == "Organista")
            {
                ddlOficializado.Visible = true;
            }
            else
            {
                ddlOficializado.Visible = false;
            }
        }
        catch
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cadastro_NovoAluno : System.Web.UI.Page
{
    BD bd = new BD();
    Log log = new Log();
    Guard guard = new Guard();
    _user usuarioLogado;
    string editMode = "0";
    string alunoID = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogado = (_user)Session["usuarioLogado"];
        editMode = Request.QueryString["editMode"];
        alunoID = Request.QueryString["alunoId"];

        if (!IsPostBack)
        {
            CarregaForm(editMode);
        }
    }

    private void CarregaForm(string editMode)
    {
        try
        {
            if (editMode == null)
            {
                btnCadastrar.Visible = true;
                btnEditar.Visible = false;

                var comum = (from p in bd.db._igrejas
                             orderby p._municipio.descricao
                             select p._municipio.descricao + " - " + p.descricao).ToList();
                ddlComum.DataSource = comum;
                ddlComum.DataBind();

                var instrumento = (from p in bd.db._instrumentos
                                   orderby p.descricao
                                   select p.descricao).ToList();
                ddlInstrumento.DataSource = instrumento;
                ddlInstrumento.DataBind();
            }
            else if (editMode == "1")
            {
                //carrega dados do aluno para editar

                btnCadastrar.Visible = false;
                btnEditar.Visible = true;

                var aluno = (from p in bd.db._alunos
                             where p.id == int.Parse(alunoID)
                             select p).Single();

                txtNome.Text = aluno.nome;
                if (aluno.dataNascimento != null) { txtDtNascimento.Text = aluno.dataNascimento.Value.Date.ToString(); }

                if (aluno.batizado != null)
                {
                    if (aluno.batizado.Value == true)
                    {
                        rbBatizado.Items.FindByText("Sim").Selected = true;
                        txtDtBatismo.Visible = true;
                        if (aluno.dataBatismo != null) { txtDtBatismo.Text = aluno.dataBatismo.Value.Date.ToString(); }
                    }
                    else if (aluno.batizado.Value == false)
                    {
                        rbBatizado.Items.FindByText("Não").Selected = true;
                        txtDtBatismo.Visible = false;
                    }
                }

                txtEndereco.Text = aluno.endereco;
                txtBairro.Text = aluno.bairro;
                txtCidade.Text = aluno.cidade;

                var comum = (from p in bd.db._igrejas
                             orderby p._municipio.descricao
                             select p._municipio.descricao + " - " + p.descricao).ToList();
                ddlComum.DataSource = comum;
                ddlComum.DataBind();

                var comumAluno = (from p in bd.db._igrejas
                                  where p.id == aluno.comumIgrejaID
                                  orderby p._municipio.descricao
                                  select p._municipio.descricao + " - " + p.descricao).ToList();

                ddlComum.SelectedValue = comumAluno[0];

                var instrumento = (from p in bd.db._instrumentos
                                   orderby p.descricao
                                   select p.descricao).ToList();
                ddlInstrumento.DataSource = instrumento;
                ddlInstrumento.DataBind();

                var instrumentoAluno = (from p in bd.db._instrumentos
                                        where p.id == aluno.instrumentoID
                                   orderby p.descricao
                                   select p.descricao).ToList();
                ddlInstrumento.DataSource = instrumento;
                ddlInstrumento.DataBind();

                ddlInstrumento.SelectedValue = instrumentoAluno[0];

                if (aluno.temiInstrumento != null)
                {
                    if (aluno.temiInstrumento.Value == true)
                    {
                        rbTemInstrumento.Items.FindByText("Sim").Selected = true;
                    }
                    else if (aluno.temiInstrumento.Value == false)
                    {
                        rbTemInstrumento.Items.FindByText("Não").Selected = true;
                    }
                }

                txtTelefone.Text = aluno.telefone;
                txtemail.Text = aluno.email;
                

            }
        }
        catch(Exception e3)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('"+e3.Message+"');", true);
        }
    }

    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        try
        {
            var testaUser = (from p in bd.db._users where p.email == txtemail.Text.Trim() select p).ToList();

            if (testaUser.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Email já cadastrado no sistema! Tente outro email ou entre em contato com o suporte.');", true);
            }
            else
            {
                //cadastra usuário
                _user novoUser = new _user();
                novoUser.email = txtemail.Text.Trim().ToLower();
                novoUser.senha = guard.EncriptaSenha("123");
                novoUser.nome = txtNome.Text.Trim().ToUpper();
                novoUser.statusID = 1;
                novoUser.tipoID = 5;

                bd.db._users.InsertOnSubmit(novoUser);
                bd.db.SubmitChanges();
                log.AdicionarEntrada(6, usuarioLogado.id, 6, "", 1, 0);


                //cadastra aluno
                _aluno novo = new _aluno();
                novo.bairro = txtBairro.Text.Trim();
                novo.batizado = bool.Parse(rbBatizado.SelectedValue.ToString());
                novo.cidade = txtCidade.Text.Trim();
                string[] comum = ddlComum.SelectedValue.Split('-');
                novo.comumIgrejaID = (from p in bd.db._igrejas where p.descricao == comum[1].Trim() && p._municipio.descricao == comum[0].Trim() select p.id).Single();
                //if (txtDtBatismo.Text.Length > 0)
                //{
                //    novo.dataBatismo = DateTime.Parse(txtDtBatismo.Text.Trim());
                //}
                //else
                //{
                //    novo.dataBatismo = null;
                //}
                //if (txtDtBatismo.Text.Length > 0)
                //{
                //    novo.dataNascimento = DateTime.Parse(txtDtNascimento.Text.Trim());
                //}
                //else
                //{
                //    novo.dataNascimento = null;
                //}                
                novo.dataNascimento = Convert.ToDateTime(txtDtNascimento.Text.Trim());
                novo.email = txtemail.Text.Trim();
                novo.endereco = txtEndereco.Text.Trim();
                novo.instrumentoID = bd.db._instrumentos.Where(x => x.descricao == ddlInstrumento.SelectedValue).Select(x => x.id).Single();
                novo.nome = txtNome.Text.ToUpper().Trim();
                novo.telefone = txtTelefone.Text.Trim();
                novo.temiInstrumento = bool.Parse(rbTemInstrumento.SelectedValue.ToString());
                novo.userID = (from p in bd.db._users where p.email == txtemail.Text.Trim() select p.id).Single();

                bd.db._alunos.InsertOnSubmit(novo);
                bd.db.SubmitChanges();
                log.AdicionarEntrada(18, usuarioLogado.id, 6, "", 1, 0);

                //imagem do perfil
                string path = Server.MapPath("~/data/user/" + (from p in bd.db._users where p.email == txtemail.Text.Trim() select p.id).Single().ToString() + "/img/");
                string defaultPath = Server.MapPath("~/data/user/default/img/");
                DirectoryInfo dir = new DirectoryInfo(path);

                if (dir.Exists)
                {
                    File.Copy(defaultPath + "perfil.jpg", path + "perfil.jpg");
                }
                else
                {
                    dir.Create();
                    File.Copy(defaultPath + "perfil.jpg", path + "perfil.jpg");
                }

                LimpaForm();
                CarregaForm(editMode);


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Aluno Cadastrado com Sucesso! Além disso foi criado um usuario com o email: " + txtemail.Text.Trim() + " e a Senha: 123. Caso o Aluno tenha que acessar o sistema.');", true);

            }
        }
        catch (Exception e32)
        {
            //deleta se o usuário foi cadastrado
            try
            {
                var user = (from p in bd.db._users
                            where p.email == txtemail.Text
                            select p).Single();
                bd.db._users.DeleteOnSubmit(user);
                bd.db.SubmitChanges();
            }
            catch
            {

            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + e32.Message + "');", true);
        }
    }

    private void LimpaForm()
    {
        txtBairro.Text = string.Empty;
        txtCidade.Text = string.Empty;
        txtDtBatismo.Text = string.Empty;
        txtDtNascimento.Text = string.Empty;
        txtemail.Text = string.Empty;
        txtEndereco.Text = string.Empty;
        txtNome.Text = string.Empty;
        txtTelefone.Text = string.Empty;
    }

    protected void rbBatizado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbBatizado.SelectedValue == "True")
        {
            txtDtBatismo.Visible = true;
        }
        else
        {
            txtDtBatismo.Visible = false;
        }
    }
    protected void btnEditar_Click(object sender, EventArgs e)
    {
        try
        {
            //edita dados do aluno
            _aluno novo = (from p in bd.db._alunos where p.id == int.Parse(alunoID) select p).Single();
            var usuarioAluno = (from p in bd.db._users where p.email == novo.email select p).Single();

            novo.bairro = txtBairro.Text.Trim();
            novo.batizado = bool.Parse(rbBatizado.SelectedValue.ToString());
            novo.cidade = txtCidade.Text.Trim();
            string[] comum = ddlComum.SelectedValue.Split('-');
            novo.comumIgrejaID = (from p in bd.db._igrejas where p.descricao == comum[1].Trim() && p._municipio.descricao == comum[0].Trim() select p.id).Single();
            //if (txtDtBatismo.Text.Length > 0)
            //{
            //    novo.dataBatismo = DateTime.Parse(txtDtBatismo.Text.Trim());
            //}
            //else
            //{
            //    novo.dataBatismo = null;
            //}
            //if (txtDtBatismo.Text.Length > 0)
            //{
            //    novo.dataNascimento = DateTime.Parse(txtDtNascimento.Text.Trim());
            //}
            //else
            //{
            //    novo.dataNascimento = null;
            //}                
            if (txtDtNascimento.Text != "") { novo.dataNascimento = Convert.ToDateTime(txtDtNascimento.Text.Trim()); }
            novo.endereco = txtEndereco.Text.Trim();
            if (txtemail.Text.Trim() != novo.email)
            {
                novo.email = txtemail.Text.Trim();
               
                //altera email de acesso ao sistema
                usuarioAluno.email = txtemail.Text.Trim();

                log.AdicionarEntrada(41, usuarioLogado.id, 6, "", 1, 0);
            }
            novo.instrumentoID = bd.db._instrumentos.Where(x => x.descricao == ddlInstrumento.SelectedValue).Select(x => x.id).Single();
            novo.nome = txtNome.Text.ToUpper().Trim();
            usuarioAluno.nome = txtNome.Text.ToUpper().Trim();
            novo.telefone = txtTelefone.Text.Trim();
            novo.temiInstrumento = bool.Parse(rbTemInstrumento.SelectedValue.ToString());

            bd.db.SubmitChanges();
            log.AdicionarEntrada(42, usuarioLogado.id, 6, "", 1, 0);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Dados do aluno editados com Sucesso! Se o email também foi alterado ele deve usar o novo email para acessar o sistema.');", true);
            
        }
        catch (Exception e1) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('"+e1.Message+"');", true); }
    }
}
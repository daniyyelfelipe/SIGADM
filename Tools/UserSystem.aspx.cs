using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_UserSystem : System.Web.UI.Page
{
    BD bd = new BD();
    Guard guard = new Guard();
    Log log = new Log();
    _user usuario = new _user();
    Email email = new Email();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuarioLogado"] != null)
        {
            usuario = (_user)Session["usuarioLogado"];
        }
        else
        {
            Response.Redirect("~/Logoff.aspx");
        }

        if(!IsPostBack)
        {
            CarregaDDL();
        }       
    }
    private void CarregaDDL()
    {
        var status = (from p in bd.db._userStatus
                      select p.descricao).ToList();
        ddlStatusA.DataSource = status;
        ddlStatusA.DataBind();

        var tipos = (from p in bd.db._userTipos
                     select p.descricao).ToList();
        ddlTipoA.DataSource = tipos;
        ddlTipoA.DataBind();
    }
    protected void btnPesquisa_Click(object sender, EventArgs e)
    {
        try
        {
            var usuarios = (from p in bd.db._users
                            select new
                            {
                                ID = p.id,
                                NOME = p.nome,
                                EMAIL = p.email,
                                SENHA = p.senha,
                                STATUS = p._userStatus.descricao,
                                TIPO = p._userTipo.descricao,
                                MATRICULA = p.matricula,
                                CIDADE = p._igreja._municipio.descricao,
                                IGREJA = p._igreja.descricao

                            }).OrderBy(x => x.NOME).ToList();


            var filter = usuarios;

            if (!string.IsNullOrEmpty(txtNome.Text))
            {
                filter = filter.Where(x => x.NOME.Contains(txtNome.Text.Trim().ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                filter = filter.Where(x => x.EMAIL.Contains(txtEmail.Text.Trim().ToLower())).ToList();
            }

            gvUser.DataSource = filter;
            gvUser.DataBind();
        }
        catch
        {

        }
    }
    protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = int.Parse(e.CommandArgument.ToString());

        if(e.CommandName == "Alterar")
        {
            var user = (from p in bd.db._users
                        where p.id == id
                        select p).Single();

            lblId.Text = user.id.ToString();
            txtEmailA.Text = user.email;
            ddlStatusA.SelectedValue = user._userStatus.descricao;
            ddlTipoA.SelectedValue = user._userTipo.descricao;
            if (user.matricula != null)
            {
                txtMatriculaA.Text = user.matricula.Value.ToString();
            }

            fdsAtualizar.Visible = true;
        }
        if (e.CommandName == "Excluir")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('O sistema não está mais aceitando exclusão de usuários! Apenas mude o status do mesmo para -Inativo-.');", true);
        }
    }
    protected void btnResetSenha_Click(object sender, EventArgs e)
    {
        try
        {
            var user = (from p in bd.db._users
                        where p.id == int.Parse(lblId.Text.Trim())
                        select p).Single();

            user.senha = guard.EncriptaSenha("123");
            bd.db.SubmitChanges();

            log.AdicionarEntrada(21, int.Parse(lblId.Text.Trim()), 6, "", 1, 0);

            btnPesquisa_Click(sender, e);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Senha resetada com sucesso!');", true);
        }
        catch
        {

        }
    }
    protected void btnAtualizar_Click(object sender, EventArgs e)
    {
        try
        {
            var user = (from p in bd.db._users
                        where p.id == int.Parse(lblId.Text.Trim())
                        select p).Single();


            user.email = txtEmailA.Text.Trim();
            user.statusID = (from p in bd.db._userStatus where p.descricao == ddlStatusA.SelectedValue select p.id).Single();
            user.tipoID = (from p in bd.db._userTipos where p.descricao == ddlTipoA.SelectedValue select p.id).Single();
            if (!string.IsNullOrEmpty(txtMatriculaA.Text))
            {
                user.matricula = int.Parse(txtMatriculaA.Text);
            }

            bd.db.SubmitChanges();

            btnPesquisa_Click(sender, e);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Dados alterados com sucesso!');", true);
        }
        catch
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cadastro_Cidades : System.Web.UI.Page
{
    BD bd = new BD();
    Log log = new Log();

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
            txtNome.Text = string.Empty;

            var estados = (from p in bd.db._cidadeEstados
                           select p.descricao).ToList();
            ddlEstado.DataSource = estados;
            ddlEstado.DataBind();

            ddlEstado.Items.Add("Estado:");
            ddlEstado.SelectedValue = "Estado:";


            var regiao = (from p in bd.db._cidadeEstadoRegiaos
                          select p.descricao).ToList();
            ddlRegiao.DataSource = regiao;
            ddlRegiao.DataBind();

            ddlRegiao.Items.Add("Região:");
            ddlRegiao.SelectedValue = "Região:";

            var cidades = (from p in bd.db._municipios
                           select new
                           {
                               ID = p.id,
                               NOME = p.descricao,
                               ESTADO = p._cidadeEstado.descricao,
                               REGIAO = p._cidadeEstadoRegiao.descricao
                           }).ToList();
            gvCidades.DataSource = cidades;
            gvCidades.DataBind();
        }
        catch
        {

        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        try
        {
            _municipio muni = new _municipio();
            muni.descricao = txtNome.Text.Trim();
            muni.estadoID = (from p in bd.db._cidadeEstados where p.descricao == ddlEstado.SelectedValue select p.id).Single();
            muni.estadoRegiaoID = (from p in bd.db._cidadeEstadoRegiaos where p.descricao == ddlRegiao.SelectedValue select p.id).Single();

            bd.db._municipios.InsertOnSubmit(muni);
            bd.db.SubmitChanges();

            CarregaForm();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Cidade cadastrada com Sucesso!');", true);
        }
        catch
        {

        }
    }
}
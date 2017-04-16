using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cadastro_Congregacoes : System.Web.UI.Page
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
            var cidades = (from p in bd.db._municipios
                           select p.descricao).ToList();
            ddlCidade.DataSource = cidades;
            ddlCidade.DataBind();
            ddlCidade.Items.Add("Selecione a Cidade:");
            ddlCidade.SelectedValue = "Selecione a Cidade:";

            var regioes = (from p in bd.db._cidadeRegiaos
                           select p.descricao).ToList();
            ddlRegiao.DataSource = regioes;
            ddlRegiao.DataBind();
            ddlRegiao.Items.Add("Selecione a Região:");
            ddlRegiao.SelectedValue = "Selecione a Região:";

            txtNome.Text = string.Empty;
            txtCodigoRelatorio.Text = string.Empty;
            txtNome.Focus();

            var igrejas = (from p in bd.db._igrejas
                           select new
                           {
                               ID = p.id,
                               DESCRICAO = p.descricao,
                               CIDADE = p._municipio.descricao,
                               REGIAO = p._cidadeRegiao.descricao,
                               CODIGO = p.codigoRelatorio
                           }).ToList();
            gvIgrejas.DataSource = igrejas;
            gvIgrejas.DataBind();
        }
        catch
        {

        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        try
        {

            var chekCodigo = (from p in bd.db._igrejas
                              where p.codigoRelatorio == int.Parse(txtCodigoRelatorio.Text.Trim())
                              select p).ToList();
            if (chekCodigo.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Já existe uma Congregação cadastrada com esse código do relatório!');", true);
            }
            else
            {
                _igreja igreja = new _igreja();
                igreja.cidadeID = (from p in bd.db._municipios where p.descricao == ddlCidade.SelectedValue select p.id).Single();
                igreja.cidadeRegiaoID = (from p in bd.db._cidadeRegiaos where p.descricao == ddlRegiao.SelectedValue select p.id).Single();
                igreja.codigoRelatorio = int.Parse(txtCodigoRelatorio.Text.Trim());
                igreja.descricao = txtNome.Text.Trim();

                bd.db._igrejas.InsertOnSubmit(igreja);
                bd.db.SubmitChanges();

                _user usuarioLogado = (_user)Session["usuarioLogado"];
                log.AdicionarEntrada(10, usuarioLogado.id,6,"",1, 0);

                CarregaForm();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Congregação cadastrada com Sucesso!');", true);
            }
        }
        catch
        {

        }
    }
}
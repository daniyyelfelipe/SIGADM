using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Instrumentos_Default : System.Web.UI.Page
{
    BD bd = new BD();
    Log log = new Log();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregaForm();
            ddlNatureza_SelectedIndexChanged(sender, e);
        }
    }
    private void CarregaForm()
    {
        try
        {
            var instrumentos = (from p in bd.db._instrumentos
                                select p.descricao).ToList();
            ddlDescricao.DataSource = instrumentos;
            ddlDescricao.DataBind();

            var natureza = (from p in bd.db._instrumentoNaturezas
                            select p.descricao).ToList();
            ddlNatureza.DataSource = natureza;
            ddlNatureza.DataBind();

            var igrejas = (from p in bd.db._igrejas
                           select p.descricao).ToList();
            ddlCongregacao.DataSource = igrejas;
            ddlCongregacao.DataBind();

            var bens = (from p in bd.db._bemPatrimonios
                        select new
                        {
                            ID = p.id,
                            INSTRUMENTO = p._instrumento.descricao,
                            TOMBAMENTO = p.numeroTombamento,
                            TONALIDADE = p.tonalidade,
                            ENTRADA = (p.dataEntrada != null) ? string.Format("{0:d}", p.dataEntrada.Value.Date) : "---",  
                            
                            STATUS = p._bemPatrimonioStatus.descricao

                        }).OrderBy(x => x.INSTRUMENTO).ToList();
            gvBens.DataSource = bens;
            gvBens.DataBind();

            lblQuant.Text = bens.Count.ToString() + " instrumentos cadastrados.";
        }
        catch
        {

        }
    }
    protected void ddlNatureza_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNatureza.SelectedIndex == 0)
        {
            divDoacao.Visible = false;
            divCompra.Visible = true;
        }
        if (ddlNatureza.SelectedIndex == 1)
        {
            divCompra.Visible = false;
            divDoacao.Visible = true;
        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        try
        {
            _bemPatrimonio novo = new _bemPatrimonio();
            novo.anoFabricacao = txtAnoFabricacao.Text.Trim();
            novo.caracteristicas = txtCaracteristicas.Text.Trim();
            novo.componentes = txtComponentes.Text.Trim();
            if (txtDtEntrada.Text != string.Empty) { novo.dataEntrada = DateTime.Parse(txtDtEntrada.Text.Trim()).Date; }            
            novo.empresa = txtEmpresa.Text.Trim();
            novo.instrumentoID = (from p in bd.db._instrumentos where p.descricao == ddlDescricao.SelectedValue select p.id).Single();
            novo.marca = txtMarca.Text.Trim();
            novo.naturezaID = (from p in bd.db._instrumentoNaturezas where p.descricao == ddlNatureza.SelectedValue select p.id).Single();
            novo.numeroNotaFiscal = txtNumeroNota.Text.Trim();
            novo.numeroTombamento = txtNumeroTombamento.Text.Trim();
            novo.obs = txtObs.Text.Trim();
            novo.tonalidade = txtTonalidade.Text.Trim();
            if (txtValor.Text.Length > 0) { novo.valor = decimal.Parse(txtValor.Text.Trim()); } else { novo.valor = 0; };
            novo.statusID = 1;

            bd.db._bemPatrimonios.InsertOnSubmit(novo);
            bd.db.SubmitChanges();           

             _user usuarioLogado = (_user)Session["usuarioLogado"];
             log.AdicionarEntrada(3, usuarioLogado.id, 6, "", 1, 0);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Bem Cadastrado com Sucesso!');", true);

            LimparForm();
            CarregaForm();
        }
        catch(Exception er1)
        {
            lblError.Text = er1.Message;
        }
    }
    private void LimparForm()
    {
        txtAnoFabricacao.Text = string.Empty;
        txtCaracteristicas.Text = string.Empty;
        txtComponentes.Text = string.Empty;
        txtDtCompra.Text = string.Empty;
        txtDtDoacao.Text = string.Empty;
        txtDtEntrada.Text = string.Empty;
        txtEmpresa.Text = string.Empty;
        txtMarca.Text = string.Empty;
        txtNumeroNota.Text = string.Empty;
        txtNumeroTombamento.Text = string.Empty;
        txtObs.Text = string.Empty;
        txtOrigemDoacao.Text = string.Empty;
        txtTonalidade.Text = string.Empty;
        txtValor.Text = string.Empty;
        ddlDescricao.SelectedIndex = 0;
        ddlNatureza.SelectedIndex = 0;
        ddlDescricao.Focus();
        lblError.Text = string.Empty;
    }
    protected void gvBens_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int id = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "alterar")
            {
                String s = "<script language='javascript'> javascript:abrirPopup('Instrumento_Edit.aspx?id=" + id.ToString() + "', 600, 500); </script>";
                Page.RegisterClientScriptBlock("Print", s);

                
            }
            //lblQuant.Text = "nm,bmnb,mb,m";
        }
        catch
        {

        }
    }
}
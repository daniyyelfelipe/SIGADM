using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Formularios_Patrimonio_PedidoInstrumento : System.Web.UI.Page
{
    BD bd = new BD();
    App app = new App();
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
            var comum  = (from p in bd.db._igrejas
                         select p.descricao + " - " + p._municipio.descricao).ToList();
            ddlCongregação.DataSource = comum;
            ddlCongregação.DataBind();

            var instrumento = (from p in bd.db._instrumentos
                               select p.descricao).ToList();
            ddlInstrumento.DataSource = instrumento;
            ddlInstrumento.DataBind();

            txtRecebedor.Focus();
        }
        catch
        {

        }
    }
    protected void btnGravar_Click(object sender, EventArgs e)
    {
        try
        {
            _user usuario = new _user();
            usuario = (_user)Session["usuarioLogado"];

            string[] comum = ddlCongregação.SelectedValue.Split('-');

            _PedidoDeInstrumento novo = new _PedidoDeInstrumento();
            if (txtEmail.Text.Length > 0) { novo.email = txtEmail.Text.Trim(); } else { novo.email = null; }
            novo.encarregado = txtEncarregado.Text.Trim();
            novo.endereco = txtEndereço.Text.Trim();
            novo.igrejaID = bd.db._igrejas.Where(x => x.descricao == comum[0].Trim()).Select(x => x.id).Single();
            novo.inicioEstudos = DateTime.Parse(txtDataInicio.Text.Trim()).Date;
            novo.instrumentoID = bd.db._instrumentos.Where(x => x.descricao == ddlInstrumento.SelectedValue).Select(x => x.id).Single();
            novo.nomeRecebedor = txtRecebedor.Text.Trim();
            novo.statusID = 1;
            novo.telefone = txtTelefone.Text.Trim();
            novo.tonalidade = ddlTonalidade.SelectedValue;
            novo.usuarioID = usuario.id;
            novo.dataPedido = app.DateTimeCorrigido();

            bd.db._PedidoDeInstrumentos.InsertOnSubmit(novo);
            bd.db.SubmitChanges();

            LimpaForm();

            log.AdicionarEntrada(19, usuario.id, 6, "", 1, 0);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Seu pedido foi Enviado com sucesso! Você pode acompanhar o status do seu pedido na tela de Pedidos Realizados.');", true);
        }
        catch
        {

        }
    }

    private void LimpaForm()
    {
        txtDataInicio.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtEncarregado.Text = string.Empty;
        txtEndereço.Text = string.Empty;
        txtRecebedor.Text = string.Empty;
        txtTelefone.Text = string.Empty;
        ddlCongregação.SelectedIndex = 0;
        ddlInstrumento.SelectedIndex = 0;
        ddlTonalidade.SelectedIndex = 0;

        txtRecebedor.Focus();
    }
}
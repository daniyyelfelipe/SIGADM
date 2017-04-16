using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Global_reports_TermoResponsabilidade : System.Web.UI.Page
{
    int idPedido;
    BD bd = new BD();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            idPedido = int.Parse(Request.QueryString["id"].ToString());
            GetDados();
        }
        catch
        {

        }
    }
    private void GetDados()
    {
        try
        {
            var pedido = (from p in bd.db._PedidoDeInstrumentos
                          where p.id == idPedido
                          select p).Single();

            //carrega dados:
            lblInstrumento.Text = pedido._instrumento.descricao;
            lblTonalidade.Text = pedido.tonalidade;
            lblTombamento.Text = "---";
            lblNome.Text = pedido.nomeRecebedor;
            lblEndereco.Text = pedido.endereco;
            lblCidade.Text = pedido._igreja._municipio.descricao;
            lblCongregacao.Text = pedido._igreja.descricao + " / " + pedido._igreja.codigoRelatorio;
            lblTelefone.Text = pedido.telefone;
            lblEmail.Text = pedido.email;
            lblEncarregado.Text = pedido.encarregado;
            lblData.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now.Date);

            lblAssRecebedor.Text = pedido.nomeRecebedor + "(responsável)";
            lblAssEntregador.Text = "Secretário";
        }
        catch
        {

        }
    }
}
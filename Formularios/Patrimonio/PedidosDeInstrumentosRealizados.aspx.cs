using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Formularios_Patrimonio_PedidosDeInstrumentosRealizados : System.Web.UI.Page
{
    BD bd = new BD();

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
             _user usuario = new _user();
            usuario = (_user)Session["usuarioLogado"];

            //encarregados locais
            if (usuario.tipoID == 2)
            {
                var pedidos = (from p in bd.db._PedidoDeInstrumentos
                               where p.usuarioID == usuario.id
                               select new
                               {
                                   ID = p.id,
                                   RECEBEDOR = p.nomeRecebedor,
                                   COMUM = p._igreja.descricao,
                                   INSTRUMENTO = p._instrumento.descricao,
                                   TONALIDADE = p.tonalidade,
                                   ENCARREGADO = p.encarregado,
                                   SOLICITANTE = p._user.nome,
                                   STATUS = p._PedidoInstrumentoStatus.descricao,
                                   OBSERVAÇÕES = (p.obs.Length > 0) ? p.obs : "---"

                               }).ToList();
                gvPedidos.DataSource = pedidos;
                gvPedidos.DataBind();
            }
            //desenvolvedores, secretarios e encarregados regionais
            else if(usuario.tipoID == 8 || usuario.tipoID == 9 || usuario.tipoID == 1)
            {
                var pedidos = (from p in bd.db._PedidoDeInstrumentos
                               select new
                               {
                                   ID = p.id,
                                   RECEBEDOR = p.nomeRecebedor,
                                   COMUM = p._igreja.descricao,
                                   INSTRUMENTO = p._instrumento.descricao,
                                   TONALIDADE = p.tonalidade,
                                   ENCARREGADO = p.encarregado,
                                   SOLICITANTE = p._user.nome,
                                   STATUS = p._PedidoInstrumentoStatus.descricao,
                                   OBSERVAÇÕES = (p.obs.Length > 0) ? p.obs : "---"

                               }).ToList();
                gvPedidos.DataSource = pedidos;
                gvPedidos.DataBind();
            }
        }
        catch
        {

        }
    }
    protected void gvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "termo")
        {
            String s = "<script language='javascript'> javascript:abrirA4('../../global/reports/TermoResponsabilidade.aspx?idg="+e.CommandArgument.ToString()+"'); </script>";
            Page.RegisterClientScriptBlock("Print", s);
        }
    }
}
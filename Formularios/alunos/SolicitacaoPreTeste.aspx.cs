using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Formularios_alunos_SolicitacaoPreTeste : System.Web.UI.Page
{
    BD bd = new BD();
    Log log = new Log();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregaCampos();
        }
    }
    private void CarregaCampos()
    {
        //var localidade = (from p in bd.db._municipios
        //                  select p.descricao).ToList();
        //ddlLocalidade.DataSource = localidade;
        //ddlLocalidade.DataBind();
    }
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        try
        {
            var pedido = new _PedidoTeste();
            



            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + Request.Form["txtData"].ToString() + "');", true);

            
        }
        catch
        {

        }
    }
    protected void btnPesquisa_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            var alunos = (from p in bd.db._alunos
                          join m in bd.db._igrejas
                          on p._igreja.id equals m.id
                          where p.nome.Contains(txtNome.Text)
                          select new
                          {
                              ID = p.id,
                              NOME = p.nome,
                              COMUM = p._igreja.descricao,
                              CIDADE = m._municipio.descricao,
                              INSTRUMENTO = p._instrumento.descricao,
                              EMAIL = p.email,
                              TELEFONE = p.telefone
                          }).ToList();
            gvPesquisa.DataSource = alunos;
            gvPesquisa.DataBind();
        }
        catch(Exception e3)
        {
            ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('"+ e3.Message +"');", true);
        }
    }
    protected void gvPesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if(e.CommandName == "Escolher")
            {
                int id = int.Parse(e.CommandArgument.ToString());

                var aluno = (from p in bd.db._alunos
                             where p.id == id
                             select p).Single();

                var pedido = new _PedidoTeste();
                pedido.alunoID = id;
                pedido.comumID = aluno.comumIgrejaID.Value;
                pedido.data = DateTime.Now;
                pedido.instrumentoID = aluno.instrumentoID.Value;
                pedido.tipoTesteID = 1;
                pedido.status = 1;

                bd.db._PedidoTestes.InsertOnSubmit(pedido);
                bd.db.SubmitChanges();

                //limpa campos
                txtNome.Text = string.Empty;
                txtNome.Focus();
                gvPesquisa.DataSource = null;
                gvPesquisa.DataBind();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Pedido realizado com sucesso! Aguarde a análise por parte dos Regionais.');", true);


            }
        }
        catch
        {

        }
    }
}
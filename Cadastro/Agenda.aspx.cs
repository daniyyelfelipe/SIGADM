using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cadastro_Agenda : System.Web.UI.Page
{
    BD bd = new BD();
    Log log = new Log();
    _user usuarioLogado;
    App app = new App();
    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogado = (_user)Session["usuarioLogado"];

        if (!IsPostBack)
        {
            CarregaForm();
            ddlCidade_SelectedIndexChanged(sender, e);
        }
    }

    private void CarregaForm()
    {
        var tipos = (from p in bd.db._agenda_Eventos
                     select p.descricao).ToList();
        ddlTIpo.DataSource = tipos;
        ddlTIpo.DataBind();

        var cidades = (from p in bd.db._municipios
                       orderby p.descricao
                       select p.descricao).ToList();
        ddlCidade.DataSource = cidades;
        ddlCidade.DataBind();

        var encarregados = (from p in bd.db._users
                            where p.tipoID == 1 || p.tipoID == 2
                            orderby p.nome
                            select p.nome).ToList();
        ddlEncarregado.DataSource = encarregados;
        ddlEncarregado.DataBind();

        var compromissos = (from p in bd.db._agendas
                            select new
                            {
                                ID = p.ID,
                                DATA = p.dataHora,
                                TIPO = p._agenda_Evento.descricao,
                                CIDADE = p._igreja._municipio.descricao,
                                IGREJA = p._igreja.descricao,
                                ENCARREGADO = p._user.nome,
                                TELEFONE = p.telefone

                            }).OrderByDescending(x => x.DATA).ToList();
        gvCompromissos.DataSource = compromissos;
        gvCompromissos.DataBind();

        txtdata.Text = string.Empty;
        txtHora.Text = string.Empty;
        txtTel.Text = string.Empty;
    }
    protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
    {
        var igrejas = (from p in bd.db._igrejas
                       where p._municipio.descricao == ddlCidade.SelectedValue
                       orderby p.descricao
                       select p.descricao).ToList();
        ddlIgreja.DataSource = igrejas;
        ddlIgreja.DataBind();
        
    }
    protected void gvCompromissos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int arg = int.Parse(e.CommandArgument.ToString());

        if(e.CommandName == "del")
        {
            var agenda = (from p in bd.db._agendas
                          where p.ID == arg
                          select p).Single();
            bd.db._agendas.DeleteOnSubmit(agenda);
            bd.db.SubmitChanges();

            CarregaForm();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Compromisso deletado!!');", true);

        }
        if (e.CommandName == "updateDate")
        {
            var evento = (from p in bd.db._agendas
                          where p.ID == arg
                          select p).Single();

            lblEventoUpdate.Text = evento._agenda_Evento.descricao + " - " + evento._igreja._municipio.descricao + " - " + evento._igreja.descricao + " - " + evento.dataHora.Value.Date.ToString().Replace(" 00:00:00", "");
            txtDateUpdate.Text = evento.dataHora.Value.Date.ToString().Replace(" 00:00:00", "");
            txtHoraUpdate.Text = evento.dataHora.Value.TimeOfDay.ToString();
            hfIdEventoUpdateDate.Value = evento.ID.ToString();

            fdsDadosEvento.Visible = false;
            fdsDataUpdate.Visible = true;

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Compromisso deletado!!');", true);

        }
    }
    protected void btnCadastrar_Click(object sender, EventArgs e)
    {
        try
        {
            var ag = new _agenda();
            ag.dataHora = DateTime.Parse(txtdata.Text + " " + txtHora.Text);
            ag.encarregadoID = (from p in bd.db._users where p.nome == ddlEncarregado.SelectedValue select p.id).Single();
            ag.eventoID = (from p in bd.db._agenda_Eventos where p.descricao == ddlTIpo.SelectedValue select p.ID).Single();
            ag.igrejaID = (from p in bd.db._igrejas where p.descricao == ddlIgreja.SelectedValue && p._municipio.descricao == ddlCidade.SelectedValue select p.id).Single();
            ag.telefone = txtTel.Text;

            bd.db._agendas.InsertOnSubmit(ag);
            bd.db.SubmitChanges();

            log.AdicionarEntrada(28, usuarioLogado.id, 6, "", 1, 0);

            CarregaForm();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Compromisso cadastrado com sucesso!!');", true);
        }
        catch(Exception er4)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('"+er4.Message+"');", true);
        }
    }
    protected void btnReplicar_Click(object sender, EventArgs e)
    {
        String s = "<script language='javascript'> javascript:abrirPopup('AgendaReplicar.aspx',450,150); </script>";
        Page.RegisterClientScriptBlock("Print", s);
    }
    protected void btnUpdateDate_Click(object sender, EventArgs e)
    {
        try 
        {
            var ag = (from p in bd.db._agendas
                      where p.ID == int.Parse(hfIdEventoUpdateDate.Value)
                      select p).Single();

            ag.dataHora = DateTime.Parse(txtDateUpdate.Text + " " + txtHoraUpdate.Text);

            bd.db.SubmitChanges();

            log.AdicionarEntrada(38, usuarioLogado.id, 6, "", 1, 0);

            CarregaForm();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Data e hora atualizados com sucesso!!');", true);
        }
        catch { }
    }
    protected void btnCancelarUpdateDate_Click(object sender, EventArgs e)
    {
        fdsDadosEvento.Visible = true;
        fdsDataUpdate.Visible = false;
    }
}
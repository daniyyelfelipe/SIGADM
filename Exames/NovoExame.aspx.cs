using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Exames_NovoExame : System.Web.UI.Page
{
    BD bd = new BD();
    _user usuarioLogado;
    Log log = new Log();

    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogado = (_user)Session["usuarioLogado"];

        if (!IsPostBack)
        {
            CarregaForm();
        }
    }
    private void CarregaForm()
    {
        try
        {
            // Desenvolvedor, Regional, anciao, Examinadora e secretario
            if (usuarioLogado.tipoID.Value == 8 || usuarioLogado.tipoID.Value == 1 || usuarioLogado.tipoID.Value == 4 || usuarioLogado.tipoID.Value == 6 || usuarioLogado.tipoID.Value == 9)
            {
                var tipos = (from p in bd.db._exame_tipos
                             select p.descricao).ToList();
                ddlExameTipo.DataSource = tipos;
                ddlExameTipo.DataBind();
            }
            // Encarregado Local
            else if (usuarioLogado.tipoID.Value == 2)
            {
                var tipos = (from p in bd.db._exame_tipos
                             where p.id == 1 || p.id == 3 || p.id == 8
                             select p.descricao).ToList();
                ddlExameTipo.DataSource = tipos;
                ddlExameTipo.DataBind();
            }
            // Instrutoras
            else if (usuarioLogado.tipoID.Value == 12)
            {
                var tipos = (from p in bd.db._exame_tipos
                             where p.id == 1 || p.id == 3 || p.id == 8 || p.id == 9
                             select p.descricao).ToList();
                ddlExameTipo.DataSource = tipos;
                ddlExameTipo.DataBind();
            }

            txtDataAbertura.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtHoraAbertura.Text = DateTime.Now.ToString("h:mm:ss");

            var localidade = (from p in bd.db._igrejas
                         orderby p._municipio.descricao
                         select p._municipio.descricao + " - " + p.descricao).ToList();
            ddlLocalidade.DataSource = localidade;
            ddlLocalidade.DataBind();
            
        }
        catch
        {

        }
    }
    protected void btnAbrirExame_Click(object sender, EventArgs e)
    {
        try
        {
            string[] localidadeSplit = ddlLocalidade.SelectedValue.ToString().Split('-');
            string cidade = localidadeSplit[0].Trim();
            string comum = localidadeSplit[1].Trim();

            var exame = new _exame();
            exame.tipoID = (from p in bd.db._exame_tipos
                            where p.descricao == ddlExameTipo.SelectedValue
                            select p.id).Single();
            exame.dataAbertura = DateTime.Parse(txtDataAbertura.Text);
            exame.horaAbertura = DateTime.Parse(txtHoraAbertura.Text.Trim()).TimeOfDay;
            exame.usuarioID = usuarioLogado.id;
            exame.statusID = 1;
            exame.municipioID = (from p in bd.db._municipios where p.descricao == cidade select p.id).Single();
            exame.igrejaID = (from p in bd.db._igrejas where p.descricao == comum && p._municipio.descricao == cidade select p.id).Single();



            bd.db._exames.InsertOnSubmit(exame);
            bd.db.SubmitChanges();

            log.AdicionarEntrada(43, usuarioLogado.id, 6, "", 1, 0);

            CarregaForm();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Exame aberto com sucesso!!');", true);

        }
        catch { }
    }
}
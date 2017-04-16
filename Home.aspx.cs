using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    BD bd = new BD();
    Log log = new Log();
    App app = new App();
    _user usuarioLogado;

    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogado = (_user)Session["usuarioLogado"];

        if (!IsPostBack)
        {
            CarregaForm();
        }

        GetAgenda();
    }
    private void CarregaEstatisticas()
    {
        //try
        //{
        //    //instrumentos cadastrados
        //    var instCadastrados = (from p in bd.db._bemPatrimonios
        //                           select p).ToList();
        //    lblInstrumentosCadastrados.Text = instCadastrados.Count.ToString();
        //}
        //catch
        //{

        //}
    }
    private void CarregaForm()
    {
        try
        {
            _user usuarioLogado = (_user)Session["usuarioLogado"];

            int postFist = (from p in bd.db._Variaveis where p.descricao == "postFirst" select int.Parse(p.value)).Single();

            var teia = (from p in bd.db._Logs
                        where p.postTipoID == 2 
                        && (p._LogAcao.id == 13 || p._LogAcao.id == 29)
                        && p.visibilidadeID == 1
                        select new
                        {
                            ID = p.id,
                            USUARIO = p._user.nome,
                            PERFIL = "~/perfil/?user=" + p._user.id,
                            DATA = p.dataHora,
                            VISIB = "Visibilidade: " + p._LogVisibilidade.descricao,
                            CURTIR = ((from c in bd.db._LogCurtirs 
                                       where c.logID == p.id                                            
                                       select p).ToList().Count() != 0) ? " | " + (from c in bd.db._LogCurtirs 
                                                                                   where c.logID == p.id
                                                                                   select p).ToList().Count() + " curtiram" : "",
                            CURTIU = string.Join(" - ", (from c in bd.db._LogCurtirs where c.logID == p.id select c._user.nome).ToList()),
                            MENSAGEM = (p.acaoID == 13 || p.acaoID == 29) ? p.mensagem : 
                            (p.acaoID == 17) ? (from c in bd.db._LogCurtirs where c.logID == p.postReferenteID select c._Log.mensagem).Single() 
                            : "Mensagem Bloqueada",
                            ACAO = (p.acaoID == 13) ? " " + p._LogAcao.menssagem + " no dia "
                            + p.dataHora.Value.Day + " do " + p.dataHora.Value.Month
                            + " de " + p.dataHora.Value.Year + " ás "
                            + p.dataHora.Value.Hour + ":"
                            + p.dataHora.Value.Minute + ":"
                            + p.dataHora.Value.Second :
                            (p.acaoID == 17) ? " " + p._LogAcao.menssagem + " de " +
                            (from c in bd.db._LogCurtirs where c.logID == p.postReferenteID select c._Log._user.nome).Single()
                            + " em " + p.dataHora.Value.Day + " do " + p.dataHora.Value.Month
                            + " de " + p.dataHora.Value.Year + " ás "
                            + p.dataHora.Value.Hour + ":"
                            + p.dataHora.Value.Minute + ":"
                            + p.dataHora.Value.Second :
                            (p.acaoID == 29) ? " " + p._LogAcao.menssagem + " no dia "
                            + p.dataHora.Value.Day + " do " + p.dataHora.Value.Month
                            + " de " + p.dataHora.Value.Year + " ás "
                            + p.dataHora.Value.Hour + ":"
                            + p.dataHora.Value.Minute + ":"
                            + p.dataHora.Value.Second
                            : "Post Bloqueado!",
                            VLIKE = (p.usuarioID == usuarioLogado.id) ? false
                            : (p.acaoID == 13) ? true
                            : (p.acaoID == 29) ? true
                            : (p.acaoID == 17) ? false 
                            : false,
                            VCOMENT = (p.acaoID == 13) ? true : (p.acaoID == 17) ? false : false,
                            STYLE = (p.id == postFist) ? "background-color:#C1FFC1;" : "",
                            imgPostVisible = (p.acaoID == 29) ? true : false,
                            imgPostUrl = "~/data/post/" + p.id.ToString() + "/" + p.id.ToString() + ".jpg",
                            imgPostHref = "data/post/" + p.id.ToString() + "/" + p.id.ToString() + ".jpg"
                        }).OrderByDescending(l => (l.ID == postFist ? int.MaxValue : l.ID)).Take(30);

            rpTeia.DataSource = teia;
            rpTeia.DataBind();
        }
        catch
        {

        }
    }
    protected void lkbCurtir_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string id = e.CommandArgument.ToString();

            var curtidas = (from p in bd.db._LogCurtirs
                            where p.logID == int.Parse(id) && p.userID == usuarioLogado.id
                            select p).ToList();
            if (curtidas.Count < 1)
            {

                _LogCurtir curtir = new _LogCurtir();
                curtir.dataHora = app.DateTimeCorrigido();
                curtir.logID = int.Parse(id);
                curtir.userID = usuarioLogado.id;

                bd.db._LogCurtirs.InsertOnSubmit(curtir);
                bd.db.SubmitChanges();

                log.AdicionarEntrada(17, usuarioLogado.id, 1, "", 2, 0);

                CarregaForm();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Você já curtiu esse post!');", true);
            }
        }
        catch
        {

        }
    }

    private void GetAgenda()
    {
        //clAgenda.SelectedDates.Add(DateTime.Parse("01/06/2015"));
        //clAgenda.SelectedDates.Add(DateTime.Parse("10/06/2015"));

        
    }
    protected void clAgenda_SelectionChanged(object sender, EventArgs e)
    {
        //GetAgenda();
    }
    protected void clAgenda_DayRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            var agenda = (from p in bd.db._agendas
                          select p).ToList();

            int[] count = new int[33];
            //int temp = 0;

            if (agenda.Count > 0)
            {

                for (int i = 0; i < agenda.Count; i++)
                {
                    //string a = String.Format("{0:dd/MM/yyyy}", agenda[i].dataHora.Value.Date);

                    if (e.Day.Date == agenda[i].dataHora.Value.Date)
                    {
                        //count[32] = int.Parse(agenda[i].dataHora.Value.Date.Month.ToString());
                        //if(count[32] != temp)
                        //{
                        //    count = new int[33];
                        //}

                        count[int.Parse(e.Day.DayNumberText)]++;
                        string day = e.Day.DayNumberText;

                        e.Cell.Font.Bold = true;
                        e.Cell.Font.Size = 10;
                        e.Cell.ForeColor = System.Drawing.Color.White;

                        if (agenda[i].eventoID == 1)//ensaio regional
                        {
                            e.Cell.BackColor = System.Drawing.Color.Blue;
                            e.Cell.Text = day + "(" + count[int.Parse(e.Day.DayNumberText)].ToString() + ")";
                        }
                        if (agenda[i].eventoID == 2)//ensaio local
                        {
                            if (e.Cell.BackColor == System.Drawing.Color.Blue || e.Cell.BackColor == System.Drawing.Color.Red || e.Cell.BackColor == System.Drawing.Color.Orange)
                            {
                                e.Cell.Text = day + "(" + count[int.Parse(e.Day.DayNumberText)].ToString() + ")";
                            }
                            else
                            {
                                e.Cell.BackColor = System.Drawing.Color.Green;                                
                                e.Cell.Text = day + "(" + count[int.Parse(e.Day.DayNumberText)].ToString() + ")";
                            }
                        }
                        if (agenda[i].eventoID == 3)//reunião
                        {
                            e.Cell.BackColor = System.Drawing.Color.Red;
                            e.Cell.Text = day + "(" + count[int.Parse(e.Day.DayNumberText)].ToString() + ")";
                        }
                        if (agenda[i].eventoID == 4)//exames
                        {
                            e.Cell.BackColor = System.Drawing.Color.Orange;
                            e.Cell.Text = day + "(" + count[int.Parse(e.Day.DayNumberText)].ToString() + ")";
                        }
                        if (agenda[i].eventoID == 5)//treinamentos
                        {
                            e.Cell.BackColor = System.Drawing.Color.Purple;
                            e.Cell.Text = day + "(" + count[int.Parse(e.Day.DayNumberText)].ToString() + ")";
                        }

                        string AM_PM = agenda[i].dataHora.Value.ToLongTimeString().Substring(agenda[i].dataHora.Value.ToLongTimeString().Length - 2);
                        e.Cell.ToolTip += " | " + agenda[i]._agenda_Evento.descricao + " em " + agenda[i]._igreja._municipio.descricao + " - " + agenda[i]._igreja.descricao + " às " + String.Format("{0:HH:mm tt}", agenda[i].dataHora.Value);
                    }
                }

            }
        }
        catch(Exception e31)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('"+e31.Message+"');", true);
        }
    }
}
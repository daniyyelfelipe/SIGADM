using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cadastro_AgendaReplicar : System.Web.UI.Page
{
    BD bd = new BD();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnConfirmarReplicar_Click(object sender, EventArgs e)
    {
        try
        {
            string[] mesReplicar = txtMesReplicar.Text.Split('/');
            string[] mesReceber = txtMesReceber.Text.Split('/');

            var eventosReplicar = (from p in bd.db._agendas
                                   where p.dataHora.Value.Month == int.Parse(mesReplicar[0])
                                   && p.dataHora.Value.Year == int.Parse(mesReplicar[1])
                                   select p).ToList();


            if (eventosReplicar.Count > 0)
            {
                for (int i = 0; i < eventosReplicar.Count; i++)
                {
                    //montar data
                    string dt = eventosReplicar[i].dataHora.Value.Day + "/" + mesReceber[0] + "/" + mesReceber[1] + " " + eventosReplicar[i].dataHora.Value.TimeOfDay;
                    DateTime dataNova = DateTime.Parse(dt);

                    _agenda a = new _agenda();
                    a.dataHora = dataNova;
                    a.encarregadoID = eventosReplicar[i].encarregadoID;
                    a.eventoID = eventosReplicar[i].eventoID;
                    a.igrejaID = eventosReplicar[i].igrejaID;
                    a.telefone = eventosReplicar[i].telefone;

                    bd.db._agendas.InsertOnSubmit(a);
                    bd.db.SubmitChanges();
                }
            }

            txtMesReplicar.Text = string.Empty;
            txtMesReceber.Text = string.Empty;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('"+eventosReplicar.Count+" Compromissos replicados!!');", true);
        }
        catch(Exception er4)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('"+er4.Message+"');", true);
        }
    }
}
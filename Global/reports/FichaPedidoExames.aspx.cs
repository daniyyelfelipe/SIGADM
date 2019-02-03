using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Global_reports_FichaPedidoExames : System.Web.UI.Page
{
    BD bd = new BD();
    int alunoID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        alunoID = int.Parse(Request.QueryString["alunoID"].ToString());
        CarregaForm();
    }

    void CarregaForm()
    {
        try
        {
            var aluno = (from p in bd.db._alunos
                         where p.id == alunoID
                         select p).Single();

            var exame = (from p in bd.db._exame_lancamentos
                         where p.alunoID == alunoID
                         && p._exame.statusID == 1
                         select p).Single();

            lblData.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now.Date);
            lblNome.Text = aluno.nome;
            lblCasaOracao.Text = aluno._igreja.descricao + " - " + aluno._igreja._municipio.descricao;
            lblInstrumento.Text = aluno._instrumento.descricao;


        }
        catch
        {

        }
    }
}
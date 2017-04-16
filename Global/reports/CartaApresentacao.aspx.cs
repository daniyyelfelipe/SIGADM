using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Global_reports_CartaApresentacao : System.Web.UI.Page
{
    BD bd = new BD();
    protected void Page_Load(object sender, EventArgs e)
    {
        CarregaForm();
    }
    private void CarregaForm()
    {
        try
        {
            int alunoID = int.Parse(Request.QueryString["id"].ToString());

            var aluno = (from p in bd.db._alunos
                         where p.id == alunoID
                         select p).Single();

            lblNome.Text = aluno.nome;
            if (aluno.dataNascimento != null)
            {
                lblDataNascimento.Text = String.Format("{0:dd/MM/yyyy}", aluno.dataNascimento.Value.Date);
            }
            if (aluno.batizado.Value.ToString() == "True") { lblBatizado.Text = "Sim"; } else { lblBatizado.Text = "Não"; }
            lblEndereco.Text = aluno.endereco;
            lblBairro.Text = aluno.bairro;
            lblCidade.Text = aluno._igreja._municipio.descricao;
            lblComum.Text = aluno._igreja.descricao;
            lblInstrumento.Text = aluno._instrumento.descricao;
            if(aluno.temiInstrumento.Value.ToString() == "True"){lblPossuiInstrumento.Text = "Sim";}else{lblPossuiInstrumento.Text = "Não";}
            lblTelefone.Text = aluno.telefone;
            lblEmail.Text = aluno.email;
            lblDataImpressao.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now.Date);
        }
        catch
        {

        }
    }
}
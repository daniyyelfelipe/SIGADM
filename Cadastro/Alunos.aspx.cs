using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cadastro_Alunos : System.Web.UI.Page
{
    BD bd = new BD();
    Log log = new Log();
    Guard guard = new Guard();
    _user usuarioLogado;

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

            //var alunos = (from p in bd.db._alunos
            //              join m in bd.db._igrejas
            //              on p._igreja.id equals m.id
            //              select new
            //              {
            //                  ID = p.id,
            //                  NOME = p.nome,
            //                  COMUM = p._igreja.descricao,
            //                  CIDADE = m._municipio.descricao,
            //                  INSTRUMENTO = p._instrumento.descricao,
            //                  EMAIL = p.email,
            //                  TELEFONE = p.telefone

            //              }).OrderBy(x => x.CIDADE).ToList();
            //gvAlunos.DataSource = alunos;
            //gvAlunos.DataBind();

            var alunos = (from p in bd.db._alunos                          
                          select p.id).ToList();

            lblTotalCandidatos.Text = "Total de Candidatos cadastrados: " + alunos.Count() + " | Utilize a pesquisa para visualizar os registros.";
        }
        catch(Exception er1)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('"+er1.Message+"');", true);
        }
    }

    
    protected void gvAlunos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int alunoID = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "Excluir")
            {
                var consulta = (from p in bd.db._GEM_Matriculas
                                where p.alunoID == alunoID && p._GEM.status == 2
                                select p).ToList();
                if (consulta.Count > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('O aluno não pode ser deletado, pois possui matricula ativa na GEM id: " + consulta[0].gemID.ToString() + ". Consulte o suporte para mais informações!');", true);
                }
                else
                {

                    var aluno = (from p in bd.db._alunos
                                 where p.id == alunoID
                                 select p).Single();
                    var usu = (from p in bd.db._users
                               where p.email == aluno.email
                               select p).Single();

                    bd.db._alunos.DeleteOnSubmit(aluno);
                    bd.db._users.DeleteOnSubmit(usu);
                    bd.db.SubmitChanges();

                    CarregaForm();

                    log.AdicionarEntrada(27, usuarioLogado.id, 6, "", 1, 0);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Candidato deletado com sucesso!');", true);
                }
            }
            if (e.CommandName == "editar")
            {
                String s = "<script language='javascript'> javascript:abrirCadastroCandidato('NovoAluno.aspx?editMode=1&alunoId=" + alunoID + "'); </script>";
                Page.RegisterClientScriptBlock("Print", s);
            }
            else if (e.CommandName == "carta")
            {
                String s = "<script language='javascript'> javascript:abrirA4('../global/reports/CartaApresentacao.aspx?id=" + e.CommandArgument.ToString() + "'); </script>";
                Page.RegisterClientScriptBlock("Print", s);
            }
            else if (e.CommandName == "historico")
            {
                String s = "<script language='javascript'> javascript:abrirA4('../global/reports/HistoricoAluno.aspx?id=" + e.CommandArgument.ToString() + "'); </script>";
                Page.RegisterClientScriptBlock("Print", s);
            }
        }
        catch (Exception er2)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + er2.Message + "');", true);
        }
    }
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        String s = "<script language='javascript'> javascript:abrirCadastroCandidato('NovoAluno.aspx'); </script>";
        Page.RegisterClientScriptBlock("Print", s);
    }
    protected void btnPesquisa_Click(object sender, EventArgs e)
    {
        try
        {
            var alunos = (from p in bd.db._alunos
                          join m in bd.db._igrejas
                          on p._igreja.id equals m.id
                          select new
                          {
                              ID = p.id,
                              NOME = p.nome,
                              COMUM = p._igreja.descricao,
                              CIDADE = m._municipio.descricao,
                              INSTRUMENTO = p._instrumento.descricao,
                              EMAIL = p.email,
                              TELEFONE = p.telefone

                          }).OrderBy(x => x.CIDADE).ToList();


            var filter = alunos;

            if(!string.IsNullOrEmpty(txtNomeP.Text))
            {
                filter = filter.Where(x => x.NOME.Contains(txtNomeP.Text.Trim().ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(txtEmailP.Text))
            {
                filter = filter.Where(x => x.EMAIL.ToLower().Contains(txtEmailP.Text.Trim().ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(txtComumP.Text))
            {
                filter = filter.Where(x => x.COMUM.ToUpper().Contains(txtComumP.Text.Trim().ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(txtCidadeP.Text))
            {
                filter = filter.Where(x => x.CIDADE.ToUpper().Contains(txtCidadeP.Text.Trim().ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(txtInstrumentoP.Text))
            {
                filter = filter.Where(x => x.INSTRUMENTO.ToUpper().Contains(txtInstrumentoP.Text.Trim().ToUpper())).ToList();
            }

            gvAlunos.DataSource = filter;
            gvAlunos.DataBind();


            lblTotalCandidatos.Text = "Total de Candidatos encontrados na Pesquisa: " + filter.Count();
        }
        catch
        {

        }
    }
    protected void gvAlunos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int alunoID = int.Parse(e.Row.Cells[4].Text);

            var matriculado = (from p in bd.db._GEM_Matriculas
                               where p.alunoID == alunoID
                               select p).ToList();

            if (matriculado.Count > 0)
            {
                e.Row.BackColor = Color.GreenYellow;
            }
            else
            {
                e.Row.BackColor = Color.LightGray;
            }
        }
    }
}
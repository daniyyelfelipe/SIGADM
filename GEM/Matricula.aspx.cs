using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GEM_Matricula : System.Web.UI.Page
{
    int idGem = 0;
    BD bd = new BD();
    Log log = new Log();
    Guard guard = new Guard();
    _user usuarioLogado;
    App app = new App();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            idGem = int.Parse(Request.QueryString["idg"].ToString());

            if(!IsPostBack)
            {
                CarregaForm();
            }
        }
        catch
        {

        }
    }
    private void CarregaForm()
    {
        try
        {
            var gem = (from p in bd.db._GEMs
                       where p.Id == idGem
                       select p).Single();
            

            var alunos = (from p in bd.db._alunos
                          join m in bd.db._GEM_Matriculas
                          on p.id equals m.alunoID
                          where m.gemID == idGem && m.statusID == 1
                          select new
                          {
                              ID = p.id,
                              MATRICULA = p.matricula,
                              NOME = p.nome,
                              COMUM = p._igreja.descricao,
                              CIDADE = p._igreja._municipio.descricao,
                              INSTRUMENTO = p._instrumento.descricao,
                              EMAIL = p.email,
                              TELEFONE = p.telefone
                          }).ToList();
            gvMatriculados.DataSource = alunos;
            gvMatriculados.DataBind();

            lblGem.Text = gem._municipio.descricao + " | " + gem._igreja.descricao + " | " + gem._GEM_Periodo.descricao + " | Turma " + gem._GEM_Turma.descricao + " | " + "Total: " + alunos.Count.ToString();
            lblGem1.Text = gem._municipio.descricao + " | " + gem._igreja.descricao + " | " + gem._GEM_Periodo.descricao + " | Turma " + gem._GEM_Turma.descricao;


        }
        catch
        {

        }
    }
    protected void btnPesquisa_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClientScript.RegisterStartupScript(GetType(), "alerta", "confirmar();", true);
            var alunos = (from p in bd.db._alunos
                          join u in bd.db._users
                          on p.userID equals u.id
                          join m in bd.db._igrejas
                          on p._igreja.id equals m.id
                          where p.nome.Contains(txtNome.Text)
                          && u.statusID == 1
                          select new
                          {
                              ID = p.id,
                              MATRICULA = p.matricula,
                              NOME = p.nome,
                              COMUM = p._igreja.descricao,
                              CIDADE = m._municipio.descricao,
                              INSTRUMENTO = p._instrumento.descricao                              
                          }).ToList();
            gvPesquisa.DataSource = alunos;
            gvPesquisa.DataBind();
        }
        catch (Exception e3)
        {
            ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('" + e3.Message + "');", true);
        }
    }
    protected void gvPesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if(e.CommandName == "Matricular")
            {
                int alunoID = int.Parse(e.CommandArgument.ToString());

                var consulta = (from p in bd.db._GEM_Matriculas
                                where p.alunoID == alunoID && p._GEM.status == 2 //verifica se o aluno já está matriculado em uma GEM ativa
                                select p).ToList();
                if (consulta.Count > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Aluno já matriculado na GEM id: "+consulta[0].gemID.ToString()+". Consulte o suporte para mais informações!');", true);
                }
                else
                {
                    var matricula = new _GEM_Matricula();
                    matricula.alunoID = alunoID;
                    matricula.data = app.DateTimeCorrigido();
                    matricula.gemID = idGem;
                    matricula.statusID = 1;

                    bd.db._GEM_Matriculas.InsertOnSubmit(matricula);

                    var gem = (from p in bd.db._GEMs
                               where p.Id == idGem
                               select p).Single();
                    gem.status = 2;

                    var aluno = (from p in bd.db._alunos
                                 where p.id == alunoID
                                 select p).Single();

                    if (aluno.matricula == null)
                    {
                        aluno.matricula = GeraMatricula(idGem, gem.periodoID.Value, alunoID).ToString();
                    }

                    bd.db.SubmitChanges();

                    //lança no log
                    _user usuarioLogado = (_user)Session["usuarioLogado"];
                    log.AdicionarEntrada(26, usuarioLogado.id, 6, "", 1, 0);

                    CarregaForm();

                    txtNome.Text = string.Empty;
                    gvPesquisa.DataSource = null;
                    gvPesquisa.DataBind();

                    ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Matrícula realizada com sucesso!');", true);
                }
            }
        }
        catch (Exception e3)
        {
            ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('" + e3.Message + "');", true);
        }
    }
    protected void gvMatriculados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int alunoID = int.Parse(e.CommandArgument.ToString());
            if(e.CommandName == "excluir")
            {
                ////!!!! AQUI FALTA CHECAR SE O ALUNO PODE OU NÃO SER DELETADO!!!

                var matricula = (from p in bd.db._GEM_Matriculas
                                 where p.alunoID == alunoID && p.gemID == idGem
                                 select p).Single();

                bd.db._GEM_Matriculas.DeleteOnSubmit(matricula);
                bd.db.SubmitChanges();

                CarregaForm();

                ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Matrícula deletada com sucesso!');", true);
            }
        }
        catch
        {

        }
    }

    private int GeraMatricula(int _gemID, int _periodoID, int _alunoID)
    {
        try
        {
            var matriculas = (from p in bd.db._GEM_Matriculas
                              select p).ToList().Count();

            string nova = DateTime.Now.Year.ToString() + "0" + _periodoID.ToString() + "0" + (matriculas + 1).ToString();
            return int.Parse(nova);
        }
        catch
        {
            return 0;
        }
    }
}
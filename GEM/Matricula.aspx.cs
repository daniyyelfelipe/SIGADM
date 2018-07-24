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

            //links dos botões de voltar
            hlBackMatricula.NavigateUrl = "~/gem/Matricula.aspx?idg=" + idGem;

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

                //lança no log
                _user usuarioLogado = (_user)Session["usuarioLogado"];
                log.AdicionarEntrada(50, usuarioLogado.id, 6, "", 1, 0);

                CarregaForm();

                ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Matrícula deletada com sucesso!');", true);
            }
            if (e.CommandName == "transferir")
            {
                var aluno = (from p in bd.db._alunos
                             where p.id == alunoID
                             select p).Single();

                var gemAtual = (from p in bd.db._GEM_Matriculas
                                where p.alunoID == alunoID
                                && p.statusID == 1
                                select p._GEM._municipio.descricao + " - " + p._GEM._igreja.descricao + " - " + p._GEM._instrumentoNipe.descricao + " - " + p._GEM._GEM_Periodo.descricao).Single();

                lblAlunoId.Text = alunoID.ToString();
                lblNomeAluno.Text = aluno.nome;
                lblGemAtual.Text = gemAtual;
                CarregaGensDdl();


                fdsPesquisa.Visible = false;
                fdsAlunosMatriculados.Visible = false;
                fdsTransferir.Visible = true;
                hlBackMatricula.Visible = true;
            }
        }
        catch(Exception e4)
        {
            ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('"+e4.Message+"');", true);
        }
    }

    private void CarregaGensDdl()
    {
        var gens = (from p in bd.db._GEMs
                    orderby p._municipio.descricao ascending
                    select p._municipio.descricao + " - " + p._igreja.descricao + " - " + p._instrumentoNipe.descricao + " - " + p._GEM_Periodo.descricao).ToList();
        ddlNovaGem.DataSource = gens;
        ddlNovaGem.DataBind();
    }

    private void TransferirAluno()
    {
        try
        {
            string novaGemDdl = ddlNovaGem.Text;
            string[] novagenDdlSplit = novaGemDdl.Split('-');
            string municipio = novagenDdlSplit[0].Trim();
            string igreja = novagenDdlSplit[1].Trim();
            string nipe = novagenDdlSplit[2].Trim();
            string periodo = novagenDdlSplit[3].Trim();

            int alunoID = int.Parse(lblAlunoId.Text);
            ////!!!! AQUI FALTA CHECAR SE O ALUNO PODE OU NÃO SER TRANSFERIDO!!!


            var novaGem = (from p in bd.db._GEMs
                           where p._municipio.descricao == municipio
                           && p._igreja.descricao == igreja
                           && p._instrumentoNipe.descricao == nipe
                           && p._GEM_Periodo.descricao == periodo
                           select p).Single();

            //checa se esse aluno já esteve nessa GEMM
            var matriculaAnteriores = (from p in bd.db._GEM_Matriculas
                                       where p.alunoID == alunoID 
                                       && p.gemID == novaGem.Id
                                       && p.statusID == 5
                                       select p).ToList();

            var matricula = (from p in bd.db._GEM_Matriculas
                             where p.alunoID == alunoID
                             && p.gemID == idGem
                             && p.statusID == 1
                             select p).Single();


            if (matriculaAnteriores.Count() > 0)
            {
                //aluno já esteve nessa gem

                matricula.statusID = 5; //muda o status da matricula atual para matricula transferida  

                var matriculaAnteriores2 = (from p in bd.db._GEM_Matriculas
                                       where p.alunoID == alunoID 
                                       && p.gemID == novaGem.Id
                                       && p.statusID == 5
                                       select p).Single();

                matriculaAnteriores2.statusID = 1;

                bd.db.SubmitChanges();
                
                //lança no log
                _user usuarioLogado = (_user)Session["usuarioLogado"];
                log.AdicionarEntrada(51, usuarioLogado.id, 6, "", 1, 0);

                CarregaForm();

                fdsPesquisa.Visible = true;
                fdsAlunosMatriculados.Visible = true;
                fdsTransferir.Visible = false;
                hlBackMatricula.Visible = false;

                ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Matrícula transferida com sucesso!');", true);
            }
            else
            {
                matricula.statusID = 5; //muda o status da matricula atual para matricula transferida              


                //gerar nova matricula
                var novaMatricula = new _GEM_Matricula();
                novaMatricula.alunoID = alunoID;
                novaMatricula.data = app.DateTimeCorrigido();
                novaMatricula.gemID = novaGem.Id;
                novaMatricula.statusID = 1;

                bd.db._GEM_Matriculas.InsertOnSubmit(novaMatricula);


                bd.db.SubmitChanges();


                //lança no log
                _user usuarioLogado = (_user)Session["usuarioLogado"];
                log.AdicionarEntrada(51, usuarioLogado.id, 6, "", 1, 0);

                CarregaForm();

                fdsPesquisa.Visible = true;
                fdsAlunosMatriculados.Visible = true;
                fdsTransferir.Visible = false;
                hlBackMatricula.Visible = false;

                ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Matrícula transferida com sucesso!');", true);
            }
        }
        catch(Exception e3)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + e3.Message + "');", true);
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
    protected void btnConfirmarTransferencia_Click(object sender, EventArgs e)
    {
       TransferirAluno();        
    }
}
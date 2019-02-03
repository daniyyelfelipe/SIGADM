using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Exames_IncluirAluno : System.Web.UI.Page
{
    BD bd = new BD();
    _user usuarioLogado;
    Log log = new Log();

    int exameID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogado = (_user)Session["usuarioLogado"];

        exameID = int.Parse(Request.QueryString["exameid"]);

        if (!IsPostBack)
        {
            CarregaAlunosIncluidos();
        }
    }
    protected void gvPesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "incluir")
            {
                //verifica se aluna ja esta nesse exame
                var verifica = (from p in bd.db._exame_lancamentos
                                where p.alunoID == int.Parse(e.CommandArgument.ToString())
                                && p.exameID == exameID
                                select p).ToList();

                //verifica se o aluno esta em outro exame ativo
                var verifica2 = (from p in bd.db._exame_lancamentos
                                where p.alunoID == int.Parse(e.CommandArgument.ToString())
                                && p.exameID != exameID
                                && p._exame.statusID == 1
                                select p).ToList();


                if (verifica.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Aluno já incluído anteriormente nesse mesmo exame!!');", true);
                    
                }
                else if(verifica2.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Aluno já incluído anteriormente em outro exame que ainda não foi concluído!!');", true);
                }
                else
                {
                    var aluno = (from p in bd.db._alunos
                                 where p.id == int.Parse(e.CommandArgument.ToString())
                                 select p).Single();

                    _exame_lancamento inclusao = new _exame_lancamento();
                    inclusao.exameID = exameID;
                    inclusao.alunoID = aluno.id;
                    inclusao.usuarioIDInclusao = usuarioLogado.id;

                    bd.db._exame_lancamentos.InsertOnSubmit(inclusao);
                    bd.db.SubmitChanges();

                    log.AdicionarEntrada(44, usuarioLogado.id, 6, "", 1, 0);

                    CarregaAlunosIncluidos();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Aluno incluído com sucesso!!');", true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "ReloadParent();", true);
                }

            }
        }
        catch { }
    }
    protected void gvIncluidos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "excluir")
        {
            //alguma verificação vai ser feita aqui!!
            var verifica = (from p in bd.db._exame_lancamentos
                            where p.alunoID == int.Parse(e.CommandArgument.ToString())
                            && p.exameID == exameID
                            select p).ToList();

            if (1 > 2)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Aluno não pode ser excluido!!');", true);
            }
            else
            {
                var aluno = (from p in bd.db._exame_lancamentos
                             where p.alunoID == int.Parse(e.CommandArgument.ToString())
                             select p).Single();

                bd.db._exame_lancamentos.DeleteOnSubmit(aluno);
                bd.db.SubmitChanges();

                log.AdicionarEntrada(49, usuarioLogado.id, 6, "", 1, 0);

                CarregaAlunosIncluidos();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Aluno excluido do exame com sucesso!!');", true);
            }

        }
    }
    protected void btnPesquisa_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            var alunos = (from p in bd.db._alunos
                          join u in bd.db._users
                          on p.userID equals u.id
                          join m in bd.db._igrejas
                          on p._igreja.id equals m.id
                          join g in bd.db._GEM_Matriculas
                          on p.id equals g.alunoID
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
        catch { }
    }
    private void CarregaAlunosIncluidos()
    {
        try
        {
            var alunos = (from p in bd.db._alunos
                          join e in bd.db._exame_lancamentos
                          on p.id equals e.alunoID
                          join m in bd.db._igrejas
                          on p._igreja.id equals m.id
                          where e.exameID == exameID
                          select new
                          {
                              ID = p.id,
                              NOME = p.nome,
                              INSTRUMENTO = p._instrumento.descricao,
                              COMUM = p._igreja.descricao,
                              CIDADE = m._municipio.descricao

                          }).ToList();

            gvIncluidos.DataSource = alunos;
            gvIncluidos.DataBind();
        }
        catch { }
    }
}
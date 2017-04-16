using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Estatisticas_Default : System.Web.UI.Page
{
    int idGem = 0;
    BD bd = new BD();
    Log log = new Log();
    Guard guard = new Guard();
    _user usuarioLogado;
    App app = new App();
    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogado = (_user)Session["usuarioLogado"];
        carregaForma();

        Usuarios();
        Acoes();
        GemsAtivas();
        CandidatosCidade();
        CandidatosInstrumento();
        AlunosInstrumento();
        
    }
    private void carregaForma()
    {
        try
        {
            //var alunosCadastrados = (from p in bd.db._alunos
            //                         select p).ToList().Count();
            //lblAlunosCadastrados.Text += alunosCadastrados.ToString();

            //var alunosMatriculados = (from m in bd.db._GEM_Matriculas
            //                          join a in bd.db._alunos
            //                              on m.alunoID equals a.id
            //                          where m.statusID == 1
            //                          select m).ToList().Count();
            //lblAlunosMatriculados.Text += alunosMatriculados;

            //var gemsCadastradas = (from p in bd.db._GEMs
            //                       select p).ToList().Count();
            //lblGemsCadastradas.Text += gemsCadastradas;

            //var encarregadosRegionaisCadastrados = (from p in bd.db._users
            //                                        where p.tipoID == 1
            //                                        select p).ToList().Count();
            //lblEncarregadosRegionaisCadastrados.Text += encarregadosRegionaisCadastrados;

            //var encarregadosLocaisCadastrados = (from p in bd.db._users
            //                                        where p.tipoID == 2
            //                                        select p).ToList().Count();
            //lblEncarregadosLocaisCadastrados.Text += encarregadosLocaisCadastrados;

            //var instrutoresCadastrados = (from p in bd.db._users
            //                                     where p.tipoID == 3
            //                                     select p).ToList().Count();
            //lblInstrutoresCadastrados.Text += instrutoresCadastrados;

            //var totalUsuariosCadastrados = (from p in bd.db._users
            //                                        select p).ToList().Count();
            //lblTotalUsuarios.Text += totalUsuariosCadastrados;
        }
        catch
        {

        }
    }

    private void Acoes()
    {
        DateTime dtInicial;
        DateTime dtFinal;

        var acoes = (from l in bd.db._Logs
                     join u in bd.db._users
                     on l.usuarioID equals u.id
                     join a in bd.db._LogAcaos
                     on l.acaoID equals a.id
                     join v in bd.db._LogVisibilidades
                     on l.visibilidadeID equals v.id
                     join p in bd.db._LogPostTipos
                     on l.postTipoID equals p.id
                     where l.dataHora.Value.Month == DateTime.Now.Month
                     && l.dataHora.Value.Year == DateTime.Now.Year
                     group new { l, a } by new
                     {
                         acao = a.descricao

                     } into g
                     orderby g.Key.acao ascending
                     select new
                     {
                         Acao = g.Key.acao,
                         Quantidade = g.Count(p => p.a.descricao != null)
                     }).ToList();

        gvAcoes.DataSource = acoes;
        gvAcoes.DataBind();
    }

    private void Usuarios()
    {
        var usuarios = (from u in bd.db._users
                        where u.statusID == 1
                     group new { u } by new
                     {
                         tipo = u._userTipo.descricao

                     } into g
                     orderby g.Key.tipo ascending
                     select new
                     {
                         Tipo = (g.Key.tipo == "Aluno")? "Candidatos" : g.Key.tipo,
                         Quantidade = g.Count(p => p.u.id != null)
                     }).ToList();

        gvUsuarios.DataSource = usuarios;
        gvUsuarios.DataBind();
    }

    private void GemsAtivas()
    {
        var usuarios = (from g1 in bd.db._GEMs
                        join g2 in bd.db._GEM_Matriculas
                        on g1.Id equals g2.gemID
                        where g1.status == 2
                        group new { g1, g2 } by new
                        {
                            gem = g1._municipio.descricao + " - " + g1._igreja.descricao

                        } into g
                        orderby g.Key.gem ascending
                        select new
                        {
                            GEM = g.Key.gem,
                            Alunos = g.Count(p => p.g2.alunoID != null)
                        }).ToList();

        gvGemAtiva.DataSource = usuarios;
        gvGemAtiva.DataBind();
    }
    private void CandidatosCidade()
    {
        var usuarios = (from a in bd.db._alunos
                        join c in bd.db._municipios
                        on a.cidade equals c.descricao
                        group new { a, c } by new
                        {
                            cidade = a.cidade

                        } into g
                        orderby g.Key.cidade ascending
                        select new
                        {
                            Cidade = g.Key.cidade,
                            Candidatos = g.Count(p => p.a.id != null)
                        }).ToList().OrderByDescending(x => x.Candidatos);

        gvCandidatosCidade.DataSource = usuarios;
        gvCandidatosCidade.DataBind();
    }
    private void CandidatosInstrumento()
    {
        var usuarios = (from a in bd.db._alunos
                        join i in bd.db._instrumentos
                        on a.instrumentoID equals i.id
                        group new { a, i } by new
                        {
                            instrumento = i.descricao

                        } into g
                        orderby g.Key.instrumento ascending
                        select new
                        {
                            Instrumento = g.Key.instrumento,
                            Candidatos = g.Count(p => p.a.id != null)
                        }).ToList().OrderByDescending(x => x.Candidatos);

        gvCandidatosInstrumento.DataSource = usuarios;
        gvCandidatosInstrumento.DataBind();
    }
    private void AlunosInstrumento()
    {
        var usuarios = (from a in bd.db._alunos
                        join i in bd.db._instrumentos
                        on a.instrumentoID equals i.id
                        join g in bd.db._GEM_Matriculas
                        on a.id equals g.alunoID
                        group new { a, i } by new
                        {
                            instrumento = i.descricao

                        } into g
                        orderby g.Key.instrumento ascending
                        select new
                        {
                            Instrumento = g.Key.instrumento,
                            Alunos = g.Count(p => p.a.id != null)
                        }).ToList().OrderByDescending(x => x.Alunos);

        gvAlunosInstrumento.DataSource = usuarios;
        gvAlunosInstrumento.DataBind();


        var matriculados = (from a in bd.db._alunos
                            join g in bd.db._GEM_Matriculas
                            on a.id equals g.alunoID
                            select a).ToList();

        lblTotalMatriculados.Text = matriculados.Count.ToString();
    }
}
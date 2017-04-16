using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Global_reports_HistoricoAluno : System.Web.UI.Page
{
    int alunoID = 0;
    BD bd = new BD();
    protected void Page_Load(object sender, EventArgs e)
    {
        alunoID = int.Parse(Request.QueryString["id"].ToString());

        CarregaDados();
    }

    private void CarregaDados()
    {
        try
        {
            var aluno = (from p in bd.db._alunos
                         where p.id == alunoID
                         select p).Single();

            var lastGemMatricula = (from p in bd.db._GEM_Matriculas
                                 where p.alunoID == alunoID
                                 && p.statusID == 1
                                 select p.gemID).ToList();
            

            imgFotoAluno.ImageUrl = "~/data/user/" + aluno.userID.Value.ToString() + "/img/perfil.jpg";

            //dados pessoais
            //-----------------------------------------------------------------------------------
            lblNome.Text = aluno.nome;
            if (aluno.dataNascimento != null)
            {
                lblDataNasc.Text = String.Format("{0:dd/MM/yyyy}", aluno.dataNascimento.Value.Date);
            }
            lblNomeMae.Text = "---";
            lblNomePai.Text = "---";
            lblEndereco.Text = aluno.endereco;
            lblBairro.Text = aluno.bairro;
            lblMunicipio.Text = aluno.cidade;
            lblComum.Text = (from p in bd.db._igrejas where p.id == aluno.comumIgrejaID select p.descricao).Single();


            //informacoes gerais
            //-----------------------------------------------------------------------------------
            lblInstrumento.Text = (from p in bd.db._instrumentos where p.id == aluno.instrumentoID select p.descricao).Single();
            lblAnoInicial.Text = "---";
            lblAnoFinal.Text = "---";

            if (lastGemMatricula.Count > 0)
            {

                var lastGem = (from p in bd.db._GEMs
                               where p.Id == lastGemMatricula[0]
                               select p).Single();

                lblUltimaGem.Text = lastGem._igreja.descricao + " - " + lastGem._municipio.descricao;
                lblInstrutor.Text = lastGem._user.nome;
                lblEncarregadoLocal.Text = lastGem._user1.nome;
                lblExaminadora.Text = "---";
                lblEncarregadoRegional.Text = lastGem._user2.nome;


                //hinos passados
                //-----------------------------------------------------------------------------------
                lblHinosSoprano.Text = (from p in bd.db._GEM_Academicos
                                        where p.alunoID == alunoID
                                        && p.vozID == 1
                                        select p).ToList().Count().ToString();
                lblHinosContralto.Text = (from p in bd.db._GEM_Academicos
                                          where p.alunoID == alunoID
                                          && p.vozID == 2
                                          select p).ToList().Count().ToString();
                lblHinosTenor.Text = (from p in bd.db._GEM_Academicos
                                      where p.alunoID == alunoID
                                      && p.vozID == 3
                                      select p).ToList().Count().ToString();
                lblHinosBaixo.Text = (from p in bd.db._GEM_Academicos
                                      where p.alunoID == alunoID
                                      && p.vozID == 4
                                      select p).ToList().Count().ToString();
                lblHinosPedaleira.Text = (from p in bd.db._GEM_Academicos
                                          where p.alunoID == alunoID
                                          && p.vozID == 5
                                          select p).ToList().Count().ToString();

                //presenca na gem
                //-----------------------------------------------------------------------------------
                //pegando presencas de todas as gens que ele frequentou
                lblPresencas.Text = (from p in bd.db._GEM_Presencas
                                     where p.alunoID == alunoID
                                     select p).ToList().Count().ToString();
                //pegando faltas apenas da ultima gem frequantada
                lblFaltas.Text = ((from p in bd.db._GEM_Presencas
                                   where p.gemID == lastGem.Id
                                   select p.dataGem).Distinct().ToList().Count() - int.Parse(lblPresencas.Text)).ToString();


                //indices gerados pelo sistema
                //-----------------------------------------------------------------------------------

                //porcentagem de presenca da ultima gem
                decimal totalAulas = (from p in bd.db._GEM_Presencas
                                      where p.gemID == lastGem.Id
                                      select p.dataGem).Distinct().ToList().Count();
                decimal presenca = int.Parse(lblPresencas.Text);
                decimal faltas = int.Parse(lblFaltas.Text);
                decimal indicePresenca = Decimal.Round(((presenca * 100) / totalAulas), 2);

                lblIndicePresencas.Text = indicePresenca.ToString() + "%";

                //porcentagem hinos passados s/c/t/b
                decimal totalHinos = 480;
                decimal totalS = int.Parse(lblHinosSoprano.Text);
                decimal totalC = int.Parse(lblHinosContralto.Text);
                decimal totalT = int.Parse(lblHinosTenor.Text);
                decimal totalB = int.Parse(lblHinosBaixo.Text);
                decimal totalP = int.Parse(lblHinosPedaleira.Text);

                decimal indiceS = Decimal.Round(((totalS * 100) / totalHinos), 2);
                decimal indiceC = Decimal.Round(((totalC * 100) / totalHinos), 2);
                decimal indiceT = Decimal.Round(((totalT * 100) / totalHinos), 2);
                decimal indiceB = Decimal.Round(((totalB * 100) / totalHinos), 2);
                decimal indiceP = Decimal.Round(((totalP * 100) / totalHinos), 2);

                lblIndiceHinosSoprano.Text = indiceS.ToString() + "%";
                lblIndiceHinosContralto.Text = indiceC.ToString() + "%";
                lblIndiceHinosTenor.Text = indiceT.ToString() + "%";
                lblIndiceHinosBaixo.Text = indiceB.ToString() + "%";
                lblIndiceHinosPedaleira.Text = indiceP.ToString() + "%";
            }
            else
            {
                dvDadosAcademicos.Visible = false;
                lblUltimaGem.Text = "ALUNO AINDA NÃO MATRICULADO!";
            }
        }
        catch { }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Exames_LancarResultado : System.Web.UI.Page
{
    BD bd = new BD();
    _user usuarioLogado;
    Log log = new Log();

    int exameID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogado = (_user)Session["usuarioLogado"];

        exameID = int.Parse(Request.QueryString["exameid"]);

        GetDadosExame();

        if (!IsPostBack)
        {
            GetAlunosExame(exameID);
        }



        //link do botao voltar
        hlBackResultado.NavigateUrl = "LancarResultado.aspx?exameid=" + exameID;
        
    }

    void GetDadosExame()
    {
        try
        {
            var exame = (from p in bd.db._exames
                         where p.id == exameID
                         select p).Single();

            lblStatusExame.Text = exame._exame_status.descricao;
        }
        catch
        {

        }
    }
    void GetAlunosExame(int exameID)
    {
        try
        {
            var alunos = (from p in bd.db._alunos
                          join u in bd.db._exame_lancamentos
                              on p.id equals u.alunoID
                          where u.exameID == exameID
                          select new 
                          {
                              ALUNO = p.id,
                              NOME = p.nome,
                              INSTRUMENTO = p._instrumento.descricao,
                              RESULTADO = u._exame_tipo_resultado.descricao
                          
                          });

            gvAlunosAvaliados.DataSource = alunos;
            gvAlunosAvaliados.DataBind();

            lblQuantAlunos.Text = alunos.Count().ToString() + " ";
        }
        catch
        {
            
        }
    }
    protected void gvAlunosAvaliados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "resultados")
            {

                var aluno = (from p in bd.db._alunos
                             where p.id == int.Parse(e.CommandArgument.ToString())
                             select p).Single();

                var resultadosTipo = (from p in bd.db._exame_tipo_resultados
                                      select p.descricao).ToList();

                var resultado = (from p in bd.db._exame_lancamentos
                                  where p.alunoID == aluno.id
                                  && p._exame.statusID == 1
                                  select p).Single();

                lblIdAlunoLancamento.Text = aluno.id.ToString();
                lblAlunoLancamento.Text = " - " + aluno.nome;
                ddlResultado.DataSource = resultadosTipo;
                ddlResultado.DataBind();


                //verifica se ja existe um resultado lancado e exibe os dados
                if (resultado.resultadoID != null)
                {
                    ddlResultado.Text = resultado._exame_tipo_resultado.descricao;
                    txtObs.Text = resultado.obs;
                }
                
                

                fdsAlunosAvaliacao.Visible = false;
                fdsDadosExame.Visible = false;
                fdsAlunoResultado.Visible = true;
                divHlBackResultado.Visible = true;



            }
            else if (e.CommandName == "ficha")
            {
                String s = "<script language='javascript'> javascript:abrirA4('../Global/reports/FichaPedidoExames.aspx?alunoID=" + e.CommandArgument.ToString() + "'); </script>";
                Page.RegisterClientScriptBlock("Print", s);
            }
        }
        catch(Exception e2)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + e2.Message + "');", true);
        }
    }
    protected void btlLancarResultadoAluno_Click(object sender, EventArgs e)
    {
        try
        {
            var exameLancamento = (from p in bd.db._exame_lancamentos
                                   where p.alunoID == int.Parse(lblIdAlunoLancamento.Text)
                                   select p).Single();

            exameLancamento.resultadoID = (from p in bd.db._exame_tipo_resultados
                                           where p.descricao == ddlResultado.Text
                                           select p.id).Single();

            exameLancamento.obs = txtObs.Text;


            bd.db.SubmitChanges();


            fdsAlunosAvaliacao.Visible = true;
            fdsDadosExame.Visible = true;
            fdsAlunoResultado.Visible = false;
            divHlBackResultado.Visible = false;

            GetAlunosExame(exameID);

            log.AdicionarEntrada(45, usuarioLogado.id, 6, "", 1, 0);


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Resultado do exame para esse aluno foi lançado com sucesso!!');", true);

            //Response.Redirect("LancarResultado.aspx?exameid=" + exameID);
        }
        catch
        {

        }
    }
    protected void btlConsolidarExame_Click(object sender, EventArgs e)
    {
        try
        {
            var exame = (from p in bd.db._exames
                         where p.id == exameID
                         select p).Single();

            if(exame.statusID == 1)
            {

                var alunosExame = (from p in bd.db._alunos
                                   join u in bd.db._exame_lancamentos
                                       on p.id equals u.alunoID
                                   where u.exameID == exameID
                                   && u.resultadoID == null
                                   select p).ToList();

                if (alunosExame.Count < 1) //todos os alunos tem resultado lancado
                {

                    exame._exame_status = bd.db._exame_status.Single(c => c.id == 2);
                    exame.dataFechamento = DateTime.Now.Date;
                    exame.horaFechamento = DateTime.Now.TimeOfDay;

                    bd.db.SubmitChanges();

                    log.AdicionarEntrada(49, usuarioLogado.id, 6, "", 1, 0);

                    GetDadosExame();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Teste / Exame consolidado com sucesso!!');", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Todos os alunos devem ter o resultado lançado antes de consolidar o teste / exame!!');", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Teste / Exame já consolidado anteriormente!!');", true);
            }

            
        }
        catch(Exception e3)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('"+e3.Message+"');", true);
        }
    }
}
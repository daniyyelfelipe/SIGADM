using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GEM_Academico : System.Web.UI.Page
{
    int idGem = 0;
    int alunoID = 0;
    BD bd = new BD();
    Global gl = new Global();
    Log log = new Log();
    _user usuarioLogado;

    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioLogado = (_user)Session["usuarioLogado"];

        idGem = int.Parse(Request.QueryString["idg"].ToString());


        //links dos botões de voltar
        hlBack.NavigateUrl = "~/gem/Academico.aspx?idg=" + idGem;
        hlBackMts.NavigateUrl = "~/gem/Academico.aspx?idg=" + idGem;
        hlBackPresenca.NavigateUrl = "~/gem/Academico.aspx?idg=" + idGem;
        hlBackMetodo.NavigateUrl = "~/gem/Academico.aspx?idg=" + idGem;

        if (!IsPostBack)
        {
            CarregaForm();
            mtv.SetActiveView(viewHome);
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
                              //COMUM = p._igreja.descricao,
                              //CIDADE = p._igreja._municipio.descricao,
                              INSTRUMENTO = p._instrumento.descricao
                              //EMAIL = p.email,
                              //TELEFONE = p.telefone
                          }).ToList();
            gvMatriculados.DataSource = alunos;
            gvMatriculados.DataBind();


            lblGem.Text = gem._municipio.descricao + " | " + gem._igreja.descricao + " | " + gem._GEM_Periodo.descricao + " | Turma " + gem._GEM_Turma.descricao + " | Matrículas: " + alunos.Count();

        }
        catch
        {

        }
    }
    protected void gvMatriculados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            alunoID = int.Parse(e.CommandArgument.ToString());
            Session["alunoID"] = alunoID.ToString();//passando a id para pegar na funcao que adiciona um novo metodo
            //lblAlunoID.Text = alunoID.ToString(); //passando a id para pegar na funcao que adiciona um novo metodo
            hfAlunoID.Value = e.CommandArgument.ToString();

            if (e.CommandName == "hinos")
            {
                var vozHinos = (from p in bd.db._instrumentoVozs
                                select p.voz).ToList();
                ddlVozHino.DataSource = vozHinos;
                ddlVozHino.DataBind();

                CarregaHinos(alunoID);
                CarregaHistoricoHinos(alunoID);
                txtData.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now.Date);

                mtv.SetActiveView(viewHinos);
            }
            if (e.CommandName == "metodo")
            {
                CarregaMetodo(alunoID);
                mtv.SetActiveView(viewMetodo);
            }
        }
        catch (Exception e3)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + e3.Message + "');", true);
        }
    }

    private void CarregaMetodo(int alunoID)
    {
        try
        {
            var aluno = (from p in bd.db._alunos
                         where p.id == alunoID
                         select p).Single();

            lblAlunoMetodo.Text = aluno.nome + " - " + aluno._instrumento.descricao;
            txtDataMetodo.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now.Date);

            var metodos = (from p in bd.db._instrumentoMetodos
                           where p.instrumentoID == aluno.instrumentoID
                           select p.descricao).ToList();

            ddlMetodos.DataSource = metodos;
            ddlMetodos.DataBind();


            var metodoPassado = (from p in bd.db._GEM_Academicos
                                // where p.gemID == idGem
                                 where p.alunoID == alunoID
                                 && p.metodo != null
                                 select new
                                 {
                                     ID = p.ID,
                                     DATA = String.Format("{0:dd/MM/yyyy}", p.data.Value.Date),
                                     METODO = p._instrumentoMetodo.descricao,
                                     PAGINA = p.metodoPagina.ToString(),
                                     LICAO = p.metodoLicao

                                 }).ToList();

            gvMetodoLancado.DataSource = metodoPassado;
            gvMetodoLancado.DataBind();
        }
        catch { }
    }
    private void CarregaHinos(int alunoID)
    {
        try
        {

            var hinosPassados = (from p in bd.db._GEM_Academicos
                                 //where p.gemID == idGem
                                 where p.alunoID == alunoID
                                 && p.vozID == (from v in bd.db._instrumentoVozs where v.voz == ddlVozHino.SelectedValue select v.id).Single()
                                 select p.hino).ToList();

            var hinos = (from p in bd.db._GEM_Hinos
                         select new
                         {
                             numero = p.numero,
                             ck = (hinosPassados.Contains(p.numero)) ? true : false,
                             en = (hinosPassados.Contains(p.numero)) ? false : true,
                             hino = "Hino " + p.numero.ToString(),
                             bColor = (hinosPassados.Contains(p.numero)) ? "green" : "red"

                         }).ToList();

            rpHinos.DataSource = hinos;
            rpHinos.DataBind();

            lblTotalHinosPassados.Text = gl.AlunoByID(alunoID, "nome") + " | Total de " + hinosPassados.Count().ToString() + " hinos passados no " + ddlVozHino.SelectedValue;

        }
        catch (Exception e42)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + e42.Message + "');", true);
        }
    }
    protected void btnLancarHinos_Click(object sender, EventArgs e)
    {
        try
        {
            //Pegar todos os itens do repeater
            for (int i = 0; i < rpHinos.Items.Count; i++)
            {
                //Pegando o HiddenField dentro do repeater
                HiddenField hf = (HiddenField)rpHinos.Items[i].FindControl("hf1");

                //Pegando o CheckBox dentro do repeater
                CheckBox cb = (CheckBox)rpHinos.Items[i].FindControl("cb1");

                //Verificar se foi selecionado
                if (cb.Checked)
                {
                    //Pegar o Value e o Text dos itens selecionados do repeater
                    //Response.Write("Value:" + hf.Value + " Text:" + cb.Text + "<br />");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + "Value:" + hf.Value + " Text:" + cb.Text + "<br />"+"');", true);

                    var hinosPassados = (from p in bd.db._GEM_Academicos
                                         where p.gemID == idGem
                                         select p).ToList();

                    if (hinosPassados.Count > 0)
                    {
                        for (int d = 0; d < hinosPassados.Count; d++)
                        {
                            //atualiza hinos hinosPassados
                            hinosPassados = (from p in bd.db._GEM_Academicos
                                             where p.gemID == idGem
                                             select p).ToList();
                            var check = (from p in bd.db._GEM_Academicos
                                         where p.gemID == idGem
                                         && p.hino == int.Parse(hf.Value)
                                         && p.vozID == (from v in bd.db._instrumentoVozs where v.voz == ddlVozHino.SelectedValue select v.id).Single()
                                         && p.alunoID == int.Parse(hfAlunoID.Value)
                                         select p).ToList();

                            //checa se o hino já não foi passado
                            if (check.Count > 0)
                            {

                            }
                            else
                            {
                                _GEM_Academico academico = new _GEM_Academico();
                                academico.data = Convert.ToDateTime(txtData.Text);
                                academico.alunoID = int.Parse(hfAlunoID.Value.ToString());
                                academico.gemID = idGem;
                                academico.hino = int.Parse(hf.Value);
                                academico.vozID = (from p in bd.db._instrumentoVozs where p.voz == ddlVozHino.SelectedValue select p.id).Single();

                                bd.db._GEM_Academicos.InsertOnSubmit(academico);
                                bd.db.SubmitChanges();

                                //lança no log
                                _user usuarioLogado = (_user)Session["usuarioLogado"];
                                log.AdicionarEntrada(31, usuarioLogado.id, 6, "", 1, 0);
                            }
                        }
                    }
                    //nenhum hino foi passado
                    else
                    {
                        _GEM_Academico academico = new _GEM_Academico();
                        academico.data = Convert.ToDateTime(String.Format("{0:MM/dd/yyyy}", txtData.Text));
                        academico.alunoID = int.Parse(hfAlunoID.Value.ToString());
                        academico.gemID = idGem;
                        academico.hino = int.Parse(hf.Value);
                        academico.vozID = (from p in bd.db._instrumentoVozs where p.voz == ddlVozHino.SelectedValue select p.id).Single();

                        bd.db._GEM_Academicos.InsertOnSubmit(academico);
                        bd.db.SubmitChanges();

                        //lança no log
                        _user usuarioLogado = (_user)Session["usuarioLogado"];
                        log.AdicionarEntrada(31, usuarioLogado.id, 6, "", 1, 0);
                    }
                }
            }

            CarregaHinos(int.Parse(hfAlunoID.Value));
            CarregaHistoricoHinos(int.Parse(hfAlunoID.Value));

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Hinos lançados com sucesso!!');", true);
        }
        catch (Exception e43)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + e43.Message + "');", true);
        }
    }

    private void CarregaHistoricoHinos(int alunoID)
    {
        try
        {
            var historico = (from p in bd.db._GEM_Academicos
                             //where p.gemID == idGem
                             where p.alunoID == alunoID
                             && p.hino != null
                             select new
                             {
                                 ID = p.ID,
                                 DATA = String.Format("{0:dd/MM/yyyy}", p.data),
                                 HINO = p.hino,
                                 VOZ = p._instrumentoVoz.voz

                             }).ToList();

            gvHinos.DataSource = historico;
            gvHinos.DataBind();

            //gvHinos.Columns.RemoveAt(0);    //Index is the index of the column you want to remove
            //gvHinos.DataBind();

            //gvHinos.Columns.Remove("ID");
            //gvHinos.Columns[0].Visible = false;
        }
        catch
        {

        }
    }
    protected void ddlVozHino_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregaHinos(int.Parse(hfAlunoID.Value));
    }
    protected void btnMts_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //carregar os campos
            var modulos = (from p in bd.db._GEM_MTS_Teoria_Assuntos
                           select p.modulo).ToList().Distinct();
            ddlModuloMts.DataSource = modulos;
            ddlModuloMts.DataBind();

            //var aulas = (from p in bd.db._GEM_MTS_Assuntos
            //               where p.modulo == int.Parse(ddlModuloMts.Text)
            //               select p.aula).ToList().Distinct();
            //ddlAulaMts.DataSource = aulas;
            //ddlAulaMts.DataBind();

            var assuntos = (from p in bd.db._GEM_MTS_Teoria_Assuntos
                            where p.modulo == int.Parse(ddlModuloMts.Text)
                            select p.assunto).ToList();
            ddlAssuntosMts.DataSource = assuntos;
            ddlAssuntosMts.DataBind();

            txtDataMts.Text = DateTime.Now.Date.ToString().Replace(" 00:00:00", "");
            txtDataMts.Focus();

            CarregaHistoricoAssuntosMTS();

            mtv.SetActiveView(viewMts);
        }
        catch (Exception err4)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + err4.Message + "');", true);
        }
    }
    protected void ddlModuloMts_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var assuntos = (from p in bd.db._GEM_MTS_Teoria_Assuntos
                            where p.modulo == int.Parse(ddlModuloMts.Text)
                            select p.assunto).ToList();
            ddlAssuntosMts.DataSource = assuntos;
            ddlAssuntosMts.DataBind();
        }
        catch
        { }
    }
    protected void ddlAulaMts_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch
        { }
    }
    protected void btnLancarMts_Click(object sender, EventArgs e)
    {
        try
        {
            var mts = (from p in bd.db._GEM_MTS_Teoria_Assuntos
                       where p.modulo == int.Parse(ddlModuloMts.SelectedValue)
                       && p.assunto == ddlAssuntosMts.SelectedValue
                       select p.id).Single();

            _GEM_Academico acad = new _GEM_Academico();
            acad.data = DateTime.Parse(txtDataMts.Text);
            acad.gemID = idGem;
            acad.mts = mts;

            bd.db._GEM_Academicos.InsertOnSubmit(acad);
            bd.db.SubmitChanges();

            //lança no log
            _user usuarioLogado = (_user)Session["usuarioLogado"];
            log.AdicionarEntrada(32, usuarioLogado.id, 6, "", 1, 0);

            CarregaHistoricoAssuntosMTS();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Assunto da aula registrado com sucesso!!');", true);

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Funcionalidade ainda não Liberada pelo desenvolvedor!');", true);
        }
        catch
        { }
    }
    private void CarregaHistoricoAssuntosMTS()
    {
        try
        {
            var assuntos = (from p in bd.db._GEM_Academicos
                            join a in bd.db._GEM_MTS_Teoria_Assuntos
                                on p.mts equals a.id
                            where p.gemID == idGem
                            select new
                            {
                                ID = p.ID,
                                DATA = String.Format("{0:dd/MM/yyyy}", p.data.Value.Date),
                                ASSUNTO = p._GEM_MTS_Teoria_Assunto.assunto

                            }).ToList();

            gvTeoriaMts.DataSource = assuntos;
            gvTeoriaMts.DataBind();
        }
        catch
        {

        }
    }

    protected void btnPresenca_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //confere a permicao para relancar as presencas
            //regionais, locais e desenvolvedor
            if (usuarioLogado.tipoID == 1 || usuarioLogado.tipoID == 2 || usuarioLogado.tipoID == 8)
            {
                lblPermicaoRelancar.ForeColor = System.Drawing.Color.Green;
                lblPermicaoRelancar.Text = " | Você tem permissão para re-lançar as presenças.";
            }
            else
            {
                lblPermicaoRelancar.ForeColor = System.Drawing.Color.Red;
                lblPermicaoRelancar.Text = " | Você não tem permissão para re-lançar as presenças.";
            }

            var gem = (from p in bd.db._GEMs
                       where p.Id == idGem
                       select p).Single();

            lblPresenca.Text = "Presença eletrônica - " + gem._municipio.descricao + " | " + gem._igreja.descricao + " | " + gem._GEM_Periodo.descricao + " | Turma " + gem._GEM_Turma.descricao;

            txtPresencaData.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now.Date);

            var alunosGem = (from p in bd.db._alunos
                             join m in bd.db._GEM_Matriculas
                             on p.id equals m.alunoID
                             where m.gemID == idGem && m.statusID == 1
                             select new
                             {
                                 ID = p.id,
                                 MATRICULA = p.matricula,
                                 ALUNOID = p.id,
                                 GEMID = m._GEM.Id,
                                 NOME = p.nome
                                 //COMUM = p._igreja.descricao,
                                 //CIDADE = p._igreja._municipio.descricao,
                                 //INSTRUMENTO = p._instrumento.descricao,
                                 //EMAIL = p.email,
                                 //TELEFONE = p.telefone
                             }).ToList();
            rpPresenca.DataSource = alunosGem;
            rpPresenca.DataBind();


            var presencasLancadas = (from p in bd.db._GEM_Presencas
                                     where p.gemID == idGem

                                     select new
                                     {
                                         //ID = p.Id,
                                         DATA = String.Format("{0:dd/MM/yyyy}", p.dataGem),
                                         PRESENCAS = (from a in bd.db._GEM_Presencas 
                                                      where a.gemID == idGem 
                                                      && a.dataGem == p.dataGem 
                                                      && a.alunoID != null 
                                                      select p).ToList().Count(),
                                         FALTAS = alunosGem.Count() - (from a in bd.db._GEM_Presencas 
                                                                       where a.gemID == idGem 
                                                                       && a.dataGem == p.dataGem 
                                                                       && a.alunoID != null select p).ToList().Count()
                                     }).ToList().Distinct();


            gvPresencaLancada.DataSource = presencasLancadas;
            gvPresencaLancada.DataBind();


            mtv.SetActiveView(viewPresenca);
        }
        catch (Exception e43)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + e43.Message + "');", true);
        }
    }
    protected void btnLancarPresenca_Click(object sender, EventArgs e)
    {
        try
        {


            //checar se a resença já foi dada naquele dia.
            DateTime dtGem = Convert.ToDateTime(String.Format("{0:MM/dd/yyyy}", txtPresencaData.Text));

            var presencaGem = (from p in bd.db._GEM_Presencas
                               where p.gemID == idGem && p.dataGem == dtGem
                               select p).ToList();

            if (presencaGem.Count < 1)
            {
                int quantPresencas = 0;
                int idGEM = 0;

                //Pegar todos os itens do repeater (item por item)
                for (int i = 0; i < rpPresenca.Items.Count; i++)
                {
                    //Pegando os HiddenFields dentro do repeater
                    HiddenField hfAlunoID = (HiddenField)rpPresenca.Items[i].FindControl("hfAlunoID");
                    HiddenField hfGemID = (HiddenField)rpPresenca.Items[i].FindControl("hfGemID");
                    idGEM = int.Parse(hfGemID.Value.ToString());

                    //Pegando o CheckBox dentro do repeater
                    CheckBox cb = (CheckBox)rpPresenca.Items[i].FindControl("cb1");

                    

                    //Verificar se foi selecionado (presenca dada)
                    if (cb.Checked)
                    {

                        //gravar presenca
                        _user usuarioLogado = (_user)Session["usuarioLogado"];

                        _GEM_Presenca presenca = new _GEM_Presenca();
                        presenca.alunoID = int.Parse(hfAlunoID.Value.ToString());
                        presenca.dataGem = dtGem;
                        presenca.dataHoraRegistro = DateTime.Now;
                        presenca.gemID = int.Parse(hfGemID.Value.ToString());
                        presenca.usuarioID = usuarioLogado.id;

                        bd.db._GEM_Presencas.InsertOnSubmit(presenca);
                        bd.db.SubmitChanges();


                        //lança no log

                        log.AdicionarEntrada(37, usuarioLogado.id, 6, "", 1, 0);
                        quantPresencas++;

                    }
                }

                if (quantPresencas == 0)
                {
                     
                    //nenhum aluno veio, e nada foi selecionado

                    //gravar presenca
                    _user usuarioLogado = (_user)Session["usuarioLogado"];

                    _GEM_Presenca presenca = new _GEM_Presenca();
                    presenca.alunoID = null;
                    presenca.dataGem = dtGem;
                    presenca.dataHoraRegistro = DateTime.Now;
                    presenca.gemID = idGEM;
                    presenca.usuarioID = usuarioLogado.id;

                    bd.db._GEM_Presencas.InsertOnSubmit(presenca);
                    bd.db.SubmitChanges();


                    //lança no log

                    log.AdicionarEntrada(37, usuarioLogado.id, 6, "", 1, 0);

                    
                }


                // tudo isso só pra regarregar os gvs!!!!!
                var alunosGem = (from p in bd.db._alunos
                                 join m in bd.db._GEM_Matriculas
                                 on p.id equals m.alunoID
                                 where m.gemID == idGem && m.statusID == 1
                                 select p).ToList();



                var presencasLancadas = (from p in bd.db._GEM_Presencas
                                         where p.gemID == idGem

                                         select new
                                         {
                                             //ID = p.Id,
                                             DATA = String.Format("{0:dd/MM/yyyy}", p.dataGem),
                                             PRESENCAS = (from a in bd.db._GEM_Presencas
                                                          where a.gemID == idGem
                                                          && a.dataGem == p.dataGem
                                                          && a.alunoID != null
                                                          select p).ToList().Count(),
                                             FALTAS = alunosGem.Count() - (from a in bd.db._GEM_Presencas
                                                                           where a.gemID == idGem
                                                                           && a.dataGem == p.dataGem
                                                                           && a.alunoID != null
                                                                           select p).ToList().Count()
                                         }).ToList().Distinct();


                gvPresencaLancada.DataSource = presencasLancadas;
                gvPresencaLancada.DataBind();

                //-------------------

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Presenças lançadas com sucesso!!');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Já foram dadas as presenças para a GEM na data " + String.Format("{0:dd/MM/yyyy}", dtGem.Date.ToString().Replace(" 00:00:00", "")) + ". Re-lançamento em desenvolvimento.');", true);
            }
        }
        catch (Exception e43)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + e43.Message + "');", true);
        }
    }
    protected void gvTeoriaMts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int registroID = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "Excluir")
            {


                var consulta = (from p in bd.db._GEM_Academicos
                                where p.ID == registroID
                                select p).Single();

                bd.db._GEM_Academicos.DeleteOnSubmit(consulta);

                bd.db.SubmitChanges();

                log.AdicionarEntrada(35, usuarioLogado.id, 6, "", 1, 0);

                CarregaHistoricoAssuntosMTS();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Registro deletado com sucesso!');", true);

            }
        }
        catch (Exception er2)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + er2.Message + "');", true);
        }
    }
    protected void btnLancarMetodo_Click(object sender, EventArgs e)
    {
        try
        {

            var aluno = (from p in bd.db._alunos
                         where p.id == int.Parse(Session["alunoID"].ToString())
                         select p).Single();

            var instrumentoAluno = (from p in bd.db._instrumentos
                                    where p.id == aluno.instrumentoID
                                    select p).Single();

            _GEM_Academico academico = new _GEM_Academico();
            academico.data = Convert.ToDateTime(txtDataMetodo.Text);
            academico.alunoID = int.Parse(Session["alunoID"].ToString());
            academico.gemID = idGem;
            academico.metodo = (from p in bd.db._instrumentoMetodos where p.descricao == ddlMetodos.Text && p.instrumentoID == instrumentoAluno.id select p.id).Single();
            academico.metodoPagina = int.Parse(txtMetodoPagina.Text.Trim());
            academico.metodoLicao = txtMetodoLicao.Text.Trim();
            academico.metodoObs = txtMetodoObs.Text.Trim();


            bd.db._GEM_Academicos.InsertOnSubmit(academico);
            bd.db.SubmitChanges();

            //lança no log
            _user usuarioLogado = (_user)Session["usuarioLogado"];
            log.AdicionarEntrada(34, usuarioLogado.id, 6, "", 1, 0);

            txtMetodoPagina.Text = "";
            txtMetodoLicao.Text = "";
            txtMetodoObs.Text = "";



            CarregaMetodo(int.Parse(Session["alunoID"].ToString()));

            //mtv.SetActiveView(viewHome);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Lançamento de prática inserido com sucesso!');", true);

        }
        catch (Exception er2)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + er2.Message + "');", true);
        }
    }
    protected void imbtnAddMethod_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            // apenas regionais, locais, examinadoras, desenvolvedores e secretarios podem adicionar metodos.
            if (usuarioLogado.tipoID == 1 || usuarioLogado.tipoID == 2 | usuarioLogado.tipoID == 4 | usuarioLogado.tipoID == 8 | usuarioLogado.tipoID == 9)
            {
                var instrumentos = (from p in bd.db._instrumentos
                                    select p.descricao).ToList();
                ddlNewMetodoInstrumento.DataSource = instrumentos;
                ddlNewMetodoInstrumento.DataBind();

                txtNewMetodoDescricao.Focus();

                fdsMetodoDefault.Visible = false;
                fdsAddMetodo.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Você não tem permissão para adicionar novos métodos. Contate o seu regional.');", true);
            }
        }
        catch { }
    }
    protected void btnNewMetodoAdd_Click(object sender, EventArgs e)
    {
        try
        {

            var metodoCheck = (from p in bd.db._instrumentoMetodos
                               where p.descricao == txtNewMetodoDescricao.Text.Trim()
                               && p.instrumentoID == (from i in bd.db._instrumentos where i.descricao == ddlNewMetodoInstrumento.Text select i.id).Single()
                               select p).ToList();

            if (metodoCheck.Count > 0)
            {
                //ja existe um metodo com esse nome cadastado
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Já existe um método com essa descrição cadastrado para esse instrumento!');", true);
            }
            else
            {
                _instrumentoMetodo metodo = new _instrumentoMetodo();
                metodo.descricao = txtNewMetodoDescricao.Text.Trim();
                metodo.instrumentoID = (from p in bd.db._instrumentos where p.descricao == ddlNewMetodoInstrumento.Text select p.id).Single();

                bd.db._instrumentoMetodos.InsertOnSubmit(metodo);
                bd.db.SubmitChanges();

                var aluno = (from p in bd.db._alunos
                             where p.id == int.Parse(Session["alunoID"].ToString())
                             select p).Single();

                var metodos = (from p in bd.db._instrumentoMetodos
                               where p.instrumentoID == aluno.instrumentoID
                               select p.descricao).ToList();

                ddlMetodos.DataSource = metodos;
                ddlMetodos.DataBind();

                txtNewMetodoDescricao.Text = "";

                fdsMetodoDefault.Visible = true;
                fdsAddMetodo.Visible = false;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Método inserido com sucesso!');", true);
            }

        }
        catch (Exception er2)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + er2.Message + "');", true);
        }
    }
    protected void gvPresencaLancada_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            DateTime gemData = Convert.ToDateTime(String.Format("{0:MM/dd/yyyy}", e.CommandArgument.ToString()));

            if (e.CommandName == "Excluir")
            {
                var presencas = (from p in bd.db._GEM_Presencas
                                 where p.dataGem == gemData
                                 select p).ToList();

                for (int i = 0; i <= presencas.Count() - 1; i++)
                {
                    bd.db._GEM_Presencas.DeleteOnSubmit(presencas[i]);
                    bd.db.SubmitChanges();
                }

                //lança no log
                log.AdicionarEntrada(40, usuarioLogado.id, 6, "", 1, 0);



                // tudo isso só pra regarregar os gvs!!!!!
                var alunosGem = (from p in bd.db._alunos
                                 join m in bd.db._GEM_Matriculas
                                 on p.id equals m.alunoID
                                 where m.gemID == idGem && m.statusID == 1
                                 select p).ToList();



                var presencasLancadas = (from p in bd.db._GEM_Presencas
                                         where p.gemID == idGem

                                         select new
                                         {
                                             //ID = p.Id,
                                             DATA = String.Format("{0:dd/MM/yyyy}", p.dataGem),
                                             PRESENCAS = (from a in bd.db._GEM_Presencas
                                                          where a.gemID == idGem
                                                          && a.dataGem == p.dataGem
                                                          && a.alunoID != null
                                                          select p).ToList().Count(),
                                             FALTAS = alunosGem.Count() - (from a in bd.db._GEM_Presencas
                                                                           where a.gemID == idGem
                                                                           && a.dataGem == p.dataGem
                                                                           && a.alunoID != null
                                                                           select p).ToList().Count()
                                         }).ToList().Distinct();


                gvPresencaLancada.DataSource = presencasLancadas;
                gvPresencaLancada.DataBind();

                //-------------------



                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Registro de presença deletado com sucesso!');", true);
            }

        }
        catch { }
    }
    protected void gvHinos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Deleta uma coluna especifica do grid 
        e.Row.Cells[1].Visible = false;
    }

    protected void gvHinos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                var lancamento = (from p in bd.db._GEM_Academicos
                                  where p.ID == int.Parse(e.CommandArgument.ToString())
                                  select p).Single();

                bd.db._GEM_Academicos.DeleteOnSubmit(lancamento);
                bd.db.SubmitChanges();

                CarregaHistoricoHinos(int.Parse(Session["alunoID"].ToString()));
                CarregaHinos(int.Parse(Session["alunoID"].ToString()));

                //lança no log
                log.AdicionarEntrada(47, usuarioLogado.id, 6, "", 1, 0);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Registro de Hinos deletado sucesso!');", true);
                                 
            }
        }
        catch { }
    }
    protected void gvMetodoLancado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                var lancamento = (from p in bd.db._GEM_Academicos
                                  where p.ID == int.Parse(e.CommandArgument.ToString())
                                  select p).Single();

                bd.db._GEM_Academicos.DeleteOnSubmit(lancamento);
                bd.db.SubmitChanges();

                CarregaMetodo(int.Parse(Session["alunoID"].ToString()));

                //lança no log
                log.AdicionarEntrada(48, usuarioLogado.id, 6, "", 1, 0);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Registro de método deletado sucesso!');", true);

            }
        }
        catch { }
    }
    protected void gvMetodoLancado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Deleta uma coluna especifica do grid 
        e.Row.Cells[1].Visible = false;
    }
}
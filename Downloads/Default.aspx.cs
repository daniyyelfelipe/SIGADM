using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Globalization;

public partial class Downloads_Default : System.Web.UI.Page
{
    BD bd = new BD();
    _user usuarioLogado;
    Log log = new Log();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            usuarioLogado = (_user)Session["usuarioLogado"];

            if (!IsPostBack)
            {
                CarregaDownloads();
            }

            if (usuarioLogado._userTipo.id == 9 || usuarioLogado._userTipo.id == 8)
            {

            }
            else
            {
                fsUpload.Visible = false;
            }
        }
        catch(Exception)
        {
            Response.Redirect("~/");
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload.HasFile)
            {
                string path;
                string path1;
                path = Server.MapPath("~/Downloads/Files/");
                path1 = path + FileUpload.FileName;
                //lbl.Text = path1;

                _download d = new _download();
                d.path = path1;
                d.fileName = FileUpload.FileName;
                d.status = 1;
                d.visible = 0;
                bd.db._downloads.InsertOnSubmit(d);
                bd.db.SubmitChanges();

                FileUpload.SaveAs(path + FileUpload.FileName);

                log.AdicionarEntrada(22, usuarioLogado.id, 6, "", 1, 0);

                CarregaDownloads();

                ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Upload realizado com sucesso!!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Selecione um arquivo!!');", true);
            }
        }
        catch(Exception e1)
        {
            lbl.Text = e1.Message;
        }
    }

    private void CarregaDownloads()
    {
        try
        {
            var down = (from p in bd.db._downloads
                        orderby p.fileName
                        select new
                        {
                            ARQUIVO = p.fileName,
                            TAMANHO = TamanhoArquivo(p.fileName),
                            DEL = (usuarioLogado.tipoID.Value == 8 || usuarioLogado.tipoID.Value == 9) ? true : false //desenvolvedor e secretario
                        }).ToList();

            gvDownloads.DataSource = down;
            gvDownloads.DataBind();
        }
        catch(Exception e2)
        {
            ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('"+e2.Message+"');", true);
        }
    }

    private string TamanhoArquivo(string fileName)
    {
        try
        {
            FileInfo info = new FileInfo("D:/WEBSITES/SIGADM/Downloads/Files/"+ fileName);
            return ConvertBytesToMegabytes(info.Length).ToString() + "MB";
        }
        catch(Exception e43)
        {
            lblError.Text = e43.Message;
            return "0";
        }
    }

    private double ConvertBytesToMegabytes(long bytes)
    {
        double n = (bytes / 1024f) / 1024f;
        return Math.Round(n, 3);
    }
    protected void gvDownloads_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "download")
            {
                string filename = e.CommandArgument.ToString();
                var path = (from p in bd.db._downloads
                            where p.fileName == filename
                            select p.path).Single();
                string virtualPath = "~/downloads/files/" + filename;
                string type = filename.Substring(filename.Length - 4).Replace(".", "");

                //WebClient webClient = new WebClient();
                //webClient.DownloadFile("http://orquestrarn.com/downloads/files/"+filename, @path);


                //Response.ContentType = "application/" + type;
                //Response.AppendHeader("Content-Disposition", "attachment; filename="+filename);
                //Response.TransmitFile(Server.MapPath(path));
                //Response.End();

                log.AdicionarEntrada(23, usuarioLogado.id, 6, "", 1, 0);

                string strURL = virtualPath;
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(strURL) + "\"");
                byte[] data = req.DownloadData(Server.MapPath(strURL));
                response.BinaryWrite(data);
                response.End();                
            }
            else if(e.CommandName == "deletar")
            {
                try
                {
                    string filename = e.CommandArgument.ToString();
                    var path = (from p in bd.db._downloads
                                where p.fileName == filename
                                select p.path).Single();
                    var download = (from p in bd.db._downloads
                                    where p.fileName == filename
                                    select p).Single();

                    File.Delete(path);
                    bd.db._downloads.DeleteOnSubmit(download);
                    bd.db.SubmitChanges();

                    log.AdicionarEntrada(24, usuarioLogado.id, 6, "", 1, 0);

                    CarregaDownloads();

                    ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Arquivo deletado com sucesso!');", true);
                }
                catch
                {
                    string filename = e.CommandArgument.ToString();
                    var download = (from p in bd.db._downloads
                                    where p.fileName == filename
                                    select p).Single();

                    bd.db._downloads.DeleteOnSubmit(download);
                    bd.db.SubmitChanges();

                    CarregaDownloads();
                }
            }
            
        }
        catch (Exception er2)
        {
            lblError.Text = er2.Message;
        }
    }
    protected void gvDownloads_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            e.Row.Cells[2].Width = Unit.Pixel(400);
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);
        Label1.Text = "Data Processed Successfully";
        
    }
    static string RemoveAccent(string text)
    {
        return string.Concat(
            text.Normalize(NormalizationForm.FormD)
            .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                                          UnicodeCategory.NonSpacingMark)
          ).Normalize(NormalizationForm.FormC);
    }
    protected void btnPesquisa_Click(object sender, EventArgs e)
    {
        try
        {
            var pesquisa = (from p in bd.db._downloads
                        orderby p.fileName
                        where p.fileName.Contains(txtPesquisa.Text.Trim())
                        select new
                        {
                            ARQUIVO = p.fileName,
                            TAMANHO = TamanhoArquivo(p.fileName),
                            DEL = (usuarioLogado.tipoID.Value == 8 || usuarioLogado.tipoID.Value == 9) ? true : false //desenvolvedor e secretario
                        }).ToList();


            gvDownloads.DataSource = pesquisa;
            gvDownloads.DataBind();
        }
        catch { }
    }
}
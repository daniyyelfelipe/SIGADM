using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logoff : System.Web.UI.Page
{
    Log log = new Log();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["usuarioLogado"] != null)
            {
                _user usuarioLogado = (_user)Session["usuarioLogado"];
                log.AdicionarEntrada(2, usuarioLogado.id, 6, "", 1, 0);
                Session.Clear();
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
           
        }
        catch
        {

        }
    }
}
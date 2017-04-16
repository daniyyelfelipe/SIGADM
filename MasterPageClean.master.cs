using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageClean : System.Web.UI.MasterPage
{
    _user usuario = new _user();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuarioLogado"] != null)
        {
            usuario = (_user)Session["usuarioLogado"];
        }
        else
        {
            Response.Redirect("~/Logoff.aspx");
        }
    }
}

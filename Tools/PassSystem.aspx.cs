using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_PassSystem : System.Web.UI.Page
{
    Guard guard = new Guard();
    BD bd = new BD();
    Log log = new Log();
    _user usuario = new _user();
    Email email = new Email();


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCodificar_Click(object sender, EventArgs e)
    {
        string senha = guard.EncriptaSenha(txtSenha1.Text.Trim());
        txtSenhaCodificada1.Text = senha;
    }
    protected void btnDecodificar_Click(object sender, EventArgs e)
    {

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class beta_email_Default : System.Web.UI.Page
{
    Email email = new Email();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        try
        {
            email.SendEmail(txtPara.Text.Trim(), txtAssunto.Text.Trim(), txtMensagem.Text.Trim());
        }
        catch
        {

        }
    }
    protected void btnPrivado_Click(object sender, EventArgs e)
    {
        email.SendEmailPrivateMensage("Daniyyel Felipe", "daniyyelfelipe@gmail.com");
    }
    protected void btnCadastro_Click(object sender, EventArgs e)
    {
        email.SendEmailSignUp("daniyyelfelipe@gmail.com", "123", "Encarregado Local");
    }
}
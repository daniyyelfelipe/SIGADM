using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    Guard guard = new Guard();
    BD bd = new BD();
    Log log = new Log();
    App app = new App();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuarioLogado"] != null)
        {
            Response.Redirect("~/home.aspx");
        }

        lblVersion.Text = app.GetVersion();
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Logar();
    }
    private void Logar()
    {
        try
        {
            string usuario = txtUsuario.Text.Trim();
            string senha = guard.EncriptaSenha(txtSenha.Text.Trim());

            var usuarioEntrando = (from p in bd.db._users
                                 where p.email == usuario
                                 && p.senha == senha
                                 select p).Single();

            if (usuarioEntrando != null)
            {
                if (usuarioEntrando.statusID == 1)
                {
                    Session["usuarioLogado"] = usuarioEntrando;
                    log.AdicionarEntrada(1, usuarioEntrando.id, 6, "", 1, 0);
                    Response.Redirect("~/home.aspx");
                }
                else if(usuarioEntrando.statusID == 2)
                {
                    lblErroLogin.Text = "O seu usuário encontra-se inativo no sistema. Para mais informações contate o suporte do SIGADM.";
                }
                else if (usuarioEntrando.statusID == 3)
                {
                    lblErroLogin.Text = "O seu usuário encontra-se bloqueado no sistema. Para mais informações contate o suporte do SIGADM.";
                }
                else if (usuarioEntrando.statusID == 4)
                {
                    lblErroLogin.Text = "O seu usuário foi banido do sistema. Para mais informações contate o suporte do SIGADM.";
                }

            }
            else
            {
                lblErroLogin.Text = "Usuário ou senha incorretos. Ou usuário não cadastrado!";
            }
        }
        catch
        {
            lblErroLogin.Text = "Usuário ou senha incorretos. Ou usuário não cadastrado!";
        }
    }

    protected void lbForgotPass_Click(object sender, EventArgs e)
    {
        try
        {
            String s = "<script language='javascript'> javascript:abrirPopup('ForgotPass.aspx',470,170); </script>";
            Page.RegisterClientScriptBlock("Print", s);
        }
        catch
        { }
    }
}
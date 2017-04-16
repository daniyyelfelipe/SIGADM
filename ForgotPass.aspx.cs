using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ForgotPass : System.Web.UI.Page
{
    BD bd = new BD();
    Guard g = new Guard();
    Email emailClass = new Email();
    string emailReculperacao = "";
    string chaveReculperacao = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        emailReculperacao = Request.QueryString["e"];
        chaveReculperacao = Request.QueryString["c"];

        if (emailReculperacao == null && chaveReculperacao == null)
        {
            divPedido.Visible = true;
        }
        else
        {
            divPedido.Visible = false;
            RecuperaSenha();
        }
    }
    protected void btnForgotPass_Click(object sender, EventArgs e)
    {
        try
        {
            string email = txtEmail.Text.Trim().ToLower();
            string chave = g.EncriptaSenha(email);

            //verifica se o email existe
            var verificaUser = (from p in bd.db._users
                                where p.email == email
                                select p).ToList();

            //usuario existe no sistema
            if (verificaUser.Count > 0)
            {
                //verificar se ja foi pedido recuperação
                var verificarPedidoAnterior = (from p in bd.db._ForgotPasses
                                               where p.email == email
                                               select p).ToList();


                if (verificarPedidoAnterior.Count() > 0)
                {
                    var pedido = (from p in bd.db._ForgotPasses
                                  where p.email == email
                                  select p).Single();

                    int numeroPedido = pedido.numeroPedido.Value + 1; 

                    pedido.dataPedido = DateTime.Now.Date;
                    pedido.dataExpiracao = DateTime.Now.Date.AddDays(1);
                    pedido.numeroPedido = numeroPedido;
                    pedido.pedidoUsado = 0;

                    bd.db.SubmitChanges();

                    txtEmail.Text = string.Empty;

                    emailClass.SendEmailForgotPass(email, pedido.chave);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Email para recuperação de senha enviado. Verifique seu email! ATENÇÃO!! A chave de recuperação tem validade de 24 horas!');", true);
                }
                else
                {
                    _ForgotPass forgotPass = new _ForgotPass();
                    forgotPass.email = email;
                    forgotPass.chave = chave;
                    forgotPass.dataPedido = DateTime.Now.Date;
                    forgotPass.dataExpiracao = DateTime.Now.Date.AddDays(1);
                    forgotPass.numeroPedido = 1;
                    forgotPass.pedidoUsado = 0;

                    bd.db._ForgotPasses.InsertOnSubmit(forgotPass);
                    bd.db.SubmitChanges();

                    txtEmail.Text = string.Empty;

                    emailClass.SendEmailForgotPass(email, chave);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Email para recuperação de senha enviado. Verifique seu email! ATENÇÃO!! A chave de recuperação tem validade de 24 horas!');", true);
                }
            }
            else
            {
                //usuario não existe no sistema
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('O email digitado não está cadastrado no sistema! Verifique o email digitado ou entre em contato com o suporte regional.');", true);

            }
            
        }
        catch { }
    }

    private void RecuperaSenha()
    {
        try
        {
            var verificaPedido = (from p in bd.db._ForgotPasses
                                  where p.email == emailReculperacao
                                  && p.chave == chaveReculperacao
                                  select p).Single();

            if (verificaPedido.dataExpiracao.Value.Date >= DateTime.Now.Date)
            {
                //pedido não usado
                if (verificaPedido.pedidoUsado == 0)
                {
                    //reseta
                    int resultadoReculperacao = g.resetarSenha(emailReculperacao);

                    if (resultadoReculperacao == 1)
                    {
                        verificaPedido.pedidoUsado = 1;
                        bd.db.SubmitChanges();

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('A sua senha foi resetada para 123!! Acesse o sistema e modifique sua senha para uma senha pessoal!');", true);
                    }
                    else
                    {
                        //erro
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Essa chave de recuperação já foi usada! Se você não lembra da senha, faça um novo pedido de recuperação.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Essa chave de reculperação já expirou. Faça um novo pedido de recuperação de senha!');", true);
            }
        }
        catch
        {

        }
    }

}
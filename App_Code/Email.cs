using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Threading;

/// <summary>
/// Summary description for Email
/// </summary>
public class Email
{

    BD bd = new BD();
    string myEmail = "";
    string myPass = "";
    string siteAndress = "www.sigadm.com";

	public Email()
	{
		//
		// TODO: Add constructor logic here
		//

        string emailSistema = (from p in bd.db._Variaveis
                               where p.descricao == "emailSistema"
                               select p.value).Single();
        string senhaEmailSIstema = (from p in bd.db._Variaveis
                                    where p.descricao == "senhaEmailSistema"
                                    select p.value).Single();

        myEmail = emailSistema;
        myPass = senhaEmailSIstema;
	}

    public void SendEmail(string _para, string _assunto, string _mensagem)
    {
        ThreadStart childthreat = new ThreadStart(() => ThreadSendEmail(_para, _assunto, _mensagem));
        Thread child = new Thread(childthreat);
        child.IsBackground = true;
        child.Start();        
    }

    private void ThreadSendEmail(string _para, string _assunto, string _mensagem)
    {
        try
        {
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(myEmail, myPass);

            MailMessage mm = new MailMessage(myEmail, _para, _assunto, _mensagem);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }
        catch
        {

        }
    }

    public void SendEmailPrivateMensage(string _de, string _para)
    {
        ThreadStart childthreat = new ThreadStart(() => ThreadSendEmailPrivateMensage(_de,_para));
        Thread child = new Thread(childthreat);
        child.IsBackground = true;
        child.Start(); 
    }

    private void ThreadSendEmailPrivateMensage(string _de, string _para)
    {
        try
        {
            string _assunto = "infoSIGADM - Mensagem Privada";
            string _mensagem = "A paz de Deus. <br/> <br/> <br/> " 
                + _de + " te enviou uma Mensagem Privada no SIGADM. <br/> <br/> "
            + "Acesse seu perfil em " + siteAndress + " e verifique sua TEIA. <br/><br/> "
            + "Deus Abençoe. <br/><br/><br/><br/>"
            + "---Team SIGADM";

            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(myEmail, myPass);

            MailMessage mm = new MailMessage("donotreply@sigadm.com", _para, _assunto, _mensagem);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.IsBodyHtml = true;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }
        catch
        {

        }
    }

    public void SendEmailSignUp(string _newEmail, string _newPass, string _newTipo)
    {
        ThreadStart childthreat = new ThreadStart(() => ThreadSendEmailSignUp(_newEmail,_newPass,_newTipo));
        Thread child = new Thread(childthreat);
        child.IsBackground = true;
        child.Start();
    }

    private void ThreadSendEmailSignUp(string _newEmail, string _newPass, string _newTipo)
    {
        try
        {
            string _assunto = "infoSIGADM - Cadastro no Sistema";
            string _mensagem = "A paz de Deus. <br/> <br/> <br/>"
            + "Você acaba de ser cadastrado no SIGADM como " + _newTipo + " <br/> <br/> "
            + "Acesse seu perfil em " + siteAndress + " com os segintes dados de acesso: <br/><br/> "
            + "Email: " + _newEmail + "<br/>"
            + "Senha: " + _newPass + "<br/><br/>"
            + "Aproveite para mudar a sua senha diretamente no seu perfil. <br/><br/>"
            + "Deus Abençoe. <br/><br/><br/><br/>"
            + "---Team SIGADM";

            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(myEmail, myPass);

            MailMessage mm = new MailMessage("donotreply@sigadm.com", _newEmail, _assunto, _mensagem);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.IsBodyHtml = true;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }
        catch
        {

        }
    }


    public void SendEmailAvisoAll(string _de)
    {

        ThreadStart childthreat = new ThreadStart(() => ThreadSendEmailAvisoAll(_de));
        Thread child = new Thread(childthreat);
        child.IsBackground = true;
        child.Start();

         
    }

    private void ThreadSendEmailAvisoAll(string _de)
    {
        try
        { 

            string _assunto = "infoSIGADM - Aviso Importante";
            string _mensagem = "A paz de Deus. <br/> <br/> <br/> "
                + _de + " enviou um aviso importante no SIGADM. <br/> <br/> "
            + "Acesse seu perfil em " + siteAndress + " e verifique sua TEIA. <br/><br/> "
            + "Deus Abençoe. <br/><br/><br/><br/>"
            + "---Team SIGADM";            

            BD bd = new BD();

         var usuarios = (from p in bd.db._users
                            where p.statusID == 1
                            select p.email).ToList();

         for (int i = 0; i < usuarios.Count; i++)
         {

             // Command line argument must the the SMTP host.
             SmtpClient client = new SmtpClient();
             client.Port = 587;
             client.Host = "smtp.gmail.com";
             client.EnableSsl = true;
             client.Timeout = 10000;
             client.DeliveryMethod = SmtpDeliveryMethod.Network;
             client.UseDefaultCredentials = false;
             client.Credentials = new System.Net.NetworkCredential(myEmail, myPass);

             MailMessage mm = new MailMessage("donotreply@sigadm.com", usuarios[i], _assunto, _mensagem);
             mm.BodyEncoding = UTF8Encoding.UTF8;
             mm.IsBodyHtml = true;
             mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

             client.Send(mm);
         }

        }
        catch
        {

        }
    }

    public void SendEmailForgotPass(string _email, string _chave)
    {
        try
        {

            ThreadStart childthreat = new ThreadStart(() => ThreadSendEmailForgotPass(_email, _chave));
            Thread child = new Thread(childthreat);
            child.IsBackground = true;
            child.Start();
        }
        catch(Exception er1)
        {

        }


    }

    private void ThreadSendEmailForgotPass(string _email, string _chave)
    {
        try
        {

            string _assunto = "infoSIGADM - Recuperação de senha de acesso";
            string _mensagem = "A paz de Deus. <br/> <br/> <br/> "
            + "Acesse o link abaixo para resetar a sua senha no sistema SIGADM. <br/> <br/> "
            + "http://www.sigadm.com/ForgotPass.aspx?c=" + _chave + "&e=" + _email + " <br/><br/> "
            + "ATENÇÃO!! Esse link de recuperação tem validade de 24 horas. <br/><br/>" 
            + "Deus Abençoe. <br/><br/><br/><br/>"
            + "---Team SIGADM";


            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(myEmail, myPass);

            MailMessage mm = new MailMessage("donotreply@sigadm.com", _email, _assunto, _mensagem);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.IsBodyHtml = true;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
           

        }
        catch(Exception er)
        {

        }
    }
}
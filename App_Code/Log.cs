using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Log
/// </summary>
public class Log
{
    BD bd = new BD();
    App app = new App();

	public Log()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void AdicionarEntrada(int acaoID, int usuarioID, int visibilidadeID, string mensagem, int postTipoID, int privadoUserID)
    {
        try
        {
            _Log log = new _Log();
            log.acaoID = acaoID;
            log.dataHora = app.DateTimeCorrigido();
            log.usuarioID = usuarioID;
            log.visibilidadeID = visibilidadeID;
            log.mensagem = mensagem;
            log.postTipoID = postTipoID;
            log.privadoUserID = privadoUserID;

            bd.db._Logs.InsertOnSubmit(log);
            bd.db.SubmitChanges();
        }
        catch
        {

        }
    }
}
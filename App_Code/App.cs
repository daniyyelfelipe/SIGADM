using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for App
/// </summary>
public class App
{
    BD bd = new BD();
	public App()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DateTime DateTimeCorrigido()
    {
        return DateTime.Now;
    }

    public string GetVersion()
    {
        try
        {
            var v = (from p in bd.db._Variaveis
                     where p.descricao == "appVersion"
                     select p.value).Single();
            return "v" + v;
        }
        catch
        {
            return "v1.0";
        }
    }

    public string DateTimeTOUSDate(string dateTime)
    {
        string d = String.Format("{0:MM/dd/yyyy}", dateTime);
        return d;
    }

    public string DateTimeTOBRDate(DateTime dateTime)
    {
        string d = String.Format("{0:dd/MM/yyyy}", dateTime.Date);
        return d;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Global
/// </summary>
public class Global
{
    BD bd = new BD();
	public Global()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     /// <summary>
    /// Get the first several words from the summary.
    /// </summary>
    public string FirstWords(string input, int numberWords)
    {
        string retorno = "";
        try
        {
            // Number of words we still want to display.
            int words = numberWords;
            // Loop through entire summary.
            for (int i = 0; i < input.Length; i++)
            {
                // Increment words on a space.
                if (input[i] == ' ')
                {
                    words--;
                }
                // If we have no more words to display, return the substring.
                if (words == 0)
                {
                    if (input.Substring(0, i).Length < 2)
                    {
                        retorno = input;
                        return retorno;
                    }
                    else
                    {
                        retorno = input.Substring(0, i);
                        return retorno;
                    }
                }
            }
        }
        catch (Exception e1)
        {
            return e1.Message;
        }

        return input;
    }

    public string LastLogin(int userID)
    {
        try
        {
            string login;

            var ultimoLogin = (from p in bd.db._Logs
                               where p.acaoID == 1 && p.usuarioID == userID
                               select p.dataHora).ToList();
            if (ultimoLogin.Count > 0)
            {
                if (ultimoLogin[ultimoLogin.Count - 1].Value.Date == DateTime.Now.Date)
                {
                    login = "Visto por ultimo hoje às " + String.Format("{0:HH:mm:ss}", ultimoLogin[ultimoLogin.Count - 1].Value);
                }
                else if (ultimoLogin[ultimoLogin.Count - 1].Value.Date == DateTime.Now.Date.AddDays(-1))
                {
                    login = "Visto por ultimo ontem às " + String.Format("{0:HH:mm:ss}", ultimoLogin[ultimoLogin.Count - 1].Value);
                }
                else
                {
                    login = "Visto por ultimo em " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", ultimoLogin[ultimoLogin.Count - 1].Value);
                }
            }
            else
            {
                login = "Nunca entrou no sistema :(";
            }

            return login;
        }
        catch
        {
            return "Nunca entrou no sistema :(";
        }
    }

    public _user UsuarioByID(int usuarioID)
    {
        try
        {
            var user = (from p in bd.db._users
                        where p.id == usuarioID
                        select p).Single();
            return user;
        }
        catch
        {
            return null;
        }
    }
    public string UsuarioByID(int usuarioID, string campo)
    {
        try
        {
            if (campo == "nome")
            {
                var user = (from p in bd.db._users
                            where p.id == usuarioID
                            select p.nome).Single();
                return user;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
    public _aluno AlunoByID(int alunoID)
    {
        try
        {
            var user = (from p in bd.db._alunos
                        where p.id == alunoID
                        select p).Single();
            return user;
        }
        catch
        {
            return null;
        }
    }
    public string AlunoByID(int alunoID, string campo)
    {
        try
        {
            if (campo == "nome")
            {
                var user = (from p in bd.db._alunos
                            where p.id == alunoID
                            select p.nome).Single();
                return user;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
}
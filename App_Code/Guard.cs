using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Guard
/// </summary>
public class Guard
{
    BD bd = new BD();
    Log log = new Log();

	public Guard()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string EncriptaSenha(string value)
    {
        try
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes;
            using (HashAlgorithm hash = SHA1.Create())
                hashBytes = hash.ComputeHash(encoding.GetBytes(value));

            StringBuilder hashValue = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }

            return hashValue.ToString();
        }
        catch
        {
            return "";
        }
    }
    public int resetarSenha(string email)
    {
        try
        {
            var user = (from p in bd.db._users
                        where p.email == email
                        select p).Single();

            user.senha = EncriptaSenha("123");
            bd.db.SubmitChanges();

            log.AdicionarEntrada(21, user.id, 6, "", 1, 0);

            //senha resetada com sucesso
            return 1;
        }
        catch
        {
            //erro
            return 0;
        }
    }

}
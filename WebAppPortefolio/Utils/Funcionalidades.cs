using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebAppPortefolio.Utils
{
    public class Funcionalidades
    {
        //Gerar hash para segurança básica nas contas
        public static string GetUInt64Hash(HashAlgorithm hasher, string texto)
        {
            if (String.IsNullOrEmpty(texto))
            {
                return null;
            }

            using (hasher)
            {
                var bytes = hasher.ComputeHash(Encoding.Default.GetBytes(texto));
                return Enumerable.Range(0, bytes.Length / 8) //8 bytes in an 64 bit interger
                    .Select(i => BitConverter.ToUInt64(bytes, i * 8))
                    .Aggregate((x, y) => x ^ y).ToString();
            }

        }

        public async static void MandarMail(string destinatario)
        {
            //smtp host "smtp.live.com" port:  25 or 587| or smtp host  - "smtp.gmail.com"

            var smtpClient = new SmtpClient
            {
                Host = "smtp.live.com", 
                Port = 587, 
                EnableSsl = true, 
                Credentials = new NetworkCredential("gonileWeb@hotmail.com", "jogos2015")
            };

            using (var message = new MailMessage("gonileWeb@hotmail.com", destinatario)
            {
                Subject = "TIKE - AUTHORIZATION RECOVERY",
                Body = "OLA" +
                "Recuperação da tua pass!" +
                "USA ESTE TOKEN PARA ENTRAR..."
            })
            {
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}

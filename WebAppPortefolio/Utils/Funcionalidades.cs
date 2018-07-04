using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebAppPortefolio.Utils
{
    public class Funcionalidades
    {
        public static string CreateHash(string unHashed)
        {
            var x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(unHashed);
            data = x.ComputeHash(data);
            return Encoding.ASCII.GetString(data);
        }
    }
}

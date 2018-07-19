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
    }
}

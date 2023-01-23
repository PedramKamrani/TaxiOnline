using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Core.Securities
{
    public  static class HashEncode
    {
        [Obsolete("Obsolete")]
        public static string GetHashCode(string password)
        {
            Byte[] mainBytes;
            Byte[] encodeBytes;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            mainBytes=Encoding.Default.GetBytes(password);
            encodeBytes = md5.ComputeHash(mainBytes);
            return BitConverter.ToString(encodeBytes);
        }
    }
}

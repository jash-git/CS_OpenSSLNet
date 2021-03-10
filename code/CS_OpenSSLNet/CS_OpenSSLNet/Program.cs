using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenSSL.Core;
using OpenSSL.Crypto;

//https://www.cnblogs.com/azeri/p/8972432.html
//https://github.com/openssl-net/openssl-net

namespace CS_OpenSSLNet
{
    class Program 
    {
        static void Pause()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }
        public static string MD5(string text, Encoding encoding)
        {
            return HashDigest(text, encoding, MessageDigest.MD5);
        }

        public static string SHA1(string text, Encoding encoding)
        {
            return HashDigest(text, encoding, MessageDigest.SHA1);
        }

        private static string HashDigest(string text, Encoding encoding, MessageDigest hashType)
        {
            using (MessageDigestContext hashDigest = new MessageDigestContext(hashType))
            {
                byte[] hashBytes = encoding.GetBytes(text);
                byte[] signByte = hashDigest.Digest(hashBytes);
                return BitConverter.ToString(signByte).Replace(" - ", "").ToLower();
            }
        }
        static void Main(string[] args)
        {
            var ciphertext = MD5(" Md5加密。", Encoding.UTF8);
            Console.Write(ciphertext+"\n");
            ciphertext = SHA1(" SHA1加密。", Encoding.UTF8);
            Console.Write(ciphertext+"\n");
            Pause();
        }
    }
}

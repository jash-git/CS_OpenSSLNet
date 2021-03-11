using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenSSL.Core;
using OpenSSL.Crypto;
using OpenSSL.Crypto.EC;
using System.Security.Cryptography;

//https://www.cnblogs.com/azeri/p/8972432.html
//https://github.com/openssl-net/openssl-net


namespace CS_OpenSSLNet_testDHForm
{
    public partial class Form1 : Form
    {
        public DH CS_DH;
        public static byte[] g_bytGSYCGAATDHKEParametersBaseNumber2048 = new byte[] { (0x02), };
        public static byte[] g_bytGSYCGAATDHKEParametersPrimeNumber2048 = new byte[256]
        {
        (0xB0), (0xEE), (0x83), (0x99), (0x7E), (0x22), (0x23), (0x28), (0xF6), (0x06), (0x5B), (0x47),
        (0x89), (0x7A), (0x93), (0x01), (0xE6), (0x90), (0x34), (0x2F), (0x2F), (0x8F), (0xF3), (0x9F),
        (0xBB), (0xEE), (0x63), (0xB8), (0xA5), (0xB4), (0xDE), (0x8E), (0x3E), (0x17), (0xA7), (0xCC),
        (0x7C), (0x2A), (0xDD), (0x30), (0x8E), (0x38), (0x94), (0x5D), (0xDE), (0x3E), (0x91), (0xAE),
        (0x0E), (0x7B), (0x79), (0x00), (0x5C), (0xCE), (0xC5), (0x65), (0x61), (0xB0), (0x03), (0x04),
        (0x8D), (0xCC), (0xBA), (0x4D), (0x42), (0x68), (0xDA), (0xA1), (0xD4), (0x9B), (0xFD), (0xCE),
        (0xAA), (0x73), (0x31), (0xBE), (0x99), (0x07), (0x2A), (0xEC), (0xC4), (0x5F), (0xED), (0x2D),
        (0xE1), (0x17), (0x06), (0x7E), (0x3F), (0x62), (0x35), (0x65), (0xDA), (0x80), (0x1E), (0x2E),
        (0xF1), (0xD3), (0x93), (0xDF), (0x7B), (0xF9), (0x81), (0xCF), (0x83), (0xC9), (0x48), (0x91),
        (0xDB), (0x46), (0x61), (0xEA), (0x53), (0x07), (0x81), (0x25), (0x90), (0xFF), (0x42), (0x7D),
        (0x43), (0x84), (0x2C), (0xE2), (0x8C), (0x38), (0x70), (0xFF), (0x85), (0xC5), (0x5B), (0xF8),
        (0x7A), (0xE8), (0x73), (0xDB), (0xDA), (0x37), (0x5F), (0xD1), (0x44), (0xC7), (0x1A), (0x2A),
        (0xD7), (0x2E), (0x82), (0x97), (0x27), (0xEF), (0xDA), (0xE8), (0x84), (0x95), (0x91), (0xBD),
        (0xD6), (0x6B), (0x6F), (0x68), (0xB5), (0x99), (0x4F), (0x60), (0x78), (0xFB), (0xBC), (0xA8),
        (0x4E), (0xEE), (0x25), (0xD1), (0x45), (0x87), (0xD1), (0x3B), (0xE7), (0x53), (0xF9), (0x38),
        (0x10), (0x68), (0x17), (0xB0), (0x76), (0x63), (0xCA), (0x81), (0x97), (0x08), (0x56), (0xCE),
        (0x95), (0x27), (0x4F), (0x2D), (0x18), (0x24), (0x98), (0xEE), (0x1C), (0xC5), (0x92), (0xE6),
        (0xE7), (0xEC), (0xF6), (0xCB), (0x97), (0xF2), (0x12), (0x08), (0x95), (0xA5), (0xC7), (0x2E),
        (0x70), (0xED), (0x34), (0x66), (0x21), (0x53), (0x30), (0xAB), (0xA4), (0x6D), (0x9C), (0x62),
        (0xC5), (0x91), (0x28), (0x37), (0xBF), (0x55), (0xE4), (0xC2), (0x2B), (0x74), (0x9D), (0xCF),
        (0x35), (0x81), (0xD6), (0xC6), (0x2F), (0x14), (0xA8), (0xFE), (0x75), (0x5E), (0xC8), (0x45),
        (0x32), (0x52), (0xC3), (0x23),
        };

        static public String Base64_encode(String StrData)
        {
            //https://www.base64encode.net/
            String StrAns;
            StrAns = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(StrData));
            return StrAns;
        }
        static public String Base64_encode(byte[] byteData)
        {
            //https://www.base64encode.net/
            String StrAns;
            StrAns = Convert.ToBase64String(byteData);
            return StrAns;
        }

        static public byte[] bytBase64_decode(String StrData)
        {
            //https://www.base64decode.net/
            String StrAns;
            byte[] data = System.Convert.FromBase64String(StrData);
            StrAns = System.Text.ASCIIEncoding.ASCII.GetString(data);
            return data;
        }
        static public String StrBase64_decode(String StrData)
        {
            //https://www.base64decode.net/
            String StrAns;
            byte[] data = System.Convert.FromBase64String(StrData);
            StrAns = System.Text.ASCIIEncoding.ASCII.GetString(data);
            return StrAns;
        }

        static public string ToHexString(byte[] bytes)
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder str = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    str.Append(bytes[i].ToString("X2"));
                }
                hexString = str.ToString();
            }
            return hexString;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CS_DH = new DH();//DH_new()
            CS_DH.P = BigNumber.FromArray(g_bytGSYCGAATDHKEParametersPrimeNumber2048);//(*pDH)->p = BN_bin2bn(g_bytGSYCGAATDHKEParametersPrimeNumber2048, sizeof(g_bytGSYCGAATDHKEParametersPrimeNumber2048), NULL);
            CS_DH.G = BigNumber.FromArray(g_bytGSYCGAATDHKEParametersBaseNumber2048);//(*pDH)->g = BN_bin2bn(g_bytGSYCGAATDHKEParametersBaseNumber2048, sizeof(g_bytGSYCGAATDHKEParametersBaseNumber2048), NULL);

            String m_lpParametersString = CS_DH.PEM.Replace("-----BEGIN DH PARAMETERS-----", "");
            m_lpParametersString = m_lpParametersString.Replace("-----END DH PARAMETERS-----", "");
            m_lpParametersString = m_lpParametersString.Replace("\n", "");//i2d_DHparams(pDH, lpParameters) + GSYCGAATBase64Encode(&lpParametersString, lpParameters, iLength)
            richTextBox1.Text = m_lpParametersString;//Console.WriteLine("Parameters: {0}\n", m_lpParametersString);

            CS_DH.GenerateKeys();//iResult = DH_generate_key(pDH)
            byte[] bybuf = new byte[256];
            CS_DH.PublicKey.ToBytes(bybuf);//iPublicKeyLength = BN_num_bytes(pDH->pub_key);
            String m_lpPublicKeyString = Base64_encode(bybuf);//GSYCGAATBase64Encode(&lpPublicKeyString, lpPublicKey, iLength)
            richTextBox2.Text = m_lpPublicKeyString; //Console.WriteLine("Public Key: {0}\n", m_lpPublicKeyString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] lpPublicKey = bytBase64_decode(richTextBox3.Text);//GSYCGAATBase64Decode(&lpPublicKey, m_lpPublicKeyString, m_dwPublicKeyStringLength)
            byte[] lpEncryptedSecret = bytBase64_decode(richTextBox4.Text);//GSYCGAATBase64Decode(&lpEncryptedSecret, m_strEncryptedSecretString, iLength)
            byte[] lpPrivatekey = CS_DH.ComputeKey(BigNumber.FromArray(lpPublicKey));//lpPublicKeyBN = BN_bin2bn(lpPublicKey, iPublicKeyLength, NULL) + DH_compute_key(lpPrivatekey, lpPublicKeyBN, pDH)

            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] bytSecureHashAlgorithmCode= sha256.ComputeHash(lpPrivatekey);//GSYCGAATSHA256(bytSecureHashAlgorithmCode, lpPrivatekey, iPrivatekeyLength);
            byte[] bytAESKey = new byte[16];
            Array.Copy(bytSecureHashAlgorithmCode, 16, bytAESKey, 0, bytAESKey.Length);//memcpy(bytAESKey, &(bytSecureHashAlgorithmCode[GSYCGAAT_SHA256_CODE_LENGTH / 2]), GSYCGAAT_AES_KEY_LENGTH);

            SyrisAES.KeySize keysize;
            keysize = SyrisAES.KeySize.Bits128;
            Array.Copy(bytAESKey, 0, SyrisAES.AESKey, 0, SyrisAES.AESKey.Length);
            SyrisAES a = new SyrisAES(keysize);
            byte[] outputByteArray = new byte[16];
            a.UnAES(lpEncryptedSecret, outputByteArray);//GSYCGAATAES128ECBDecrypt((*lpSecret), lpSecretCipher, GSYCGAAT_AES_UNIT_LENGTH, bytAESKey)
            richTextBox5.Text= ToHexString(outputByteArray);//GSYCGAATBinaryToHEXString(strSecretCodeString, m_bytSecretCode, GSYCGAAT_SECRET_LENGTH);


        }
    }
}

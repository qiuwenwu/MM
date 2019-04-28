using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// 加解密拓展类
    /// </summary>
    public static class EncryptExpand
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public static string Ex { get; private set; }

        #region 不可逆加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">被解码的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string Md5(this string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] byteArray = Encoding.Default.GetBytes(str);
            byte[] output = md5.ComputeHash(byteArray);
            //释放资源
            md5.Clear();
            return BitConverter.ToString(output).Replace("-", "");
        }

        /// <summary>
        /// 哈希加密
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string EncodeHash(this string str)
        {
            byte[] Value;
            UnicodeEncoding Code = new UnicodeEncoding();
            byte[] Message = Code.GetBytes(str);
            SHA512Managed Arithmetic = new SHA512Managed();
            Value = Arithmetic.ComputeHash(Message);
            str = "";
            foreach (byte o in Value)
            {
                str += (int)o + "O";
            }
            return str;
        }

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string EncodeSHA1(this string str)
        {
            var sha1 = SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }
            return enText.ToString();
        }
        #endregion


        #region 加解密
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>返回加密后的字符串</returns>
        public static string EncodeDES(this string str, string key = "MATICSOFT")
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider()
            {
                Key = Encoding.ASCII.GetBytes(key.Substring(0, 8)),
                IV = Encoding.ASCII.GetBytes(key.Substring(0, 8))
            };
            byte[] bytes = Encoding.Default.GetBytes(str);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();

            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            stream.Close();
            return builder.ToString();
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="str">被解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>返回解密后的字符串</returns>
        public static string DecodeDES(this string str, string key = "MATICSOFT")
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider()
            {
                Key = Encoding.ASCII.GetBytes(key.Substring(0, 8)),
                IV = Encoding.ASCII.GetBytes(key.Substring(0, 8))
            };
            byte[] buffer = new byte[str.Length / 2];
            for (int i = 0; i < (str.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream.Close();
            return Encoding.Default.GetString(stream.ToArray());
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string EncodeBase64(this string str)
        {
            char[] Base64Code = new char[]{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
                                            'U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n',
                                            'o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
                                            '8','9','+','/','='};
            byte empty = 0;
            ArrayList byteMessage = new ArrayList(Encoding.Default.GetBytes(str));
            StringBuilder outmessage;
            int messageLen = byteMessage.Count;
            int page = messageLen / 3;
            int use;
            if ((use = messageLen % 3) > 0)
            {
                for (int i = 0; i < 3 - use; i++)
                    byteMessage.Add(empty);
                page++;
            }
            outmessage = new StringBuilder(page * 4);
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[3];
                instr[0] = (byte)byteMessage[i * 3];
                instr[1] = (byte)byteMessage[i * 3 + 1];
                instr[2] = (byte)byteMessage[i * 3 + 2];
                int[] outstr = new int[4];
                outstr[0] = instr[0] >> 2;
                outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                if (!instr[1].Equals(empty))
                    outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                else
                    outstr[2] = 64;
                if (!instr[2].Equals(empty))
                    outstr[3] = (instr[2] & 0x3f);
                else
                    outstr[3] = 64;
                outmessage.Append(Base64Code[outstr[0]]);
                outmessage.Append(Base64Code[outstr[1]]);
                outmessage.Append(Base64Code[outstr[2]]);
                outmessage.Append(Base64Code[outstr[3]]);
            }
            return outmessage.ToString();
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str">被解密的字符串</param>
        /// <returns>返回解密后的字符串</returns>
        public static string DecodeBase64(this string str)
        {
            //将空格替换为加号
            str = str.Replace(" ", "+");
            if ((str.Length % 4) != 0)
            {
                Ex = "包含不正确的BASE64编码";
                return "";
            }
            if (!Regex.IsMatch(str, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
            {
                Ex = "包含不正确的BASE64编码";
                return "";
            }
            string Base64Code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            int page = str.Length / 4;
            ArrayList outMessage = new ArrayList(page * 3);
            char[] message = str.ToCharArray();
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                byte[] outstr = new byte[3];
                outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                if (instr[2] != 64)
                {
                    outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                }
                else
                {
                    outstr[2] = 0;
                }
                if (instr[3] != 64)
                {
                    outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                }
                else
                {
                    outstr[2] = 0;
                }
                outMessage.Add(outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add(outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add(outstr[2]);
            }
            byte[] outbyte = (byte[])outMessage.ToArray(Type.GetType("System.Byte"));
            return Encoding.Default.GetString(outbyte);
        }
        #endregion

        #region 非对称加密
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <param name="xmlPubKey">xml公钥</param>
        /// <returns>返回加密后的字符串</returns>
        public static string EncodeRSA(this string str, string xmlPubKey)
        {
            byte[] PlainTextBArray;
            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPubKey);
            PlainTextBArray = (new UnicodeEncoding()).GetBytes(str);
            CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="str">被解密的字符串</param>
        /// <param name="xmlKey">xml私钥</param>
        /// <returns>返回解密后的字符串</returns>
        public static string DecodeRSA(this string str, string xmlKey)
        {
            byte[] PlainTextBArray;
            byte[] DypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlKey);
            PlainTextBArray = Convert.FromBase64String(str);
            DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
            Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            return Result;
        }
        #endregion
    }
}

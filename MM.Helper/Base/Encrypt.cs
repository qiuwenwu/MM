using System;

namespace MM.Helper.Base
{
    /// <summary>
    /// 加解密帮助类
    /// </summary>
    public class Encrypt
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; private set; }

        #region 不可逆加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">被解码的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public string Md5(string str)
        {
            return str.Md5();
        }

        /// <summary>
        /// 哈希加密
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public string EncodeHash(string str)
        {
            return str.EncodeHash();
        }

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public string EncodeSHA1(string str)
        {
            return str.EncodeSHA1();
        }
        #endregion


        #region 加解密
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>返回加密后的字符串</returns>
        public string EncodeDES(string str, string key = "MATICSOFT")
        {
            return str.EncodeDES(key);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="str">被解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>返回解密后的字符串</returns>
        public string DecodeDES(string str, string key = "MATICSOFT")
        {
            return str.DecodeDES(key);
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public string EncodeBase64(string str)
        {
            return str.EncodeBase64();
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str">被解密的字符串</param>
        /// <returns>返回解密后的字符串</returns>
        public string DecodeBase64(string str)
        {
            return str.DecodeBase64();
        }
        #endregion

        #region 非对称加密
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <param name="xmlPubKey">xml公钥</param>
        /// <returns>返回加密后的字符串</returns>
        public string EncodeRSA(string str, string xmlPubKey)
        {
            return str.EncodeRSA(xmlPubKey);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="str">被解密的字符串</param>
        /// <param name="xmlKey">xml私钥</param>
        /// <returns>返回解密后的字符串</returns>
        public string DecodeRSA(string str, string xmlKey)
        {
            return str.DecodeRSA(xmlKey);
        }
        #endregion
    }
}

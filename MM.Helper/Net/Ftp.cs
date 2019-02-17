using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MM.Helper.Net
{
    /// <summary>
    /// Ftp传输帮助类
    /// </summary>
    public class Ftp
    {
        #region 配置
        /// <summary>
        /// FTP服务器IP地址
        /// </summary>
        public string Host     { get; set; }

        /// <summary>
        /// FTP服务器端口
        /// </summary>
        public int Port        { get; set; } = 21;

        /// <summary>
        /// 当前服务器目录
        /// </summary>
        public string Dir      { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否登录
        /// </summary>
        public bool Connected  { get; private set; }

        /// <summary>
        /// 编码方式
        /// </summary>
        public Encoding Encode { get; set; } = Encoding.ASCII;
        #endregion


        #region 私有属性
        /// <summary>
        /// 服务器返回的应答信息(包含应答码)
        /// </summary>
        private string strMsg;
        /// <summary>
        /// 服务器返回的应答信息(包含应答码)
        /// </summary>
        private string strReply;
        /// <summary>
        /// 服务器返回的应答码
        /// </summary>
        private int iReplyCode;
        /// <summary>
        /// 进行控制连接的socket
        /// </summary>
        private Socket socket;
        /// <summary>
        /// 传输模式
        /// </summary>
        private TransferType trType;
        /// <summary>
        /// 接收和发送数据的缓冲区
        /// </summary>
        private static readonly int BLOCK_SIZE = 512;
        /// <summary>
        /// 字节数组
        /// </summary>
        private Byte[] buffer = new Byte[BLOCK_SIZE];

        /// <summary>
        /// 传输对象
        /// </summary>
        public static object obj = new object();
        #endregion


        #region 构造函数
        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public Ftp()
        {
            Host = "";
            Dir = "";
            Username = "";
            Password = "";
            Port = 21;
            Connected = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="host">远程主机地址</param>
        /// <param name="dir">远程路径</param>
        /// <param name="username">远程登录用户名</param>
        /// <param name="password">远程登录密码</param>
        /// <param name="port">远程端口</param>
        public Ftp(string host, string dir, string username, string password, int port)
        {
            Host = host;
            Dir = dir;
            Username = username;
            Password = password;
            Port = port;
            Connect();
        }

        /// <summary>
        /// 建立连接 
        /// </summary>
        public void Connect()
        {
            lock (obj)
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Host), Port);
                try
                {
                    socket.Connect(ep);
                }
                catch (Exception)
                {
                    throw new IOException("不能连接ftp服务器");
                }
            }
            ReadReply();
            if (iReplyCode != 220)
            {
                DisConnect();
                throw new IOException(strReply.Substring(4));
            }
            SendCmd("USER " + Username);
            if (!(iReplyCode == 331 || iReplyCode == 230))
            {
                Close();
                throw new IOException(strReply.Substring(4));
            }
            if (iReplyCode != 230)
            {
                SendCmd("PASS " + Password);
                if (!(iReplyCode == 230 || iReplyCode == 202))
                {
                    Close();
                    throw new IOException(strReply.Substring(4));
                }
            }
            Connected = true;
            SetDir(Dir);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void DisConnect()
        {
            if (socket != null)
            {
                SendCmd("QUIT");
            }
            Close();
        }

        /// <summary>
        /// 传输模式:二进制类型、ASCII类型
        /// </summary>
        public enum TransferType
        {
            /// <summary>
            /// 二进制模式
            /// </summary>
            Binary,
            /// <summary>
            /// ASCII模式
            /// </summary>
            ASCII
        };

        /// <summary>
        /// 设置传输模式
        /// </summary>
        /// <param name="type">传输模式</param>
        public void SetType(string type) {
            if (type.ToLower() == "ascii")
            {
                Encode = Encoding.ASCII;
            }
            else
            {
                Encode = Encoding.UTF7;
            }
        }

        /// <summary>
        /// 设置传输模式
        /// </summary>
        /// <param name="ttType">传输模式</param>
        public void SetTransferType(TransferType ttType)
        {
            if (ttType == TransferType.Binary)
            {
                SendCmd("TYPE I");//binary类型传输
            }
            else
            {
                SendCmd("TYPE A");//ASCII类型传输
            }
            if (iReplyCode != 200)
            {
                throw new IOException(strReply.Substring(4));
            }
            else
            {
                trType = ttType;
            }
        }

        /// <summary>
        /// 获得传输模式
        /// </summary>
        /// <returns>传输模式</returns>
        public TransferType GetTransferType()
        {
            return trType;
        }
        
        /// <summary>
        /// 获得文件列表
        /// </summary>
        /// <param name="search">文件名的匹配字符串</param>
        public string[] GetDir(string search)
        {
            if (!Connected)
            {
                Connect();
            }
            Socket socketData = CreateSocket();
            SendCmd("NLST " + search);
            if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226))
            {
                throw new IOException(strReply.Substring(4));
            }
            strMsg = "";
            Thread.Sleep(2000);
            while (true)
            {
                int iBytes = socketData.Receive(buffer, buffer.Length, 0);
                strMsg += Encode.GetString(buffer, 0, iBytes);
                if (iBytes < buffer.Length)
                {
                    break;
                }
            }
            char[] seperator = { '\n' };
            string[] strsFileList = strMsg.Split(seperator);
            socketData.Close(); //数据socket关闭时也会有返回码
            if (iReplyCode != 226)
            {
                ReadReply();
                if (iReplyCode != 226)
                {

                    throw new IOException(strReply.Substring(4));
                }
            }
            return strsFileList;
        }

        /// <summary>
        /// 新的传输标识符
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="guid">全局唯一标识符</param>
        public void NewPutByGuid(string file, string guid)
        {
            if (!Connected)
            {
                Connect();
            }
            string str = file.Substring(0, file.LastIndexOf("\\"));
            string strTypeName = file.Substring(file.LastIndexOf("."));
            guid = str + "\\" + guid;
            Socket socketData = CreateSocket();
            SendCmd("STOR " + Path.GetFileName(guid));
            if (!(iReplyCode == 125 || iReplyCode == 150))
            {
                throw new IOException(strReply.Substring(4));
            }
            FileStream input = new FileStream(guid, FileMode.Open);
            input.Flush();
            int iBytes = 0;
            while ((iBytes = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                socketData.Send(buffer, iBytes, 0);
            }
            input.Close();
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                ReadReply();
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    throw new IOException(strReply.Substring(4));
                }
            }
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>文件大小</returns>
        public long GetFileSize(string file)
        {
            if (!Connected)
            {
                Connect();
            }
            SendCmd("SIZE " + Path.GetFileName(file));
            long lSize = 0;
            if (iReplyCode == 213)
            {
                lSize = Int64.Parse(strReply.Substring(4));
            }
            else
            {
                throw new IOException(strReply.Substring(4));
            }
            return lSize;
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>文件大小</returns>
        public string GetFileInfo(string file)
        {
            if (!Connected)
            {
                Connect();
            }
            Socket socketData = CreateSocket();
            SendCmd("LIST " + file);
            string strResult = "";
            if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226 || iReplyCode == 250))
            {
                throw new IOException(strReply.Substring(4));
            }
            byte[] b = new byte[512];
            MemoryStream ms = new MemoryStream();

            while (true)
            {
                int iBytes = socketData.Receive(b, b.Length, 0);
                ms.Write(b, 0, iBytes);
                if (iBytes <= 0)
                {

                    break;
                }
            }
            byte[] bt = ms.GetBuffer();
            strResult = Encoding.ASCII.GetString(bt);
            ms.Close();
            return strResult;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="file">待删除文件名</param>
        public void Del(string file)
        {
            if (!Connected)
            {
                Connect();
            }
            SendCmd("DELE " + file);
            if (iReplyCode != 250)
            {
                throw new IOException(strReply.Substring(4));
            }
        }

        /// <summary>
        /// 重命名(如果新文件名与已有文件重名,将覆盖已有文件)
        /// </summary>
        /// <param name="file_old">旧文件名</param>
        /// <param name="file_new">新文件名</param>
        public void Move(string file_old, string file_new)
        {
            if (!Connected)
            {
                Connect();
            }
            SendCmd("RNFR " + file_old);
            if (iReplyCode != 350)
            {
                throw new IOException(strReply.Substring(4));
            }
            //  如果新文件名与原有文件重名,将覆盖原有文件
            SendCmd("RNTO " + file_new);
            if (iReplyCode != 250)
            {
                throw new IOException(strReply.Substring(4));
            }
        }

        /// <summary>
        /// 下载一批文件
        /// </summary>
        /// <param name="search">文件名的匹配字符串</param>
        /// <param name="dir">本地目录(不得以\结束)</param>
        public void Download(string search, string dir)
        {
            if (!Connected)
            {
                Connect();
            }
            string[] strFiles = GetDir(search);
            foreach (string strFile in strFiles)
            {
                if (!strFile.Equals(""))//一般来说strFiles的最后一个元素可能是空字符串
                {
                    Download(strFile, dir, strFile);
                }
            }
        }

        /// <summary>
        /// 下载一个文件
        /// </summary>
        /// <param name="file">要下载的文件名</param>
        /// <param name="dir">本地目录(不得以\结束)</param>
        /// <param name="localFile">保存在本地时的文件名</param>
        public void Download(string file, string dir, string localFile)
        {
            Socket socketData = CreateSocket();
            try
            {
                if (!Connected)
                {
                    Connect();
                }
                SetTransferType(TransferType.Binary);
                if (localFile.Equals(""))
                {
                    localFile = file;
                }
                SendCmd("RETR " + file);
                if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226 || iReplyCode == 250))
                {
                    throw new IOException(strReply.Substring(4));
                }
                FileStream output = new FileStream(dir + "\\" + localFile, FileMode.Create);
                while (true)
                {
                    int iBytes = socketData.Receive(buffer, buffer.Length, 0);
                    output.Write(buffer, 0, iBytes);
                    if (iBytes <= 0)
                    {
                        break;
                    }
                }
                output.Close();
                if (socketData.Connected)
                {
                    socketData.Close();
                }
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    ReadReply();
                    if (!(iReplyCode == 226 || iReplyCode == 250))
                    {
                        throw new IOException(strReply.Substring(4));
                    }
                }
            }
            catch
            {
                socketData.Close();
                socketData = null;
                socket.Close();
                Connected = false;
                socket = null;
            }
        }

        /// <summary>
        /// 下载一个文件
        /// </summary>
        /// <param name="file">要下载的文件名</param>
        /// <param name="dir">本地目录(不得以\结束)</param>
        /// <param name="localFile">保存在本地时的文件名</param>
        public void GetNoBinary(string file, string dir, string localFile)
        {
            if (!Connected)
            {
                Connect();
            }

            if (localFile.Equals(""))
            {
                localFile = file;
            }
            Socket socketData = CreateSocket();
            SendCmd("RETR " + file);
            if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226 || iReplyCode == 250))
            {
                throw new IOException(strReply.Substring(4));
            }
            FileStream output = new FileStream(dir + "\\" + localFile, FileMode.Create);
            while (true)
            {
                int iBytes = socketData.Receive(buffer, buffer.Length, 0);
                output.Write(buffer, 0, iBytes);
                if (iBytes <= 0)
                {
                    break;
                }
            }
            output.Close();
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                ReadReply();
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    throw new IOException(strReply.Substring(4));
                }
            }
        }

        /// <summary>
        /// 上传一批文件
        /// </summary>
        /// <param name="dir">本地目录(不得以\结束)</param>
        /// <param name="search">文件名匹配字符(可以包含*和?)</param>
        public void Upload(string dir, string search)
        {
            string[] strFiles = Directory.GetFiles(dir, search);
            foreach (string strFile in strFiles)
            {
                Upload(strFile);
            }
        }

        /// <summary>
        /// 上传一个文件
        /// </summary>
        /// <param name="localFile">本地文件名</param>
        public void Upload(string localFile)
        {
            if (!Connected)
            {
                Connect();
            }
            Socket socketData = CreateSocket();
            if (Path.GetExtension(localFile) == "")
                SendCmd("STOR " + Path.GetFileNameWithoutExtension(localFile));
            else
                SendCmd("STOR " + Path.GetFileName(localFile));

            if (!(iReplyCode == 125 || iReplyCode == 150))
            {
                throw new IOException(strReply.Substring(4));
            }

            FileStream input = new FileStream(localFile, FileMode.Open);
            int iBytes = 0;
            while ((iBytes = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                socketData.Send(buffer, iBytes, 0);
            }
            input.Close();
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                ReadReply();
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    throw new IOException(strReply.Substring(4));
                }
            }
        }

        /// <summary>
        /// 上传一个文件
        /// </summary>
        /// <param name="localFile">本地文件名</param>
        /// <param name="guid">全局唯一标识符</param>
        public void UploadByGuid(string localFile, string guid)
        {
            if (!Connected)
            {
                Connect();
            }
            string str = localFile.Substring(0, localFile.LastIndexOf("\\"));
            string strTypeName = localFile.Substring(localFile.LastIndexOf("."));
            guid = str + "\\" + guid;
            File.Copy(localFile, guid);
            File.SetAttributes(guid, FileAttributes.Normal);
            Socket socketData = CreateSocket();
            SendCmd("STOR " + Path.GetFileName(guid));
            if (!(iReplyCode == 125 || iReplyCode == 150))
            {
                throw new IOException(strReply.Substring(4));
            }
            FileStream input = new FileStream(guid, FileMode.Open, FileAccess.Read, FileShare.Read);
            int iBytes = 0;
            while ((iBytes = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                socketData.Send(buffer, iBytes, 0);
            }
            input.Close();
            File.Delete(guid);
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                ReadReply();
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    throw new IOException(strReply.Substring(4));
                }
            }
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dir">目录名</param>
        public void AddDir(string dir)
        {
            if (!Connected)
            {
                Connect();
            }
            SendCmd("MKD " + dir);
            if (iReplyCode != 257)
            {
                throw new IOException(strReply.Substring(4));
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dir">目录名</param>
        public void DelDir(string dir)
        {
            if (!Connected)
            {
                Connect();
            }
            SendCmd("RMD " + dir);
            if (iReplyCode != 250)
            {
                throw new IOException(strReply.Substring(4));
            }
        }

        /// <summary>
        /// 改变目录
        /// </summary>
        /// <param name="dir">新的工作目录名</param>
        public void SetDir(string dir)
        {
            if (dir.Equals(".") || dir.Equals(""))
            {
                return;
            }
            if (!Connected)
            {
                Connect();
            }
            SendCmd("CWD " + dir);
            if (iReplyCode != 250)
            {
                throw new IOException(strReply.Substring(4));
            }
            this.Dir = dir;
        }

        /// <summary>
        /// 将一行应答字符串记录在strReply和strMsg,应答码记录在iReplyCode
        /// </summary>
        private void ReadReply()
        {
            strMsg = "";
            strReply = ReadLine();
            iReplyCode = Int32.Parse(strReply.Substring(0, 3));
        }

        /// <summary>
        /// 建立进行数据连接的socket
        /// </summary>
        /// <returns>数据连接socket</returns>
        private Socket CreateSocket()
        {
            SendCmd("PASV");
            if (iReplyCode != 227)
            {
                throw new IOException(strReply.Substring(4));
            }
            int index1 = strReply.IndexOf('(');
            int index2 = strReply.IndexOf(')');
            string ipData = strReply.Substring(index1 + 1, index2 - index1 - 1);
            int[] parts = new int[6];
            int len = ipData.Length;
            int partCount = 0;
            string buf = "";
            for (int i = 0; i < len && partCount <= 6; i++)
            {
                char ch = Char.Parse(ipData.Substring(i, 1));
                if (Char.IsDigit(ch))
                    buf += ch;
                else if (ch != ',')
                {
                    throw new IOException("Malformed PASV strReply: " + strReply);
                }
                if (ch == ',' || i + 1 == len)
                {
                    try
                    {
                        parts[partCount++] = Int32.Parse(buf);
                        buf = "";
                    }
                    catch (Exception)
                    {
                        throw new IOException("Malformed PASV strReply: " + strReply);
                    }
                }
            }
            string ipAddress = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];
            int port = (parts[4] << 8) + parts[5];
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            try
            {
                s.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("无法连接ftp服务器");
            }
            return s;
        }

        /// <summary>
        /// 关闭socket连接(用于登录以前)
        /// </summary>
        private void Close()
        {
            lock (obj)
            {
                if (socket != null)
                {
                    socket.Close();
                    socket = null;
                }
                Connected = false;
            }
        }

        /// <summary>
        /// 读取Socket返回的所有字符串
        /// </summary>
        /// <returns>包含应答码的字符串行</returns>
        private string ReadLine()
        {
            lock (obj)
            {
                while (true)
                {
                    int iBytes = socket.Receive(buffer, buffer.Length, 0);
                    strMsg += Encode.GetString(buffer, 0, iBytes);
                    if (iBytes < buffer.Length)
                    {
                        break;
                    }
                }
            }
            char[] seperator = { '\n' };
            string[] mess = strMsg.Split(seperator);
            if (strMsg.Length > 2)
            {
                strMsg = mess[mess.Length - 2];
            }
            else
            {
                strMsg = mess[0];
            }
            if (!strMsg.Substring(3, 1).Equals(" ")) //返回字符串正确的是以应答码(如220开头,后面接一空格,再接问候字符串)
            {
                return ReadLine();
            }
            return strMsg;
        }

        /// <summary>
        /// 发送命令并获取应答码和最后一行应答字符串
        /// </summary>
        /// <param name="cmd">命令</param>
        public void SendCmd(string cmd)
        {
            lock (obj)
            {
                Byte[] cmdBytes = Encoding.ASCII.GetBytes((cmd + "\r\n").ToCharArray());
                socket.Send(cmdBytes, cmdBytes.Length, 0);
                Thread.Sleep(500);
                ReadReply();
            }
        }
        #endregion
    }
}

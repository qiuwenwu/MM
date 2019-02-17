using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;

namespace MM.Helper.Sys
{
    /// <summary>
    /// Zip压缩帮助类
    /// </summary>
    public class Zip
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>  
        /// 获取所有文件  
        /// </summary>  
        /// <returns></returns>
        private Dictionary<string, DateTime> GetAllFies(string dir)
        {
            var dict = new Dictionary<string, DateTime>();
            DirectoryInfo fileDire = new DirectoryInfo(dir);
            if (!fileDire.Exists)
            {
                Ex = "目录:" + fileDire.FullName + "没有找到!";
                return null;
            }

            GetAllDirFiles(fileDire, dict);
            GetAllDirsFiles(fileDire.GetDirectories(), dict);
            return dict;
        }

        /// <summary>  
        /// 获取一个文件夹下的所有文件夹里的文件  
        /// </summary>  
        /// <param name="dirs"></param>  
        /// <param name="dict"></param>
        private void GetAllDirsFiles(IEnumerable<DirectoryInfo> dirs, Dictionary<string, DateTime> dict)
        {
            foreach (DirectoryInfo dir in dirs)
            {
                foreach (FileInfo file in dir.GetFiles("*.*"))
                {
                    dict.Add(file.FullName, file.LastWriteTime);
                }
                GetAllDirsFiles(dir.GetDirectories(), dict);
            }
        }

        /// <summary>  
        /// 获取一个文件夹下的文件  
        /// </summary>  
        /// <param name="dir">目录名称</param>
        /// <param name="dict">文件列表HastTable</param>  
        private static void GetAllDirFiles(DirectoryInfo dir, Dictionary<string, DateTime> dict)
        {
            foreach (FileInfo file in dir.GetFiles("*.*"))
            {
                dict.Add(file.FullName, file.LastWriteTime);
            }
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="dir">文件夹</param>
        /// <param name="zipFile">压缩后的文件名</param>
        /// <param name="password">压缩密码</param>
        /// <param name="level">压缩率0（无压缩）9（压缩率最高）</param>
        public void ZipDir(string dir, string zipFile, string password = null, int level = 9)
        {
            if (Path.GetExtension(zipFile) != ".zip")
            {
                zipFile = zipFile + ".zip";
            }
            using (var zipOutput = new ZipOutputStream(File.Create(zipFile)))
            {
                zipOutput.SetLevel(level);
                if (!string.IsNullOrEmpty(password))
                {
                    zipOutput.Password = password;
                }
                Crc32 crc = new Crc32();
                var dict = GetAllFies(dir);
                foreach (var o in dict)
                {
                    FileStream fs = new FileStream(o.Key, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    // ZipEntry entry = new ZipEntry(item.Key.ToString().Substring(dirToZip.Length + 1));
                    ZipEntry entry = new ZipEntry(Path.GetFileName(o.Key))
                    {
                        DateTime = o.Value,
                        Size = fs.Length
                    };
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zipOutput.PutNextEntry(entry);
                    zipOutput.Write(buffer, 0, buffer.Length);
                }
            }
        }

        /// <summary>  
        /// 解压zip格式的文件  
        /// </summary>  
        /// <param name="zipFile">压缩文件路径</param>
        /// <param name="dir">解压文件存放路径,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹</param>  
        /// <returns>解压是否成功</returns>
        public bool UnZip(string zipFile, string dir)
        {
            if (zipFile == string.Empty)
            {
                Ex = "压缩文件不能为空！";
                return false;
            }
            if (!File.Exists(zipFile))
            {
                Ex = "压缩文件不存在！";
                return false;
            }
            var bl = false;
            //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹  
            if (dir == string.Empty)
            {
                dir = zipFile.Replace(Path.GetFileName(zipFile), Path.GetFileNameWithoutExtension(zipFile));
            }
            if (!dir.EndsWith("/"))
            {
                dir += "/";
            }
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var zipInput = new ZipInputStream(File.OpenRead(zipFile)))
            {
                ZipEntry theEntry;
                while ((theEntry = zipInput.GetNextEntry()) != null)
                {
                    string dirName = Path.GetDirectoryName(theEntry.Name);
                    if (!string.IsNullOrEmpty(dirName))
                    {
                        Directory.CreateDirectory(dir + dirName);
                    }

                    string fileName = Path.GetFileName(theEntry.Name);
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        using (FileStream streamWriter = File.Create(dir + theEntry.Name))
                        {
                            int size;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = zipInput.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return bl;
        }

        /// <summary> 
        /// 压缩单个文件 
        /// </summary> 
        /// <param name="file">要进行压缩的文件名，全路径</param> 
        /// <param name="zipFile">压缩后生成的压缩文件名,全路径</param>
        /// <param name="password">压宿密码</param>
        /// <param name="level">压缩级别</param>
        public bool ZipFile(string file, string zipFile, string password = null, int level = 6)
        {
            // 如果文件没有找到，则报错 
            if (!File.Exists(file))
            {
                Ex = "指定要压缩的文件: " + file + " 不存在!";
                return false;
            }
            var bl = false;
            using (FileStream fileStream = File.OpenRead(file))
            {
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length);
                fileStream.Close();
                using (FileStream zipStream = File.Create(zipFile))
                {
                    using (ZipOutputStream zipOutput = new ZipOutputStream(zipStream))
                    {
                        // string fileName = fileToZip.Substring(fileToZip.LastIndexOf("\\") + 1);
                        string fileName = Path.GetFileName(file);
                        var zipEntry = new ZipEntry(fileName)
                        {
                            DateTime = DateTime.Now,
                            IsUnicodeText = true
                        };
                        zipOutput.PutNextEntry(zipEntry);
                        zipOutput.SetLevel(level);
                        if (!string.IsNullOrEmpty(password))
                        {
                            zipOutput.Password = password;
                        }
                        zipOutput.Write(buffer, 0, buffer.Length);
                        zipOutput.Finish();
                        zipOutput.Close();
                        bl = true;
                    }
                }
            }
            return bl;
        }

        /// <summary>
        /// 压缩多个目录或文件
        /// </summary>
        /// <param name="DirOrFileList">待压缩的文件夹或者文件，全路径格式,是一个集合</param>
        /// <param name="zipFile">压缩后的文件名，全路径格式</param>
        /// <param name="password">压宿密码</param>
        /// <param name="level">压缩级别</param>
        /// <returns>压缩成功返回true，失败返回false</returns>
        public bool ZipMore(IEnumerable<string> DirOrFileList, string zipFile, string password = null, int level = 6)
        {
            bool res = true;
            using (var s = new ZipOutputStream(File.Create(zipFile)))
            {
                s.SetLevel(level);
                if (!string.IsNullOrEmpty(password))
                {
                    s.Password = password;
                }
                foreach (string fileOrDir in DirOrFileList)
                {
                    //是文件夹
                    if (Directory.Exists(fileOrDir))
                    {
                        res = ZipFileDir(fileOrDir, s, "");
                    }
                    else
                    {
                        //文件
                        res = ZipFileStream(fileOrDir, s);
                    }
                }
                s.Finish();
                s.Close();
                return res;
            }
        }

        /// <summary>
        /// 带压缩流压缩单个文件
        /// </summary>
        /// <param name="file">要进行压缩的文件名</param>
        /// <param name="zipOutput"></param>
        /// <returns></returns>
        private bool ZipFileStream(string file, ZipOutputStream zipOutput)
        {
            //如果文件没有找到，则报错
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("指定要压缩的文件: " + file + " 不存在!");
            }
            //FileStream fs = null;
            FileStream zipFile = null;
            ZipEntry zipEntry = null;
            bool res = true;
            try
            {
                zipFile = File.OpenRead(file);
                byte[] buffer = new byte[zipFile.Length];
                zipFile.Read(buffer, 0, buffer.Length);
                zipFile.Close();
                zipEntry = new ZipEntry(Path.GetFileName(file));
                zipOutput.PutNextEntry(zipEntry);
                zipOutput.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (zipEntry != null)
                {
                }

                if (zipFile != null)
                {
                    zipFile.Close();
                }
                GC.Collect();
                GC.Collect(1);
            }
            return res;

        }

        /// <summary>
        /// 递归压缩文件夹
        /// </summary>
        /// <param name="dir">文件夹</param>
        /// <param name="zipOutput">zip输出流</param>
        /// <param name="parentDir">父文件夹的名字</param>
        private bool ZipFileDir(string dir, ZipOutputStream zipOutput, string parentDir)
        {
            bool res = true;
            ZipEntry entry = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();
            try
            {
                //创建当前文件夹
                entry = new ZipEntry(Path.Combine(parentDir, Path.GetFileName(dir) + "/")); //加上 “/” 才会当成是文件夹创建
                zipOutput.PutNextEntry(entry);
                zipOutput.Flush();
                //先压缩文件，再递归压缩文件夹
                var filenames = Directory.GetFiles(dir);
                foreach (string file in filenames)
                {
                    //打开压缩文件
                    fs = File.OpenRead(file);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    entry = new ZipEntry(Path.Combine(parentDir, Path.GetFileName(dir) + "/" + Path.GetFileName(file)))
                    {
                        DateTime = DateTime.Now,
                        Size = fs.Length
                    };
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zipOutput.PutNextEntry(entry);
                    zipOutput.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
                if (entry != null)
                {
                }
                GC.Collect();
                GC.Collect(1);
            }
            var folders = Directory.GetDirectories(dir);
            foreach (string folder in folders)
            {
                if (!ZipFileDir(folder, zipOutput, Path.Combine(parentDir, Path.GetFileName(dir))))
                {
                    return false;
                }
            }
            return res;
        }
    }
}
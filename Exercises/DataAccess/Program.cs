using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            #region FILE
            //IO 
            //DriveInfo
            DriveInfo[] driveInfos = DriveInfo.GetDrives();
            //Directories
            //one operation
            var dir = Directory.CreateDirectory(@"C:\Users\roberto_2\TestEsame\testdir");
            dir.MoveTo(@"C:\Users\roberto_2\TestEsame\testdir1");
            //more operation is better
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\roberto_2\TestEsame\testdir1");
            if (!di.Exists)
                di.Create();
            //se l'utent con cui gira la app ha i giusti pemressi può dare permessi aal directory
            DirectorySecurity ds = di.GetAccessControl();
            ds.AddAccessRule(new FileSystemAccessRule("everyone", FileSystemRights.ReadAndExecute, AccessControlType.Allow));
            di.SetAccessControl(ds);
            ListDir(di, "*a*", 5, 0);
            di.MoveTo(@"C:\Users\roberto_2\TestEsame\testdir5");
            foreach (string file in Directory.GetFiles(@"C:\Users\roberto_2\TestEsame\testdir2"))
            {
                Console.WriteLine(file);
            }

            foreach (FileInfo file in di.GetFiles())
            {
                Console.WriteLine(file.Name);
            }
            DirectoryInfo di4 = new DirectoryInfo(@"C:\Users\roberto_2\TestEsame\testdir4");
            di4.Create();
            string path = @"C:\Users\roberto_2\TestEsame\testdir2\test.txt";
            string destpath = @"C:\Users\roberto_2\TestEsame\testdir4\test.txt";
            if (File.Exists(path))
                File.Delete(path);
            FileInfo finfo = new FileInfo(path);
            finfo.MoveTo(destpath);
            string path1 = @"C:\Users\roberto_2\TestEsame\testdir4";
            string filename = @"test.dat";
            Path.Combine(path1, filename);//C:\\Users\\roberto_2\\TestEsame\\testdir3\\test.dat
            Console.WriteLine(Path.GetDirectoryName(path1));

            #endregion

            #region STREAM
            //stream is series of byte
            //file is series of byte stored in hd
            //socket is series of byte
            //READ STREAM :
            //can translate bytes in file
            //can deserialize in obj
            //WRITE STREAM
            //translate obj in series of bytes and send to stream then can send cross a network or store in file
            //convert char in bytes is call encode  CHAR take 2 byte in memory
            //encode in UTF8 or ASCII
            //SEEKING
            //position of stream file have cocept of position, sokcet no.

            string path0 = @"C:\Users\roberto_2\TestEsame\test1.dat";

            //WRITE
            //1 si crea uno stream
            //2 si fa un encode di una stringa(una serie di char) in un array di bytes
            //3 scrivo bytes dentro stream file for store
            using (FileStream fileStream = File.Create(path0))
            {
                string content = "test";
                byte[] data = Encoding.UTF8.GetBytes(content);
                fileStream.Write(data,0,data.Length);
            }

            using (StreamWriter streamWriter = File.CreateText(path0))
            {
                string content = "test";
                streamWriter.Write(content);
            }

            //READ
            using (FileStream fileStream = File.OpenRead(path0))
            {
                byte[] data = new byte[fileStream.Length];

                for (int i = 0; i < fileStream.Length; i++)
                {
                    data[i] = (byte) fileStream.ReadByte();
                }
                Console.WriteLine(Encoding.UTF8.GetString(data));
            }
          

            using (StreamReader streamWriter = File.OpenText(path0))
            {
                Console.WriteLine(streamWriter.ReadLine());
            }


            using (FileStream fileStream = File.Create(path0))
            {
                using (BufferedStream buffer = new BufferedStream(fileStream))
                {
                    using (StreamWriter streamWriter = new StreamWriter(buffer))
                    {
                        streamWriter.WriteLine("asas");
                    } 
                }
            }
            #endregion

            #region NETWORK
            WebRequest request = WebRequest.Create("http:\\www.google.it");
            WebResponse response = request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            string resp = reader.ReadToEnd();
            Console.WriteLine(resp);
            response.Close();

            #endregion


            Console.ReadLine();
        }
           
       


        private static void ListDir(DirectoryInfo di, string pattern, int levelmax, int currentlev)
        {
            if (currentlev >= levelmax)
            {
                return;
            }
            try
            {
                DirectoryInfo[] subDirs = di.GetDirectories(pattern);
                foreach (var subdir in subDirs)
                {
                    ListDir(subdir, pattern, levelmax, currentlev);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return;
            }
            catch (DirectoryNotFoundException ex1)
            {
                return;
            }
        }
    }
}

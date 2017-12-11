using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

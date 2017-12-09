using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {

            //IO 
            //DriveInfo
            DriveInfo[] driveInfos = DriveInfo.GetDrives();
            //Directories
            //one operation
            var dir = Directory.CreateDirectory(@"C:\Users\roberto_2\TestEsame\testdir");
            //more operation is better
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\roberto_2\TestEsame\testdir1");
            if (!di.Exists)
                di.Create();




        }
    }
}

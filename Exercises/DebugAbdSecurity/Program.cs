#define MySymbol
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DebugAbdSecurity
{

    class Program
    {
        static void Main(string[] args)
        {

            //TRACE
            TraceSource traceSource = new TraceSource("my", SourceLevels.All);
            traceSource.TraceInformation("Trace AppDomain");
            traceSource.TraceEvent(TraceEventType.Critical, 0,"critical");
            traceSource.TraceData(TraceEventType.Information,1,new object[] {"s", "s"});
            traceSource.Flush();
            traceSource.Close();
            
            //EVENT
            //se succede qualcosa vedrò nel event viwer un log chiamato newlog con un source chiamamto mysource con un evento nella descriono 
            //con scritto logevent
            if (!EventLog.SourceExists("mysource"))
            {
            EventLog.CreateEventSource("mysource", "newlog");
              //
                return;
            }
            EventLog mylog = new EventLog();
            mylog.Source = "mysource";
            mylog.WriteEntry("log event");


            //PROFILING
            Stopwatch sw = new Stopwatch();
            sw.Start();
            LongTAsk();
            sw.Stop();
            sw.Reset();
            Console.WriteLine(sw.Elapsed);//milliecond
            
            //Visaul Studios .studiare meglio la parte visuali di profiler have wizard tool for profile .Menu_> Analyze


            //Parse tryparse 
            string value = "true";
            bool res;
            var success = bool.TryParse(value, out res);
            string ss = "1";
            try
            {
                bool ress = bool.Parse(ss);
            }
            catch (Exception ex)
            {

                Debug.Assert(ss == "1");
                Debug.WriteLine(ex.Message);
            }
            //parse take only string convert take evythingconvert

            int rr = 5;
            double sss = Convert.ToDouble(rr);

            //regex

            //Javascriptserializer
            //ValidateXML


            //SYMMETRIC ASYMMETRIC ENCRYPTION
            //AES symmetric

            //RSACryptoserviceProvider asymmetric
            //DSACryptoserviceProvider asymmetric

            //SHA256hashcodeManage for hash

            //CA certificate authority
            //PKI public key infrastructure
            //x.509 certificates

            //NET FRAMEWORK use CAS (System.Security.CodeAccessPermission)
            //code access security restrict type resoucere can app use and opration can do
            //in pc or intarent app is FULL TRUSTED
            //in sandbox like nrowser clr chack what app can do:
            //use file
            //use unmanged code
            //SO CAS check:permission for resource, etc..
            //every call stack is check
            //Specify CAS : DCLARATIVE OR IMPERATIVE 

            //SECURE STRING !!!!!

            //File PDb program database include:
            //1 source file name and lines
            //2 local varible names
            //The file have guid that is same of dll .
            //the guid is generated at comile time so you compile dll e create a pdb with same guid and you can debug


            //DEBUG LOG AND TARCE APP
            //1 debug class only use in debug mode System.Diagnostics;

        }

        private static void LongTAsk()
        {
            for (int i = 0; i < 1000; i++)
            {
                int count = 0;
            }
        }

        //DIRECTIVES ce ne sono molte!!!!pagg 224
        //avoid directivers make code hard to undestud 
        //for example if use winrt or .net 4.5 the asswmbly property by reflection change 
        //and can use directive for choose 
        public static void DebugDirectve()
        {
              #if DEBUG
                  Console.WriteLine("debugprod");
              #else
                   Console.WriteLine("prod");
              #endif
        }

        public static void MySymbolDirectve()
        {
          #if MySymbol
            Console.WriteLine("MySymbol");
         #else
                   Console.WriteLine("prod");
         #endif
        }

        [FileIOPermission(SecurityAction.Demand, AllLocalFiles = FileIOPermissionAccess.Read)]
        static void casDeclarativeMethod()
        {
            //...
        }

        static void casImperativeMethod()
        {
            FileIOPermission fio = new FileIOPermission(PermissionState.None);
            fio.AllLocalFiles = FileIOPermissionAccess.Read;
        }
    }

    //data annotations
    class user
    {
        [Required, MaxLength(20)]
        private string name;
        private string surname;

    }
}

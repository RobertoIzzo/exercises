using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            //Parse tryparse 
            string value = "true";
            bool res;
            var success = bool.TryParse(value, out res);

            try
            {
                string ss = "1";
                bool ress = bool.Parse(ss);
            }
            catch (Exception)
            {
                    
                throw;
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

        }

        [FileIOPermission(SecurityAction.Demand, AllLocalFiles = FileIOPermissionAccess.Read)]
        static void casDeclarativeMethod()
        {
            //...
        }

        static void casImperativeMethod()
        {
            FileIOPermission fio = new FileIOPermission(PermissionState.None);
            fio.AllLocalFiles =FileIOPermissionAccess.Read;
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FCollector
{
    /// <summary>
    /// Clr provide free memroy for all obj (managed) bultin and own user obj
    /// for unmanaged resource or the own obj that use unmanaged resource we HAVE use IDISPOSABLE interface that foce to call UnmanagedResource.Dispose 
    /// for free memory 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Pippo pippo = new Pippo();
                pippo.Dispose();
                pippo.end();
            }
            catch (Exception ex) 
            {
                
                Console.WriteLine(ex.Message);
            }

          

            //finalizer
            StreamWriter st = File.CreateText("");
            st.Write("");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.Delete("");

            //IDisposable
            StreamWriter st1 = File.CreateText("");
            st1.Write("");
            st1.Dispose();
            File.Delete("");

            //using
            using (st)
            {

            }

            using (Test tt = new Test())
            {

            }

          

        }


        // Unsafe method: takes pointer to int:
        unsafe static void SquarePtrParam(int* p)
        {
            *p *= *p;
        }

        unsafe static void Main()
        {
            int i = 5;
            // Unsafe method: uses address-of operator (&):
            SquarePtrParam(&i);
            Console.WriteLine(i);
        }

        void static testUnsafe()
        {
            
        }
    }

    public class Test : IDisposable
    {
        private IntPtr unmanagedBuffer;
        public FileStream stream { get; private set; }
        public Test()
        {
            CreateBuffer();
            stream = File.Open("", FileMode.Create);
        }

        //finalizer => GC sta chiamando il finalizer perchè qualcuno si è dimenticato di disposare l' oggetto allora lo uccidiamo con il marshal
        ~Test()
        {
            Dispose(false);
        }

        private void CreateBuffer()
        {
            Byte[] data = new byte[2014];

            unmanagedBuffer = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, unmanagedBuffer, data.Length);
        }

        public void Close()
        {
            Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            //de l utente si ricorda di disposare l'oggetto allora lo togliamo dalla lista perchè poi il GC lo vuole uccidere ma non lo trova 
            //più perchè lo abbiamo ucciso noi ;)
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //free unmanaged resource
            Marshal.FreeHGlobal(unmanagedBuffer);
            if (disposing)
            {
                // free managed resources
                //il close fa il DISPOSE :)
                stream?.Close();
            }
        }
    }


    public class Pippo :IDisposable
    {
        private FileStream stream;
        public Pippo()
        {
            stream = File.Open(@"C:\Users\roberto_2\TestEsame\test.txt", FileMode.Create);
        }
        public void Dispose()
        {
            stream.Dispose();
        }

        public void end()
        {
            File.Delete(@"C:\Users\roberto_2\TestEsame\test.txt");
        }
    }
}

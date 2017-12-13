using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait1
{

    //I/O ASYNC AWAIT
    //wpf main thread id UI
    //asp.net  main thread is iis
    class Program
    {
        static void Main(string[] args)
        {
            //page 267
            //file async

            //MULTIPLE REQUEST

        }

        static async Task ExecMultiple()
        {
            HttpClient client = new HttpClient();
            Task microsoft = client.GetStringAsync(@"http:\\www.microsoft.com");
            Task google = client.GetStringAsync(@"http:\\www.google.com");
            Task firefox = client.GetStringAsync(@"http:\\www.firefox.com");

            //non aspetta che finisca microsoft ma esegue google e firefox
            await Task.WhenAll(microsoft, google, firefox);

        }
    }
}

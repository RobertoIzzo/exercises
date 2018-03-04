using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMyClass _myClass;
        public HomeController(IMyClass myClass )
        {
            _myClass = myClass;
        }

        public async Task<ActionResult> Index()
        {
            string res = await _myClass.DoAsync();
            Console.WriteLine("fien");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    public class MyClass :IMyClass
    {
        private Idoit _Idoit;
        public MyClass(Idoit idoit)
        {
            this._Idoit = idoit;
        }
        public async Task<string> DoAsync()
        {
           var res = await _Idoit.DoIt();
            Console.WriteLine("");
            return res;
        }
    }

    public interface IMyClass
    {
        Task<string> DoAsync();
    }

    public interface Idoit
    {
         Task<string> DoIt();
    }


    public class Do : Idoit
    {
        public Task<string> DoIt()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(10000);
                return "";

            });
        }
    }
}
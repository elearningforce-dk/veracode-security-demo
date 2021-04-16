using System.Web.Mvc;

namespace VeraDemoNet.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult AccessDenied()  
        {  
            return View();  
        }  
    }
}
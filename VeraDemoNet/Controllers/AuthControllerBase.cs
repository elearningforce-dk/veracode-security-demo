using System.Linq;
using System.Web.Mvc;
using VeraDemoNet.DataAccess;
using System.Web.Helpers;

namespace VeraDemoNet.Controllers
{
    public abstract class AuthControllerBase : Controller
    {
        protected BasicUser LoginUser(string userName, string passWord)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }

            using (var dbContext = new BlabberDB())
            {
                var found = dbContext.Users.FirstOrDefault(x => x.UserName == userName);

                if(found != null && Crypto.VerifyHashedPassword(found.Password, passWord))
                {
                    Session["username"] = userName;
                    return new BasicUser(found.UserName, found.BlabName, found.RealName);
                }
            }

            return null;
        }

        protected string GetLoggedInUsername()
        {
            return Session["username"].ToString();
        }

        protected void LogoutUser()
        {
            Session["username"] = null;
        }

        protected bool IsUserLoggedIn()
        {
            return string.IsNullOrEmpty(Session["username"] as string) == false;

        }

        protected RedirectToRouteResult RedirectToLogin(string targetUrl)
        {
            return new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                (new
                {
                    controller = "Account",
                    action = "Login",
                    ReturnUrl = HttpContext.Request.RawUrl
                }));
        }
    }
}
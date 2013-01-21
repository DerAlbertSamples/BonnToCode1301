using System.Web.Mvc;

namespace Sample.Controllers
{
    public class ToDoController : Controller
    {
         public ActionResult Index()
         {
             return View();
         }
    }
}
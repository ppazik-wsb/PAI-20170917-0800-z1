using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public string Index()
        {
            return "TestController>Index";
        }

        public string TestString()
        {
            return "TestController>TestString";
        }

        public ActionResult TestRawJson()
        {
            return Json(new { foo = "bar", atr = "atribute" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TestObjJson()
        {
            var oStudent = new Models.Student { ID = 666, Imie = "Jan", Nazwisko = "Kowalski" };
            return Json(oStudent, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TestAuth(string poufne)
        {
            if (poufne?.CompareTo("JanKowalski") == 0)
            {
                return Content("Super, że jesteś Janie Kowalski");
            }

            return new HttpUnauthorizedResult("Niestety ale nie masz uprawnień!");
        }

        public ActionResult TestAuthExt(string name)
        {
            return TestAuth(name);
        }

        public ActionResult TestRedirect(string actionName)
        {
            if(String.IsNullOrWhiteSpace(actionName))
            {
                return new EmptyResult();
            }

            return RedirectToRoute(actionName);
        }
    }
}
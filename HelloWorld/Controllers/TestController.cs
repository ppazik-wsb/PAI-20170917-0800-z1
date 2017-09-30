using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public ActionResult IndexView()
        {
            Dictionary<string,string> lista = new Dictionary<string, string>();

            lista.Add("Index", "Index");
            lista.Add("Index View", "IndexView");
            lista.Add("Test String", "TestString");
            lista.Add("Test Raw Json", "TestRawJson");
            lista.Add("Test Object Json", "TestObjJson");
            lista.Add("Test Auth", "TestAuth");
            lista.Add("Test Auth Ext", "TestAuthExt");
            lista.Add("Test Redirect", "TestRedirect");

            ViewBag.ListaObiektow = lista;

            return View("Index");
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
                // Zwrócić pustą stronę
                //return new EmptyResult();

                // Zwrócić 404
                return new HttpNotFoundResult("Niestety strony nie znaleziono!");
            }

            return RedirectToRoute(actionName);
        }

        public ActionResult Table()
        {
            List<Student> listaStudentow = new List<Student>();

            listaStudentow.Add(new Student() { ID = 1, Imie = "Jan", Nazwisko = "Nowak", DataNarodzin = DateTime.Parse("1999-02-03 05:45") });
            listaStudentow.Add(new Student() { ID = 2, Imie = "Anna", Nazwisko = "Złotopolska", DataNarodzin = DateTime.Parse("2001-03-09 12:15") });
            listaStudentow.Add(new Student() { ID = 3, Imie = "Adam", Nazwisko = "Mickiewicz", DataNarodzin = DateTime.Parse("1920-12-01 12:12") });
            listaStudentow.Add(new Student() { ID = 4, Imie = "Zdzisław", Nazwisko = "Testowy", DataNarodzin = DateTime.Parse("1900-01-03 05:45") });

            return View(listaStudentow);
        }
    }
}
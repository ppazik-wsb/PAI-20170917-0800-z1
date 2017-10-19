using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Owin;
using System.Web.Http;

namespace PAI.Selfhosted.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8081";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Server started on {url}");
                Console.WriteLine("Press Enter to stop server.");
                Console.ReadLine();

                Console.WriteLine("Server is stopping...");
            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(async (envirotment, next) =>
            {
                Console.WriteLine("Request: " + envirotment.Request.Path);

                await next();

                Console.WriteLine("Response: " + envirotment.Response.StatusCode);
            });

            ConfigureWebApi(app);

            // Gdy jest podpięty ten kontroler do pipeline, będziemy dostawać wypis wszystkich wpisów do środowiska oraz dopisze swój nowy wpis WebApiFive.
            app.Use<HighFiveComponent>();

            // Komponent LowFive będzie sprawdzał czy dany klucz występuje w środowisku i wypisze jego wartość. Na koniec wyświetli zawartość odpowiedzi do klienta.
            app.Use<LowFiveComponent>();

            // By przetestować welcome page skompiluj, uruchom aplikację i w swojej przeglądarce np. Chrome wpisz http://localhost:8081/
            app.UseWelcomePage();


        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html")); // Obsługa serializacji do JSON zamiast standardowo XML
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new
            {
                id = RouteParameter.Optional
            });
            app.UseWebApi(config); // Dorzucenie obsługi WebApi do OWIN
        }
    }

    // Middleware component - dodatkowy komponent w pipeline Owin/Katana
    public class HighFiveComponent
    {
        Func<IDictionary<string, object>, Task> _next;

        public HighFiveComponent(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> enviroment)
        {
            var envCopy = new Dictionary<string, object>(enviroment);

            foreach (var item in enviroment)
            {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }

            enviroment.Add("WebApiFive", "To jest mój nowy klucz :)");

            await _next(enviroment);
        }
    }

    public class LowFiveComponent
    {
        Func<IDictionary<string, object>, Task> _next;

        public LowFiveComponent(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> enviroment)
        {
            IOwinContext ctx = new OwinContext(enviroment);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(enviroment.ContainsKey("WebApiFive") ? $"Wartość klucza WebApiFive: {enviroment["WebApiFive"]}" : "Brakuje klucza WebApiFive!!!");
            Console.ResetColor();

            await _next(enviroment);
        }
    }
}

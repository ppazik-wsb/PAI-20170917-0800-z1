using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Owin;

namespace PAI.Selfhosted.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8080";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Server started on {url}");
                Console.ReadKey();

                Console.WriteLine("Server is stopping...");
            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Gdy jest podpięty ten kontroler do pipeline, będziemy dostawać wypis wszystkich wpisów do środowiska oraz dopisze swój nowy wpis WebApiFive.
            app.Use<HighFiveComponent>();

            // Komponent LowFive będzie sprawdzał czy dany klucz występuje w środowisku i wypisze jego wartość. Na koniec wyświetli zawartość odpowiedzi do klienta.
            app.Use<LowFiveComponent>();

            // By przetestować welcome page skompiluj, uruchom aplikację i w swojej przeglądarce np. Chrome wpisz http://localhost:8080/
            app.UseWelcomePage();
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

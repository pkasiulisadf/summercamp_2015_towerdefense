using Microsoft.Owin.Hosting;

namespace Adform.SummerCamp.TowerDefense.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
            // for more information.
            string url = "http://localhost:43210";
            using (WebApp.Start<Startup>(url))
            {
                System.Console.WriteLine("Server running on {0}", url);
                System.Console.ReadLine();
            }
        }
    }
}
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        const string userName = "myuser@busiontech.com";
        const string password = "mysecurepassword#:)";
        const string apiBaseUrl = "http://localhost:63240";
        
        public ActionResult Index()
        {
            var token = GetApiToken(userName, password, apiBaseUrl).Result;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization","Bearer "+ token);

                HttpResponseMessage response = client.GetAsync("/api/Employee/Get").Result;
                ViewBag.ResultString = response.Content.ReadAsStringAsync().Result;

            }
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

        private async Task<string> GetApiToken(string userName, string password, string apiBaseUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var formContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("grant_type","password"),
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("password", password)

                });

                HttpResponseMessage responseMessage = client.PostAsync("/Token", formContent).Result;

                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(responseJson);
                return jObject.GetValue("access_token").ToString();
            }
        }
    }
}
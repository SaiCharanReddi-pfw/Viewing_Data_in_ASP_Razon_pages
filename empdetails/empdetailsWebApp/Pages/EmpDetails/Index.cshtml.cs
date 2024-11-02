using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using empdetailsAPI.Models;

namespace empdetailsWebApp.Pages.EmpDetails
{
    public class IndexModel : PageModel
    {
        public List<employeedetails> emp = new();
        /// <summary>
        /// The method to print the data on web page is implemented.
        /// </summary>
        public async void OnGet()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5136");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:5143/Emp/GetData");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    emp = JsonConvert.DeserializeObject<List<employeedetails>>(readTask);
                }
            }
        }
    }
}

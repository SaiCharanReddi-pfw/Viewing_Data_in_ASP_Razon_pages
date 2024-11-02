using empdetailsAPI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.Json;
using empdetailsAPI.Repositories;

namespace empdetailsWebApp.Pages.EmpDetails
{
    public class JobtrendsModel : PageModel
    {
        public Dictionary<int, float> trends1 = new();
        public Dictionary<int, float> trends2 = new();
        public string job1;
        public string job2;
        public List<string> jobList = new();

        public async void OnGet()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5136");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:5143/Emp/jobtitles");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    jobList = JsonConvert.DeserializeObject<List<string>>(readTask);
                }
            }
        }


        /// <summary>
        /// Function to process data and set results in HTML form on POST call.
        /// </summary>
        public async Task OnPostAsync()
        {
            job1 = Request.Form["job_title_1"];

            job2 = Request.Form["job_title_2"];

            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string json1 = System.Text.Json.JsonSerializer.Serialize<string>(job1, opt);
            string json2 = System.Text.Json.JsonSerializer.Serialize<string>(job2, opt);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5136");
                var content1 = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");

                var response1 = await client.PostAsync("http://localhost:5143/Emp/JobTrend", content1);

                var content2 = new StringContent(json2, System.Text.Encoding.UTF8, "application/json");

                var response2 = await client.PostAsync("http://localhost:5143/Emp/JobTrend", content2);

                if (response1.IsSuccessStatusCode)
                {
                    var readTask = await response1.Content.ReadAsStringAsync();
                    trends1 = JsonConvert.DeserializeObject<Dictionary<int, float>>(readTask);
                }

                if (response2.IsSuccessStatusCode)
                {
                    var readTask = await response2.Content.ReadAsStringAsync();
                    trends2 = JsonConvert.DeserializeObject<Dictionary<int, float>>(readTask);
                }
            }
        }
    }
}

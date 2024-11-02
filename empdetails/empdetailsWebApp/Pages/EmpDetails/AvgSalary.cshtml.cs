using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using empdetailsAPI.Models;
using Newtonsoft.Json;

namespace empdetailsWebApp.Pages.EmpDetails
{
    public class AvgSalaryModel : PageModel
    {
        public float avgsal = new();
        public string job;
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
        /// Function to find the average salary for job title
        /// </summary>
        /// <returns>Average salary for job title</returns>
        public async Task OnPostAsync()
        {
            job = Request.Form["job_title"];

            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string json = System.Text.Json.JsonSerializer.Serialize<string>(job, opt);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5136");
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:5143/Emp/AvgSalary", content);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = await response.Content.ReadAsStringAsync();
                    avgsal = JsonConvert.DeserializeObject<float>(readTask);
                }
            }
        }
    }
}


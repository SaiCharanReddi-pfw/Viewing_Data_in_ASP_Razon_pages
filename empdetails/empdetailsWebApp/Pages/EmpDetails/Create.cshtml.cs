using empdetailsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace empdetailsWebApp.Pages.EmpDetails
{
    public class CreateModel : PageModel
    {
        public employeedetails emp = new();
        public string errorMessage = "";
        public string successMessage = "";

        /// <summary>
        /// Function to process data and set results in HTML form on POST call.
        /// </summary>
        public async Task OnPost()
        {
            emp.work_year = int.Parse(Request.Form["work_year"]);
            emp.experience_level = Request.Form["experience_level"];
            emp.job_title = Request.Form["job_title"];
            emp.salary_in_usd = int.Parse(Request.Form["salary_in_usd"]);
            emp.employee_residence = Request.Form["employee_residence"];
            emp.company_location = Request.Form["company_location"];
            emp.company_size = Request.Form["company_size"];

            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string json = System.Text.Json.JsonSerializer.Serialize<employeedetails>(emp, opt);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5136");

                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var result = await client.PostAsync("http://localhost:5143/Emp/AddData", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                Console.WriteLine(resultContent);

                if (!result.IsSuccessStatusCode)
                {
                    errorMessage = "Error adding";
                }
                else
                {
                    successMessage = "Successfully added";
                }
            }
        }
    }
}
using System.Text.Json;
using empdetailsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace empdetailsWebApp.Pages.EmpDetails
{
    public class EditModel : PageModel
    {
        public employeedetails emp = new();
        public string errorMessage = "";
        public string successMessage = "";

        /// <summary>
        /// Function to populate data and set results in HTML form on GET call in edit page.
        /// </summary>
        public async void OnGet()
        {
            string id = Request.Query["id"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5143");

                 /// <summary>
                /// HTTP Get to Get the Data By Id.
                /// </summary>
                var responseTask = client.GetAsync("Emp/GetDatabyID?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    emp = JsonConvert.DeserializeObject<employeedetails>(readTask);
                }

            }
        }

        /// <summary>
        /// Function to process data and set results in HTML form on POST call.
        /// </summary>
        public async Task OnPost()
        {
            //String id = Request.Query["id"];
            emp.id = int.Parse(Request.Form["id"]);
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
                client.BaseAddress = new Uri("http://localhost:5143");
                 /// <summary>
                /// HTTP Post to update the Data.
                /// </summary>

                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var result = await client.PutAsync("Emp/UpdateData", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                Console.WriteLine(resultContent);

                if (!result.IsSuccessStatusCode)
                {
                    errorMessage = "Error Editing";
                }
                 /// <summary>
                /// Display Error Message, if the status code is false.
                /// </summary>
                else
                {
                    successMessage = "Successfully Edited";
                }
                /// <summary>
                /// Display Sucess Message, if the status code is true.
                /// </summary>
            }
        }
    }
}

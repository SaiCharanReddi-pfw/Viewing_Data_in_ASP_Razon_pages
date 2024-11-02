using empdetailsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace empdetailsWebApp.Pages.EmpDetails
{
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// Function to delete data from the database
        /// </summary>
    
        public employeedetails emp = new();
        public string errorMessage = "";
        public string successMessage = "";
        public async Task OnGet()
        {
            string id = Request.Query["id"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5136");

                // HTTP GET
                var responseTask = client.GetAsync("http://localhost:5143/Emp/GetDatabyID?id=" + id);
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
            bool isDeleted = false;
            int id = int.Parse(Request.Form["id"]);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5136");

                var response = await client.DeleteAsync("http://localhost:5143/Emp/DeleteData?id=" + id);

                if (response.IsSuccessStatusCode)
                {
                    isDeleted = true;
                }
            }
            if (isDeleted)
            {
                successMessage = "Successfully deleted";
            }
            else
            {
                errorMessage = "Error deleting";
            }

        }
    }
}

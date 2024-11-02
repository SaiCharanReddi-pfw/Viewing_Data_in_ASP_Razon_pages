using System;
using System.ComponentModel.DataAnnotations;

namespace empdetailsAPI.Models
{
    /// <summary>
    /// class encapsulating individual records from the database
    /// </summary>
    public class employeedetails
    {
        [Key]
        public int id { get; set; }
        public int work_year { get; set; }
        public string experience_level { get; set; }
        public string job_title { get; set; }
        public int salary_in_usd { get; set; }
        public string employee_residence { get; set; }
        public string company_location { get;set; }
        public string company_size { get; set; }

    }
}

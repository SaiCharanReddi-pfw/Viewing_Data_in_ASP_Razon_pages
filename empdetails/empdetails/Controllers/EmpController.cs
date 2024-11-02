using empdetailsAPI.Interfaces;
using empdetailsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace empdetailsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpController : ControllerBase
    {
        private readonly ILogger<EmpController> _logger;
        private readonly IEmployeeRepository _EmployeeRepository;

        public EmpController(ILogger<EmpController> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _EmployeeRepository = employeeRepository;
        }

        /// <summary>
        /// API for getting all the records in the database
        /// </summary>
        /// <returns></returns>
        [Route("GetData")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<employeedetails>))]

        public IActionResult GetData()
        {
            _logger.Log(LogLevel.Information, "Get Data");
            return Ok(_EmployeeRepository.GetData());
        }

        /// <summary>
        /// API for getting the records in the database pertaining to the id provided
        /// </summary>
        /// <param name="id">id of the record required</param>
        /// <returns></returns>
        [Route("GetDatabyID")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(employeedetails))]
        [ProducesResponseType(404)]
        public IActionResult GetDataById(int id)
        {
            _logger.Log(LogLevel.Information, "Get Data by id");
            return Ok(_EmployeeRepository.getDatabyId(id));

        }

        /// <summary>
        /// API to add a record to the database
        /// </summary>
        /// <param name="emp">body of the record to be added</param>
        /// <returns></returns>
        [Route("AddData")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult AddData([FromBody] employeedetails emp)
        {
            _logger.Log(LogLevel.Information, "Add Data");
            return Ok(_EmployeeRepository.AddData(emp));
        }

        /// <summary>
        /// API to delete a record from the database
        /// </summary>
        /// <param name="id">id of the record to be deleted</param>
        /// <returns></returns>
        [Route("DeleteData")]
        [HttpDelete()]
        public IActionResult DeleteRecord(int id)
        {
            _logger.Log(LogLevel.Information, "Delete Data");
            return Ok(_EmployeeRepository.DeleteData(id));
        }

        /// <summary>
        /// API to update a record in the database
        /// </summary>
        /// <param name="emp">new body of the record to be updated</param>
        /// <returns></returns>
        [Route("UpdateData")]
        [HttpPut()]
        public IActionResult UpdateRecord([FromBody] employeedetails emp)
        {
            _logger.Log(LogLevel.Information, "Update Data");
            return Ok(_EmployeeRepository.UpdateData(emp));
        }

        /// <summary>
        /// API to return the average salary of the job title
        /// </summary>
        /// <param name="jobTitle">Parameter containing a string of the job title passed through the body of the request</param>
        /// <returns>Average salary for job title</returns>
        [Route("AvgSalary")]
        [HttpPost()]
        public IActionResult AvgSalary([FromBody] string jobTitle)
        {
            _logger.Log(LogLevel.Information, "Update Data");
            return Ok(_EmployeeRepository.AvgSalaryByJobTitle(jobTitle));
        }

        /// <summary>
        /// API to get a list of average salaries for the job title for past three years
        /// </summary>
        /// <param name="jobTitle">Parameter containing a string of the job title passed through the body of the request</param>
        /// <returns>average salaries for the job title for past three years as a List of floating point numbers</returns>
        [Route("JobTrend")]
        [HttpPost()]
        public IActionResult JobTrend([FromBody] string jobTitle)
        {
            _logger.Log(LogLevel.Information, "Update Data");
            return Ok(_EmployeeRepository.JobTrend(jobTitle));
        }

        [Route("jobtitles")]
        [HttpGet()]
        public IActionResult jobTitles()
        {
            _logger.Log(LogLevel.Information, "Update Data");
            return Ok(_EmployeeRepository.getJobtitles());
        }
    }
}

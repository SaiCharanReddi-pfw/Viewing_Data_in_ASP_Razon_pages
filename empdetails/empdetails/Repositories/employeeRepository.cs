using empdetailsAPI;
using empdetailsAPI.Data;
using empdetailsAPI.Interfaces;
using empdetailsAPI.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using System;

namespace empdetailsAPI.Repositories
{
    public class employeeRepository : IEmployeeRepository
    {
        private DataContext _context;

        public employeeRepository(DataContext context)

        {
            _context = context;
        }
        /// <summary>
        /// Function to get all the records in the database
        /// </summary>
        /// <returns>Colection of all records in the database</returns>
        public ICollection<employeedetails> GetData()
        {
            return _context.empdetails.ToList();
        }

        /// <summary>
        /// Function to add a record to the database
        /// </summary>
        /// <param name="emp">body of the record to be added</param>
        /// <returns>true if record added, false otherwise</returns>
        public bool AddData(employeedetails emp)
        {
            _context.Add(emp);
            return Save();
        }

        /// <summary>
        /// function to save the transactions performed on database
        /// </summary>
        /// <returns>true if transaction saved, false otherwise</returns>
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved == 1;
        }

        /// <summary>
        /// function to get data record by id 
        /// </summary>
        /// <param name="id">id of the record to be retreived</param>
        /// <returns>record that is requested</returns>
        public employeedetails getDatabyId(int id)
        {
            ICollection<employeedetails> emp = _context.empdetails.ToList();
            foreach (employeedetails b in emp)
            {
                if (id == b.id)
                {
                    return b;
                }
            }
            return null;
        }

        /// <summary>
        /// delete data record from the database
        /// </summary>
        /// <param name="id">id of the record to be deleted</param>
        /// <returns>true if record deleted, false otherwise</returns>
        public bool DeleteData(int id)
        {
            _context.Remove(GetData().FirstOrDefault(a => a.id == id));
            return Save();
        }

        /// <summary>
        /// Function to find the average salary for job title
        /// </summary>
        /// <param name="jt">job title for which the analysis is to be performed</param>
        /// <returns>avg salary for the job title</returns>
        public float AvgSalaryByJobTitle(string jt)
        {
            int totalRecords = 0;
            int sum = 0;
            ICollection<employeedetails> emp = _context.empdetails.ToList();
            foreach (employeedetails b in emp)
            {
                if (jt == b.job_title)
                {
                    sum = sum + b.salary_in_usd;
                    totalRecords++;
                }
            }
            if (totalRecords == 0) 
            {
                return 0;
            }
            else
            {
                return sum / totalRecords;
            }
        }

        /// <summary>
        /// Function to find the average salaries of past three years for job title 
        /// </summary>
        /// <param name="jt">job title for which the analysis is to be performed</param>
        /// <returns>List of floats containing the avg salaries for the job title for past three years</returns>
        public Dictionary<int, float> JobTrend(string jt)
        {
            int totalRecords2020 = 0;
            int totalRecords2021 = 0;
            int totalRecords2022 = 0;

            int sum2020 = 0;
            int sum2021 = 0;
            int sum2022 = 0;

            int avg2020 = 0;
            int avg2021 = 0;
            int avg2022 = 0;

            ICollection<employeedetails> emp = _context.empdetails.ToList();

            foreach (employeedetails b in emp)
            {
                if (jt == b.job_title)
                {
                    if (b.work_year == 2020)
                    {
                        sum2020 = sum2020 + b.salary_in_usd;
                        totalRecords2020++;
                    }
                    else if (b.work_year == 2021) 
                    {
                        sum2021 = sum2021 + b.salary_in_usd;
                        totalRecords2021++;
                    }
                    else if (b.work_year == 2022)
                    {
                        sum2022 = sum2022 + b.salary_in_usd;
                        totalRecords2022++;
                    }
                }
            }
            Dictionary<int, float> result = new Dictionary<int, float>();

            if (totalRecords2020 != 0)
            {
                result.Add(2020,sum2020 / totalRecords2020); 
            }
            if (totalRecords2021 != 0)
            {
                result.Add(2021, sum2021 / totalRecords2021);
            }
            if (totalRecords2022 != 0)
            {
                result.Add(2022, sum2022 / totalRecords2022);
            }
            return result;
        }

        public List<string> getJobtitles()
        {
            List<string> jobtitles = _context.empdetails.Select(e => e.job_title).Distinct().ToList();
            return jobtitles;
        }

        /// <summary>
        /// Function to update a record in the database
        /// </summary>
        /// <param name="emp">record to be updated in the database</param>
        /// <returns>true if record updated</returns>
        public bool UpdateData(employeedetails emp)
        {
            _context.Update(emp);
            return Save();
        }
    }
}

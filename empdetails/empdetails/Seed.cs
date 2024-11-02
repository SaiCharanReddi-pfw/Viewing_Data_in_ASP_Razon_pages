using empdetailsAPI.Data;
using empdetailsAPI.Models;

namespace empdetailsAPI
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext dataContext) { this.dataContext = dataContext; }

        /// <summary>
        /// contains functionality to seed data into the database. *unused for this project*
        /// </summary>
        public void SeedDataContext()
        {
            if (!dataContext.empdetails.Any())
            {
                List<empdetailsAPI.Models.employeedetails> emp = new()
                {

                };

                dataContext.empdetails.AddRange(emp);
                dataContext.SaveChanges();

            }
        }
    }
}

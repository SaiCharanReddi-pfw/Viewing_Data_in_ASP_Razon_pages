using Microsoft.EntityFrameworkCore;
using empdetailsAPI.Models;

namespace empdetailsAPI.Data
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// sets data context for database
        /// </summary>
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        /// <summary>
        /// sets the table to be transacted in the database
        /// </summary>
        public DbSet<empdetailsAPI.Models.employeedetails> empdetails { get; set; }
    }
}

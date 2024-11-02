using empdetailsAPI.Models;

namespace empdetailsAPI.Interfaces
{
    /// <summary>
    /// interface for repository class
    /// </summary>
    public interface IEmployeeRepository
    {
        ICollection<employeedetails> GetData();
        employeedetails getDatabyId(int id);
        bool AddData(employeedetails emp);
        bool DeleteData(int id);
        bool Save();
        bool UpdateData(employeedetails emp);
        float AvgSalaryByJobTitle(string jt);
        Dictionary<int, float> JobTrend(string jt);
        List<string> getJobtitles();
    }
}

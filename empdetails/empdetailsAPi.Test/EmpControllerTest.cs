using System.Net;
using System.Net.NetworkInformation;
using empdetailsAPI.Controllers;
using empdetailsAPI.Interfaces;
using empdetailsAPI.Models;
using empdetailsAPI.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using Moq;
using Xunit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace empdetailsAPI.Tests

{
    public class EmpcontrollerTest
    {
        private readonly EmpController _empController;
        private readonly Mock<ILogger<EmpController>> _loggerMock;
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;

        public EmpcontrollerTest()
        {
            _loggerMock = new Mock<ILogger<EmpController>>();
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _empController = new EmpController(_loggerMock.Object, _employeeRepositoryMock.Object);
        }
        [Test]
        public void TestGetData()
        {
            //Arrange
            var employeeDetailsList = new List<employeedetails> { new employeedetails { id = 1, work_year = 2021, salary_in_usd = 0 } };
            _employeeRepositoryMock.Setup(x => x.GetData()).Returns(employeeDetailsList);

            //Act
            var result = _empController.GetData();

            //Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Xunit.Assert.IsType<List<employeedetails>>(okResult.Value);
            var employeedetailsResult = (List<employeedetails>)okResult.Value;
            Xunit.Assert.Equal(employeeDetailsList, employeedetailsResult);

        }
        [Test]
        public void TestGetById()
        {
            //Arrange
            var employeeDetails = new employeedetails { id = 1, work_year = 2021, salary_in_usd = 0 };
            _employeeRepositoryMock.Setup(x => x.getDatabyId(1)).Returns(employeeDetails);

            //Act
            var result = _empController.GetDataById(1);

            //Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Xunit.Assert.IsType<employeedetails>(okResult.Value);
            var employeeDetailsResult = (employeedetails)okResult.Value;
            Xunit.Assert.Equal(employeeDetails, employeeDetailsResult);
        }
        [Test]
        public void TestAddData()
        {
            // Arrange           
            var employeeDetails = new employeedetails { id = 1, work_year = 2021, salary_in_usd = 0 };
            _employeeRepositoryMock.Setup(x => x.AddData(employeeDetails)).Returns(true);
            // Act           
            var result = _empController.AddData(employeeDetails);
            // Assert      
            Xunit.Assert.IsType<OkObjectResult>(result);
        }
        [Test]
        public void TestDeleteRecord()
        {
            // Arrange      
            _employeeRepositoryMock.Setup(x => x.DeleteData(1)).Returns(true);

            // Act       
            var result = _empController.DeleteRecord(1);
            // Assert      
            Xunit.Assert.IsType<OkObjectResult>(result);
        }
        [Test]
        public void TestUpdateRecord()
        {
            // Arrange        
            var employeeDetails = new employeedetails { id = 1, work_year = 2021, salary_in_usd = 0 };
            _employeeRepositoryMock.Setup(x => x.UpdateData(employeeDetails)).Returns(true);

            //Act
            var result = _empController.UpdateRecord(employeeDetails);

            //Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
        }
        [Test]
        public void TestAvgSalary()
        {
            //Arrange

            //var employeeDetails = new employeedetails { id = 1, work_year = 2021, salary_in_usd = 0 };
            _employeeRepositoryMock.Setup(x => x.AvgSalaryByJobTitle("SE")).Returns(10000);

            //Act
            var result = _empController.AvgSalary("SE");

            //Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
        }
        [Test]
        public void TestJobTrend()
        {
            //Arrange
            //var employeeDetails = new employeedetails { id = 1, work_year = 2021, salary_in_usd = 0 };
            var job = "SE";
            _employeeRepositoryMock.Setup(x => x.JobTrend(job));

            //Act
            var result = _empController.JobTrend(job);

            //Assert
            Xunit.Assert.IsType<OkObjectResult>(result);

        }

    }
}


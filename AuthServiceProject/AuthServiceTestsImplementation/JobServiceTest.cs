using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using WebSiteAPI.Models.Dtos;
using WebSiteAPI.Services.Implementations;
using WebSiteAPI.Services.Interfaces;


namespace AuthServiceProject.AuthServiceTestsImplementation
{
    [TestClass]
    public class JobServiceTest
    {
        private readonly Random _random = new Random();
        private readonly JobService _jobService;
        private readonly Mock<IJobRepo> _jobRepoStub;
        private readonly Mock<IJobNature> _jobNature;
        private readonly Mock<ICategoryRepo> _categoryRepostub;
        private readonly Mock<ILocationRepo> _locationRepoStub;
        private readonly Mock<IIndustryRepo> _industryRepoStub;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<JobService> jobService;
       
      
        public JobServiceTest()
        {
            var jobToCreate = new JobDto()
            {
                JobNatureId = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                Description = "Job description",
                MinimumSalary = _random.Next(100000, 10000000),
                MaximumSalary = _random.Next(100000, 10000000),
                Title = "Job Title",
                IndustryId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Company = "Company name",
                JobValidDays = _random.Next(1, 30)

            };
            ResponseDto<Job> result = new ResponseDto<Job>()
            {
               
                IsSuccessful = true
            };
            _jobRepoStub = new Mock<IJobRepo>();
            _jobRepoStub.Setup(x => x.Add(It.IsAny<JobDto>())).ReturnsAsync(true);
             _jobNature = new Mock<IJobNature>();
             _categoryRepostub = new Mock<ICategoryRepo>();
             _locationRepoStub = new Mock<ILocationRepo>();
             _industryRepoStub = new Mock<IIndustryRepo>();
             _mapper = new Mock<IMapper>();
            
            _jobService = new JobService(_jobRepoStub.Object,
                     _mapper.Object, _locationRepoStub.Object,
                     _industryRepoStub.Object, _categoryRepostub.Object,
                     _jobNature.Object);
            
        }
        [TestMethod()]
        public async Task AddUser_WithCorrect_CreatesUser()
        {
            //Assert
            //var expectedjob = CreateJobAsync(job);


            var jobToCreate = new JobDto()
            {
                JobNatureId = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                Description = "Job description",
                MinimumSalary = _random.Next(100000, 10000000),
                MaximumSalary = _random.Next(100000, 10000000),
                Title = "Job Title",
                IndustryId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Company = "Company name",
                JobValidDays = _random.Next(1, 30)

            };
            //Act
            var result = await _jobService.AddJob(jobToCreate);
            var rs = result.Data;
            //Assert
            Assert.AreSame(jobToCreate.Description, rs.Description);
           
           
           
           
            
        }

    }
}

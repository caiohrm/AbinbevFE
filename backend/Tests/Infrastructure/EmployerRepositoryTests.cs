using System;
using Moq.EntityFrameworkCore;
using CrossCutting.Factories;
using CrossCutting.Models;
using Moq;
using Infrastructure.Repositories;
using Xunit;
using NSubstitute;
using Application.Services;
using AutoMapper;
using FluentAssertions;
using CrossCutting.ViewModels.Employers;
using CrossCutting.Enums;
namespace Tests.Infrastructure
{
	public class EmployerRepositoryTests
	{
		private readonly IMapper _mapper;
		private readonly IEmployerRepository _employerRepository;
		private readonly EmployerService _employerService;
        public EmployerRepositoryTests()
		{

			_employerRepository = Substitute.For<IEmployerRepository>();
			_mapper = Substitute.For<IMapper>();
            _employerService = new EmployerService(_employerRepository, _mapper);
		}

        [Fact(DisplayName = "Should throw an exception if the employer with the same DocNumber exists")]
        public async Task Should_Throw_Exception_If_Employer_Exists()
        {
            // Arrange
            var request = new AddEmployerRequest
            {
                FirstName = "Caio",
                LastName = "Martins",
                Email = "caio@company.com",
                BirthDate = new DateTime(1990, 01, 01),
                Password = "Password123",
                Role = Role.Employer,
                DocNumber = "123456789"
            };

            var employer = new Employer()
            {
                FirstName = "Caio",
                LastName = "Martins",
                Email = "caio@company.com",
                BirthDate = new DateTime(1990, 01, 01),
                Password = "Password123",
                Role = Role.Employer,
                DocNumber = "123456789"
            };
            _mapper.Map<Employer>(request).Returns(employer);

            var response = new List<Employer>() { new Employer { DocNumber = request.DocNumber } };
            _employerRepository
                .GetEmployers(1, 1, request.DocNumber)
                .Returns(response);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _employerService.AddEmployer(request));
            Assert.Equal("User with the same documents exist's", exception.Message);
        }

        [Fact(DisplayName = "Should add the employer and map to response")]
        public async Task Should_Add_Employer_And_Map_To_Response()
        {
            // Arrange
            var request = new AddEmployerRequest
            {
                FirstName = "Caio",
                LastName = "Martins",
                Email = "caio@company.com",
                BirthDate = new DateTime(1990, 01, 01),
                Password = "Password123",
                Role = Role.Employer,
                DocNumber = "123456789"
            };

            var employer = new Employer
            {
                FirstName = "Caio",
                LastName = "Martins",
                Email = "caio@company.com",
                BirthDate = new DateTime(1990, 01, 01),
                Password = "HashedPassword",
                Role = Role.Employer,
                DocNumber = "123456789"
            };

            var employerResponse = new Employer
            {
                Id = 1,
                FirstName = "Caio",
                LastName = "Martins",
                Email = "caio@company.com",
                BirthDate = new DateTime(1990, 01, 01),
                Password = "HashedPassword",
                Role = Role.Employer,
                DocNumber = "123456789"
            };

            var expectedResponse = new AddEmployerResponse
            {
                FirstName = "Caio",
                LastName = "Martins",
                Email = "caio@company.com",
                BirthDate = new DateTime(1990, 01, 01),
                Role = Role.Employer,
                DocNumber = "123456789"
            };

            
            _employerRepository
                .GetEmployers(1, 1, request.DocNumber)
                .Returns(new List<Employer>());

            
            _employerRepository
                .AddEmployer(Arg.Any<Employer>())
                .Returns(Task.FromResult(employerResponse));

            
            _mapper
                .Map<Employer>(request)
                .Returns(employer);

            _mapper
                .Map<AddEmployerResponse>(employerResponse)
                .Returns(expectedResponse);

            // Act
            var result = await _employerService.AddEmployer(request);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(expectedResponse.FirstName);
            result.LastName.Should().Be(expectedResponse.LastName);
            result.Email.Should().Be(expectedResponse.Email);
            result.Role.Should().Be(expectedResponse.Role);
            result.DocNumber.Should().Be(expectedResponse.DocNumber);            
            await _employerRepository.Received(1).AddEmployer(Arg.Any<Employer>());
        }



        
		[Fact(DisplayName = "Should return the selected employer")]
        public async Task Should_Return_Selected_Employer()
        {

            // Arrange
            var expectedEmployer = new Employer() { Id = 1, FirstName = "Test data", Enabled = true };
            var restult = new AddEmployerResponse() { Id = 1, FirstName = "Test data", Enabled = true };
            _mapper.Map<AddEmployerResponse>(expectedEmployer).Returns(restult);

            _employerRepository.GetEmployer(Arg.Is<int>(x => x == 1)).Returns(Task.FromResult(expectedEmployer));

            // Act
            var result = await _employerService.GetEmployer(1); 

            // Assert
            result.Should().NotBeNull();
            result.Enabled.Should().BeTrue();
            result.Id.Should().Be(expectedEmployer.Id);

            
            _employerRepository.Received(1).GetEmployer(Arg.Is<int>(x => x == 1));

        }



        [Fact(DisplayName = "Should throw an exception if employer doesn't exist")]
        public async Task Should_Throw_Exception_If_Employer_Does_Not_Exist()
        {
            // Arrange
            int employerId = 1;

            // Mockando o repositório para retornar null, indicando que o empregador não foi encontrado
            _employerRepository
                .GetEmployer(employerId)
                .Returns(Task.FromResult<Employer>(null));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _employerService.DeleteEmployer(employerId));
            Assert.Equal("Employer doesn't exists", exception.Message);
        }

        [Fact(DisplayName = "Should delete the employer successfully")]
        public async Task Should_Delete_Employer_Successfully()
        {
            // Arrange
            int employerId = 1;
            var employerToDelete = new Employer
            {
                Id = employerId,
                FirstName = "Caio",
                LastName = "Martins",
                Email = "caio@company.com",
                BirthDate = new DateTime(1990, 01, 01),
                Password = "Password123",
                Role = Role.Employer,
                DocNumber = "123456789"
            };

            
            _employerRepository
                .GetEmployer(employerId)
                .Returns(Task.FromResult(employerToDelete));

            
            _employerRepository
                .UpdateEmployer(Arg.Any<Employer>())
                .Returns(Task.FromResult(true));

            // Act
            var result = await _employerService.DeleteEmployer(employerId);

            // Assert
            result.Should().BeTrue();  

            await _employerRepository.Received(1).UpdateEmployer(Arg.Any<Employer>());
        }


    }
}


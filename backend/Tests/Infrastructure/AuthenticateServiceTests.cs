using System;
using NSubstitute;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Application.Commom;
using Application.Services;
using CrossCutting.Commom;
using CrossCutting.Models;
using CrossCutting.Services;
using CrossCutting.ViewModels.Authentication;
namespace Tests.Infrastructure
{
    public class AuthenticateServiceTests
    {
        private readonly IEmployerService _employerService;
        private readonly IJwtManager _jwtManager;
        private readonly AuthenticateService _authenticateService;

        public AuthenticateServiceTests()
        {
            _employerService = Substitute.For<IEmployerService>();
            _jwtManager = Substitute.For<IJwtManager>();
            _authenticateService = new AuthenticateService(_employerService, _jwtManager);
        }

        [Fact(DisplayName = "Should return a valid token when employer exists and password is correct")]
        public async Task Authenticate_ShouldReturnToken_WhenEmployerExistsAndPasswordIsCorrect()
        {
            // Arrange
            var request = new AutenticateRequest { Document = "123456789", Password = "password123" };
            var employer = new Employer
            {
                Id = 1,
                FirstName = "Caio",
                LastName = "Martins",
                Email = "caio@mail.com",
                Password = HashManager.GetStringHash("password123"),
                Role = CrossCutting.Enums.Role.Employer,
                Enabled = true,
                DocNumber = "123456789",
                BirthDate = new System.DateTime(1990, 5, 15),
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now
            };

            var employers = new List<Employer> { employer };
            _employerService.GetEmployers(1, 5, request.Document).Returns(employers);
            _jwtManager.GenerateToken(Arg.Any<Employer>()).Returns("valid-jwt-token");

            // Act
            var response = await _authenticateService.Authenticate(request);

            // Assert
            response.Should().NotBeNull();
            response.Token.Should().Be("valid-jwt-token");
            response.FirstName.Should().Be(employer.FirstName);
            response.LastName.Should().Be(employer.LastName);
            response.Email.Should().Be(employer.Email);
            response.DocNumber.Should().Be(employer.DocNumber);
            response.Role.Should().Be(employer.Role);
            response.Enabled.Should().Be(employer.Enabled);
            response.Id.Should().Be(employer.Id);
        }

        [Fact(DisplayName = "Should return null when employer does not exist")]
        public async Task Authenticate_ShouldReturnNull_WhenEmployerDoesNotExist()
        {
            // Arrange
            var request = new AutenticateRequest { Document = "123456789", Password = "password123" };
            var employers = new List<Employer>();
            _employerService.GetEmployers(1, 5, request.Document).Returns(employers);

            // Act
            var response = await _authenticateService.Authenticate(request);

            // Assert
            response.Should().BeNull();
        }

        [Fact(DisplayName = "Should return null when employer is disabled")]
        public async Task Authenticate_ShouldReturnNull_WhenEmployerIsDisabled()
        {
            // Arrange
            var request = new AutenticateRequest { Document = "123456789", Password = "password123" };
            var employer = new Employer
            {
                Id = 1,
                FirstName = "Caio",
                LastName = "Martins",
                Email = "caio@mail.com",
                Password = HashManager.GetStringHash("password123"),
                Role = CrossCutting.Enums.Role.Employer,
                Enabled = false,  
                DocNumber = "123456789",
                BirthDate = new System.DateTime(1990, 5, 15),
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now
            };
            var employers = new List<Employer> { employer };
            _employerService.GetEmployers(1, 5, request.Document).Returns(employers);

            // Act
            var response = await _authenticateService.Authenticate(request);

            // Assert
            response.Should().BeNull();
        }

        [Fact(DisplayName = "Should return null when password is incorrect")]
        public async Task Authenticate_ShouldReturnNull_WhenPasswordIsIncorrect()
        {
            // Arrange
            var request = new AutenticateRequest { Document = "123456789", Password = "wrongpassword" };
            var employer = new Employer
            {
                Id = 1,
                FirstName = "Caio",
                LastName = "Martins",
                Email = "caio@mail.com",
                Password = HashManager.GetStringHash("password123"),
                Role = CrossCutting.Enums.Role.Employer,
                Enabled = true,
                DocNumber = "123456789",
                BirthDate = new System.DateTime(1990, 5, 15),
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now
            };
            var employers = new List<Employer> { employer };
            _employerService.GetEmployers(1, 5, request.Document).Returns(employers);

            // Act
            var response = await _authenticateService.Authenticate(request);

            // Assert
            response.Should().BeNull();
        }
    }

}


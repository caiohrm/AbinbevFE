using System;
using Application.Services;
using AutoMapper;
using CrossCutting.Factories;
using CrossCutting.Models;
using CrossCutting.ViewModels.PhoneNumber;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Tests.Infrastructure
{
    public class PhoneNumberServiceTests
    {
        private readonly IPhoneNumberRepository _phoneNumberRepository;
        private readonly IMapper _mapper;
        private readonly PhoneNumberService _phoneNumberService;

        public PhoneNumberServiceTests()
        {
            _phoneNumberRepository = Substitute.For<IPhoneNumberRepository>();
            _mapper = Substitute.For<IMapper>();
            _phoneNumberService = new PhoneNumberService(_phoneNumberRepository, _mapper);
        }

        [Fact(DisplayName = "Should delete phone number successfully")]
        public async Task Should_Delete_PhoneNumber_Successfully()
        {
            // Arrange
            var employerId = 1;
            var phoneNumberId = 1;
            _phoneNumberRepository.DeletePhoneNumbers(employerId, phoneNumberId).Returns(Task.FromResult(true));

            // Act
            var result = await _phoneNumberService.DeletePhoneNumbers(employerId, phoneNumberId);

            // Assert
            result.Should().BeTrue();
            await _phoneNumberRepository.Received(1).DeletePhoneNumbers(employerId, phoneNumberId);
        }

        [Fact(DisplayName = "Should fail to delete phone number")]
        public async Task Should_Fail_To_Delete_PhoneNumber()
        {
            // Arrange
            var employerId = 1;
            var phoneNumberId = 1;
            _phoneNumberRepository.DeletePhoneNumbers(employerId, phoneNumberId).Returns(Task.FromResult(false));

            // Act
            var result = await _phoneNumberService.DeletePhoneNumbers(employerId, phoneNumberId);

            // Assert
            result.Should().BeFalse();
            await _phoneNumberRepository.Received(1).DeletePhoneNumbers(employerId, phoneNumberId);
        }
        [Fact(DisplayName = "Should get phone numbers successfully")]
        public async Task Should_Get_PhoneNumbers_Successfully()
        {
            // Arrange
            var employerId = 1;
            var phoneNumbers = new List<PhoneNumber>()
            {
                new PhoneNumber { Id = 1, Number = "12345" },
                new PhoneNumber { Id = 2, Number = "67890" }
            };
            _phoneNumberRepository.GetPhoneNumbers(employerId).Returns(Task.FromResult<IEnumerable<PhoneNumber>>(phoneNumbers));

            // Act
            var result = await _phoneNumberService.GetPhoneNumbers(employerId);

            // Assert
            result.Should().NotBeNull().And.HaveCount(2);
            result.Should().BeEquivalentTo(phoneNumbers);
            await _phoneNumberRepository.Received(1).GetPhoneNumbers(employerId);
        }

        [Fact(DisplayName = "Should add phone number successfully")]
        public async Task Should_Add_PhoneNumber_Successfully()
        {
            // Arrange
            var employerId = 1;
            var request = new AddPhoneNumberRequest { Number = "1234567890" };
            var phoneNumber = new PhoneNumber { Number = "1234567890" };

            _mapper.Map<PhoneNumber>(request, Arg.Any<Action<IMappingOperationOptions>>()).Returns(phoneNumber);
            _phoneNumberRepository.PostPhoneNumbers(phoneNumber).Returns(Task.FromResult(true));

            // Act
            var result = await _phoneNumberService.PostPhoneNumbers(employerId, request);

            // Assert
            result.Should().BeTrue();
            _mapper.Received(1).Map<PhoneNumber>(request, Arg.Any<Action<IMappingOperationOptions>>());
            await _phoneNumberRepository.Received(1).PostPhoneNumbers(phoneNumber);
        }

        [Fact(DisplayName = "Should fail to add phone number")]
        public async Task Should_Fail_To_Add_PhoneNumber()
        {
            // Arrange
            var employerId = 1;
            var request = new AddPhoneNumberRequest { Number = "1234567890" };
            var phoneNumber = new PhoneNumber { Number = "1234567890" };

            _mapper.Map<PhoneNumber>(request, Arg.Any<Action<IMappingOperationOptions>>()).Returns(phoneNumber);
            _phoneNumberRepository.PostPhoneNumbers(phoneNumber).Returns(Task.FromResult(false));

            // Act
            var result = await _phoneNumberService.PostPhoneNumbers(employerId, request);

            // Assert
            result.Should().BeFalse();
            _mapper.Received(1).Map<PhoneNumber>(request, Arg.Any<Action<IMappingOperationOptions>>());
            await _phoneNumberRepository.Received(1).PostPhoneNumbers(phoneNumber);
        }

        [Fact(DisplayName = "Should update phone number successfully")]
        public async Task Should_Update_PhoneNumber_Successfully()
        {
            // Arrange
            var employerId = 1;
            var phoneNumberId = 1;
            var request = new UpdatePhoneNumberRequest { Number = "9876543210" };
            var phoneNumber = new PhoneNumber { Id = phoneNumberId, Number = "9876543210" };

            _mapper.Map<PhoneNumber>(request, Arg.Any<Action<IMappingOperationOptions>>()).Returns(phoneNumber);
            _phoneNumberRepository.PutPhoneNumbers(phoneNumber).Returns(Task.FromResult(true));

            // Act
            var result = await _phoneNumberService.PutPhoneNumbers(employerId, phoneNumberId, request);

            // Assert
            result.Should().BeTrue();
             _mapper.Received(1).Map<PhoneNumber>(request, Arg.Any<Action<IMappingOperationOptions>>());
            await _phoneNumberRepository.Received(1).PutPhoneNumbers(phoneNumber);
        }

        [Fact(DisplayName = "Should fail to update phone number")]
        public async Task Should_Fail_To_Update_PhoneNumber()
        {
            // Arrange
            var employerId = 1;
            var phoneNumberId = 1;
            var request = new UpdatePhoneNumberRequest { Number = "9876543210" };
            var phoneNumber = new PhoneNumber { Id = phoneNumberId, Number = "9876543210" };

            _mapper.Map<PhoneNumber>(request, Arg.Any<Action<IMappingOperationOptions>>()).Returns(phoneNumber);
            _phoneNumberRepository.PutPhoneNumbers(phoneNumber).Returns(Task.FromResult(false));

            // Act
            var result = await _phoneNumberService.PutPhoneNumbers(employerId, phoneNumberId, request);

            // Assert
            result.Should().BeFalse();
            _mapper.Received(1).Map<PhoneNumber>(request, Arg.Any<Action<IMappingOperationOptions>>());
            await _phoneNumberRepository.Received(1).PutPhoneNumbers(phoneNumber);
        }
    }

}


using AutoMapper;
using Haceb.Demanda.Application.DTO;
using Haceb.Demanda.Application.UseCase;
using Haceb.Demanda.Common.Exceptions;
using Haceb.Demanda.Domain.Entities;
using Haceb.Demanda.Domain.Enum;
using Haceb.Demanda.Domain.IRepository;
using Haceb.Demanda.Domain.Unit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Test.Application
{
    public class UserUseCaseTest
    {
        private readonly Mock<IUserRepository> _mockIUserRepository;
        private readonly Mock<IMapper> _mockIMapper;
        private readonly UserUseCase _userUseCase;

        public UserUseCaseTest()
        {
            _mockIUserRepository = new();
            _mockIMapper = new();
            _userUseCase = new(_mockIUserRepository.Object, _mockIMapper.Object);
        }

        [Fact]
        public async Task GetById_WhenUserExists_ShouldReturnMappedResponse()
        {
            // Arrange
            int id = 1;
            var entity = new UserEntity { Id = id, Username = "Tecnología" };
            var response = new UserResponse() { Id = id, Username = "Tecnología" };

            _mockIUserRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(entity);
            _mockIMapper.Setup(x => x.Map<UserResponse>(entity)).Returns(response);

            // Act
            var result = await _userUseCase.GetById(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(response.Id, result.Id);
            _mockIUserRepository.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetById_WhenUserNotExists_ShouldThrowBadRequestException()
        {
            // Arrange
            int id = 10;
            _mockIUserRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((UserEntity)null);

            // Act 
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _userUseCase.GetById(id));

            //Assert
            Assert.Contains("Usuario no encontrado.", exception.Message);
        }

        [Fact]
        public async Task GetByFilter_ShouldReturnMappedList()
        {
            // Arrange
            var entities = new List<UserEntity> { new() { Id = 1, Username = "Tecnología" }, new() { Id = 2, Username = "Financiera" } };
            var response = new List<UserResponse> { new() { Id = 1, Username = "Tecnología" }, new() { Id = 2, Username = "Financiera" } };

            _mockIUserRepository.Setup(x => x.GetListAsync()).ReturnsAsync(entities);

            _mockIMapper.Setup(x => x.Map<List<UserResponse>>(entities)).Returns(response);

            // Act
            var result = await _userUseCase.GetList();

            // Assert
            Assert.True(result.Count > 0);
            _mockIUserRepository.Verify(r => r.GetListAsync(), Times.Once);
        }
    }
}

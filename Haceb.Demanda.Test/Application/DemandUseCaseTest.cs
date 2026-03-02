using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Haceb.Demanda.Application.DTO;
using Haceb.Demanda.Application.UseCase;
using Haceb.Demanda.Common.Exceptions;
using Haceb.Demanda.Domain.Entities;
using Haceb.Demanda.Domain.Enum;
using Haceb.Demanda.Domain.IRepository;
using Haceb.Demanda.Domain.Unit;
using Moq;

namespace Haceb.Demanda.Test.Application
{
    public class DemandUseCaseTest
    {
        private readonly Mock<IDemandRepository> _mockIDemandRepository;
        private readonly Mock<IUnitOfWork> _mockIUnitOfWork;
        private readonly Mock<IDemandHistoryRepository> _mockIDemandHistoryRepository;
        
        private readonly Mock<IMapper> _mockIMapper;
        private readonly DemandUseCase _demandUseCase;

        public DemandUseCaseTest()
        {
            _mockIDemandRepository = new();
            _mockIUnitOfWork = new();
            _mockIDemandHistoryRepository = new();
            _mockIMapper = new();
            _demandUseCase = new(_mockIDemandRepository.Object, _mockIUnitOfWork.Object, _mockIMapper.Object, _mockIDemandHistoryRepository.Object);
        }

        #region Get

        [Fact]
        public async Task GetList_ShouldReturnMappedList()
        {
            // Arrange
            var entities = new List<DemandEntity> { new() };
            var response = new List<DemandResponse> { new() };

            _mockIDemandRepository.Setup(x => x.GetListAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(entities);
            _mockIMapper.Setup(x => x.Map<List<DemandResponse>>(It.IsAny<List<DemandEntity>>())).Returns(response);

            // Act
            var result = await _demandUseCase.GetList(1, 10);

            // Assert
            Assert.Single(result);
            _mockIDemandRepository.Verify(r => r.GetListAsync(1, 10), Times.Once);
        }

        [Fact]
        public async Task GetById_WhenDemandExists_ShouldReturnMappedResponse()
        {
            // Arrange
            int id = 999;
            var entity = new DemandEntity { Id = id };
            var response = new DemandResponse() { Id = id };

            _mockIDemandRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(entity);
            _mockIMapper.Setup(x => x.Map<DemandResponse>(entity)).Returns(response);

            // Act
            var result = await _demandUseCase.GetById(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(response.Id, result.Id);
            _mockIDemandRepository.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetById_WhenDemandNotExists_ShouldThrowBadRequestException()
        {
            // Arrange
            int id = 999;
            _mockIDemandRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((DemandEntity)null);
            
            // Act 
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _demandUseCase.GetById(id));

            //Assert
            Assert.Contains("Demanda no encontrada.", exception.Message);
        }

        [Fact]
        public async Task GetByFilter_ShouldReturnMappedList()
        {
            var entities = new List<DemandEntity> { new() };
            var response = new List<DemandResponse> { new() };

            _mockIDemandRepository.Setup(x => x.GetByTypeStatusAsync(It.IsAny<int>(), It.IsAny<DemandStatus>())).ReturnsAsync(entities);

            _mockIMapper.Setup(x => x.Map<List<DemandResponse>>(entities)).Returns(response);

            var result = await _demandUseCase.GetByFilter(1, DemandStatus.Received);

            _mockIDemandRepository.Verify(r => r.GetByTypeStatusAsync(1, DemandStatus.Received), Times.Once);
        }

        #endregion

        #region AddDemand

        [Fact]
        public async Task AddDemand_ShouldCreateDemand_AndSaveChanges()
        {
            // Arrange
            var request = new DemandRequest();
            var entity = new DemandEntity();

            _mockIMapper.Setup(x => x.Map<DemandEntity>(request)).Returns(entity);

            _mockIUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act 
            await _demandUseCase.AddDemand(request);

            //Assert
            _mockIDemandRepository.Verify(x => x.Create(It.IsAny<DemandEntity>()), Times.Once);
            _mockIUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        #endregion

        #region UpdateDemand

        [Fact]
        public async Task UpdateDemand_WhenDemandNotExists_ShouldThrowBadRequestException()
        {
            var request = new DemandUpdateRequest { Id = 1 };

            _mockIDemandRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((DemandEntity)null);

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _demandUseCase.UpdateDemand(request));

            Assert.Contains("Demanda no encontrada.", exception.Message);
        }

        [Fact]
        public async Task UpdateDemand_WhenStatusChanges_ShouldAddHistory()
        {
            var entity = new DemandEntity
            {
                Id = 1,
                Status = DemandStatus.Received,
                RatingId = 1,
                Prioritize = DemandPrioritize.Low,
                UserId = 1
            };

            var request = new DemandUpdateRequest
            {
                Id = 1,
                Status = DemandStatus.Received,
                RatingId = 1,
                Prioritize = DemandPrioritize.Low,
                UserId = 1,
                Comments = "Test"
            };

            _mockIDemandRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

            _mockIUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            await _demandUseCase.UpdateDemand(request);

            _mockIDemandHistoryRepository.Verify(x => x.AddAsync(It.IsAny<DemandHistoryEntity>()), Times.AtLeastOnce);
            _mockIUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDemand_WhenNoChanges_ShouldNotAddHistory()
        {
            var entity = new DemandEntity
            {
                Id = 1,
                Status = DemandStatus.Received,
                RatingId = 1,
                Prioritize = DemandPrioritize.Medium,
                UserId = 1
            };

            var request = new DemandUpdateRequest
            {
                Id = 1,
                Status = DemandStatus.Received,
                RatingId = 1,
                Prioritize = DemandPrioritize.Medium,
                UserId = 1,
                Comments = null
            };

            _mockIDemandRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

            _mockIUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            await _demandUseCase.UpdateDemand(request);

            _mockIDemandHistoryRepository.Verify(x => x.AddAsync(It.IsAny<DemandHistoryEntity>()), Times.Never);
        }

        #endregion
    }
}

using AutoMapper;
using Haceb.Demanda.Application.DTO;
using Haceb.Demanda.Application.Port;
using Haceb.Demanda.Common.Exceptions;
using Haceb.Demanda.Common.ResponseModel;
using Haceb.Demanda.Domain.Entities;
using Haceb.Demanda.Domain.Enum;
using Haceb.Demanda.Domain.IRepository;
using Haceb.Demanda.Domain.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Haceb.Demanda.Application.UseCase
{
    public class DemandUseCase(IDemandRepository _demandRepository, IUnitOfWork _unitOfWork, IMapper _mapper,
        IDemandHistoryRepository _historyRepository) : IDemandPort
    {
        private DemandEntity _demandEntity = new();
        private readonly DateTime DateUpdate = DateTime.Now.ToLocalTime();

        public async Task<List<DemandResponse>> GetList(int? pageNumber = null, int? pageSize = null)
        {
            var Demands = await _demandRepository.GetListAsync(pageNumber, pageSize);
            var MapDemand = _mapper.Map<List<DemandResponse>>(Demands);
            return MapDemand;
        }

        public async Task<DemandResponse> GetById(int id)
        {
            var Demand = await _demandRepository.GetByIdAsync(id) ??
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = "Demanda no encontrada" }));
            var MapDemand = _mapper.Map<DemandResponse>(Demand);
            return MapDemand;
        }

        public async Task<List<DemandResponse>> GetByFilter(int type, DemandStatus status)
        {
            var Demand = await _demandRepository.GetByTypeStatusAsync(type, status);
            var MapDemand = _mapper.Map<List<DemandResponse>>(Demand);
            return MapDemand;
        }

        public async Task AddDemand(DemandRequest request)
        {
            var Demand = _mapper.Map<DemandEntity>(request);
            Demand.Status = DemandStatus.Received;
            Demand.DateRegistry = DateTime.Now.ToLocalTime();

            await _demandRepository.Create(Demand);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDemand(DemandUpdateRequest request)
        {
            _demandEntity = await _demandRepository.GetByIdAsync(request.Id) ??
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = "Demanda no encontrada" }));
            await ChangedStatus(request);
            await ChangedRating(request);
            await ChangedPrioritize(request);
            await ChangedUser(request);
            await AddComments(request);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task ChangedStatus(DemandUpdateRequest request)
        {
            if (_demandEntity.Status != request.Status)
            {
                var history = BuildDemandHistory(
                    _demandEntity.Id,
                    request.UserId,
                    "StatusChanged",
                    $"Status cambio de {_demandEntity.Status} a {request.Status}");
                _demandEntity.Status = request.Status;

                await _historyRepository.AddAsync(history);
            }
        }

        private async Task ChangedRating(DemandUpdateRequest request)
        {
            if (_demandEntity.RatingId != request.RatingId)
            {
                var history = BuildDemandHistory(
                    _demandEntity.Id,
                    request.UserId,
                    "RatingChanged",
                    $"Cambio de clasificación de {_demandEntity.RatingId} a {request.RatingId}");
                _demandEntity.RatingId = request.RatingId;

                await _historyRepository.AddAsync(history);
            }
        }

        private async Task ChangedPrioritize(DemandUpdateRequest request)
        {
            if (_demandEntity.Prioritize != request.Prioritize)
            {
                var history = BuildDemandHistory(
                    _demandEntity.Id,
                    request.UserId,
                    "PrioritizeChanged",
                    $"Cambio de usuario de {_demandEntity.Prioritize} a {request.Prioritize}");
                _demandEntity.Prioritize = request.Prioritize;

                await _historyRepository.AddAsync(history);
            }
        }

        private async Task ChangedUser(DemandUpdateRequest request)
        {
            if (_demandEntity.UserId != request.UserId)
            {
                var history = BuildDemandHistory(
                    _demandEntity.Id,
                    request.UserId,
                    "Assigned",
                    $"Cambio de usuario de {_demandEntity.UserId} a {request.UserId}");
                _demandEntity.UserId = request.UserId;

                await _historyRepository.AddAsync(history);
            }
        }

        private async Task AddComments(DemandUpdateRequest request)
        {
            if (string.IsNullOrEmpty(request.Comments))
            {
                var history = BuildDemandHistory(
                    _demandEntity.Id,
                    request.UserId,
                    "Traceability",
                    request.Comments);

                await _historyRepository.AddAsync(history);
            }
        }

        private DemandHistoryEntity BuildDemandHistory(int DemandId, int User, string Action, string Comment) => new()
        {
            DemandId = DemandId,
            UserId = User,
            Action = Action,
            Comments = Comment,
            DateRegistry = DateUpdate
        };
    }
}

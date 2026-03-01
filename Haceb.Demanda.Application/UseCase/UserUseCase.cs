using AutoMapper;
using Haceb.Demanda.Application.DTO;
using Haceb.Demanda.Application.Port;
using Haceb.Demanda.Common.Exceptions;
using Haceb.Demanda.Common.ResponseModel;
using Haceb.Demanda.Domain.IRepository;
using System.Text.Json;

namespace Haceb.Demanda.Application.UseCase
{
    public class UserUseCase(IUserRepository _userRepository, IMapper _mapper) : IUserPort
    {
        public async Task<UserResponse> GetById(int id)
        {
            var User = await _userRepository.GetByIdAsync(id) ??
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = "Usuario no encontrado." }));
            var MapUser = _mapper.Map<UserResponse>(User);
            return MapUser;
        }

        public async Task<List<UserResponse>> GetList()
        {
            var Users = await _userRepository.GetListAsync();
            var MapUsers = _mapper.Map<List<UserResponse>>(Users);
            return MapUsers;
        }
    }
}

using Haceb.Demanda.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Application.Port
{
    public interface IUserPort
    {
        Task<UserResponse> GetById(int id);
        Task<List<UserResponse>> GetList();
    }
}

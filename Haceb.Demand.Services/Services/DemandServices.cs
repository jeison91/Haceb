using Haceb.Demand.Services.Dto;
using Haceb.Demand.Services.IPort;
using Haceb.Demand.Services.RestClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace Haceb.Demand.Services.Services
{
    public class DemandServices(IApiClient _apiClient, IConfiguration _configuration) : IDemandServices
    {
        private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };
        private readonly string Endpoint = _configuration["Api"]!;
        public async Task<List<LookupItemDto>> GetLookup()
        {
            var response = await _apiClient.GetAsync<MessageResponseDto>($"{Endpoint}/Lookups");
            return ParseReponse<List<LookupItemDto>>(response) ?? [];
        }

        public async Task<DemandResponseDto?> GetIdDemand(int id)
        {
            var response = await _apiClient.GetAsync<MessageResponseDto>($"{Endpoint}/Demand/{id}");
            return ParseReponse<DemandResponseDto>(response) ?? null;
        }

        public async Task<List<DemandResponseDto>> GetListDemand()
        {
            var response = await _apiClient.GetAsync<MessageResponseDto>($"{Endpoint}/Demand");
            return ParseReponse<List<DemandResponseDto>>(response) ?? [];
        }

        public async Task<List<UserResponseDto>> GetListUser()
        {
            var response = await _apiClient.GetAsync<MessageResponseDto>($"{Endpoint}/User");
            return ParseReponse<List<UserResponseDto>>(response) ?? [];
        }

        public async Task<bool> PostCreateDemand(DemandRequestDto demandRequest)
        {
            var resultado = await _apiClient.PostAsync<object, MessageResponseDto>($"{Endpoint}/Demand", demandRequest);
            return resultado?.Status == (int)HttpStatusCode.Created;
        }

        public async Task<bool> PutProgressDemand(DemandUpdateRequestDto demandRequest)
        {
            var resultado = await _apiClient.PutAsync<object, MessageResponseDto>($"{Endpoint}/Demand/{demandRequest.Id}", demandRequest);
            return resultado?.Status == (int)HttpStatusCode.OK;
        }

        private T? ParseReponse<T>(MessageResponseDto? response)
        {
            if (response!.Data is null)
                return default;

            var jsonData = response.Data.ToString();
            return jsonData != null ? JsonSerializer.Deserialize<T>(jsonData, _options) : default;
        }
    }
}

using System.Text;
using System.Text.Json;
using BankSimulator.DTOs;
using BankSimulator.Models;
using BankSimulator.Utils;

namespace BankSimulator.Services
{
    static class RecoverService
    {
        public static async Task<bool> SendPostRequest<T>(string endpoint, T data)
        {
            try
            {
                var response = await HttpClientStatic.HttpClient
                    .PostAsync(endpoint, new StringContent(JsonSerializer
                    .Serialize(data), Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                ApplicationLog.RegisterLog("Request Error: " + ex.Message);
                return false;
            }
        }

        public static async Task<bool> Create(RecoverDTO recoverDto, int clientId)
        {
            if (string.IsNullOrWhiteSpace(recoverDto.Email))
                return false;

            return await SendPostRequest("Recover/CreateVerificationCode",
                new Recover
                {
                    Code = recoverDto.Code,
                    CreateAt = DateTime.Now,
                    ExpireAt = DateTime.Now.AddMinutes(5),
                    Used = false,
                    ClientId = clientId
                });
        }

        public static async Task<bool> CheckingCode(RecoverDTO recoverDto)
        {
            return await SendPostRequest("Recover/CheckVerificationCode", recoverDto);
        }

        public static async Task<bool> Change(LoginDTO loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.Password) || (loginDTO.Password.Length < 6))
                {
                    return false;
                }

                var response = await HttpClientStatic.HttpClient.PostAsync("Recover/ChangePassword",
                                                           new StringContent(JsonSerializer.Serialize(loginDTO),
                                                           Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;

            }
            catch (HttpRequestException ex)
            {
                ApplicationLog.RegisterLog("Request Error: " + ex.Message);
                return false;
            }
        }
    }
}
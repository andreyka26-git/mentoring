using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebAPI.Client.DataTransferObjects;

namespace WebAPI.Client
{
    public class EmployeeService
    {
        private readonly Uri _domain;

        public EmployeeService(string domain)
        {
            _domain = new Uri(domain);
        }

        public async Task<IEnumerable<GetEmployeeDto>> GetEmployees(string fieldOrderBy = null)
        {
            var uriBuilder = new UriBuilder(_domain);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["fieldOrder"] = fieldOrderBy;
            uriBuilder.Query = query.ToString();

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uriBuilder.Uri);
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadAsAsync<IEnumerable<GetEmployeeDto>>();
        }

        public async Task<GetEmployeeDto> GetEmployee(int id)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(_domain + $"/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<GetEmployeeDto>();
        }

        public async Task<GetEmployeeDto> AddEmployee(PostEmployeeDto employee)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(_domain, employee);
            response.EnsureSuccessStatusCode();

            var location = response.Headers.Location;
            response = await httpClient.GetAsync(location);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<GetEmployeeDto>();
        }

        public async Task UpdateEmployee(int id, PostEmployeeDto employee)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.PutAsJsonAsync(_domain + $"/{id}", employee);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEmployee(int id)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync(_domain + $"/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebAPI.Application.DataTransferObjects.Employee;

namespace WebAPI.Client
{
    public class EmployeeService : IDisposable
    {
        private readonly Uri _domain;
        private readonly HttpClient _httpClient = new();
        private bool _isDisposed;

        public EmployeeService(string domain)
        {
            _domain = new Uri(domain);
        }

        public async Task<List<GetEmployeeDto>> GetEmployees(string fieldOrderBy = null)
        {
            var uriBuilder = new UriBuilder(_domain);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["fieldOrder"] = fieldOrderBy;
            uriBuilder.Query = query.ToString();

            using var response = await _httpClient.GetAsync(uriBuilder.Uri);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<GetEmployeeDto>>();
        }

        public async Task<GetEmployeeDto> GetEmployee(int id)
        {
            using var response = await _httpClient.GetAsync($"{_domain}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<GetEmployeeDto>();
        }

        public async Task<GetEmployeeDto> AddEmployee(PostEmployeeDto employee)
        {
            using var postResponse = await _httpClient.PostAsJsonAsync(_domain, employee);
            postResponse.EnsureSuccessStatusCode();

            var location = postResponse.Headers.Location;
            using var getResponse = await _httpClient.GetAsync(location);
            getResponse.EnsureSuccessStatusCode();

            return await getResponse.Content.ReadAsAsync<GetEmployeeDto>();
        }

        public async Task UpdateEmployee(int id, PostEmployeeDto employee)
        {
            using var response = await _httpClient.PutAsJsonAsync($"{_domain}/{id}", employee);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEmployee(int id)
        {
            using var response = await _httpClient.DeleteAsync($"{_domain}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public string EmployeeToString(GetEmployeeDto employee)
        {
            return
                $"Id: {employee.Id}, Full name: {employee.FirstName} {employee.LastName}, Higher education: {employee.IsHigherEducation}";
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
                _httpClient.Dispose();

            _isDisposed = true;
        }
    }
}

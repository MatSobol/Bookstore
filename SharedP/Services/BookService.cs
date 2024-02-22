using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Configuration;
using SharedP;
using SharedP.Books;
using SharedP.Services.BookService.Token;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SharedP.Services.AuthService;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;

namespace Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;
        public readonly AppSettings _appSettings;
        public BookService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings= appSettings.Value;
        }
        public async Task<ServiceResponse<string>> CreateBookAsync(Book book, string token)
        {
            JsonContent content = JsonContent.Create(book);
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
                var response = await _httpClient.PostAsync(_appSettings.BaseBookEndpoint.AddBookAsync, content);
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ServiceResponse<string>>(json);
                return result;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }

        public async Task<ServiceResponse<string>> DeleteBookAsync(int id, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
                var response = await _httpClient.DeleteAsync(_appSettings.BaseBookEndpoint.DeleteBookAsync + "/" + id.ToString());
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ServiceResponse<string>>(json);
                return result;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }

        public async Task<ServiceResponse<List<Book>>> ReadBooksAsync()
        {
            try
            {
                string xd = _appSettings.BaseBookEndpoint.GetBooksAsync;
                var response = await _httpClient.GetAsync(_appSettings.BaseBookEndpoint.GetBooksAsync);
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ServiceResponse<List<Book>>>(json);
                return result;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }
        public async Task<ServiceResponse<string>> UpdateBookAsync(Book book, int id, string token)
        {
            try
            {
                JsonContent content = JsonContent.Create(book);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
                var response = await _httpClient.PutAsync(_appSettings.BaseBookEndpoint.UpdateBookAsync + "/" + id.ToString(), content);
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ServiceResponse<string>>(json);
                return result;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }
    }
}

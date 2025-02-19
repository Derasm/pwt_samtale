using Blazored.SessionStorage;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public interface IAuthService
{
    Task<string> GetTokenAsync();
}

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly ISessionStorageService _sessionStorage;

    public AuthService(HttpClient http, ISessionStorageService sessionStorage)
    {
        _http = http;
        _sessionStorage = sessionStorage;
    }

    public async Task<string> GetTokenAsync()
    {
        // Check if token exists in session storage
        var token = await _sessionStorage.GetItemAsStringAsync("jwtToken");
        if (!string.IsNullOrEmpty(token))
        {
            return token; // Return existing token
        }

        // Fetch new token from API
        var response = await _http.GetFromJsonAsync<TokenResponse>("api/auth/get-token");
        if (response?.Token != null)
        {
            await _sessionStorage.SetItemAsStringAsync("jwtToken", response.Token);
            return response.Token;
        }

        return string.Empty;
    }
}

// DTO for JWT response
public class TokenResponse
{
    public string Token { get; set; }
}
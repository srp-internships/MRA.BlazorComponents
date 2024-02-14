using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.Identity.Application.Contract.User.Queries;
using MRA.Identity.Application.Contract.User.Responses;
using Blazored.LocalStorage;
namespace MRA.BlazorComponents;

public class CustomAuthStateProvider(
   IConfiguration configuration,
    IHttpClientService httpClient,
    ILocalStorageService localStorageService)
    : AuthenticationStateProvider
{

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authToken = await GetTokenAsync();
        var identity = new ClaimsIdentity();
        if (authToken != null)
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken.AccessToken), "jwt");
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    private async Task<JwtTokenResponse?> GetTokenAsync()
    {
        var token = await localStorageService.GetItemAsync<JwtTokenResponse>("authToken");
        if (token == null!)
        {
            return null;
        }

        if (token.AccessTokenValidateTo <= DateTime.Now)
        {
            var refreshResponse = await httpClient.PostAsJsonAsync<JwtTokenResponse>(
                configuration["HttpClients:Identity"]! + "auth/refresh",
                new GetAccessTokenUsingRefreshTokenQuery
                {
                    RefreshToken = token.RefreshToken, AccessToken = token.AccessToken
                });
            if (!refreshResponse.Success)
            {
                return null;
            }

            await localStorageService.SetItemAsync("authToken", refreshResponse.Result!);
        }

        return token;
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var jwtObject = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
        return jwtObject.Claims;
    }
}
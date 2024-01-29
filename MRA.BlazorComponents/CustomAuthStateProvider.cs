using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AltairCA.Blazor.WebAssembly.Cookie;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.Identity.Application.Contract.User.Queries;
using MRA.Identity.Application.Contract.User.Responses;

namespace MRA.BlazorComponents;

public class CustomAuthStateProvider(
    IAltairCABlazorCookieUtil cookieUtil,
    IConfiguration configuration,
    HttpClientService httpClient)
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
        var token = await cookieUtil.GetValueAsync<JwtTokenResponse>("authToken");
        if (token == null)
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

            await cookieUtil.SetValueAsync("authToken", refreshResponse.Result, secure: true);
        }

        return token;
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var jwtObject = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
        return jwtObject.Claims;
    }
}
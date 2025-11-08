using Core.Application.Responses;
using Core.Security.Enums;
using Core.Security.JWT;

namespace Application.Features.Auth.Commands.Login;

public class LoggedResponse : IResponse
{
    public AccessToken? AccessToken { get; set; }
    public Core.Security.Entities.RefreshToken? RefreshToken { get; set; }
    public AuthenticatorType? RequiredAuthenticatorType { get; set; }
    public List<string> Roles { get; set; } = new();

    public LoggedHttpResponse ToHttpResponse() =>
        new() { AccessToken = AccessToken, RequiredAuthenticatorType = RequiredAuthenticatorType, Roles = Roles };

    public class LoggedHttpResponse
    {
        public AccessToken? AccessToken { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}

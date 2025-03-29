using System.Globalization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WoWsShipBuilder.Web.Infrastructure;
using System.ComponentModel.DataAnnotations;
using OpenTelemetry.Trace;

namespace WoWsShipBuilder.Web.Features.Authentication;

public class AuthRequest
{
    [Required]
    public string AccessToken { get; set; } = "";

    [Required]
    public string AccountId { get; set; } = "";

    [Required]
    public string Nickname { get; set; } = "";

    [Required]
    public string Status { get; set; } = "";

    public string Message { get; set; } = "";

    public int Code { get; set; }
}

[Route("/api/[controller]")]
[ApiController]
public class AuthController(IOptions<AdminOptions> options, ILogger<AuthController> logger, AuthenticationService authenticationService) : ControllerBase
{
    private readonly AdminOptions options = options.Value;

    [HttpGet]
    public async Task<ActionResult> Authenticate([FromQuery] AuthRequest authRequest)
    {
        logger.LogDebug("Authenticating with API Key: {Key}. Status: {Status}, Message: {Message}, Code: {Code}", this.options.WgApiKey, authRequest.Status, authRequest.Message, authRequest.Code);
        if (authRequest.Status == "ok")
        {
            return await this.AuthenticationConfirmed(authRequest.AccessToken, authRequest.AccountId, authRequest.Nickname);
        }

        return await this.AuthenticationCanceled(authRequest.Status, authRequest.Message, authRequest.Code);
    }

    [HttpPost("confirm")]
    public async Task<ActionResult> AuthenticationConfirmed(string accessToken, string accountId, string nickname)
    {
        logger.LogDebug("Authentication confirmed for Account: {AccountId}, Nickname: {Nickname}", accountId, nickname);
        var authValid = await authenticationService.VerifyToken(accountId, accessToken);
        if (!authValid)
        {
            return this.Redirect("/auth-failed");
        }

        var principal = authenticationService.CreatePrincipalForUser(accessToken, accountId, nickname);
        await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new() { IsPersistent = true });
        return this.Redirect("/");
    }

    [HttpPost("cancel")]
    public Task<ActionResult> AuthenticationCanceled(string status, string message, int code)
    {
        logger.LogDebug("Authentication canceled. Status: {Status}, Message: {Message}, Code: {Code}", status, message, code);
        return Task.FromResult<ActionResult>(this.Redirect("/"));
    }

    [Route("/api/[controller]")]
    [ApiController]
    public class LoginController(IOptions<AdminOptions> options, ILogger<AuthController.LoginController> logger) : ControllerBase
    {
        private readonly AdminOptions options = options.Value;

        [HttpGet("login/{server:regex(^eu|asia|com$):required}")]
        public Task<ActionResult> Login(string server)
        {
            logger.LogDebug("Loging to SErver: {Server}", server);
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var pageUrl = $"{baseUrl}/api/auth";
            var url = @$"https://api.worldoftanks.{server}/wot/auth/login/?application_id={this.options.WgApiKey}&redirect_uri={pageUrl}";
            return Task.FromResult<ActionResult>(this.Redirect(url));
        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.Redirect("/");
        }
    }
}

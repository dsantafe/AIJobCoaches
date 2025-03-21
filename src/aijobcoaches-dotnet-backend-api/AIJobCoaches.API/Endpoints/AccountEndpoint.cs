namespace AIJobCoaches.API.Endpoints
{
    using AIJobCoaches.Application.DTOs;
    using AIJobCoaches.Application.Interfaces;
    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    /// <summary>
    /// Minimal APIs Account
    /// </summary>
    public static class AccountEndpoint
    {
        /// <summary>
        /// Registro de APIs
        /// </summary>
        /// <param name="app"></param>
        public static WebApplication RegisterApis(this WebApplication app)
        {
            var root = app.MapGroup("/api/accounts")
               .WithTags(new[] { "Accounts" })
               .WithOpenApi();

            _ = root.MapPost("/login", Login)
                .WithName("Login")
                .WithDescription("Login")
                .Produces<ResponseDTO>(StatusCodes.Status200OK)
                .Produces<ResponseDTO>(StatusCodes.Status400BadRequest)
                .Produces<ResponseDTO>(StatusCodes.Status401Unauthorized)
                .Produces<ResponseDTO>(StatusCodes.Status500InternalServerError);

            return app;
        }

        static async Task<IResult> Login([FromServices] IAccountService accountService,
            [FromServices] IValidator<LoginDTO> validator,
            [FromBody] LoginDTO request)
        {
            try
            {
                ResponseDTO response = new() { IsSuccess = false, Data = null };
                ValidationResult validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    response.Message = string.Join(", ", validationResult.Errors.Select(failure => $"Error: {failure.ErrorMessage}"));
                    return Results.BadRequest(response);
                }

                AccountDTO account = accountService.Login(request.Username, request.Password);

                if (account != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Login OK";
                    response.Data = account;

                    return Results.Ok(response);
                }
                else
                    return Results.Unauthorized();
            }
            catch (Exception ex)
            {
                string message = string.IsNullOrEmpty(ex.InnerException?.Message) ? ex.Message : ex.InnerException?.Message;
                string stackTrace = $"{ex?.StackTrace} | {ex?.InnerException?.StackTrace}";

                ResponseDTO error = new() { Message = $"{message}, {stackTrace}" };
                return Results.Json(error, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}

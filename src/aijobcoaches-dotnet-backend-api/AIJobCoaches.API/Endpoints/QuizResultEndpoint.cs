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
    public static class QuizResultEndpoint
    {
        /// <summary>
        /// Registro de APIs
        /// </summary>
        /// <param name="app"></param>
        public static WebApplication RegisterApis(this WebApplication app)
        {
            var root = app.MapGroup("/api/quiz-results")
               .WithTags(new[] { "Quiz Results" })
            .WithOpenApi();

            _ = root.MapPost("", CreateQuizResults)
                .WithName("Create Quiz Results")
                .WithDescription("Create Quiz Results")
                .Produces<ResponseDTO>(StatusCodes.Status200OK)
                .Produces<ResponseDTO>(StatusCodes.Status400BadRequest)
                .Produces<ResponseDTO>(StatusCodes.Status500InternalServerError);

            return app;
        }

        static async Task<IResult> CreateQuizResults([FromServices] IQuizResultService quizResultService,
            [FromServices] IValidator<QuizResultDTO> validator,
            [FromBody] QuizResultDTO request)
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

                quizResultService.CreateQuizResult(request);
                return Results.Ok(new ResponseDTO { IsSuccess = true });
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
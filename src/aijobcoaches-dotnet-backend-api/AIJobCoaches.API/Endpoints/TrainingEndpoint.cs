namespace AIJobCoaches.API.Endpoints
{
    using AIJobCoaches.Application.DTOs;
    using AIJobCoaches.Application.Interfaces;
    using AIJobCoaches.Domain.Entities;
    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    /// <summary>
    /// Minimal APIs Account
    /// </summary>
    public static class TrainingEndpoint
    {
        /// <summary>
        /// Registro de APIs
        /// </summary>
        /// <param name="app"></param>
        public static WebApplication RegisterApis(this WebApplication app)
        {
            var root = app.MapGroup("/api/trainings")
               .WithTags(new[] { "Trainings" })
               .WithOpenApi();

            _ = root.MapGet("/{trainingID:int}/topics", GetTopicsWithItemsByTrainingId)
                .WithName("Get Topics With Items By TrainingId")
                .WithDescription("Get Topics With Items By TrainingId")
                .Produces<ResponseDTO>(StatusCodes.Status200OK);

            _ = root.MapGet("/{trainingID:int}/courses", GetCoursesByTraining)
                .WithName("Get Courses By Training")
                .WithDescription("Get Courses By Training")
                .Produces<ResponseDTO>(StatusCodes.Status200OK);

            _ = root.MapPost("", CreateTraining)
                .WithName("Create Training")
                .WithDescription("Create Training")
                .Produces<ResponseDTO>(StatusCodes.Status200OK)
                .Produces<ResponseDTO>(StatusCodes.Status400BadRequest)
                .Produces<ResponseDTO>(StatusCodes.Status500InternalServerError);

            return app;
        }

        static IResult GetTopicsWithItemsByTrainingId([FromServices] ITopicService topicService,
            [FromRoute] int trainingID)
        {
            IEnumerable<TopicDTO> topics = topicService.GetTopicsWithItemsByTrainingId(trainingID);
            return Results.Ok(new ResponseDTO { IsSuccess = true, Data = topics });
        }

        static IResult GetCoursesByTraining([FromServices] ICourseService courseService,
            [FromRoute] int trainingID)
        {
            IEnumerable<CourseDTO> courses = courseService.GetCoursesByTraining(trainingID);
            return Results.Ok(new ResponseDTO() { IsSuccess = true, Data = courses });
        }

        static async Task<IResult> CreateTraining([FromServices] ITrainingService trainingService,
            [FromServices] IValidator<TrainingDTO> validator,
            [FromBody] TrainingDTO request)
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

                trainingService.CreateTraining(request);
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

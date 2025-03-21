namespace AIJobCoaches.API.Endpoints
{
    using AIJobCoaches.Application.DTOs;
    using AIJobCoaches.Application.Interfaces;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Minimal APIs Account
    /// </summary>
    public static class EmployeeEndpoint
    {
        /// <summary>
        /// Registro de APIs
        /// </summary>
        /// <param name="app"></param>
        public static WebApplication RegisterApis(this WebApplication app)
        {
            var root = app.MapGroup("/api/employees")
               .WithTags(new[] { "Employees" })
               .WithOpenApi();

            _ = root.MapGet("/{employeeID:int}/trainings", GetTrainingsByEmployeeID)
                .WithName("Get Trainings By Employee ID")
                .WithDescription("Get Trainings By Employee ID")
                .Produces<ResponseDTO>(StatusCodes.Status200OK);

            return app;
        }

        static IResult GetTrainingsByEmployeeID([FromServices] IEnrollmentService enrollmentService,
            [FromRoute] int employeeID)
        {
            IEnumerable<TrainingDTO> trainings = enrollmentService.GetTrainingsByEmployeeID(employeeID);
            return Results.Ok(new ResponseDTO { IsSuccess = true, Data = trainings });
        }
    }
}

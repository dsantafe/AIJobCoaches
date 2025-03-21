using AIJobCoaches.API.Bootstrapper;
using AIJobCoaches.API.Endpoints;

// Get WebApplication instance
WebApplication app = AppBuilder.GetApp(args);

// Configure Request Pipeline
RequestPipelineBuilder.Configure(app);

// Configure APIs 
AccountEndpoint.RegisterApis(app);
TrainingEndpoint.RegisterApis(app);
EmployeeEndpoint.RegisterApis(app);
QuizResultEndpoint.RegisterApis(app);

// Start the app
app.Run();

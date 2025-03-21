namespace AIJobCoaches.API.Bootstrapper
{
    /// <summary>
    /// Request Pipeline Builder
    /// </summary>
    public static class RequestPipelineBuilder
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        public static void Configure(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
        }
    }
}

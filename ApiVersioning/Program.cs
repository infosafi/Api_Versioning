using Asp.Versioning;
using Asp.Versioning.Conventions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger Interect with Version
builder.Services.AddSwaggerGen(options =>
{
    // Define Swagger documents for different versions
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "API v1",
        Description = "API documentation for version 1"
    });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v2",
        Title = "API v2",
        Description = "API documentation for version 2"
    });
    options.SwaggerDoc("v3", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v3",
        Title = "API v3",
        Description = "API documentation for version 3"
    });
    // Use the ConflictingActionsResolver workaround
    options.ResolveConflictingActions(apiDescriptions =>
    {
        // Your conflict resolution strategy here
        // Example: Choose the first action
        return apiDescriptions.First();
    });
    // Add more versions as needed
});
#endregion

#region Api Version 
builder.Services.AddApiVersioning(options => {
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1,0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-API-Version"),
        new MediaTypeApiVersionReader("api-version")
    );

}).AddMvc(options => {
    options.Conventions.Add(new VersionByNamespaceConvention());
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
#region Swagger Settings 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Define the endpoints for each version
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");
        options.SwaggerEndpoint("/swagger/v3/swagger.json", "API v3");

    });
}
#endregion
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

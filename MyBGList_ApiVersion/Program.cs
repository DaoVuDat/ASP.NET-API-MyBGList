using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Generate swagger.json
builder.Services.AddSwaggerGen(
    // options => options.ResolveConflictingActions(apiDesc => apiDesc.First())
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "MyBGList", Version = "v1.0"
        });
        options.SwaggerDoc("v2", new OpenApiInfo
        {
            Title = "MyBGList", Version = "v2.0"
        });
        options.SwaggerDoc("v3", new OpenApiInfo
        {
            Title = "MyBGList", Version = "v3.1"
        });
        options.SwaggerDoc("v3.1", new OpenApiInfo
        {
            Title = "MyBGList", Version = "v3.1"
        });
    }
);

builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policyBuilder =>
        {
            policyBuilder.AllowAnyHeader();
            policyBuilder.WithOrigins(builder.Configuration["AllowedOrigins"] ?? string.Empty);
            policyBuilder.AllowAnyMethod();
        });
        options.AddPolicy(name: "AnyOrigins", policyBuilder =>
        {
            policyBuilder.AllowAnyHeader();
            policyBuilder.AllowAnyMethod();
            policyBuilder.AllowAnyOrigin();
        });
        options.AddPolicy(name: "AnyOrigin_GetOnly", policyBuilder =>
        {
            policyBuilder.WithMethods(HttpMethods.Get);
            policyBuilder.AllowAnyHeader();
            policyBuilder.AllowAnyOrigin();
        });
    }
);

builder.Services.AddApiVersioning(
    options =>
        options.ApiVersionReader = new UrlSegmentApiVersionReader()
);

// add the versioned IApiExplorer and capture the strongly-typed
// implementation (e.g. VersionedApiExplorer vs IApiExplorer)
// note: the specified format code will format the version as "'v'major[.minor][-status]"
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // https://github.com/dotnet/aspnet-api-versioning/wiki/Version-Format
    
    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Configuration.GetValue<bool>("UseSwagger"))
{
    app.UseSwagger();
    
    // Create UI for swagger.json
    // /swagger/[name_in_swaggerDoc]/swagger.json => point to the Endpoint which is created at SwaggerDoc in Services.AddSwaggerGen
    // "MyBGList v3.1" => Name will be shown in UI
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            $"/swagger/v1/swagger.json",
            "MyBGList v1");
        options.SwaggerEndpoint(
            $"/swagger/v2/swagger.json",
            "MyBGList v2");
        options.SwaggerEndpoint(
            $"/swagger/v3/swagger.json",
            "MyBGList v3");
        options.SwaggerEndpoint(
            $"/swagger/v3.1/swagger.json",
            "MyBGList v3.1");
    });
}

if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler("/error");


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapGet("/error/test",
    [EnableCors("AnyOrigins")] 
    [ResponseCache(NoStore = true)]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    () => { throw new Exception("test"); });

app.MapGet("/error",
    [EnableCors("AnyOrigins")] 
    [ResponseCache(NoStore = true)]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    () => Results.Problem());

app.MapGet("/v{version:ApiVersion}/cod/test",
    [EnableCors("AnyOrigin_GetOnly")] 
    [ResponseCache(NoStore = true)]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    () => Results.Text("<script>" +
                       "window.alert('Your client supports JavaScript!" +
                       "\\r\\n\\r\\n" +
                       $"Server time (UTC): {DateTime.UtcNow.ToString("o")}" +
                       "\\r\\n" +
                       "Client time (UTC): ' + new Date().toISOString());" +
                       "</script>" +
                       "<noscript>Your client does not support JavaScript</noscript>",
        "text/html"));

app.MapControllers();

app.Run();
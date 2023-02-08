using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyBGList;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    // options => options.ResolveConflictingActions(apiDesc => apiDesc.First())    
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
    }
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Configuration.GetValue<bool>("UseSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler("/error");


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapGet("/error/test",
    [EnableCors("AnyOrigins")] [ResponseCache(NoStore = true)]
    () => { throw new Exception("test"); });
// app.MapGet("/BoardGames", () => new[]
//     {
//         new BoardGame()
//         {
//             Id = 1,
//             Name = "Axis & Allies",
//             Year = 1981
//         },
//         new BoardGame()
//         {
//             Id = 2,
//             Name = "Citadels",
//             Year = 2000
//         },
//         new BoardGame()
//         {
//             Id = 3,
//             Name = "Terraforming Mars",
//             Year = 2016
//         }
//     }
// );

app.MapGet("/error",
    [EnableCors("AnyOrigins")] [ResponseCache(NoStore = true)]
    () => Results.Problem());

app.MapGet("/cod/test",
    [EnableCors("AnyOrigins")] [ResponseCache(NoStore = true)]
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
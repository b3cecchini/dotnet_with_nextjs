using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

var configSection = builder.Configuration.GetSection("ReverseProxy");

Console.WriteLine("IdentityUrl is: " +builder.Configuration["IdentityServiceUrl"]);

builder.Services.AddReverseProxy()
    .LoadFromConfig(configSection);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
    {
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "username";
    });

// builder.Services.AddCors(options => 
// {
//     options.AddPolicy("customPolicy", b => 
//     {
//         b.AllowAnyHeader()
//             .AllowAnyMethod().AllowCredentials().WithOrigins(builder.Configuration["ClientApp"]);
//     });
// });

var app = builder.Build();

// app.UseCors();

app.MapReverseProxy();

app.UseAuthentication();
app.UseAuthorization();

/* 
    App Kepps Trying To Bind To Localhost:5000 (same port as Identity Service) 
    Need to run with command "dotnet watch --urls "localhost:6001"
    But app does not seem to respect appSettings.Development.json values
    Had to move all config values to bae appSettings.json 
*/
app.Run();
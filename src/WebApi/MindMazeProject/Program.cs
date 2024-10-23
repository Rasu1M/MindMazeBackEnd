using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Registration;
using MindMaze.Infrastructure.infrastructure.Data;
using MindMaze.Infrastructure.infrastructure.Hubs;
using MindMaze.Infrastructure.infrastructure.Registration;
using MindMaze.Infrastructure.infrastructure.Services.EmailMessages;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//SIgnalR
builder.Services.AddSignalR();

//Cors
builder.Services.AddCors(co =>
{
    co.AddPolicy("MindMazePolicy", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.SetIsOriginAllowed(origin => true);
        policy.AllowCredentials();
    });
});

//Email Services
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("EmailOptions"));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.RegisterApplication();

builder.Services.RegisterInfrastructure(builder.Configuration);


builder.Services.AddAuthentication(ao =>
{
    ao.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    ao.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(con =>
    {
        var singingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSecurityKey"]));

        con.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = singingkey,
            ValidAudience = "MindMazeProject",
            ValidIssuer = "MindMazeProject"
        };
    });

builder.Services.AddAuthorization();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Cors
app.UseCors("MindMazePolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//SIgnalR
app.MapHub<PlayHub>("playhub");

app.MapControllers();

app.Run();

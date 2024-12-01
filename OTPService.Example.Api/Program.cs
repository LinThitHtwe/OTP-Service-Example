using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OTPService.Example.Api.AppDbContextModels;
using OTPService.Example.Models.Features.Signin;
using OTPService.Example.Models.Features.Signup;
using OTPService.Example.Services.Features.Signin;
using OTPService.Example.Services.Features.Signup;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
},
ServiceLifetime.Transient,
ServiceLifetime.Transient);

builder.AddSigninService();
builder.AddSignupService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapGet("/test", async (SigninService signinService) =>
//{
//    var users = await signInService.Test();
//    return users;
//})
//.WithName("Test")
//.WithOpenApi();

app.MapPost("/signup",async (SignupService signupService,SignupRequestModel signupRequestModel) =>
{
    await signupService.Signup(signupRequestModel);
})
.WithName("Signup")
.WithOpenApi();

app.MapPost("/signin", async (SigninService signinService, SigninRequestModel signinRequestModel) =>
{
    var response = await signinService.SigninAsync(signinRequestModel);

    if (response.IsValidationError)
    {
        return Results.BadRequest(new { message = "Validation failed", errors = response.Message });
    }

    return Results.Ok(response);
})
.WithName("Signin")
.WithOpenApi();


app.Run();

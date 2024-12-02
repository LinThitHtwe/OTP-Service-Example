using Microsoft.EntityFrameworkCore;
using OTPService.Example.Api;
using OTPService.Example.Api.AppDbContextModels;
using OTPService.Example.Models.Features.OTP;
using OTPService.Example.Models.Features.Signin;
using OTPService.Example.Models.Features.Signup;
using OTPService.Example.Services.Features.FluentEmail;
using OTPService.Example.Services.Features.OTPVerify;
using OTPService.Example.Services.Features.ResendOTP;
using OTPService.Example.Services.Features.SendMail;
using OTPService.Example.Services.Features.SendOTP;
using OTPService.Example.Services.Features.Signin;
using OTPService.Example.Services.Features.Signup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentEmail();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
},
ServiceLifetime.Transient,
ServiceLifetime.Transient);

builder.AddSigninService();
builder.AddSignupService();
builder.AddSendMailService();
builder.AddOTPVerifyService();
builder.AddSendOTPService();
builder.AddResendOTPService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/signup",async (SignupService signupService,SignupRequestModel signupRequestModel) =>
{
    var response = await signupService.Signup(signupRequestModel);
    return BaseResponseHelper.Execute(response);
})
.WithName("Signup")
.WithOpenApi();

app.MapPost("/verify-signup-otp", async (SignupService signupService, VerifySignupOTPRequestModel verifySignupOTPRequest) =>
{
    var response = await signupService.VerifySignupOTP(verifySignupOTPRequest);
    return BaseResponseHelper.Execute(response);
})
.WithName("VerifySignupOTP")
.WithOpenApi();

app.MapPost("/signin", async (SigninService signinService, SigninRequestModel signinRequestModel) =>
{
    var response = await signinService.Signin(signinRequestModel);
    return BaseResponseHelper.Execute(response);
})
.WithName("Signin")
.WithOpenApi();

app.MapPost("/resent-otp", async (ResendOTPService resendOTPService, ResentOTPRequestModel resentOTPRequest) =>
{
    var response = await resendOTPService.ResendOTP(resentOTPRequest);
    return BaseResponseHelper.Execute(response);
})
.WithName("ResentOTP")
.WithOpenApi();

app.Run();

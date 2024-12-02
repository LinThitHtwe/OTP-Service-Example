namespace OTPService.Example.Services.Features.ResendOTP;

public static class ResendOTPExtentions
{
    public static void AddResendOTPService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ResendOTPService>();
    }
}

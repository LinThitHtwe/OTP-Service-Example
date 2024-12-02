namespace OTPService.Example.Services.Features.OTPVerify;

public static class OTPVerifyExtentions
{
    public static void AddOTPVerifyService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<OTPVerifyService>();
    }
}

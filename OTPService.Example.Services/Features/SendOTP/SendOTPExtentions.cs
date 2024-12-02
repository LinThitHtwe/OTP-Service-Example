namespace OTPService.Example.Services.Features.SendOTP;

public static class SendOTPExtentions
{
    public static void AddSendOTPService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<SendOTPService>();
    }
}

namespace OTPService.Example.Services.Features.SendMail;

public static class SendMailExtentions
{
    public static void AddSendMailService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<SendMailService>();
    }
}
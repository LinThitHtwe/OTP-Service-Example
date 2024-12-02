using dotenv.net;
using System.Net;
using System.Net.Mail;

namespace OTPService.Example.Services.Features.FluentEmail;

public static class FluentEmailExtentions
{
    public static void AddFluentEmail(this IServiceCollection services)
    {
        var envVars = DotEnv.Read();
        var defaultFromEmail = envVars["DEFAULT_EMAIL"];
        var host = envVars["EMAIL_HOST"];
        var port = Int32.Parse(envVars["EMAIL_PORT"]);
        var username = envVars["EMAIL"];
        var password = envVars["EMAIL_PASSWORD"];

        services.AddFluentEmail(defaultFromEmail)
        .AddSmtpSender(() => new SmtpClient(host, port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(username, password)
        })
        .AddRazorRenderer();
    }
}

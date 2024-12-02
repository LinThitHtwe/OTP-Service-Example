using FluentEmail.Core;
using OTPService.Example.Database.AppDbContextModels;
using OTPService.Example.Models.Features.OTP;

namespace OTPService.Example.Services.Features.SendMail;

public class SendMailService
{
    private readonly IFluentEmail _fluentEmail;
    private readonly IFluentEmailFactory _fluentEmailFactory;

    public SendMailService(IFluentEmail fluentEmail, IFluentEmailFactory fluentEmailFactory)
    {
        _fluentEmail = fluentEmail;
        _fluentEmailFactory = fluentEmailFactory;
    }

    public async Task SendOTPMail(string toMail, OTPEmailViewModel otpEmailViewModel)
    {
        await _fluentEmail.To(toMail)
                    .Subject("One-Time Passcode")
                    .UsingTemplateFromFile
                    ($"{Directory.GetCurrentDirectory()}/Template/OTPTemplate.cshtml", otpEmailViewModel)
                    .SendAsync();
    }
}
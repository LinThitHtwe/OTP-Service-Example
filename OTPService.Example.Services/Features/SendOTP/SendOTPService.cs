using OTPService.Example.Database.AppDbContextModels;

namespace OTPService.Example.Services.Features.SendOTP;

public class SendOTPService
{
    private readonly AppDbContext _db;
    private readonly SendMailService _sendMailService;

    public SendOTPService(AppDbContext db, SendMailService sendMailService)
    {
        _db = db;
        _sendMailService = sendMailService;
    }

    public async Task SendOTP(int userId, string toMail)
    {
        string otpCode = OTPHelper.GenerateOTPCode();

        Otp otp = new()
        {
            Otpcode = otpCode,
            UserId = userId,
            CreatedTime = DateTime.Now,
            ExpireTime = DateTime.Now.AddMinutes(3),
            Status = nameof(OTPStatusEnum.Active),
        };

        _db.Otps.Add(otp);
        await _db.SaveChangesAsync();

        OTPEmailViewModel otpEmailViewModel = new()
        {
            ExpiryTime = otp.ExpireTime,
            OTPCode = otp.Otpcode
        };

        await _sendMailService.SendOTPMail(toMail, otpEmailViewModel);
    }
}

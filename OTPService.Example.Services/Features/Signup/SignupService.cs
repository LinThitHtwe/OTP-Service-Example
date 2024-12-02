
using Microsoft.EntityFrameworkCore;
using OTPService.Example.Database.AppDbContextModels;
using OTPService.Example.Models.Features.OTP;


namespace OTPService.Example.Services.Features.Signup;

public class SignupService
{
    private readonly AppDbContext _db;
    private readonly SendMailService _sendMailService;

    public SignupService(AppDbContext db, SendMailService sendMailService)
    {
        _db = db;
        _sendMailService = sendMailService;
    }

    public async Task<Result<SignupResponseModel>> Signup(SignupRequestModel signupRequestModel)
    {
        try
        {
            var isEmailExist = await _db.Users.AnyAsync(x => x.Email == signupRequestModel.Email);
            if (isEmailExist)
            {
                return Result<SignupResponseModel>.ValidationError("");
            }

            User user = new()
            {
                Username = signupRequestModel.Name,
                Email = signupRequestModel.Email,
                Password = PasswordHasher.HashPassword(signupRequestModel.Password),
                Status = nameof(UserStatusEnum.Pending)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            await SendOTP(user.Id, user.Email);

            SignupResponseModel response = new()
            {
                IsOTPSent = true,
                Message = "Successful"
            };

            return Result<SignupResponseModel>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<SignupResponseModel>.Failure(ex.Message);
        }
    }

    private async Task SendOTP(int userId,string toMail)
    {
        string otpCode = OTPHelper.GenerateOTPCode();

        Otp otp = new()
        {
            Otpcode = otpCode,
            UserId = userId,
            CreatedTime = DateTime.Now,
            ExpireTime = DateTime.Now.AddMinutes(3),
            Status = nameof(OPTStatusEnum.Active),
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


using Microsoft.EntityFrameworkCore;
using OTPService.Example.Database.AppDbContextModels;

namespace OTPService.Example.Services.Features.Signup;

public class SignupService
{
    private readonly AppDbContext _db;
    private readonly SendMailService _sendMailService;
    private readonly OTPVerifyService _otpVerifyService;

    public SignupService(AppDbContext db, SendMailService sendMailService, OTPVerifyService otpVerifyService)
    {
        _db = db;
        _sendMailService = sendMailService;
        _otpVerifyService = otpVerifyService;
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
                UserId = user.Id,
                Name = user.Username,
                Email = user.Email,
                Status = user.Status,
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

    public async Task<Result<VerifySignupOTPResponseModel>> VerifySignupOTP(VerifySignupOTPRequestModel verifySignupOTPModel)
    {
        try
        {
            var isSignupOTPVerified = await _otpVerifyService
                .VerifyOTP(verifySignupOTPModel.OTPCode, verifySignupOTPModel.UserId);


            if (!isSignupOTPVerified)
            {
                return Result<VerifySignupOTPResponseModel>.ValidationError("Invalid OTP");
            }

            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == verifySignupOTPModel.UserId);

            user!.Status = nameof(UserStatusEnum.Varified);
            _db.Entry(user).State = EntityState.Modified;

            VerifySignupOTPResponseModel verifySignupOTPResponse = new()
            {
                IsOTPVerified = true,
                UserId = user!.Id,
                Name = user.Username,
                Email = user.Email,
                Status = user.Status,
            };


            await _db.SaveChangesAsync();

            verifySignupOTPResponse.UserId = user!.Id;
            verifySignupOTPResponse.Name = user.Username;
            verifySignupOTPResponse.Email = user.Email;
            verifySignupOTPResponse.Status = user.Status;

            return Result<VerifySignupOTPResponseModel>.Success(verifySignupOTPResponse);
        }
        catch (Exception ex)
        {
            return Result<VerifySignupOTPResponseModel>.Failure(ex.Message);
        }
    }

    private async Task SendOTP(int userId, string toMail)
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

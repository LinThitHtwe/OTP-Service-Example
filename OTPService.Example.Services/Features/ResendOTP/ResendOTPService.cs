using Microsoft.EntityFrameworkCore;
using OTPService.Example.Database.AppDbContextModels;

namespace OTPService.Example.Services.Features.ResendOTP;

public class ResendOTPService
{
    private readonly AppDbContext _db;
    private readonly SendOTPService _sendOTPService;

    public ResendOTPService(AppDbContext context, SendOTPService sendOTPService)
    {
        _db = context;
        _sendOTPService = sendOTPService;
    }

    public async Task<Result<ResentOTPResponseModel>> ResendOTP(ResentOTPRequestModel resentOTPRequest)
    {
        try
        {
            var user = resentOTPRequest.UserId != 0
                       ? await _db.Users
                       .FirstOrDefaultAsync(x => x.Id == resentOTPRequest.UserId)
                       : await _db.Users
                       .FirstOrDefaultAsync(x => x.Email == resentOTPRequest.Email);

            if (user is null)
            {
                return Result<ResentOTPResponseModel>
                    .ValidationError("Invalid User");
            }

            var invalidStatuses = new[]
            {
            nameof(OTPStatusEnum.Invalid),
            nameof(OTPStatusEnum.Expired),
            nameof(OTPStatusEnum.Used)
            };

            await _db.Otps
            .Where(otp => otp.UserId == user.Id
                //&& otp.CreatedTime >= DateTime.Now.AddMinutes(-5) 
                //&& otp.CreatedTime <= DateTime.Now
                && !invalidStatuses.Contains(otp.Status))
                .ExecuteUpdateAsync
                (update => update.SetProperty
                (otp => otp.Status, nameof(OTPStatusEnum.Invalid))
                );

            await _sendOTPService.SendOTP(user.Id, user.Email);

            ResentOTPResponseModel resentOTPResponse = new()
            {
                IsOTPSent = true
            };

            return Result<ResentOTPResponseModel>.Success(resentOTPResponse);
        }
        catch (Exception ex)
        {
            return Result<ResentOTPResponseModel>.ValidationError(ex.Message);
        }
    }
}

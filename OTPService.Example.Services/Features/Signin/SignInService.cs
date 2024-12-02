using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OTPService.Example.Services.Helpers;

namespace OTPService.Example.Services.Features.Signin;

public class SigninService
{
    private readonly AppDbContext _db;

    public SigninService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<SigninResponseModel>> Signin(SigninRequestModel requestModel)
    {
        var user = await _db.Users
                 .FirstOrDefaultAsync(u => u.Email == requestModel.Email);

        if (user == null)
        {
            return Result<SigninResponseModel>.ValidationError("");
        }

        bool isPasswordValid = PasswordHasher.VerifyPassword(requestModel.Password, user.Password);

        if (!isPasswordValid)
        {
            return Result<SigninResponseModel>.ValidationError("");
        }

        SigninResponseModel signin = new()
        {
            Email = user.Email,
            Name = user.Username,
            SessionExpiredTime = DateTime.Now.AddMinutes(5),
            UserId = user.Id,
        };

        var token = signin.ToJson().ToEncrypt();
        signin.Token = token;

        return Result<SigninResponseModel>.Success(signin);
    }
}

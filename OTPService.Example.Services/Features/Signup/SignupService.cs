
using Microsoft.EntityFrameworkCore;

namespace OTPService.Example.Services.Features.Signup;

public class SignupService
{
    private readonly AppDbContext _db;

    public SignupService(AppDbContext db)
    {
        _db = db;
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
                Status = "Test"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            SignupResponseModel response = new()
            {
                IsOTPSent = true,
                Message = "Successful"
            };

            return Result<SignupResponseModel>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<SignupResponseModel>.Failure("");
        }
    }
}

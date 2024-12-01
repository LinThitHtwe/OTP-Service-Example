using Microsoft.EntityFrameworkCore;

namespace OTPService.Example.Services.Features.Signin;

public class SignInService
{
    private readonly AppDbContext _db;

    public SignInService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<User>> Test()
    {
        return await _db.Users.ToListAsync();
    }
}

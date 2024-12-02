using Microsoft.EntityFrameworkCore;

namespace OTPService.Example.Services.Features.OTPVerify;

public class OTPVerifyService
{
    private readonly AppDbContext _db;

    public OTPVerifyService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> VerifyOTP(string otpCode, int userId)
    {
        var otpRecord = await _db.Otps
                        .Where(x => x.Otpcode == otpCode
                        && x.ExpireTime > DateTime.Now  
                        && x.Status == nameof(OTPStatusEnum.Active)
                        && x.UserId == userId)
                        .FirstOrDefaultAsync();

        if (otpRecord != null)
        {
            otpRecord.Status = nameof(OTPStatusEnum.Used);
            _db.Entry(otpRecord).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true; 
        }

        return false; 
    }

}

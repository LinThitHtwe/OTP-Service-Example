namespace OTPService.Example.Database.AppDbContextModels;

public partial class Otp
{
    public int Id { get; set; }

    public string Otpcode { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime ExpireTime { get; set; }

    public DateTime CreatedTime { get; set; }

    public virtual User User { get; set; } = null!;
}

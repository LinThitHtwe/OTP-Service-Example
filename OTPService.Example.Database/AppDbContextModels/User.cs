namespace OTPService.Example.Database.AppDbContextModels;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Otp> Otps { get; set; } = new List<Otp>();
}

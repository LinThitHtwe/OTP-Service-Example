namespace OTPService.Example.Models.Features.OTP;

public class ResentOTPRequestModel
{
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
}

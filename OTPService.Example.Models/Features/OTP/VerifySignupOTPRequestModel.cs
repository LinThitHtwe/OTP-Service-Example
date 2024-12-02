namespace OTPService.Example.Models.Features.OTP;

public class VerifySignupOTPRequestModel
{
    public int UserId { get; set; }
    public string OTPCode { get; set; }
}

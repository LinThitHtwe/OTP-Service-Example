namespace OTPService.Example.Models.Features.OTP;

public class VerifySignupOTPResponseModel
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Status {  get; set; }
    public bool IsOTPVerified { get; set; }
}

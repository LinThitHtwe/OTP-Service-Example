namespace OTPService.Example.Models.Features.Signup;

public class SignupResponseModel
{
    public int UserId { get; set; }
    public string Name {  get; set; }
    public string Email { get; set; }
    public string Status {  get; set; }
    public string Message { get; set; }
    public bool IsOTPSent { get; set; }
}

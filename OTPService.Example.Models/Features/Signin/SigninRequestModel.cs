namespace OTPService.Example.Models.Features.Signin;

public record SigninRequestModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

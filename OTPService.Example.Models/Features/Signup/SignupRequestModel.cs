namespace OTPService.Example.Models.Features.Signup;

public record SignupRequestModel
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}


namespace OTPService.Example.Models.Features.Signin;

public class SigninResponseModel
{
    public int UserId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; } 

    public DateTime? SessionExpiredTime { get; set; }

    public string Token { get; set; }
}

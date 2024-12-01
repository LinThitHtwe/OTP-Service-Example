namespace OTPService.Example.Services.Features.Signup;

public static class SignupExtention
{
    public static void AddSignupService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<SignupService>();
    }
}

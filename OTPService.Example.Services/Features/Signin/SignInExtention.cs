namespace OTPService.Example.Services.Features.Signin;

public static class SigninExtention
{
    public static void AddSigninService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<SigninService>();
    }
}

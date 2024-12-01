namespace OTPService.Example.Services.Features.Signin;

public static class SignInExtention
{
    public static void AddSignInService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<SignInService>();
    }
}

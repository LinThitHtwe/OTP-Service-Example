namespace OTPService.Example.Services.Helpers;

public static class OTPHelper
{
    public static string GenerateOTPCode()
    {
        int length = 6;
        const string digits = "0123456789";
        var random = new Random();
        var otpCode = new string(Enumerable.Repeat(digits, length)
                          .Select(s => s[random.Next(s.Length)]).ToArray());
        return otpCode;
    }
}

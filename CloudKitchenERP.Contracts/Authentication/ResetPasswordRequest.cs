public class ResetPasswordRequest
{
    public string MobileNumber { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;
}
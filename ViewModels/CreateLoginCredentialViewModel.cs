using System.ComponentModel.DataAnnotations;

public class CreateLoginCredentialViewModel
{
    public int UserID { get; set; }
    public string Email { get; set; }
    public string UserType { get; set; } // "Student" or "Faculty"
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

// filepath: /workspaces/KNC/ViewModels/ResetPasswordViewModel.cs
public class ResetPasswordViewModel
{
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
}
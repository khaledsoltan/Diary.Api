using System.ComponentModel.DataAnnotations;

namespace OIDC.Entities.ViewModels;

public class ForgotPasswordModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }
}

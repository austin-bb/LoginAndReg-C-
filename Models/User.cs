// Disabled because we know the framework will assign non-null values when it
// constructs this class for us.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User 
{
  [Key]
  public int UserId { get; set; }

  [Required(ErrorMessage = "is required")]
  [MinLength(2, ErrorMessage = "must be at least 2 characters")]
  [Display(Name = "First Name")]
  public string FName { get; set; }

  [Required(ErrorMessage = "is required")]
  [MinLength(2, ErrorMessage = "must be at least 2 characters")]
  [Display(Name = "Last Name")]
  public string LName { get; set; }

  [Required(ErrorMessage = "is required")]
  [EmailAddress]
  public string Email { get; set; }

  [Required(ErrorMessage = "is required")]
  [MinLength(8, ErrorMessage = "must be at least 8")]
  [DataType(DataType.Password)]
  public string Password { get; set; }

  [NotMapped]
  [Required(ErrorMessage = "is required")]
  [DataType(DataType.Password)]
  [Compare("Password", ErrorMessage = "does not match password")]
  [Display(Name = "Confirm Password")]
  public string ConfirmPassword { get; set; }

  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
using System.ComponentModel.DataAnnotations;

namespace NUnit.Test.Application.Models.DTOs
{
    public class UserSignInDTO
    { 
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Remember { get; set; }
}
}

using Microsoft.AspNetCore.Identity;

namespace NUnit.Test.Application.Domain
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
    }
}

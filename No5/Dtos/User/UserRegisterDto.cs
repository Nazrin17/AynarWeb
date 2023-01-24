using Microsoft.CodeAnalysis.Operations;

namespace No5.Dtos.User
{
    public class UserRegisterDto
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}

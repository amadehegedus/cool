namespace Cool.Common.DTOs
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}

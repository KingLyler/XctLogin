namespace XctLogin.Models
{
    public class LoginViewModel
    {
        public User User { get; set; } = new User();
        public bool Loading { get; set; }
        public string LoginErrorMessage { get; set; }
        public string UsernameNotFound { get; set; }
        public string IncorrectPassword { get; set; }
    }
}
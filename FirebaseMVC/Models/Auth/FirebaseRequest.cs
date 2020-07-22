namespace FirebaseMVC.Models.Auth
{
    public class FirebaseRequest
    {
        public FirebaseRequest(string email, string password)
        {
            Email = email;
            Password = password;
            ReturnSecureToken = true;
        }

        public string Email { get; }
        public string Password { get; }
        public bool ReturnSecureToken { get; }
    }
}

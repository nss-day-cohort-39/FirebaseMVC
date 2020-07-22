namespace FirebaseMVC.Models.Auth
{
    public class FirebaseSignInRequest
    {
        public FirebaseSignInRequest(string email, string password)
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

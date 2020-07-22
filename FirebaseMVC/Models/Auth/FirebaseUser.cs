namespace FirebaseMVC.Models.Auth
{
    public class FirebaseUser
    {
        public string Email { get; }
        public string FirebaseUserId { get; }

        public FirebaseUser(string email, string firebaseUserId)
        {
            Email = email;
            FirebaseUserId = firebaseUserId;
        }
    }
}

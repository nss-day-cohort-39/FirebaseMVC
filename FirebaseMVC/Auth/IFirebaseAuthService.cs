using System.Threading.Tasks;
using FirebaseMVC.Auth.Models;

namespace FirebaseMVC.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}
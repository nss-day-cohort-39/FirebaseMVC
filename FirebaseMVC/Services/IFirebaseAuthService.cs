using FirebaseMVC.Models.Auth;
using System.Threading.Tasks;

namespace FirebaseMVC.Services
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}
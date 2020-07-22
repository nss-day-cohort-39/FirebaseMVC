using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using FirebaseMVC.Auth.Models;

namespace FirebaseMVC.Auth
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        private const string FIREBASE_SIGN_IN_BASE_URL =
            "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=";
        private const string FIREBASE_SIGN_UP_BASE_URL =
            "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _firebaseApiKey;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public FirebaseAuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _firebaseApiKey = configuration.GetValue<string>("FirebaseApiKey");
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        // See firebase documentation for more information
        //  https://firebase.google.com/docs/reference/rest/auth/#section-sign-in-email-password
        public async Task<FirebaseUser> Login(Credentials credentials)
        {
            var url = FIREBASE_SIGN_IN_BASE_URL + _firebaseApiKey;
            return await SignUpOrSignIn(credentials.Email, credentials.Password, url);
        }

        public async Task<FirebaseUser> Register(Registration registration)
        {
            var url = FIREBASE_SIGN_UP_BASE_URL + _firebaseApiKey;
            return await SignUpOrSignIn(registration.Email, registration.Password, url);
        }

        private async Task<FirebaseUser> SignUpOrSignIn(string email, string password, string url)
        {
            var firebaseRequest = new FirebaseRequest(email, password);

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(
                JsonSerializer.Serialize(firebaseRequest, _jsonSerializerOptions),
                Encoding.UTF8,
                "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            var firebaseResponse =
                JsonSerializer.Deserialize<FirebaseResponse>(content, _jsonSerializerOptions);

            return firebaseResponse.LocalId != null
                ? new FirebaseUser(firebaseResponse.Email, firebaseResponse.LocalId)
                : null;
        }
    }
}

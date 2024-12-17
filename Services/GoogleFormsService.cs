namespace learning_asp_core.Services
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Forms.v1;
    using Google.Apis.Services;
    using System.IO;

    public class GoogleFormsService
    {
        public static FormsService GetFormsService()
        {
            // Path to the service account credentials JSON file
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "credentials.json");

            // Load the service account credentials as GoogleCredential
            GoogleCredential credential;
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Create GoogleCredential directly from the stream
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(new string[] { FormsService.Scope.FormsBody });
            }

            // Create the service using the service account credentials
            var service = new FormsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Forms API Integration",
            });

            return service;
        }
    }
}

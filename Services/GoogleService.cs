using Google.Apis.Forms.v1.Data;

namespace learning_asp_core.Services
{
    using Google.Apis.Forms.v1.Data;

    public class GoogleService
    {
        private readonly ILogger<GoogleService> _logger;

        public GoogleService(ILogger<GoogleService> logger)
        {
            _logger = logger;
        }

        public void CreateForm()
        {
            var service = GoogleFormsService.GetFormsService();

            // Define a new form
            var newForm = new Form
            {
                Info = new Info
                {
                    Title = "My Sample Form",
                }
            };

            // Create the form
            var createdForm = service.Forms.Create(newForm).Execute();
            _logger.LogInformation($"Form created: {createdForm.ResponderUri}");
        }
    }
}

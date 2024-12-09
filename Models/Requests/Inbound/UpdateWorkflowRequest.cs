namespace learning_asp_core.Models.Requests.Inbound
{
    using System;

        public class Notification
        {
            public string SubscriptionId { get; set; }
            public int NotificationId { get; set; }
            public string Id { get; set; }
            public string EventType { get; set; }
            public string PublisherId { get; set; }
            public string? Message { get; set; } // Nullable string
            public string? DetailedMessage { get; set; } // Nullable string
            public Resource Resource { get; set; }
            public string ResourceVersion { get; set; }
            public ResourceContainers ResourceContainers { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class Resource
        {
            public int Id { get; set; }
            public string Url { get; set; }
            public int WorkItemId { get; set; }
        }

        public class ResourceContainers
        {
            public Container Collection { get; set; }
            public Container Account { get; set; }
            public Container Project { get; set; }
        }

        public class Container
        {
            public string Id { get; set; }
            public string BaseUrl { get; set; }
        }
}

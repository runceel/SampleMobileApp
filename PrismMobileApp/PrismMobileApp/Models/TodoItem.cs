using System;

namespace PrismMobileApp.Models
{
    public class TodoItem
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public bool Complete { get; set; }

        public byte[] Version { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}

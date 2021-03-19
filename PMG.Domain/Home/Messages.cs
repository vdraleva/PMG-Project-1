namespace PMG.Domain.Home
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Messages
    {
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }
        public DateTime PublishedOn { get; set; }
    }
}
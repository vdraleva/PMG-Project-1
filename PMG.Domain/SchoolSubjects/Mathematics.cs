namespace PMG.Domain.SchoolSubjects
{
    using System;

    public class Mathematics : ISubject
    {
        public string Id { get; set; }
        public decimal Mark { get; set; }
        public string BookmarkId { get; set; }
        public Bookmark bookmark { get; set; }
        public DateTime Day { get; set; }

    }
}
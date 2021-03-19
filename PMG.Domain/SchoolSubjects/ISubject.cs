namespace PMG.Domain.SchoolSubjects
{
    using System;
    public interface ISubject
    {
        public string Id { get; }
        public decimal Mark { get; }
        public string BookmarkId { get; set; }
        public Bookmark bookmark { get; set; }
        public DateTime Day { get; set; }
    }
}
namespace PMG.Domain
{
    using PMG.Domain.SchoolSubjects;
    using System.Collections.Generic;

    public class Bookmark
    {
        public string Id { get; set; }
        public ICollection<Philosophy> PhilosophyMarks { get; set; } = new List<Philosophy>();
        public ICollection<Mathematics> MathematicsMarks { get; set; } = new List<Mathematics>();
        public ICollection<English> EnglishMarks { get; set; } = new List<English>();
    }
}
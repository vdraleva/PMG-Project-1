namespace PMG.App.Models.Home
{
    public class Timetable : IBindingModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string PublishedOn { get; set; }
    }
}